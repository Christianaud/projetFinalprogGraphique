using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projet.dialogue
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BoiteHeuresDialog : ContentDialog
    {
        public int Heures { get; set; }
        public BoiteHeuresDialog()
        {
            InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string texte = tbx_heures.Text;

            if (!int.TryParse(texte, out int h) || h <= 0)
            {
                txt_erreur.Text = "Veuillez entrer un nombre d'heures valide (minimum 1).";
                txt_erreur.Visibility = Visibility.Visible;

                // Empêche la fermeture
                args.Cancel = true;
            }
            else
            {
                Heures = h;
                txt_erreur.Visibility = Visibility.Collapsed;
            }
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            // Si on clique sur Annuler → on laisse fermer
            if (args.Result != ContentDialogResult.Primary)
            {
                args.Cancel = false;
                return;
            }

            // Si on clique sur Confirmer mais que pas valide → ne pas fermer
            if (Heures <= 0)
            {
                args.Cancel = true;
            }
        }
    }
}
