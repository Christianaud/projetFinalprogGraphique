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
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageEmpDetails : Page
    {
        Employe employeAafficher;
        public PageEmpDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            employeAafficher = e.Parameter as Employe;
            if (employeAafficher != null)
            {
                tbxMatricule.Text = employeAafficher.Matricule;
                tbxNom.Text += employeAafficher.Nom;
                tbxPrenom.Text += employeAafficher.Prenom;
                tbxEmail.Text += employeAafficher.Email;
                tbxAdresse.Text += employeAafficher.Adresse;
                tbxDateEmbauche.Text += employeAafficher.DateEmbauche.Date.ToString();
                tbxDateNaissance.Text += employeAafficher.DateNaissance.ToString();
                tbxTauxHoraire.Text += employeAafficher.TauxHoraire.ToString();
                tbxStatut.Text += employeAafficher.Statut.ToString();

                Uri uri = new Uri(employeAafficher.Photo);
                imgPhoto.Source = new BitmapImage(uri);
                
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageAjoutEmp), employeAafficher);
        }
    }

   
}
