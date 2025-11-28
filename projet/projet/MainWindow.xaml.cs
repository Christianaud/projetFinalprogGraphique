using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projet.pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        int Test = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;
            if (item != null)
            {
                switch (item.Tag)
                {
                    case "importer":
                        Debug.WriteLine("NOUVEAU");
                        //code pour nouveau
                        break;
                    case "exporter":
                        Debug.WriteLine("OUVERTURE");
                        //code pour ouverture
                        break;
                }
            }

        }

        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer is NavigationViewItem item)
            {
                switch (item.Tag)
                {
                    case "employes":
                        mainFrame.Navigate(typeof(PageAffichEmp));
                        break;
                    case "clients":
                        //mainFrame.Navigate(typeof(PageAffichCl));
                        break;
                    case "projets":
                        //mainFrame.Navigate(typeof(PageCommandes));
                        break;
                    default:
                        break;
                }
            }
        }
        private void navView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (mainFrame.CanGoBack)
                mainFrame.GoBack();
        }
    }
}
