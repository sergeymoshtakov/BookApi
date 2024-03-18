using BookApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController : Controller
    {
        private static List<Book> _books = new List<Book>()
        {
            new Book() { Id = 1, Name = "Book 1", Author = "Author 1", Description = "Description 1", Year = 2021 },
            new Book() { Id = 2, Name = "Book 2", Author = "Author 2", Description = "Description 2", Year = 2022 },
            new Book() { Id = 3, Name = "Book 3", Author = "Author 3", Description = "Description 3", Year = 2023 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            var item = _books.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book item)
        {
            var existingBook = _books.FirstOrDefault(i => i.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Name = item.Name;
            existingBook.Author = item.Author;
            existingBook.Description = item.Description;
            existingBook.Year = item.Year;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _books.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            _books.Remove(item);
            return NoContent();
        }
    }
}
