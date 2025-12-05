using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using projet.classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.Singletons
{
    internal class SingletonListe
    {
        string connectionString;
        ObservableCollection<classes.Client> listeClient;
        ObservableCollection<Projet> listeProjets;
        static SingletonListe instance = null;

        //constructeur de la classe
        public SingletonListe()
        {
            connectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq1;Uid=2471354;Pwd=2471354;";
            listeClient = new ObservableCollection<classes.Client>();
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
        public ObservableCollection<classes.Client> ListeClients { get => listeClient; }
        public ObservableCollection<Projet> ListeProjets { get => listeProjets; }



        /*MÉTHODES*/
        //retourne un client à une position précise
        public classes.Client getClient(int position)
        {
            return listeClient[position];
        }

        public Projet GetProjet(int position)
        {
            return listeProjets[position];
        }

        public void getAllClients()
        {
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
                    classes.Client client = new classes.Client(id, nom, adresse, num_tel, email);
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
                using MySqlCommand commande = new MySqlCommand("affiche_projet_client");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string numero = r.GetString("numero");
                    string titre = r.GetString("titre");
                    DateTime dateDebut = r.GetDateTime("dateDebut");
                    string description = r.GetString("description");
                    int budget = r.GetInt32("budget");
                    int nbrEmployes = r.GetInt32("nbEmploye");
                    int totalSalaire = r.GetInt32("totalSalaireAPayer");
                    int idClient = r.GetInt32("idClient");
                    string statut = r.GetString("statut");
                    string nomClient = r.GetString("nom");
                    Projet projet = new Projet(numero, titre, dateDebut, description, budget, nbrEmployes, totalSalaire, idClient, nomClient, statut);
                    listeProjets.Add(projet);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        //ajoute un client dans la liste
        public void ajouterClient(string nom, string adresse, string num_tel, string email)
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

        public void modifierClient(int client_id, string nom, string adresse, string num_tel, string email)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update client set nom = @nom, email = @email, adresse = @adresse, numTel = @num_tel where id = @client_id";
                commande.Parameters.AddWithValue("@client_id", client_id);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@num_Tel", num_tel);
                commande.Parameters.AddWithValue("@email", email);
                con.Open();
                commande.ExecuteNonQuery();

                getAllClients();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ajouterProjer(string titre, DateTime dateDebut, string description, double budget, int nbrEmployes, double totalSalaire, int idClient, string statut)
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

        public void modifierProjet(string numero, string titre, DateTime dateDebut, string description, int budget, int nbrEmployes, int totalSalaire, int idClient, string statut)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update projet set titre = @titre, dateDebut = @date_debut, description = @description, budget = @budget, nbEmploye = @nbrEmployes, statut = @statut, idClient = @idClient, totalSalaireAPayer = @totalSalaire where numero = @numero";
                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@date_debut", dateDebut);
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@budget", budget);
                commande.Parameters.AddWithValue("@nbrEmployes", nbrEmployes);
                commande.Parameters.AddWithValue("@totalSalaire", totalSalaire);
                commande.Parameters.AddWithValue("@idClient", idClient);
                commande.Parameters.AddWithValue("@statut", statut);
                commande.Parameters.AddWithValue("@numero", numero);
                con.Open();
                commande.ExecuteNonQuery();

                getAllClients();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
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

        public Projet DernierProjet()
        {
            return listeProjets[listeProjets.Count - 1];
        }
    }
}
