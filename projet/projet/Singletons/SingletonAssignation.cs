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
                    int heures = r.GetInt32("heuresTravaillees");
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

        public void ajouterAssignation(string projetNumero, string employeMatricule, int heuresTravaillees, double salaireProjet)
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

        public void modifierAssignation(int id, int heuresTravaillees, double salaireProjet)
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

        public bool EmployeOccupe(string matricule)
        {
            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = con.CreateCommand();

                cmd.CommandText = @"SELECT COUNT(*) 
                            FROM assignation a 
                            INNER JOIN projet p ON p.numero = a.projetNumero
                            WHERE employeMatricule = @mat AND p.statut = 'En cours'";

                cmd.Parameters.AddWithValue("@mat", matricule);

                con.Open();
                int nb = Convert.ToInt32(cmd.ExecuteScalar());
                return nb > 0;
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
                return true;
            }
        }

        // Méthode pour obtenir la liste des matricules des employés occupés
        public List<string> GetMatriculesEmployesOccupes()
        {
            List<string> matriculesOccupes = new List<string>();

            try
            {
                using MySqlConnection con = new MySqlConnection(connectionString);
                using MySqlCommand cmd = con.CreateCommand();

                // Sélectionne les employés assignés à des projets "En cours"
                cmd.CommandText = @"SELECT DISTINCT a.employeMatricule 
                                FROM assignation a 
                                INNER JOIN projet p ON p.numero = a.projetNumero
                                WHERE p.statut = 'En cours'";

                con.Open();
                using MySqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string matricule = r.GetString("employeMatricule");
                    matriculesOccupes.Add(matricule);
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return matriculesOccupes;
        }

        // Optionnel: Méthode pour vérifier si un employé est disponible
        public bool EstEmployeDisponible(string matricule)
        {
            return !EmployeOccupe(matricule);
        }
    }
}
