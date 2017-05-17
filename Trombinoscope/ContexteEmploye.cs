using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Trombinoscope
{
    public class ContexteEmploye : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Employé> LstEmploye { get; }
        private Employé _nouvelEmploye;
        public Employé NouvelEmploye
        {
            get
            {
                return _nouvelEmploye;
            }
            set
            {
                _nouvelEmploye = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NouvelEmploye"));
            }
        }
        public ContexteEmploye()
        {
            LstEmploye = new ObservableCollection<Employé>(DAL.GetInfoEmployésFromDataReader());
            NouvelEmploye = new Employé();
        }



        private ICommand _cmdSupprimer;

        public ICommand CmdSupprimer
        {
            get
            {
                if (_cmdSupprimer == null)
                    _cmdSupprimer = new RelayCommand(SupprimerEmploye);

                return _cmdSupprimer;
            }
        }

        private void SupprimerEmploye(object parameter)
        {
            try
            {
                var e = (Employé)CollectionViewSource.GetDefaultView(LstEmploye).CurrentItem;
                DAL.SupprimerEmploye(e.Id);
                LstEmploye.Remove(e);
            }
            catch (Exception)
            {

                MessageBox.Show("Impossible de supprimer l'employé");
            }
        }
        private ICommand _cmdAjouter;

        public ICommand CmdAjouter
        {
            get
            {
                if (_cmdAjouter == null)
                    _cmdAjouter = new RelayCommand(AjouterEmploye);

                return _cmdAjouter;
            }
        }
        private void AjouterEmploye(object parameter)
        {
                try
                {
                    var d = new Insert(NouvelEmploye);
                    if (d.ShowDialog().Value)
                    {
                        DAL.InsertEmploye(NouvelEmploye);
                        LstEmploye.Add(NouvelEmploye);
                        NouvelEmploye = new Employé();
                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Impossible de créer l'employé");
                }
            
        }

    }
}
