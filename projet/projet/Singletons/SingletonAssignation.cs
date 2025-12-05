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
    internal class SingletonAssignation
    {
        string connectionString;
        ObservableCollection<Assignation> listeAssignations;
        static SingletonAssignation? instance = null;

        // Constructeur privé
        private SingletonAssignation()
        {
            connectionString = "Server=cours.cegep3r.info;Database=a2025_420335-345ri_greq1;Uid=6303034;Pwd=6303034";
            listeAssignations = new ObservableCollection<Assignation>();
        }

        // Retourne l’instance du singleton
        public static SingletonAssignation getInstance()
        {
            if (instance == null)
                instance = new SingletonAssignation();

            return instance;
        }

        // Propriété qui retourne la liste des assignations
        public ObservableCollection<Assignation> Liste
        {
            get { return listeAssignations; }
        }

        public void getAllAssignations()
        {
            listeAssignations.Clear();

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = con.CreateCommand();

                cmd.CommandText = "SELECT * FROM assignation";
                con.Open();

                using MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int id = r.GetInt32("id");
                    string projetNumero = r.GetString("projetNumero");
                    string employeMatricule = r.GetString("employeMatricule");
                    double heures = r.GetDouble("heuresTravaillees");
                    double salaire = r.GetDouble("salaireProjet");

                    Assignation a = new Assignation(id, projetNumero, employeMatricule, heures, salaire);
                    listeAssignations.Add(a);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ajouterAssignation(string projetNumero, string employeMatricule, double heuresTravaillees, double salaireProjet)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = con.CreateCommand();

                cmd.CommandText = @"INSERT INTO assignation (projetNumero, employeMatricule, heuresTravaillees, salaireProjet)
                                    VALUES (@projetNumero, @employeMatricule, @heures, @salaire)";

                cmd.Parameters.AddWithValue("@projetNumero", projetNumero);
                cmd.Parameters.AddWithValue("@employeMatricule", employeMatricule);
                cmd.Parameters.AddWithValue("@heures", heuresTravaillees);
                cmd.Parameters.AddWithValue("@salaire", salaireProjet);

                con.Open();
                cmd.ExecuteNonQuery();

                getAllAssignations();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void modifierAssignation(int id, double heuresTravaillees, double salaireProjet)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = con.CreateCommand();

                cmd.CommandText = @"UPDATE assignation 
                                    SET heuresTravaillees = @heures,
                                        salaireProjet = @salaire
                                    WHERE id = @id";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@heures", heuresTravaillees);
                cmd.Parameters.AddWithValue("@salaire", salaireProjet);

                con.Open();
                cmd.ExecuteNonQuery();

                getAllAssignations();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
