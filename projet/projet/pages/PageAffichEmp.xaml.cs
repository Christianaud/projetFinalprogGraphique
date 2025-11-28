using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using projet.classes;
using projet.Singletons;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PageAffichEmp : Page
{
    public PageAffichEmp()
    {
        InitializeComponent();
        grEmp.ItemsSource = SingletonEmploye.getInstance().Liste;
        SingletonEmploye.getInstance().getAllEmpls();
    }

    private void grEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Employe employeSelectionnee = (Employe)grEmp.SelectedItem;

        if(employeSelectionnee != null ) {
            Frame.Navigate(typeof(PageEmpDetails), employeSelectionnee);
        }
    }

    private void appBarAjoutEmp_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(pageAjoutEmp));

    }
}
