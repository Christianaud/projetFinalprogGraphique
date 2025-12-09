using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using projet.classes;
using projet.Singletons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class pageAjoutEmp : Page
{
    Employe employeAModifier;
    bool modeEdition = false;
    public pageAjoutEmp()
    {
        InitializeComponent();
    }

    private async void btnAjout_Click(object sender, RoutedEventArgs e)
    {
        bool flagValide = true;

        tblMatriculeErreur.Text = string.Empty;
        tblNomErreur.Text = string.Empty;
        tblPrenomErreur.Text = string.Empty;
        tblDateNaissanceErreur.Text = string.Empty;
        tblDateEmbaucheErreur.Text = string.Empty;
        tblEmailErreur.Text= string.Empty;
        tblAdresseErreur.Text = string.Empty;
        tblTauxHoraireErreur.Text = string.Empty;
        tblPhotoErreur.Text = string.Empty ;
        tblStatutErreur.Text = string.Empty ;

        // validation matricule
        if (string.IsNullOrWhiteSpace(tbxMatricule.Text))
        {
            flagValide = false;
            tblMatriculeErreur.Text = "Le champs matricule ne doit etre vide !!!";

        }

        if (string.IsNullOrWhiteSpace(tbxNom.Text))
        {
            flagValide = false;
            tblNomErreur.Text = "Ce champ nom ne doit pas etre vide !!! ";
        } else if (tbxNom.Text.Length < 3)
        {
            tblNomErreur.Text = "Un nom doit avoir plus de 2 mots !!!";
        }

        if (string.IsNullOrWhiteSpace(tbxPrenom.Text))
        {
            flagValide = false;
            tblPrenomErreur.Text = "Ce champ nom ne doit pas etre vide !!! ";
        } else if(tbxPrenom.Text.Length < 3)
        {
            tblPrenomErreur.Text = "Un prénom doit avoir plus de 2 mots !!!";
        }

        // validation date de naissance
        if (dprNaissance.Date==null)
        {
            flagValide = false;
            tblDateNaissanceErreur.Text = "La date de naissance ne doit pas être vide !!!";
        }
        else
        {
            DateTime date_Naissance = dprNaissance.Date.DateTime;

            if (date_Naissance > DateTime.Now)
            {
                flagValide = false;
                tblDateNaissanceErreur.Text = "La date de naissance ne peut pas etre dans le futur";
            }else {
                DateTime dixHuitAns = DateTime.Now.AddYears(-18);

                if (date_Naissance> dixHuitAns)
                {
                    flagValide=false;
                    tblDateNaissanceErreur.Text = "L'employé doit avoir au moins 18 ans !!!";
                }
            }
        }

        // validation date d'embauche
        if (dprEmbauche.Date == null)
        {
            flagValide = false;
            tblDateEmbaucheErreur.Text = "La date d'embauche ne doit pas être vide !!!";
        }
        else
        {
            DateTime date_Embauche = dprEmbauche.Date.DateTime;

            if (date_Embauche > DateTime.Now)
            {
                flagValide = false;
                tblDateEmbaucheErreur.Text = "La date d'embauche ne peut pas être dans le futur !!!";
            }

        }

        // validation email
        if (string.IsNullOrWhiteSpace(tbxEmail.Text))
        {
            flagValide = false;
            tblEmailErreur.Text = "Le champ email ne doit pas être vide !!!";
        }
        else if (!tbxEmail.Text.Contains("@") || !tbxEmail.Text.Contains("."))
        {
            flagValide = false;
            tblEmailErreur.Text = "Format email invalide !!!";
        }

        // validation adresse
        if (string.IsNullOrWhiteSpace(tbxAdresse.Text))
        {
            flagValide = false;
            tblAdresseErreur.Text = "L'adresse ne doit pas être vide !!!";
        }
        else if (tbxAdresse.Text.Length < 5)
        {
            flagValide = false;
            tblAdresseErreur.Text = "L'adresse doit contenir au moins 5 caractères !!!";
        }

        // validation taux horaire
        if (string.IsNullOrWhiteSpace(nbxTauxHoraire.Text))
        {
            flagValide = false;
            tblTauxHoraireErreur.Text = "Le taux horaire ne doit pas être vide !!!";
        }
        else
        {
            double taux;
            bool valide = double.TryParse(nbxTauxHoraire.Text, out taux);

            if (!valide)
            {
                flagValide = false;
                tblTauxHoraireErreur.Text = "Le taux horaire doit être un nombre !!!";
            }
            else if (taux < 15)
            {
                flagValide = false;
                tblTauxHoraireErreur.Text = "Le taux horaire doit être supérieur à 15$ !!!";
            }
        }

        // validation photo
        if (string.IsNullOrWhiteSpace(tbxPhoto.Text))
        {
            flagValide = false;
            tblPhotoErreur.Text = "Veuillez sélectionner une photo !!!";
        }

        // validation statut
        if (cmbxStatut.SelectedIndex == -1)
        {
            flagValide = false;
            tblStatutErreur.Text = "Veuillez sélectionner un statut !!!";
        }

        // on arrete si une erreur existe
        if (flagValide == false) return;

        string matricule = tbxMatricule.Text;
        string nom = tbxNom.Text;
        string prenom = tbxPrenom.Text;
        string dateNaissance = dprNaissance.Date.DateTime.ToString();
        string dateEmbauche = dprEmbauche.Date.DateTime.ToString();
        string email = tbxEmail.Text;
        string adresse = tbxAdresse.Text;
        string tauxHoraire = nbxTauxHoraire.Text;
        string photo = tbxPhoto.Text;
        string statut = cmbxStatut.SelectedItem as string;

        if(modeEdition == true) {
            employeAModifier.Matricule = matricule;
            employeAModifier.Nom = nom;
            employeAModifier.Prenom = prenom;
            employeAModifier.DateNaissance = dprNaissance.Date.DateTime;
            employeAModifier.DateEmbauche = dprEmbauche.Date.DateTime;
            employeAModifier.Email = email;
            employeAModifier.Adresse = adresse;
            employeAModifier.TauxHoraire = double.Parse(nbxTauxHoraire.Text);
            employeAModifier.Photo = photo;
            employeAModifier.Statut = statut;
            SingletonEmploye.getInstance().modifierEmploye(matricule, nom, prenom, email, adresse, double.Parse(nbxTauxHoraire.Text), photo, statut);
            ContentDialog dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Title = "Projet ajouté",
                Content = $"L'employé {tbxNom.Text} {tbxPrenom.Text} a été modifié avec succès.",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
            employeAModifier = null;
            modeEdition = false;
            btnAjout.Content = "Ajouter";
        }else
        {
            Employe nouveauE = new Employe(matricule, nom, prenom, dprNaissance.Date.DateTime, email, adresse,dprEmbauche.Date.DateTime, double.Parse(nbxTauxHoraire.Text), photo, statut);
            SingletonEmploye.getInstance().ajouterEmploye( nom, prenom, dprNaissance.Date.DateTime, email, adresse, dprEmbauche.Date.DateTime, double.Parse(nbxTauxHoraire.Text), photo, statut);
            ContentDialog dialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Title = "Projet ajouté",
                Content = $"L'employé {tbxNom.Text} {tbxPrenom.Text} a été créé avec succès.",
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }

        tbxMatricule.Text = "";
        tbxNom.Text = "";
        tbxEmail.Text = "";
        tbxAdresse.Text = "";
        nbxTauxHoraire.Text = "";
        tbxPhoto.Text = "";
        cmbxStatut.SelectedIndex = -1;

        Frame.Navigate(typeof(PageAffichEmp));
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        employeAModifier = e.Parameter as Employe;
        if (employeAModifier != null)
        {
            tbxMatricule.Text = employeAModifier.Matricule;
            tbxNom.Text = employeAModifier.Nom;
            tbxPrenom.Text = employeAModifier.Prenom;
            dprNaissance.SelectedDate = new DateTimeOffset(employeAModifier.DateNaissance);
            tbxEmail.Text = employeAModifier.Email;
            tbxAdresse.Text = employeAModifier.Adresse;
            dprEmbauche.SelectedDate = new DateTimeOffset( employeAModifier.DateEmbauche);
            nbxTauxHoraire.Text = employeAModifier.TauxHoraire.ToString();
            tbxPhoto.Text = employeAModifier.Photo;
            cmbxStatut.SelectedItem = employeAModifier.Statut;

            modeEdition = true;
            btnAjout.Content = "Modifier";
        }
    }
}
