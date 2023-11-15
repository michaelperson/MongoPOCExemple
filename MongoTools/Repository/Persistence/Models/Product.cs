using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTools.Repository.Persistence.Models
{
	public class Product
	{
		public Product(string description)
		{
			Description = description;
			Id = Guid.NewGuid();
		}

		public Product(Guid id, string description)
		{
			Id = id;
			Description = description;
		}

		public Guid Id { get; private set; }
		public string Description { get; private set; }
	}
}
