using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projet.Singletons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjouterClient : Page
    {
        public PageAjouterClient()
        {
            InitializeComponent();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool flagValide = true;

            tblIdErreur.Text = string.Empty;
            tblNomErreur.Text = string.Empty;
            tblAdresseErreur.Text = string.Empty;
            tblNumTelErreur.Text = string.Empty;
            tblEmailErreur.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(tbxId.Text))
            {
                flagValide = false;
                tblIdErreur.Text = "L'identifiant ne doit pas être vide !!!";
            }
            else
            {
                int id;
                bool valideId = int.TryParse(tbxId.Text, out id);

                if (!valideId)
                {
                    flagValide = false;
                    tblIdErreur.Text = "L'identifiant doit être un nombre !!!";
                }
                else if (id < 100 || id > 999)
                {
                    flagValide = false;
                    tblIdErreur.Text = "L'identifiant doit être entre 100 et 999 !!!";
                }
            }

            if (string.IsNullOrWhiteSpace(tbxNom.Text))
            {
                flagValide = false;
                tblNomErreur.Text = "Le nom ne doit pas être vide !!!";
            }
            else if (tbxNom.Text.Length < 2)
            {
                flagValide = false;
                tblNomErreur.Text = "Le nom doit contenir au moins 2 lettres !!!";
            }

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

            if (string.IsNullOrWhiteSpace(tbxNumTel.Text))
            {
                flagValide = false;
                tblNumTelErreur.Text = "Le téléphone ne doit pas être vide !!!";
            }
            else
            {
                string tel = tbxNumTel.Text;
                string valideNumTel = @"^(\+?\d{1,3})?[- ]?\d{3}[- ]?\d{3}[- ]?\d{4}$";

                if (tel.Length < 10)
                {
                    flagValide = false;
                    tblNumTelErreur.Text = "Numéro de téléphone trop court !!!";
                }
                else if (!Regex.IsMatch(tel, valideNumTel))
                {
                    flagValide = false;
                    tblNumTelErreur.Text = "Le format de téléphone invalide !!!";
                }

            }

            if (string.IsNullOrWhiteSpace(tbxEmail.Text))
            {
                flagValide = false;
                tblEmailErreur.Text = "L'email ne doit pas être vide !!!";
            }
            else if (!tbxEmail.Text.Contains("@") || !tbxEmail.Text.Contains("."))
            {
                flagValide = false;
                tblEmailErreur.Text = "Le format email invalide !!!";
            }

            if(flagValide == true)
            {
                SingletonListe.getInstance().ajouterClient(tbxNom.Text, tbxAdresse.Text, tbxNumTel.Text, tbxEmail.Text);
            }

        }
    }
}
