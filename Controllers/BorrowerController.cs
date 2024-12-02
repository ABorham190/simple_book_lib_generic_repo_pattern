using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace simple_book_lib_generic_repo_pattern.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly BorrowerService _borrowerService;
        public BorrowerController(BorrowerService borrowerService)
        {
            _borrowerService = borrowerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var BorrowersDtos = await _borrowerService.GetAllAsync();

                return Ok(BorrowersDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
