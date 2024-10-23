

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/books/")]
public class BookController : ControllerBase
{
    private readonly IBookService bookService;
    public BookController(IBookService bookService)
    {
        this.bookService = bookService;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] BookCreateInfo bookCreate)
    {
        bool res = await bookService.Create(bookCreate);
        if (res == false) return BadRequest(ApiResponse<bool>.Fail(null, false));
        return Ok(ApiResponse<bool>.Success(null, true));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] BookUpdateInfo book)
    {
        bool res = await bookService.Update(book);
        if (res == false) return BadRequest(ApiResponse<bool>.Fail(null, false));
        return Ok(ApiResponse<bool>.Success(null, true));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        bool res = await bookService.Delete(id);
        if (res == false) return BadRequest(ApiResponse<bool>.Fail(null, false));
        return Ok(ApiResponse<bool>.Success(null, true));
    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        BookGetInfo? bookGet = await bookService.GetById(id);
        if (bookGet == null) return NotFound(ApiResponse<BookGetInfo?>.Fail(null, null));
        return Ok(ApiResponse<BookGetInfo?>.Success(null, bookGet));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAll([FromQuery] BookFilter filter)
    {
        return Ok(ApiResponse<PaginationResponse<IEnumerable<BookGetInfo>>>.Success(null, bookService.GetAll(filter)));
    }
}