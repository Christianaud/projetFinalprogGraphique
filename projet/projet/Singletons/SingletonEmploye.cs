using MySql.Data.MySqlClient;
using projet.classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.Singletons
{
    class SingletonEmploye
    {
        string connectionString;
        ObservableCollection<Employe> listeEmpls;
        static SingletonEmploye instance = null;

        private SingletonEmploye()
        {
            connectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq1;Uid=6303034;Pwd=6303034";
            listeEmpls = new ObservableCollection<Employe>();
        }
        //retourne l’instance du singleton
        public static SingletonEmploye getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonEmploye();

            }
            return instance;
        }
        //Propriété qui retourne la liste des Maisons
        public ObservableCollection<Employe> Liste { get => listeEmpls; }

        public void getAllEmpls() //charge la liste avec tous les Maisons
        {
            listeEmpls.Clear(); //permet de vider la liste avant de la recharger
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from employe";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string matricule = r.GetString("matricule");
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");
                    DateTime dateNaissance = r.GetDateTime("dateNaissance");
                    string email = r.GetString("email");
                    string adresse = r.GetString("adresse");
                    DateTime dateEmbauche = r.GetDateTime("dateEmbauche");
                    double tauxHoraire = r.GetDouble("tauxHoraire");
                    string photo = r.GetString("photo");
                    string statut = r.GetString("statut");

                    Employe e = new Employe(matricule, nom, prenom, dateNaissance, email, adresse, dateEmbauche, tauxHoraire, photo, statut);
                    listeEmpls.Add(e);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        //ajoute un Maison dans la liste
        public void ajouterEmploye(string matricule, string nom, string prenom, DateTime dateNaissance, string email, string adresse, DateTime dateEmbauche, double tauxHoraire, string photo, string statut)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into employe values(@matricule, @nom, @prenom, @dateNaissance, @email, @adresse, @dateEmbauche, @tauxHoraire, @photo, @statut) ";
                commande.Parameters.AddWithValue("@matricule", matricule);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@dateNaissane", dateNaissance);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@dateEmbauche", dateEmbauche);
                commande.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@photo", photo);
                commande.Parameters.AddWithValue("@statut", statut);

                con.Open();
                commande.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void modifierEmploye(string matricule, string nom, string prenom, string email, string adresse, double tauxHoraire, string photo, string statut)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update employe set nom = @nom, prenom = @prenom, email = @email, adresse = @adresse, tauxHoraire = @tauxHoraire, photo = @photo, statut = @statut where matricule = @matricule";
                commande.Parameters.AddWithValue("@matricule", matricule);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@tauxHoraire", tauxHoraire);
                commande.Parameters.AddWithValue("@photo", photo);
                commande.Parameters.AddWithValue("@statut", statut);
                con.Open();
                commande.ExecuteNonQuery();

                getAllEmpls();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
