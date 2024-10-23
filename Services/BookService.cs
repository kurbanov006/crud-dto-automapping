
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private readonly AppDbContext dbContext;
    public BookService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<bool> Create(BookCreateInfo book)
    {
        try
        {
            MapperConfiguration configuration = new MapperConfiguration(x => x.CreateMap<BookCreateInfo, Book>());
            IMapper mapper = configuration.CreateMapper();

            int maxId = (from b in dbContext.Books
                         orderby b.Id descending
                         select b.Id).FirstOrDefault();

            Book bookCreate = mapper.Map<Book>(book);
            bookCreate.Id = maxId + 1;

            await dbContext.Books.AddAsync(bookCreate);
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            Book? book = await dbContext.Books.FindAsync(id);
            if (book == null) return false;

            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public PaginationResponse<IEnumerable<BookGetInfo>> GetAll(BookFilter filter)
    {
        try
        {
            MapperConfiguration configuration = new MapperConfiguration(x => x.CreateMap<Book, BookGetInfo>());
            IMapper mapper = configuration.CreateMapper();

            IQueryable<Book> books = dbContext.Books;

            if (filter.Author != null)
                books = books.Where(x => x.Author.ToLower()
                .Contains(filter.Author.ToLower()));

            IEnumerable<BookGetInfo> bookGet = books.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
            .ProjectTo<BookGetInfo>(mapper.ConfigurationProvider);

            int totalRecord = dbContext.Books.Count();

            return PaginationResponse<IEnumerable<BookGetInfo>>.Create(filter.PageNumber, filter.PageSize, totalRecord, bookGet);

        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<BookGetInfo> GetById(int id)
    {
        try
        {
            MapperConfiguration configuration = new MapperConfiguration(x => x.CreateMap<Book, BookGetInfo>());
            IMapper mapper = configuration.CreateMapper();

            Book? book = await dbContext.Books.FindAsync(id);
            if (book == null) return new BookGetInfo();

            BookGetInfo bookGet = mapper.Map<BookGetInfo>(book);
            return bookGet;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> Update(BookUpdateInfo book)
    {
        try
        {
            MapperConfiguration configuration = new MapperConfiguration(x => x.CreateMap<BookUpdateInfo, Book>());
            IMapper mapper = configuration.CreateMapper();

            Book? updateBook = await dbContext.Books.FindAsync(book.Id);
            if (updateBook == null) return false;

            dbContext.Books.Update(mapper.Map(book, updateBook));

            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }
}