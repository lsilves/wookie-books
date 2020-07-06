using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;
using Wookie_Books.Models;
using Wookie_Books.Services;
using Xunit;


namespace Tests
{
    public class Class1
    {

        // Test: GET Books
        [Fact]
        public async Task testBooks()
        {
            
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:5001/books");
            Console.WriteLine("RESPONSE -> " + response);
        }

        // Test: GET Book id = 5eff6d7ce9aea8add9fc4290
        [Fact]
        public async Task testBook()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:5001/books/book/5eff6d7ce9aea8add9fc4290");
            Console.WriteLine("RESPONSE -> " + response);
        }


        // Test: CREATE New Book
        [Fact]
        public async Task newBook()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:5001/books/NewBook");
            Console.WriteLine("RESPONSE -> " + response);

        }


        // Test: UPDATE Book
        [Fact]
        public async Task updateBook()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:5001/books/UpdateDoc/5eff6d7ce9aea8add9fc4290");
            Console.WriteLine("RESPONSE -> " + response);
        }

        // Test: DELETE Book
        [Fact]
        public async Task deleteBook()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://localhost:5001/books/DeleteDoc/5eff6d7ce9aea8add9fc4290");
            Console.WriteLine("RESPONSE -> " + response);
        }


    }

}
