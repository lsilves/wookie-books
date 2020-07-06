using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using Wookie_Books.Models;
using Wookie_Books.Services;

namespace Wookie_Books.Controllers
{
    public class BooksController : Controller
    {
        private readonly DocumentService _documentService;

        // initialize document service
        public BooksController(DocumentService documentService)
        {
            _documentService = documentService;
        }


        public async Task<IActionResult> Index(string searchString)
        {
            var viewModel = new BooksViewModel()
            {
                Database = "wookie_books",
                Collection = "books"
            };
            viewModel.Documents = await _documentService.GetDocuments(viewModel.Database, viewModel.Collection);

            // convert BSON list to JSON to BookViewModel
            var dotNetObjList = viewModel.Documents.ConvertAll(BsonTypeMapper.MapToDotNetValue); 
            string jsonBooksList = JsonConvert.SerializeObject(dotNetObjList);
            viewModel.BooksList = JsonConvert.DeserializeObject<List<BookViewModel>>(jsonBooksList);

            if (!String.IsNullOrEmpty(searchString))
            {
                var viewModelWithSearch = new BooksViewModel()
                {
                    Database = "wookie_books",
                    Collection = "books"
                };
                viewModel.BooksList = viewModel.BooksList.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            return View(viewModel);

        }





        public async Task<IActionResult> Book(string id)
        {
            var booksList = new BooksViewModel()
            {
                Database = "wookie_books",
                Collection = "books"
            };
            booksList.Documents = await _documentService.GetDocuments(booksList.Database, booksList.Collection);

            var dotNetObjList = booksList.Documents.ConvertAll(BsonTypeMapper.MapToDotNetValue);
            string jsonBooksList = JsonConvert.SerializeObject(dotNetObjList);
            booksList.BooksList = JsonConvert.DeserializeObject<List<BookViewModel>>(jsonBooksList);

            var viewModel = new BookViewModel(); 
            viewModel = booksList.BooksList.Find(x => x._Id == id);

            return View(viewModel);
        }








        public IActionResult NewBook() 
        {
            BookViewModel newBook = new BookViewModel();
            return View(newBook);
        }





        public async Task<IActionResult> CreateDoc(
          string database = "wookie_books",
          string collection = "books",
          string title = "",
          string description = "",
          string coverImage = "",
          decimal price = 0
        )
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = this.User.FindFirstValue(ClaimTypes.Name);
            await _documentService.CreateDocument(database, collection, userId, userName, title, description, coverImage, price);
            return RedirectToAction("Index");
        }





        public async Task<IActionResult> UpdateDoc(
          string database = "wookie_books",
          string collection = "books",
          string id = "",
          string title = "",
          string description = "",
          string coverImage = "",
          decimal price = 0
        )
        {
            await _documentService.UpdateDocument(database, collection, id, title, description, coverImage, price);
            return RedirectToAction("Index");
        }





        public async Task<IActionResult> DeleteDoc(
          string database = "wookie_books",
          string collection = "books",
          string id = "",
          int index = -1,
          string authorId = ""
        )
            {
                if (index != -1 && id != "")
                {
                    if (authorId == this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                    {
                        var delete = await _documentService.DeleteDocument(database, collection, id);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }


    }





}