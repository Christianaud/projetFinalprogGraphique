using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.classes
{
    internal class Employe  : INotifyPropertyChanged, IComparable<Employe>, IEquatable<Employe>
    {
        string matricule, nom, prenom, email, adresse, photo, statut;
        DateTime dateNaissance, dateEmbauche;
        double tauxHoraire;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Employe(string matricule, string nom, string prenom, DateTime dateNaissance, string email, string adresse, DateTime dateEmbauche, double tauxHoraire, string photo, string statut)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.email = email;
            this.adresse = adresse;
            this.dateEmbauche = dateEmbauche;
            this.tauxHoraire = tauxHoraire;
            this.photo = photo;
            this.statut = statut;
        }

        public string Matricule
        {
            get { return matricule; }
            set
            {
                matricule = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Matricule"));

            }
        }

        public string GenererMatricule(string nom, DateTime dateNaissance)
        {
            string deuxLettreNoms = nom.Substring(0, 2);
            string deuxLettreNomsMajuscule = deuxLettreNoms.ToUpper();
            string annee = dateNaissance.Year.ToString();
            Random random = new Random();
            int nbrAleatoire = random.Next(10, 100);

            return $"{deuxLettreNomsMajuscule}-{annee}-{nbrAleatoire}";
        }

        public string Nom
        {
            get { return nom; }
            set
            {
                nom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nom"));
            }
        }

        public string Prenom
        {
            get { return prenom; }
            set
            {
                prenom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Prenom"));
            }
        }

        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set
            {
                dateNaissance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateNaissance"));

            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));

            }
        }

        public string Adresse
        {
            get { return adresse; }
            set
            {
                adresse = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Adresse"));

            }
        }

        public DateTime DateEmbauche
        {
            get { return dateEmbauche; }
            set
            {
                dateEmbauche = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateEmbauche"));

            }
        }

        public double TauxHoraire
        {
            get
            {
                return tauxHoraire;
            }
            set
            {
                tauxHoraire = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TauxHoraire"));
            }
        }

        public string Photo
        {
            get { return photo; }
            set
            {
                photo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Photo"));
            }
        }

        public string Statut
        {
            get { return statut; }
            set
            {
                statut = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Statut"));

            }
        }

        public int CompareTo(Employe? other)
        {
            return this.nom.CompareTo(other?.nom);
        }

        public bool Equals(Employe? other)
        {
            if (this.matricule.Equals(other?.matricule) &&
                this.nom.Equals(other?.nom) &&
                this.prenom.Equals(other?.prenom) &&
                this.dateNaissance.Equals(other?.dateNaissance) &&
                this.email.Equals(other?.email) &&
                this.adresse.Equals(other?.adresse) &&
                this.dateEmbauche.Equals(other?.dateEmbauche) &&
                this.tauxHoraire == other?.tauxHoraire &&
                this.photo.Equals(other?.photo) &&
                this.statut.Equals(other?.statut))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(matricule);
            hash.Add(nom);
            hash.Add(prenom);
            hash.Add(dateNaissance);
            hash.Add(email);
            hash.Add(adresse);
            hash.Add(dateEmbauche);
            hash.Add(tauxHoraire);
            hash.Add(photo);
            return hash.ToHashCode();
        }

        public override string? ToString()
        {
            return $"{matricule} - {nom} {prenom} {dateNaissance} {email} {adresse} {dateEmbauche} {tauxHoraire} {photo} {statut}";
        }
    }
}
