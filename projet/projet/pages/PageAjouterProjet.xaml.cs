using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI.Relational;
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
        Client clientSelectionne = null;

        public PageAjouterProjet()
        {
            InitializeComponent();
            SingletonListe.getInstance().getAllClients();
            SingletonEmploye.getInstance().GetEmployesDisponibles();

            cmbxEmp.ItemsSource = SingletonEmploye.getInstance().Liste;

            ChargerClients();

            lvAssignations.ItemsSource = employesAssignes;
        }

        private void ChargerClients()
        {
            // Récupérer la liste des clients depuis votre singleton ou base de données
            // Exemple avec un singleton Client
            var listeClients = SingletonListe.getInstance().ListeClients;

            if (listeClients != null && listeClients.Any())
            {
                cmbxClients.ItemsSource = listeClients;
                cmbxClients.DisplayMemberPath = "Nom"; 
            }
        }

        private void cmbxClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clientSelectionne = cmbxClients.SelectedItem as Client;

            if (clientSelectionne != null && clientSelectionne.Id > 0)
            {
                // Afficher les informations du client
                panelClientInfo.Visibility = Visibility.Visible;
                tbxClientId.Text = $"ID: {clientSelectionne.Id}";
                tbxClientNom.Text = $"Nom: {clientSelectionne.Nom}";

                cmbxClientsErreur.Text = "";
            }
            else
            {
                panelClientInfo.Visibility = Visibility.Collapsed;
            }
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

            tbxTitreErreur.Text = "";
            dpDateDebutErreur.Text = "";
            tbxDescriptionErreur.Text = "";
            tbxBudgetErreur.Text = "";
            tbxNbrEmployesErreur.Text = "";
            tbxTotalSalaireErreur.Text = "";
            cmbxEmpErreur.Text = "";


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

            if (clientSelectionne == null || clientSelectionne.Id == 0)
            {
                cmbxClientsErreur.Text = "Veuillez sélectionner un client valide.";
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


            string numeroCree;
            // AJOUT DU PROJET
            var resultatAjout = SingletonListe.getInstance().ajouterProjer(
                tbxTitre.Text,
                dpDateDebut.Date.DateTime,
                tbxDescription.Text,
                budget,
                employesAssignes.Count,
                double.Parse(tbxTotalSalaire.Text),
                clientSelectionne.Id,
                "En cours",
                out numeroCree  // paramètre out
            );

            bool assignationReussie = true;

            foreach (var emp in employesAssignes)
            {
                if (SingletonAssignation.getInstance().EmployeOccupe(emp.Matricule))
                {
                    cmbxEmpErreur.Text = $"L'employé {emp.Nom} est déjà sur un projet.";
                    assignationReussie = false;
                    continue;
                }

                int heures = emp.HeuresProjet;
                double salaire = emp.TauxHoraire * heures;

                //assignation
                SingletonAssignation.getInstance().ajouterAssignation(
                    numeroCree,            
                    emp.Matricule,
                    heures,
                    salaire
                );
            }

            ContentDialog dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Title = "Projet ajouté",
                Content = $"Le projet {tbxTitre.Text} a été créé avec succès.\n" ,
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();

            await Task.Delay(100);
            Frame.Navigate(typeof(PageListeProjet));
        }
    }
}
