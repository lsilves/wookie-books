using System;
namespace Wookie_Books.Models
{
    public class BookViewModel
    {
        public string _Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; } // author name string 
        public string CoverImage { get; set; }
        public decimal Price { get; set; }
        public int Index { get; set; } // location of book in collection
        public string AuthorId { get; set; } // author id string
        public BookViewModel()
        {
        }
    }
}
