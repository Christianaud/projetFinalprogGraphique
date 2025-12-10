using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projet.Classes;
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

namespace projet.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
            SingletonCompte.getInstance().VerifyAdmin();
            if (SingletonCompte.getInstance().IsConnected)
            {
                Frame.Navigate(typeof(PageListeProjet));
            }
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            // FAIRE LA VALIDATION
            SingletonCompte.getInstance().setAdmin(tbxUsername.Text, tbxPassword.Text);
            SingletonCompte.getInstance().doesInfoMatch(tbxUsername.Text, tbxPassword.Text);
        }
    }
}
