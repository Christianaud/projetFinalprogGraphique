using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projet.ClassesParJacques;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.PagesParJacques;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PageModifierProjets : Page
{
    Projet projet;
    public PageModifierProjets()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        projet = e.Parameter as Projet;
        if (projet != null)
        {
            tbxTitre.Text = "Titre: " + projet.Titre;
            tbxStatut.Text = "Statut: " + projet.Statut;
            tbxDescription.Text = "Description: " + projet.Description;
            tbxDateDebut.Text = "Date de debut: " + projet.DateDebut.ToString();
            tbxIdClient.Text = "Id Client: " + projet.IdClient.ToString();
            tbxBudget.Text = "Budget: " + projet.Budget.ToString();
            tbxNomClient.Text = "Nom Client: " + projet.NomClient;
            tbxNbrEmployes.Text = "Employe: " + projet.NbrEmployes.ToString();
            tbxTotalSalaire.Text = "Total Salaires: " + projet.TotalSalaire.ToString();
        }
    }
    private void btnModifier_Click(object sender, RoutedEventArgs e)
    {

    }
}
