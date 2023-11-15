using MongoTools.Repository.Interfaces;
using MongoTools.Repository.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTools.Repository
{
	public class ProductRepository : BaseRepository<Product>, IProductRepository
	{
		public ProductRepository(IMongoContext context) : base(context)
		{
		}
	}
}
