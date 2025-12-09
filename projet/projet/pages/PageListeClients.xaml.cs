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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageListeClients : Page
    {
        public PageListeClients()
        {
            InitializeComponent();
            listeClient.ItemsSource = SingletonListe.getInstance().ListeClients;
            SingletonListe.getInstance().getAllClients();
        }

        private void listeClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client newC = (Client)listeClient.SelectedItem;

            if (newC != null)
            {
                Frame.Navigate(typeof(PageDetailsClients), newC);
            }
        }
    }
}
