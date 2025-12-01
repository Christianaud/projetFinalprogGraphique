using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.ClassesParJacques
{
    internal class Projet
    {
        string numero;
        string titre;
        DateTime dateDebut;
        string description;
        int budget;
        int nbrEmployes;
        int totalSalaire;
        int idClient;
        string nomClient;
        string statut;

        public Projet(string numero, string titre, DateTime dateDebut, string description, int budget, int nbrEmployes, int totalSalaire, int idClient, string nomClient, string statut)
        {
            this.numero = numero;
            this.titre = titre;
            this.dateDebut = dateDebut;
            this.description = description;
            this.budget = budget;
            this.nbrEmployes = nbrEmployes;
            this.totalSalaire = totalSalaire;
            this.idClient = idClient;
            this.nomClient = nomClient;
            this.statut = statut;
        }

        public string Numero { get => numero; set => numero = value;}
        public string Titre { get => titre; set => titre = value;}
        public DateTime DateDebut { get => dateDebut; set => dateDebut = value;}
        public string Description { get => description; set => description = value;}
        public int Budget { get => budget; set => budget = value;}
        public int NbrEmployes { get => nbrEmployes; set => nbrEmployes = value; }
        public int TotalSalaire { get => totalSalaire; set => totalSalaire = value; }
        public int IdClient { get => idClient; set => idClient = value; }
        public string Statut { get => statut; set => statut = value; }

        public string NomClient { get => nomClient; set => nomClient = value; }

        public override string ToString()
        {
            return $"{numero};{titre};{dateDebut.ToString()};{description};{budget};{nbrEmployes};{statut};{idClient};{nomClient};{totalSalaire}";
        }
    }
}
