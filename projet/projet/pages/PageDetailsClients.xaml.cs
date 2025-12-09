using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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

namespace projet.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageDetailsClients : Page
    {
        Client client;
        public PageDetailsClients()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            client = e.Parameter as Client;
            if (client != null)
            {
                tbxNom.Text = "Nom:\n " + client.Nom;
                tbxId.Text = "Id:\n " + client.Id;
                tbxAdresse.Text = "Adresse:\n " + client.Adresse;
                tbxNumTel.Text = "Telephone:\n " + client.Num_tel;
                tbxEmail.Text = "Email:\n " + client.Email.ToString();

            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            Client newC = client;

            if (newC != null)
            {
                Frame.Navigate(typeof(PageModifierClient), newC);
            }
        }
    }
}
