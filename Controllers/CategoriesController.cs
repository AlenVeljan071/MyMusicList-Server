namespace MyMusicList_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory_Repository _repository;

        public CategoriesController(ICategory_Repository repository)
        {
            _repository = repository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _repository.GetCategoriesAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category_Response_Model>> GetCategory(string id)
        {
            var categoryRes = await _repository.GetCategoryAsync(id);
            if (categoryRes == null) return NotFound();
            return categoryRes.CategoryResponse();
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCategory(Category_Response_Model category)
        {
            var response = await _repository.PutCategoryAsync(category);
            if (response == null) return BadRequest();
            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category_Response_Model>> PostCategory([FromForm] Category_Request_Model category, IFormFile image)
        {
            var response = await _repository.PostCategoryAsync(category, image);
            if (response == null) return BadRequest();
            return CreatedAtAction("GetCategoryAsync", new { id = response.CategoryId }, response.CategoryResponse());
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var response = await _repository.DeleteCategoryAsync(id);
            if (!response) return BadRequest();
            return NoContent();
        }

    }
}
