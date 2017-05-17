using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaisieDeTaches
{
    public class Contexte
    {
        public ObservableCollection<Tache> Taches { get; private set; }
        public Contexte()
        {
            Taches =new  ObservableCollection<Tache>(AccesDonnes.ChargerTaches());
        }


        private ICommand _cmdSupprimer;

        public ICommand CmdSupprimer
        {
            get
            {
                if (_cmdSupprimer == null)
                    _cmdSupprimer = new RelayCommand(SupprimerTache);

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
                    _cmdAjouter = new RelayCommand(AjouterTache);

                return _cmdAjouter;
            }
        }

        private void AjouterTache(object parameter)
        {

        }


        private ICommand _cmdEnregistrer;

        public ICommand CmdEnregistrer
        {
            get
            {
                if (_cmdEnregistrer == null)
                    _cmdEnregistrer = new RelayCommand(EnregistrerTache);

                return _cmdEnregistrer;
            }
        }

        private void EnregistrerTache(object parameter)
        {

        }


        private ICommand _cmdAnnuler;

        public ICommand CmdAnnuler
        {
            get
            {
                if (_cmdAnnuler == null)
                    _cmdAnnuler = new RelayCommand(AnnulerTache);

                return _cmdAnnuler;
            }
        }

        private void AnnulerTache(object parameter)
        {

        }


    }
}
