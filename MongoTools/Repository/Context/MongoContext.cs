﻿using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoTools.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTools.Repository.Context
{
	public class MongoContext : IMongoContext
	{
		private IMongoDatabase Database { get; set; }
		public IClientSessionHandle Session { get; set; }
		public MongoClient MongoClient { get; set; }
		private readonly List<Func<Task>> _commands;
		private readonly IConfiguration _configuration;

		public MongoContext(IConfiguration configuration)
		{
			_configuration = configuration;

			// Every command will be stored and it'll be processed at SaveChanges
			_commands = new List<Func<Task>>();
		}

		public async Task<int> SaveChanges()
		{
			ConfigureMongo();

			using (Session = await MongoClient.StartSessionAsync())
			{
				Session.StartTransaction();

				var commandTasks = _commands.Select(c => c());

				await Task.WhenAll(commandTasks);

				await Session.CommitTransactionAsync();
			}

			return _commands.Count;
		}

		/// <summary>
		/// Exemple dans appsettings :  "MongoSettings": {
		 ///  "Connection": "mongodb://root:password123@localhost:27017",
		 ///  "DatabaseName": "Products"
		///}
		/// </summary>
	private void ConfigureMongo()
		{
			if (MongoClient != null)
			{
				return;
			}

			// Configure mongo (You can inject the config, just to simplify)
			MongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);

			Database = MongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);
		}

		public IMongoCollection<T> GetCollection<T>(string name)
		{
			ConfigureMongo();

			return Database.GetCollection<T>(name);
		}

		public void Dispose()
		{
			Session?.Dispose();
			GC.SuppressFinalize(this);
		}

		public void AddCommand(Func<Task> func)
		{
			_commands.Add(func);
		}
	}
}