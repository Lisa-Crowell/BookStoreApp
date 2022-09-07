using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BookStoreDbContext> _logger;

        public BooksController(BookStoreDbContext context, IMapper mapper, ILogger<BookStoreDbContext> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .ProjectTo<BookReadOnlyDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            //var booksDto = _mapper.Map<IEnumerable<BookReadOnlyDto>>(books);
            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b=> b.Author)
                .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookUpdateDto)
        {
            if (id != bookUpdateDto.Id)
            {
                return BadRequest();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _mapper.Map(bookUpdateDto, book);
            _context.Entry(bookUpdateDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookCreateDto)
        {
            var book = _mapper.Map<Book>(bookCreateDto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> BookExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }
    }
}
