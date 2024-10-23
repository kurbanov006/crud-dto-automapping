public interface IBookService
{
    Task<bool> Create(BookCreateInfo book);
    Task<bool> Update(BookUpdateInfo book);
    Task<bool> Delete(int id);
    Task<BookGetInfo> GetById(int id);
    PaginationResponse<IEnumerable<BookGetInfo>> GetAll(BookFilter filter);
}