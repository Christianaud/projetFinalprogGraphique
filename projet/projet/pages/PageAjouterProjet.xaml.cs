using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projet.classes;
using projet.dialogue;
using projet.Singletons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjouterProjet : Page
    {
        ObservableCollection<Employe> employesAssignes = new ObservableCollection<Employe>();
        double totalSalaire = 0; 
        string numProjet; 


        public PageAjouterProjet()
        {
            InitializeComponent();
            SingletonEmploye.getInstance().GetEmployesDisponibles();

            cmbxEmp.ItemsSource = SingletonEmploye.getInstance().Liste;

            lvAssignations.ItemsSource = employesAssignes;
        }

        private async void cmbxEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employe emp = cmbxEmp.SelectedItem as Employe;

            if (emp == null)
            {
                cmbxEmpErreur.Text ="Sélectionnez un employé.";
                return;
            }

            if (employesAssignes.Any(x => x.Matricule == emp.Matricule))
            {
                cmbxEmpErreur.Text = "Cet employé est déjà assigné.";
                return;
            }

            if (employesAssignes.Count >= 5)
            {
                cmbxEmpErreur.Text= "Un projet ne peut pas avoir plus de 5 employés.";
                return;
            }

            BoiteHeuresDialog dlg = new BoiteHeuresDialog
            {
                XamlRoot = this.XamlRoot,
                Title = $"Heures travaillées pour {emp.Nom} {emp.Prenom}",
                PrimaryButtonText = "OK",
                CloseButtonText = "Annuler"
            };

            ContentDialogResult result = await dlg.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                int heures = dlg.Heures;
                emp.HeuresProjet = heures;
                double salaire = emp.TauxHoraire * heures;

                employesAssignes.Add(emp);
                tbxNbrEmployes.Text = employesAssignes.Count.ToString();
                totalSalaire += salaire;
                tbxTotalSalaire.Text = totalSalaire.ToString("F2");

                cmbxEmpErreur.Text = "";
                cmbxEmp.SelectedIndex = -1;
            }
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool estValide = true;

            // RESET erreurs
            tbxTitreErreur.Text = "";
            tbxStatutErreur.Text = "";
            tbxIdClientErreur.Text = "";
            tbxNomClientErreur.Text = "";
            dpDateDebutErreur.Text = "";
            tbxDescriptionErreur.Text = "";
            tbxBudgetErreur.Text = "";
            tbxNbrEmployesErreur.Text = "";
            tbxTotalSalaireErreur.Text = "";
            cmbxEmpErreur.Text = "";

            // ------------------------------
            // VALIDATIONS
            // ------------------------------

            // Titre
            if (string.IsNullOrWhiteSpace(tbxTitre.Text))
            {
                tbxTitreErreur.Text = "Le titre est obligatoire.";
                estValide = false;
            }
            else if (tbxTitre.Text.Length < 3)
            {
                tbxTitreErreur.Text = "Minimum 3 caractères.";
                estValide = false;
            }

            // Statut
            if (string.IsNullOrWhiteSpace(tbxStatut.Text))
            {
                tbxStatutErreur.Text = "Le statut est obligatoire.";
                estValide = false;
            }

            // ID client
            if (!int.TryParse(tbxIdClient.Text, out int idClient))
            {
                tbxIdClientErreur.Text = "L'ID client doit être un nombre.";
                estValide = false;
            }

            // Nom client
            if (string.IsNullOrWhiteSpace(tbxNomClient.Text))
            {
                tbxNomClientErreur.Text = "Nom obligatoire.";
                estValide = false;
            }

            // Date
            if (dpDateDebut.SelectedDate == null)
            {
                dpDateDebutErreur.Text = "Date obligatoire.";
                estValide = false;
            }

            // Description
            if (string.IsNullOrWhiteSpace(tbxDescription.Text) || tbxDescription.Text.Length < 10)
            {
                tbxDescriptionErreur.Text = "Description trop courte.";
                estValide = false;
            }

            // Budget
            if (!double.TryParse(tbxBudget.Text, out double budget) || budget <= 0)
            {
                tbxBudgetErreur.Text = "Budget invalide.";
                estValide = false;
            }

            // Employés assignés
            if (employesAssignes.Count == 0)
            {
                tbxNbrEmployesErreur.Text = "Il faut au moins 1 employé.";
                estValide = false;
            }

            if (!estValide)
                return;

            // ------------------------------
            // AJOUT DU PROJET
            // ------------------------------

            SingletonListe.getInstance().ajouterProjer(
                tbxTitre.Text,
                dpDateDebut.Date.DateTime,
                tbxDescription.Text,
                budget,
                employesAssignes.Count,   // ?? FIX
                double.Parse(tbxTotalSalaire.Text),
                idClient,
                tbxStatut.Text
            );

            Projet projetCree = SingletonListe.getInstance().DernierProjet();
            string numProjet = projetCree.Numero;

            // ------------------------------
            // AJOUT DES ASSIGNATIONS
            // ------------------------------
            foreach (var emp in employesAssignes)
            {
                if (SingletonAssignation.getInstance().EmployeOccupe(emp.Matricule))
                {
                    cmbxEmpErreur.Text = $"L'employé {emp.Nom} est déjà sur un projet.";
                    continue;
                }
                int heures = emp.HeuresProjet; // si tu ajoutes cette propriété à Employe temporairement
                double salaire = emp.TauxHoraire * heures;

                SingletonAssignation.getInstance().ajouterAssignation(
                    numProjet,
                    emp.Matricule,
                    heures,
                    salaire
                );

                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = "Projet ajouté",
                    Content = $"Le projet {tbxTitre.Text} a été créé avec succès.",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
                Frame.Navigate(typeof(PageListeProjet));
            }
        }

    }
}
