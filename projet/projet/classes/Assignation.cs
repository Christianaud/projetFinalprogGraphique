using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.classes
{
    public class Assignation
    {
        int id;
        string projetNumero;
        string employeMatricule;
        int heuresTravaillees;
        double salaireProjet;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Assignation(int id, string projetNumero, string employeMatricule, int heuresTravaillees, double salaireProjet)
        {
            this.id = id;
            this.projetNumero = projetNumero;
            this.employeMatricule = employeMatricule;
            this.heuresTravaillees = heuresTravaillees;
            this.salaireProjet = salaireProjet;
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        public string ProjetNumero
        {
            get { return projetNumero; }
            set
            {
                projetNumero = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjetNumero)));
            }
        }

        public string EmployeMatricule
        {
            get { return employeMatricule; }
            set
            {
                employeMatricule = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EmployeMatricule)));
            }
        }

        public int HeuresTravaillees
        {
            get { return heuresTravaillees; }
            set
            {
                heuresTravaillees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeuresTravaillees)));
            }
        }

        public double SalaireProjet
        {
            get { return salaireProjet; }
            set
            {
                salaireProjet = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SalaireProjet)));
            }
        }

        public bool Equals(Assignation? other)
        {
            if (other == null) return false;

            return this.id == other.id &&
                   this.projetNumero == other.projetNumero &&
                   this.employeMatricule == other.employeMatricule &&
                   this.heuresTravaillees == other.heuresTravaillees &&
                   this.salaireProjet == other.salaireProjet;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(id);
            hash.Add(projetNumero);
            hash.Add(employeMatricule);
            hash.Add(heuresTravaillees);
            hash.Add(salaireProjet);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"{id} - Projet:{projetNumero} - Employé:{employeMatricule} - Heures:{heuresTravaillees} - Salaire:{salaireProjet}";
        }
    }

}
