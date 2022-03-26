namespace MyMusicList_Server.ResponseModel
{
    public class Category_Response_Model
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
