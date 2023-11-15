using MongoDB.Bson.Serialization;
using MongoTools.Repository.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTools.Repository.Persistence.Mappers
{
	public class ProductMap
	{
		public static void Configure()
		{
			BsonClassMap.RegisterClassMap<Product>(map =>
			{
				map.AutoMap();
				map.SetIgnoreExtraElements(true);
				map.MapIdMember(x => x.Id);
				map.MapMember(x => x.Description).SetIsRequired(true);
			});
		}
	}
}
