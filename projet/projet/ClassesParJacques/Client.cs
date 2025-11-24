using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet.ClassesParJacques
{
    class Client
    {
        int id;
        string nom;
        string adresse;
        int num_tel;
        string email;

        public Client(int id, string nom, string adresse, int num_tel, string email)
        {
            this.id = id;
            this.nom = nom;
            this.adresse = adresse;
            this.num_tel = num_tel;
            this.email = email;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Email { get => email; set => email = value; }
        public int Num_tel { get => num_tel; set => num_tel = value; }

        public override string ToString()
        {
            return $"Id: {id}, Nom: {nom}, Num_tel: {Num_tel} , Adresse: {adresse}, Email: {email}";
        }
    }
}
