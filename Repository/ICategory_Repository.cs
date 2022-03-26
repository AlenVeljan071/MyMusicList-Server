namespace MyMusicList_Server.Repository
{
    public interface ICategory_Repository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(string id);
        Task<Category> PostCategoryAsync(Category_Request_Model song, IFormFile image);
        Task<Category> PutCategoryAsync(Category_Response_Model song);
        Task<bool> DeleteCategoryAsync(string id);
    }
}