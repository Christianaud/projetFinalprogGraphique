using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageModifierProjet : Page
    {
        public PageModifierProjet()
        {
            InitializeComponent();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
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

            if (string.IsNullOrWhiteSpace(tbxStatut.Text))
            {
                flagValide = false;
                tblStatutErreur.Text = "Le statut ne doit pas être vide !!!";
            }
            else
            {
                string statut = tbxStatut.Text.ToLower();
                if (statut != "en cours" && statut != "terminé")
                {
                    flagValide = false;
                    tblStatutErreur.Text = "Le statut doit être 'en cours' ou 'terminé' !!!";
                }
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

            DateTime dateDebut;
            if (string.IsNullOrWhiteSpace(tbxDateDebut.Text))
            {
                flagValide = false;
                tblDateDebutErreur.Text = "La date de début ne doit pas être vide !!!";
            }
            else if (!DateTime.TryParse(tbxDateDebut.Text, out dateDebut))
            {
                flagValide = false;
                tblDateDebutErreur.Text = "le format de date invalide !!!";
            }
            else if (dateDebut > DateTime.Now)
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

        }
    }
}
