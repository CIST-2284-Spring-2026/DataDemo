using DataDemo.Data;
using DataDemo.Data.Models;
using DataDemo.Data.Repos;

namespace DataDemo.Pages;

public partial class MajorPage : ContentPage
{
    private readonly DatabaseService db;
    private readonly MajorRepository repo;
    private Major? selectedMajor;

    public MajorPage()
    {
        InitializeComponent();
        db = new DatabaseService();
        repo = new MajorRepository(db);
        LoadMajors();
    }

    private void LoadMajors()
    {
        cvMajors.ItemsSource = repo.GetAll();
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        string titla = txtMajorTitle.Text?.Trim() ?? "";
        if (titla == "")
        {
            lblStatus.Text = "Enter a major name before adding.";
            return;
        }

        Major major = new Major
        {
            Title = titla
        };

        repo.Add(major);
        LoadMajors();
        ClearForm();
        lblStatus.Text = "Campus added.";
    }

    private void OnUpdateClicked(object sender, EventArgs e)
    {
        if (selectedMajor == null)
        {
            lblStatus.Text = "Select a major before updating.";
            return;
        }

        string name = txtMajorTitle.Text?.Trim() ?? ""; if (name == "")
        {
            lblStatus.Text = "Enter a major name before updating.";
            return;
        }

        selectedMajor.Title = name;
        repo.Update(selectedMajor);
        LoadMajors();
        ClearForm(); lblStatus.Text = "Major updated.";
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (selectedMajor == null)
        {
            lblStatus.Text = "Select a major before deleting.";
            return;
        }

        repo.Delete(selectedMajor.Id);
        LoadMajors();
        ClearForm();
        lblStatus.Text = "Major deleted.";
    }
    private void OnClearClicked(object sender, EventArgs e)
    {
        ClearForm();
        lblStatus.Text = "Form cleared.";
    }

    private void OnMajorSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedMajor = e.CurrentSelection.FirstOrDefault() as Major;

        if (selectedMajor != null)
        {
            txtMajorTitle.Text = selectedMajor.Title;
            lblStatus.Text = $"Selected major Id {selectedMajor.Id}.";
        }
    }

    private void ClearForm()
    {
        selectedMajor = null;
        txtMajorTitle.Text = "";
        cvMajors.SelectedItem = null;
    }
}