using ImageUploader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicList_Server.Data;
using MyMusicList_Server.Models;
using MyMusicList_Server.RequestModel;
using MyMusicList_Server.ResponseModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MyMusicList_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DbInteractorSqlite _context;

        public CategoriesController(DbInteractorSqlite context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public ActionResult<CategoryResponseModel> GetCategory(string id)
        {
            var category = _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }
            var categoryRes = new CategoryResponseModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };

            return categoryRes;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCategory(CategoryResponseModel category)
        {
            if (category.CategoryId == null)
            {
                return BadRequest();
            }
            if (_context.Categories.Where(x => x.CategoryId == category.CategoryId).Any())
            {
                var cat = _context.Categories.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();
                cat.CategoryName = category.CategoryName;
                _context.Update(cat);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryResponseModel>> PostCategory([FromForm]CategoryRequestModel category, IFormFile image)
        {
          
            var stream = new MemoryStream();
            image.CopyTo(stream);
            var file = $"{category.ImageUrl}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, folder, file);
            if (!response)
            {
                return BadRequest();
            }
            else
            {
                var dbCategory = new Category
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    CategoryName = category.CategoryName,
                    ImageUrl = file,
                };
                _context.Categories.Add(dbCategory);

                var categoryRes = new CategoryResponseModel
                {
                    CategoryId = dbCategory.CategoryId,
                    CategoryName = dbCategory.CategoryName,
                };
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (CategoryExists(dbCategory.CategoryId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
                return CreatedAtAction("GetCategory", new { id = dbCategory.CategoryId }, categoryRes);
            }
           

            
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
