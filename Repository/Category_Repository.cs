namespace MyMusicList_Server.Repository
{
    public class Category_Repository : ICategory_Repository
    {
        private readonly DbInteractorSqlite _context;
        public Category_Repository(DbInteractorSqlite context)
        {
            _context = context;
        }
        public async Task<bool> DeleteCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<Category> GetCategory(string id)
        {
            var category = await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> PostCategory(Category_Request_Model category, IFormFile image)
        {
            var stream = new MemoryStream();
            image.CopyTo(stream);
            var file = $"{category.ImageUrl}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, folder, file);
            if (!response) return null;
            var dbCategory = new Category
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = category.CategoryName,
                ImageUrl = file,
            };
            _context.Categories.Add(dbCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return dbCategory;
        }

        public async Task<Category> PutCategory(Category_Response_Model category)
        {
            var cat = _context.Categories.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();
            if (cat == null) return null;
            cat.CategoryName = category.CategoryName;
            _context.Update(cat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return cat;
        }
    }
}
