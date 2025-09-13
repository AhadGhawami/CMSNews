namespace CMSNews.Models.ViewModels
{
    public class UserListViewModel
    {
        public Guid UserId { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
        public int NewsCount { get; set; }
    }

}

