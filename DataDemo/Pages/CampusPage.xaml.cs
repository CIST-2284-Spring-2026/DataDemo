using DataDemo.Data;
using DataDemo.Data.Models;
using DataDemo.Data.Repos;

namespace DataDemo.Pages;

public partial class CampusPage : ContentPage
{
    private readonly DatabaseService db;
    private readonly CampusRepository repo;
    private Campus? selectedCampus;

    public CampusPage()
    {
        InitializeComponent();
        db = new DatabaseService();
        repo = new CampusRepository(db);
        LoadCampuses();
    }

    private void LoadCampuses()
    {
        cvCampuses.ItemsSource = repo.GetAll();
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        string name = txtCampusName.Text?.Trim() ?? "";
        if (name == "")
        {
            lblStatus.Text = "Enter a campus name before adding.";
            return;
        }

        Campus campus = new Campus
        {
            Name = name
        };

        repo.Add(campus);
        LoadCampuses();
        ClearForm();
        lblStatus.Text = "Campus added.";
    }

    private void OnUpdateClicked(object sender, EventArgs e)
    {
        if (selectedCampus == null)
        {
            lblStatus.Text = "Select a campus before updating.";
            return;
        }

        string name = txtCampusName.Text?.Trim() ?? ""; if (name == "")
        {
            lblStatus.Text = "Enter a campus name before updating.";
            return;
        }

        selectedCampus.Name = name;
        repo.Update(selectedCampus);
        LoadCampuses();
        ClearForm(); lblStatus.Text = "Campus updated.";
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (selectedCampus == null)
        {
            lblStatus.Text = "Select a campus before deleting.";
            return;
        }

        repo.Delete(selectedCampus.Id);
        LoadCampuses();
        ClearForm();
        lblStatus.Text = "Campus deleted.";
    }
    private void OnClearClicked(object sender, EventArgs e)
    {
        ClearForm();
        lblStatus.Text = "Form cleared.";
    }

    private void OnCampusSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedCampus = e.CurrentSelection.FirstOrDefault() as Campus;

        if (selectedCampus != null)
        {
            txtCampusName.Text = selectedCampus.Name;
            lblStatus.Text = $"Selected campus Id {selectedCampus.Id}.";
        }
    }

    private void ClearForm()
    {
        selectedCampus = null;
        txtCampusName.Text = "";
        cvCampuses.SelectedItem = null;
    }
}