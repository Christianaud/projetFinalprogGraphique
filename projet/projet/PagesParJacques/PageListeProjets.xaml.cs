using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projet.ClassesParJacques;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.PagesParJacques
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageListeProjets : Page
    {
        public PageListeProjets()
        {
            InitializeComponent();
            listeProjets.ItemsSource = SingletonListe.getInstance().ListeProjets;
            SingletonListe.getInstance().getAllProjets();
        }

        private void listeProjets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projet newP = (Projet)listeProjets.SelectedItem;

            if (newP != null) { 
                Frame.Navigate(typeof(PageDetailsProjets), newP);
            }
        }
    }
}
