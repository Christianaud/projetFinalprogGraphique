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
using System.Collections.ObjectModel;
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
    public sealed partial class PageAjouterProjet : Page
    {
        // Collection des employés assignés
        ObservableCollection<Employe> employesAssignes = new ObservableCollection<Employe>();

        public PageAjouterProjet()
        {
            InitializeComponent();
            cmbxEmp.ItemsSource = SingletonEmploye.getInstance().Liste;
            lvAssignations.ItemsSource = employesAssignes;
        }

        private void cmbxEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employe emp = cmbxEmp.SelectedItem as Employe;

            if (emp == null)
            {
                cmbxEmpErreur.Text ="Sélectionnez un employé.";
                return;
            }

            if (employesAssignes.Contains(emp))
            {
                cmbxEmp.Text = "Cet employé est déjà assigné.";
                return;
            }

            if (employesAssignes.Count >= 5)
            {
                cmbxEmp.Text= "Un projet ne peut pas avoir plus de 5 employés.";
                return;
            }

            employesAssignes.Add(emp);

            cmbxEmp.SelectedIndex = -1;
        }

        private void btnCreer_Click(object sender, RoutedEventArgs e)
        {
            SingletonListe.getInstance().ajouterProjer(
                tbxTitre.Text,
                dpDateDebut.Date.DateTime,  
                tbxDescription.Text,
                Convert.ToDouble(tbxBudget.Text),
                Convert.ToInt32(tbxNbrEmployes.Text),
                Convert.ToDouble(tbxTotalSalaire.Text), 
                Convert.ToInt32(tbxIdClient.Text),                  
                tbxStatut.Text                     
            );

            foreach (var emp in employesAssignes)
            {
                // Vérifier si l'employé est déjà occupé
                if (SingletonAssignation.getInstance().EmployeOccupe(emp.Matricule))
                {
                    cmbxEmp.Text = $"L'employé {emp.Nom} est déjà sur un projet en cours.";
                    continue; // on saute cet employé
                }

                SingletonAssignation.getInstance().ajouterAssignation(
                    numProjet,
                    emp.Matricule,
                    0, // heures par défaut
                    0  // salaire par défaut
                );
            }
        }
    }
}
