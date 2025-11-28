using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using projet.classes;
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

    private void btnAjout_Click(object sender, RoutedEventArgs e)
    {
        bool flagValide = true;
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

        } else if (tbxMatricule.Text.Length < 11)
        {
            tblMatriculeErreur.Text = "Le champs matricule ne doit excéder 10 lettres";
        }

        if (string.IsNullOrWhiteSpace(tbxNom.Text))
        {
            flagValide = false;
            tblNomErreur.Text = "Ce champ nom ne doit pas etre vide !!! ";
        } else if (tbxMatricule.Text.Length < 3)
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



        if (string.IsNullOrWhiteSpace(tbxNom.Text))
        {
            flagValide = false;
            tblNomErreur.Text = "Ce champ nom ne doit pas etre vide !!! ";
        }



    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        employeAModifier = e.Parameter as Employe;
        if (employeAModifier != null)
        {
            tbxNom.Text = employeAModifier.Nom;
            tbxPrenom.Text = employeAModifier.Prenom;
            dprNaissance.SelectedDate = employeAModifier.DateNaissance;
            tbxEmail.Text = employeAModifier.Email;
            tbxAdresse.Text = employeAModifier.Adresse;
            dprEmbauche.SelectedDate = employeAModifier.DateEmbauche;
            nbxTauxHoraire.Text = employeAModifier.TauxHoraire;
            tbxPhoto.Text = employeAModifier.Photo;
            cmbxStatut.Text = employeAModifier.Statut;

            btnAjout.Content = "Modifier";

        }
    }


}
