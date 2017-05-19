using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SaisieDeTaches
{
    public static class AccesDonnes
    {
        public static List<Tache> ChargerTaches()
        {
            List<Tache> LstTaches = new List<Tache>();

            XmlSerializer deserializer = new XmlSerializer(typeof(List<Tache>), new XmlRootAttribute("Taches"));
            using (var dsr = new StreamReader(@"..\..\Taches.xml"))
            {
                LstTaches = (List<Tache>)deserializer.Deserialize(dsr);
            }

            return LstTaches;
        }

        public static void EnregistrerTaches(List<Tache> Taches)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Tache>), new XmlRootAttribute("Taches"));
            using (var srt = new StreamWriter(@"..\..\Taches.xml"))
            {
                serializer.Serialize(srt, Taches);
            }
        }
    }
}
