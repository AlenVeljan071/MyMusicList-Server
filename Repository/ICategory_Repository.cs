namespace MyMusicList_Server.Repository
{
    public interface ICategory_Repository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(string id);
        Task<Category> PostCategory(Category_Request_Model song, IFormFile image);
        Task<Category> PutCategory(Category_Response_Model song);
        Task<bool> DeleteCategory(string id);
    }
}