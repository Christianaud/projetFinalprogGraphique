using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.ClassesParJacques
{
    internal class SingletonListe
    {
        string connectionString;
        ObservableCollection<Client> listeClient;
        ObservableCollection<Projet> listeProjets;
        static SingletonListe instance = null;
        //constructeur de la classe
        public SingletonListe()
        {
            connectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq1;Uid=2471354;Pwd=2471354;";
            listeClient = new ObservableCollection<Client>();
            listeProjets = new ObservableCollection<Projet>();
        }
        //retourne l’instance du singleton
        public static SingletonListe getInstance()
        {
            if (instance == null)
                instance = new SingletonListe();
            return instance;
        }
        /*PROPRIÉTÉS*/
        //Propriété qui retourne la liste des clients
        public ObservableCollection<Client> ListeClients { get => listeClient; }
        public ObservableCollection<Projet> ListeProjets { get => listeProjets; }

        /*MÉTHODES*/
        //retourne un client à une position précise
        public Client getClient(int position)
        {
            return listeClient[position];
        }

        public Projet GetProjet(int position)
        { 
            return listeProjets[position]; 
        }

        public void getAllClients() {
            listeClient.Clear(); //permet de vider la liste avant de la recharger
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from client";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    int id = r.GetInt32("id");
                    string nom = r.GetString("nom");
                    string adresse = r.GetString("adresse");
                    string num_tel = r.GetString("numTel");
                    string email = r.GetString("email");
                    Client client = new Client(id, nom, adresse, num_tel, email);
                    listeClient.Add(client);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void getAllProjets()
        {
            listeProjets.Clear(); //permet de vider la liste avant de la recharger
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from programs";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    int matricule = r.GetInt32("matricule");
                    string titre = r.GetString("titre");
                    string adresse = r.GetString("adresse");
                    DateTime dateDebut = r.GetDateTime("date_debut");
                    string description = r.GetString("description");
                    int budget = r.GetInt32("budget");
                    int nbrEmployes = r.GetInt32("nbr_employes");
                    int totalSalaire = r.GetInt32("total_salaires");
                    int idClient = r.GetInt32("id_client");
                    string statut = r.GetString("statut");
                    Projet projet = new Projet(matricule, titre, dateDebut, description, budget, nbrEmployes, totalSalaire, idClient, statut);
                    listeProjets.Add(projet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        //ajoute un client dans la liste
        public void ajouterClient(int id, string nom, string adresse, int num_tel, string email)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into client values(null, @nom, @adresse, @numTel, @email) ";
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@numTel", num_tel);
                commande.Parameters.AddWithValue("@email", email);
                con.Open();
                int i = commande.ExecuteNonQuery();
                using MySqlCommand commande2 = new MySqlCommand();
                commande2.Connection = con;
                commande2.CommandText = "select LAST_INSERT_ID() ";
                var res = commande2.ExecuteScalar();
                getAllClients(); //permet de recharger la liste des clients après un ajout
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ajouterProjer(int numero, string titre, DateTime dateDebut, string description, int budget, int nbrEmployes, int totalSalaire, int idClient, string statut)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into projet values(null, @titre, @date_debut, @description, @budget, @nbrEmployes, @totalSalaire, @idClient, @statut);";
                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@date_debut", dateDebut);
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@budget", budget);
                commande.Parameters.AddWithValue("@nbrEmployes", nbrEmployes);
                commande.Parameters.AddWithValue("@totalSalaire", totalSalaire);
                commande.Parameters.AddWithValue("@idClient", idClient);
                commande.Parameters.AddWithValue("@statut", statut);
                con.Open();
                int i = commande.ExecuteNonQuery();
                using MySqlCommand commande2 = new MySqlCommand();
                commande2.Connection = con;
                commande2.CommandText = "select LAST_INSERT_ID() ";
                var res = commande2.ExecuteScalar();
                getAllProjets(); //permet de recharger la liste des projets après un ajout
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        //modifie un client à une position précise
        public void modifierClient(int position, Client client)
        {
            listeClient[position] = client;
        }
        //supprime à une position précise
        public void supprimerProjet(int id)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "delete from projet where id = @id";
                commande.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = commande.ExecuteNonQuery();
                getAllProjets(); //permet de recharger la liste des projet après un ajout
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void supprimerClient(int id)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "delete from client where id = @id";
                commande.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = commande.ExecuteNonQuery();
                getAllClients(); //permet de recharger la liste des clients après un ajout
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
