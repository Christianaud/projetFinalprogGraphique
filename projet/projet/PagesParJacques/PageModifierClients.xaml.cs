using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projet.Classes;
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

namespace projet.PagesParJacques;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PageModifierClients : Page
{
    Client client;
    public PageModifierClients()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        client = e.Parameter as Client;
        if (client != null)
        {
            tbxNom.Text = "Nom: " + client.Nom;
            tbxAdresse.Text = "Adresse: " + client.Adresse;
            tbxNumTel.Text = "Telephone: " + client.Num_tel;
            tbxEmail.Text = "Email: " + client.Email.ToString();
        }
    }

    private void btnModifier_Click(object sender, RoutedEventArgs e)
    {
        SingletonListe.getInstance().modifierClient(client.Id, tbxNom.Text, tbxAdresse.Text, tbxNumTel.Text, tbxEmail.Text);
    }
}
