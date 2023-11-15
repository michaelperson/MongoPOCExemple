using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;
using MongoTools.Repository.Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTools.Repository.Persistence
{
	public static class MongoDbPersistence
	{
		public static void Configure()
		{
			ProductMap.Configure();

			BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy; 

			// Conventions
			var pack = new ConventionPack
				{
					new IgnoreExtraElementsConvention(true),
					new IgnoreIfDefaultConvention(true)
				};
			ConventionRegistry.Register("MongoDb Conventions", pack, t => true);
		}
	}
}
