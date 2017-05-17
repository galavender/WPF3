using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Trombinoscope
{
    public class Employé
    {
        public int Id { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public string NomManager { get; set; }
        public string PrenomManager { get; set; }
        public List<territoire> LstTerritoires { get; set; }
    }

    public class territoire
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class EmployéPhoto
    {
        public ImageSource Image { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomManager { get; set; }
        public string PrenomManager { get; set; }
    }
}
