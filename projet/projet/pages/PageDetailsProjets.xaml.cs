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
using System.Diagnostics;
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
    public sealed partial class PageDetailsProjets : Page
    {
        Projet projet;
        public PageDetailsProjets()
        {
            InitializeComponent();
            SingletonAssignation.getInstance().getAllAssignations();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            projet = e.Parameter as Projet;
            if (projet != null)
            {
               
                tbxNumero.Text = projet.Numero;
                tbxTitre.Text = projet.Titre;
                tbxStatut.Text =  projet.Statut;
                tbxDescription.Text = projet.Description;
                tbxDateDebut.Text = projet.DateDebut.ToString();
                tbxBudget.Text = projet.Budget.ToString()+ " $";
                tbxNomClient.Text =  projet.NomClient;
                tbxNbrEmployes.Text = projet.NbrEmployes.ToString()+ " employés";
                var toutesAssignations = SingletonAssignation.getInstance().Liste;
                Debug.WriteLine($"Nombre total assignations : {toutesAssignations.Count}");
                List<Assignation>assignerAuProjet = new List<Assignation>();
                foreach(var assignation in toutesAssignations) {
                    if(assignation.ProjetNumero == projet.Numero) {
                        assignerAuProjet.Add(assignation);
                    }
                }
                lvEmployesAssigne.ItemsSource = assignerAuProjet;
                tbxTotalSalaire.Text = projet.TotalSalaire.ToString()+ " $";
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Projet newP = projet;

            if (newP != null)
            {
                Frame.Navigate(typeof(PageModifierProjets), newP);
            }
        }
    }
}
