using BuggyBackend.Models;
using BuggyBackend.Repositories;

namespace BuggyBackend.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public Book GetBookById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid book ID");
            }
            return _bookRepository.GetById(id);
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return new List<Book>();
            }
            return _bookRepository.SearchByTitle(title);
        }

        public Book AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentException("Book title is required");
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                throw new ArgumentException("Book author is required");
            }

            return _bookRepository.Create(book);
        }

        public Book UpdateBook(int id, Book book)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid book ID");
            }

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            var existingBook = _bookRepository.GetById(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found");
            }

            return _bookRepository.Update(id, book);
        }

        public bool DeleteBook(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid book ID");
            }

            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return false;
            }

            return _bookRepository.Delete(id);
        }

        public List<Book> GetAvailableBooks()
        {
            var allBooks = _bookRepository.GetAll();
            return allBooks.Where(b => b.IsAvailable()).ToList();
        }
    }
}
