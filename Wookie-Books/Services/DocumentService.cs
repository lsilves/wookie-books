using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Wookie_Books.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wookie_Books.Services
{
    public class DocumentService
    {

        private readonly MongoClient _client;

        public DocumentService(MyDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
        }


        public DocumentService()
        {
        }




        public async Task<BsonDocument> GetDocument(string databaseName, string collectionName, int index)
        {
            var collection = GetCollection(databaseName, collectionName);
            BsonDocument document = null;
            await collection.Find(doc => true)
              .Skip(index)
              .Limit(1)
              .ForEachAsync(doc => document = doc);
            return document;
        }




        public async Task<List<BsonDocument>> GetDocuments(string databaseName, string collectionName) 
        {
            var collection = GetCollection(databaseName, collectionName);
            List<BsonDocument> documentsList = null;
            documentsList = await (await collection.FindAsync(_ => true)).ToListAsync();
            return documentsList;
        }





        private IMongoCollection<BsonDocument> GetCollection(string databaseName, string collectionName)
        {
            var db = _client.GetDatabase(databaseName);
            return db.GetCollection<BsonDocument>(collectionName);
        }





        public async Task<DeleteResult> DeleteDocument(string databaseName, string collectionName, string id)
        {
            if (id != "" && id != null)
            {
                var collection = GetCollection(databaseName, collectionName);
                return await collection.DeleteOneAsync(CreateIdFilter(id));
            }
            else return null;
        }




        private static BsonDocument CreateIdFilter(string id)
        {
            return new BsonDocument("_id", new BsonObjectId(new ObjectId(id)));
        }




        public async Task CreateDocument(string databaseName, string collectionName, string userId, string userName, string title, string description, string coverImage, decimal price) 
        {
            var collection = GetCollection(databaseName, collectionName);

            string documentJson = "{\"author\":\" " + userName + "\", \"authorId\":\"" + userId + "\", \"title\":\" " + title + "\", \"description\":\"" + description + "\", \"coverImage\":\" " + coverImage + "\", \"price\":" + price + "}";

            BsonDocument newBook = BsonDocument.Parse(documentJson);

            BsonDocument document = new BsonDocument();
            document.AddRange(newBook);
            await collection.InsertOneAsync(document);
        }


        public async Task<UpdateResult> UpdateDocument(string databaseName, string collectionName, string id, string title, string description, string coverImage, decimal price)
        {
            var update = Builders<BsonDocument>.Update
                .Set("title", title)
                .Set("description", description)
                .Set("coverImage", coverImage)
                .Set("price", price);
            var collection = GetCollection(databaseName, collectionName);
            return await collection.UpdateOneAsync(CreateIdFilter(id), update);

        }

    }
}
