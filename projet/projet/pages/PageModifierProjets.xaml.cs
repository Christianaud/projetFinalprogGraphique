using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projet.classes;
using projet.Singletons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                tbxTitre.Text = projet.Titre;
                cmbxStatut.SelectedItem = projet.Statut;
                tbxDescription.Text = projet.Description;
                dprDateDebut.Date = projet.DateDebut.Date;
                tbxIdClient.Text = projet.IdClient.ToString();
                tbxBudget.Text = projet.Budget.ToString();
                tbxNomClient.Text = projet.NomClient;
                tbxNbrEmployes.Text = projet.NbrEmployes.ToString();
                tbxTotalSalaire.Text = projet.TotalSalaire.ToString();
            }
        }
        private async void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            bool flagValide = true;

            tblTitreErreur.Text = "";
            tblStatutErreur.Text = "";
            tblIdClientErreur.Text = "";
            tblNomClientErreur.Text = "";
            tblDateDebutErreur.Text = "";
            tblDescriptionErreur.Text = "";
            tblBudgetErreur.Text = "";
            tblNbrEmployesErreur.Text = "";
            tblTotalSalaireErreur.Text = "";

            if (string.IsNullOrWhiteSpace(tbxTitre.Text))
            {
                flagValide = false;
                tblTitreErreur.Text = "Le titre ne doit pas être vide !!!";
            }
            else if (tbxTitre.Text.Length < 3)
            {
                flagValide = false;
                tblTitreErreur.Text = "Le titre doit contenir au moins 3 caractères !!!";
            }

            if (cmbxStatut.SelectedItem == null)
            {
                flagValide = false;
                tblStatutErreur.Text = "Veuillez choisir un statut !!!";
            }

            if (string.IsNullOrWhiteSpace(tbxIdClient.Text))
            {
                flagValide = false;
                tblIdClientErreur.Text = "L'identifiant du client ne doit pas être vide !!!";
            }
            else
            {
                int idClient;
                if (!int.TryParse(tbxIdClient.Text, out idClient))
                {
                    flagValide = false;
                    tblIdClientErreur.Text = "L'identifiant du client doit être un nombre !!!";
                }
            }

            if (string.IsNullOrWhiteSpace(tbxNomClient.Text))
            {
                flagValide = false;
                tblNomClientErreur.Text = "Le nom du client ne doit pas être vide !!!";
            }

            DateTime dateDebut = dprDateDebut.Date.DateTime;
            if (dateDebut > DateTime.Now)
            {
                flagValide = false;
                tblDateDebutErreur.Text = "La date de début ne peut pas être dans le futur !!!";
            }

            if (string.IsNullOrWhiteSpace(tbxDescription.Text))
            {
                flagValide = false;
                tblDescriptionErreur.Text = "La description ne doit pas être vide !!!";
            }
            else if (tbxDescription.Text.Length < 10)
            {
                flagValide = false;
                tblDescriptionErreur.Text = "La description doit contenir au moins 10 caractères !!!";
            }

            double budget;
            if (string.IsNullOrWhiteSpace(tbxBudget.Text))
            {
                flagValide = false;
                tblBudgetErreur.Text = "Le budget ne doit pas être vide !!!";
            }
            else if (!double.TryParse(tbxBudget.Text, out budget))
            {
                flagValide = false;
                tblBudgetErreur.Text = "Le budget doit être un nombre !!!";
            }
            else if (budget < 1000)
            {
                flagValide = false;
                tblBudgetErreur.Text = "Le budget doit être supérieur à 1000 $ !!!";
            }

            int nbEmployes;
            if (string.IsNullOrWhiteSpace(tbxNbrEmployes.Text))
            {
                flagValide = false;
                tblNbrEmployesErreur.Text = "Veuillez entrer un nombre d'employés !!!";
            }
            else if (!int.TryParse(tbxNbrEmployes.Text, out nbEmployes))
            {
                flagValide = false;
                tblNbrEmployesErreur.Text = "Le nombre d'employés doit être un entier !!!";
            }
            else if (nbEmployes < 1 || nbEmployes > 5)
            {
                flagValide = false;
                tblNbrEmployesErreur.Text = "Maximum 5 employés requis !!!";
            }

            double totalSalaires;
            if (string.IsNullOrWhiteSpace(tbxTotalSalaire.Text))
            {
                flagValide = false;
                tblTotalSalaireErreur.Text = "Le total des salaires ne doit pas être vide !!!";
            }
            else if (!double.TryParse(tbxTotalSalaire.Text, out totalSalaires))
            {
                flagValide = false;
                tblTotalSalaireErreur.Text = "Le total des salaires doit être un nombre !!!";
            }

            if (flagValide == true)
            {
                SingletonListe.getInstance().modifierProjet(projet.Numero, tbxTitre.Text, dprDateDebut.Date.DateTime, tbxDescription.Text, int.Parse(tbxBudget.Text), int.Parse(tbxNbrEmployes.Text), int.Parse(tbxTotalSalaire.Text), int.Parse(tbxIdClient.Text), cmbxStatut.SelectedItem as string);
                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Title = " Le projet modifié ",
                    Content = $"Le projet {tbxTitre.Text} a été modifié avec succès.",
                    CloseButtonText = "OK"
                };
                await dialog.ShowAsync();
                await Task.Delay(100);
                Frame.Navigate(typeof(PageListeProjet));
            }
        }
    }
}
