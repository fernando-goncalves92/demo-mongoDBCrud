using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongodbCrudDemo
{
    class MongoCrud<T> where T : class
    {   
        private readonly IMongoDatabase _db;
        private readonly string _collection;

        public MongoCrud(string collection)
        {   
            _db = new MongoClient().GetDatabase("Example");
            
            _collection = collection;
        }

        public void CreateDocument(T document)
        {
            _db.GetCollection<T>(_collection).InsertOne(document);
        }

        public void UpdateDocument(T document, Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            _db.GetCollection<T>(_collection).ReplaceOne(filter, document);
        }

        public void DeleteDocument(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            _db.GetCollection<T>(_collection).DeleteOne(filter);
        }

        public List<T> LoadCollection()
        {
            return _db.GetCollection<T>(_collection).Find(new BsonDocument()).ToList();
        }

        public T LoadDocument(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            return _db.GetCollection<T>(_collection).Find(filter).FirstOrDefault();
        }
    }
}
