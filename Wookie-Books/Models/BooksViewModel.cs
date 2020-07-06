using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Wookie_Books.Models;

namespace Wookie_Books.Models
{
    public class BooksViewModel
    {
        public string Database { get; set; }
        public string Collection { get; set; }
        public BsonDocument Document { get; set; }

        public List<BsonDocument> Documents { get; set; } 

        public int Index { get; set; }
        public long CollectionCount { get; set; }

        public List<BookViewModel> BooksList { get; set; }

        public BooksViewModel()
        {
        }
    }
}
