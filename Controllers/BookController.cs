using BLL.Services;
using DLA.Data.Dtos;
using DLA.Data.Specific_interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace simple_book_lib_generic_repo_pattern.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(int ID)
        {
            if (ID < 1)
            {
                return BadRequest($"Invalid User Input {ID}");
            }

            try
            {
                var book = await _bookService.GetBookByIDAsync(ID);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                    error = "Internal server error"
                };
                return StatusCode(500, response);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int ID)
        {
            if (ID < 1)
            {
                return BadRequest($"Invalid User Input {ID}");
            }

            try
            {
                if (await _bookService.Delete(ID))
                {
                    return Ok("Deleted Successfully");
                }

                return BadRequest("Not Deleted Successfully");
            }
            catch (Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                    error = "Internal server error ",
                    details = ex,
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var booksDtos = await _bookService.GetAllAsync();
                if (booksDtos == null)
                    return NotFound("No books Found");

                return Ok(booksDtos);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                    error = "Internal server error ",
                    details = ex,
                };
                return StatusCode(500, response);

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithRelatedEntities()
        {
            try
            {
                var booksDtos = await _bookService.GetAllWithRelatedEntitiesAsync();
                if (booksDtos == null)
                    return NotFound("No books Found");

                return Ok(booksDtos);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                    error = "Internal server error ",
                    details = ex,
                };
                return StatusCode(500, response);

            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update(int ID, UpdateBookDto updatedFields)
        {
            if (ID < 1 || updatedFields == null)
            {
                return BadRequest($"Invalid User Input");
            }

            try
            {
                await _bookService.Update(ID, updatedFields);
                return NoContent();
            }
            catch (Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                    error = "Internal server error ",
                    details = ex,
                };
                return StatusCode(500, response);
            }
        }
    }
}
