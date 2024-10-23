
public record PaginationResponse<T> : BaseFilter
{
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public T Data { get; set; }

    public PaginationResponse(int pageNumber, int pageSize, int totalRecord, T data) : base(pageNumber, pageSize)
    {
        Data = data;
        TotalRecords = totalRecord;
        TotalPages = (int)Math.Ceiling((double)totalRecord / pageSize);
    }

    public static PaginationResponse<T> Create(int pageNumber, int pageSize, int totalRecord, T data)
    => new PaginationResponse<T>(pageNumber, pageSize, totalRecord, data);
}