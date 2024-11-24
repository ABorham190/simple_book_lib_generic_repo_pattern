using BLL.Services;
using DLA.Data.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace simple_book_lib_generic_repo_pattern.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(int ID)
        {
            if (ID < 1)
            {
                return BadRequest("Invalid user input");
            }

            try
            {
                var authorDto = await _authorService.GetByIDAsync(ID);
                if (authorDto == null)
                {
                    return NotFound($"No Author with ID {ID}");
                }
                return Ok(authorDto);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                    error = "Internal server error",
                    
                };
                return StatusCode(500, response);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var authors =await _authorService.GetAll();
                if (authors == null)
                {
                    return NotFound("No authors found");
                }

                return Ok(authors);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                    error = "Internal server error",
                    
                };
                return StatusCode(500, response);
            }
        }
        [HttpDelete]
        public async Task<IActionResult>Delete(int ID)
        {
            try
            {
                await _authorService.Delete(ID);
                return NoContent();
            }catch(Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                    error = "Internal server error",
                    
                };
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task <IActionResult>AddNew(AuthorDto author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            try
            {
                await _authorService.AddNew(author);
                return Ok(author);

            }catch (Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                };
                return StatusCode(500, response);
            }
        }

        [HttpPost]

        public async Task<IActionResult>AddNewAuthorToExistingPerson(int PersonID,string Title)
        {
            if (PersonID < 1)
            {
                return BadRequest($"Invalid User input PersonID {PersonID}");
            }
            if(Title == null)
            {
                return BadRequest($"Invalid User input Title {Title}");
            }

            try
            {
                await _authorService.AddNewAuthorToExistantPerson(PersonID, Title);
                return NoContent();
            }catch(Exception ex)
            {
                var response = new
                {
                    message = ex.Message,
                };
                return StatusCode(500, response);
            }
        }

    }
}
