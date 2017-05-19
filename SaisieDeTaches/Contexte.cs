using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaisieDeTaches
{
    public enum ModesEdition
    {
        Consultation, Edition
    }

    public class Contexte
    {
        public ModesEdition ModeEdit { get; set; }
        public ObservableCollection<Tache> Taches { get; private set; }
        public Contexte()
        {
            Taches = new ObservableCollection<Tache>(AccesDonnes.ChargerTaches());
            ModeEdit = ModesEdition.Consultation;
        }


        private ICommand _cmdSupprimer;

        public ICommand CmdSupprimer
        {
            get
            {
                if (_cmdSupprimer == null)
                    _cmdSupprimer = new RelayCommand(SupprimerTache, ActiverConsultation);

                return _cmdSupprimer;
            }
        }

        private void SupprimerTache(object parameter)
        {

        }


        private ICommand _cmdAjouter;

        public ICommand CmdAjouter
        {
            get
            {
                if (_cmdAjouter == null)
                    _cmdAjouter = new RelayCommand(AjouterTache, ActiverConsultation);

                return _cmdAjouter;
            }
        }

        private void AjouterTache(object parameter)
        {
            var task = new Tache { Id = Taches.Count + 1, Creation = DateTime.Today, Term = DateTime.Today, Prio = 1 };
            Taches.Add(task);
            ModeEdit = ModesEdition.Edition;
        }

        private ICommand _cmdEnregistrer;

        public ICommand CmdEnregistrer
        {
            get
            {
                if (_cmdEnregistrer == null)
                    _cmdEnregistrer = new RelayCommand(EnregistrerTache, ActiverEdition);

                return _cmdEnregistrer;
            }
        }

        private void EnregistrerTache(object parameter)
        {
            AccesDonnes.EnregistrerTaches(Taches.ToList());
            ModeEdit = ModesEdition.Consultation;
        }


        private ICommand _cmdAnnuler;

        public ICommand CmdAnnuler
        {
            get
            {
                if (_cmdAnnuler == null)
                    _cmdAnnuler = new RelayCommand(AnnulerTache, ActiverEdition);

                return _cmdAnnuler;
            }
        }

        private void AnnulerTache(object parameter)
        {
            ModeEdit = ModesEdition.Consultation;
        }


        private bool ActiverEdition(object o)
        {
            return ModeEdit == ModesEdition.Edition;
        }
        private bool ActiverConsultation(object o)
        {
            return ModeEdit == ModesEdition.Consultation;
        }



    }
}
