namespace CMSNews.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int TotalNews { get; set; }
        public int NewsGroupsCount { get; set; }
    }
}
