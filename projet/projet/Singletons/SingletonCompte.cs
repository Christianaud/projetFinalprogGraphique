using Microsoft.WindowsAppSDK.Runtime.Packages;
using MySql;
using MySql.Data.MySqlClient;
using projet.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.Classes
{
    internal class SingletonCompte
    {
        string connectionString;
        string username;
        string password;
        bool doesAdminExist;
        bool isConnected = false;
        List<Admin> listeAdmins;

        // vv Doit changer vv

        public SingletonCompte()
        {
            connectionString = "Server=localhost;Database=mabd;Uid=utilisateur;Pwd=1234;";
        }

        static SingletonCompte instance = null;

        public static SingletonCompte getInstance()
        {
            if (instance == null)
                instance = new SingletonCompte();
            return instance;
        }

        public void getAllAdmins()
        {
            listeAdmins.Clear();

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand commande = con.CreateCommand();
                commande.CommandText = "Select * from administrateur";
                con.Open();
                using MySqlDataReader r = commande.ExecuteReader();
                while (r.Read())
                {
                    string nomUsager = r.GetString("nomUsager");
                    string motDePasse = r.GetString("motDePasse");
                    Admin admin = new Admin(nomUsager, motDePasse);
                    listeAdmins.Add(admin);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool doesInfoMatch(string nomUtilisateur, string motDePasse)
        {
            Admin check = new Admin(nomUtilisateur, motDePasse);
            if (listeAdmins.Contains(check))
            {
                return true;
                isConnected = true;
            }
            else
                return false;
        }

        public bool IsConnected { get => isConnected; set => isConnected = value; }

        // Vérifie l'esxistence du compte admin. Lancer au démarrage de l'app.
        public void VerifyAdmin()
        {
            using MySqlConnection con = new MySqlConnection(connectionString);
            try
            {
                using var cmd = new MySqlCommand("SELECT COUNT(*) AS counter FROM admin", con);
                con.Open();

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int count = reader.GetInt32("counter");
                    doesAdminExist = count > 0;
                }
            }
            catch (MySqlException)
            {
                // handle error if needed
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void setAdmin(string username, string password)
        {
            using MySqlConnection con = new MySqlConnection(connectionString);
            try
            {
                using MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into admin(username, password) values(@username, @password)";
                commande.Parameters.AddWithValue("@username", username);
                commande.Parameters.AddWithValue("@password", password);
                con.Open();
                int i = commande.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException ex)
            {
                //message d'erreur eventuel
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }
    }
}