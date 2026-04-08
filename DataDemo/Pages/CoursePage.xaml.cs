using DataDemo.Data;
using DataDemo.Data.Models;
using DataDemo.Data.Repos;

namespace DataDemo.Pages;

public partial class CoursePage : ContentPage
{
    private readonly DatabaseService db;
    private readonly CourseRepository repo;
    private Course? selectedCourse;

    public CoursePage()
    {
        InitializeComponent();
        db = new DatabaseService();
        repo = new CourseRepository(db);
        LoadCampuses();
    }

    private void LoadCampuses()
    {
        cvCourses.ItemsSource = repo.GetAll();
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        string name = txtCourseName.Text?.Trim() ?? "";
        if (name == "")
        {
            lblStatus.Text = "Enter a course name before adding.";
            return;
        }

        Course course = new Course
        {
            Name = name
        };

        repo.Add(course);
        LoadCampuses();
        ClearForm();
        lblStatus.Text = "Campus added.";
    }

    private void OnUpdateClicked(object sender, EventArgs e)
    {
        if (selectedCourse == null)
        {
            lblStatus.Text = "Select a campus before updating.";
            return;
        }

        string name = txtCourseName.Text?.Trim() ?? ""; if (name == "")
        {
            lblStatus.Text = "Enter a campus name before updating.";
            return;
        }

        selectedCourse.Name = name;
        repo.Update(selectedCourse);
        LoadCampuses();
        ClearForm(); lblStatus.Text = "Campus updated.";
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (selectedCourse == null)
        {
            lblStatus.Text = "Select a campus before deleting.";
            return;
        }

        repo.Delete(selectedCourse.Id);
        LoadCampuses();
        ClearForm();
        lblStatus.Text = "Campus deleted.";
    }
    private void OnClearClicked(object sender, EventArgs e)
    {
        ClearForm();
        lblStatus.Text = "Form cleared.";
    }

    private void OnCourseSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedCourse = e.CurrentSelection.FirstOrDefault() as Course;

        if (selectedCourse != null)
        {
            txtCourseName.Text = selectedCourse.Name;
            lblStatus.Text = $"Selected course Id {selectedCourse.Id}.";
        }
    }

    private void ClearForm()
    {
        selectedCourse = null;
        txtCourseName.Text = "";
        cvCourses.SelectedItem = null;
    }
}