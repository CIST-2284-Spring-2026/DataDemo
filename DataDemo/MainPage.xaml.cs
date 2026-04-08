namespace DataDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var db = new DatabaseService();
            var repo = new SchoolRepository(db);
            pkMajor.ItemsSource = repo.GetMajors();
            pkCourse.ItemsSource = repo.GetCourses();
            pkCampus.ItemsSource = repo.GetCampuses();
        }
    }
}
