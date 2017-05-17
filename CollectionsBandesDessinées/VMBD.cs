using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CollectionsBandesDessinées
{
    public class VMBD
    {
        public ObservableCollection<CollectionBD> CollecBD { get; set; }
        public VMBD()
        {
            CollecBD = new ObservableCollection<CollectionBD>(BD_DAL.ChargerCollectionsBD());
        }
        private ICommand _cmdFirst;
        public ICommand CmdFirst
        {
            get
            {
                if (_cmdFirst == null)
                    _cmdFirst = new RelayCommand(Btn_Click);
                return _cmdFirst;
            }
        }

        private ICommand _cmdPrevious;
        public ICommand CmdPrevious
        {
            get
            {
                if (_cmdPrevious == null)
                    _cmdPrevious = new RelayCommand(Btn_Click);
                return _cmdPrevious;
            }
        }

        private ICommand _cmdNext;
        public ICommand CmdNext
        {
            get
            {
                if (_cmdNext == null)
                    _cmdNext = new RelayCommand(Btn_Click);
                return _cmdNext;
            }
        }

        private ICommand _cmdLast;
        public ICommand CmdLast
        {
            get
            {
                if (_cmdLast == null)
                    _cmdLast = new RelayCommand(Btn_Click);
                return _cmdLast;
            }
        }


        private void Btn_Click(object o)
        {

            ICollectionView view = CollectionViewSource.GetDefaultView(this.CollecBD);
            var s = (string)o;
            switch (s)
            {
                case "F":
                    view.MoveCurrentToFirst();
                    break;
                case "P":
                    if (!(view.CurrentPosition == 0))
                        view.MoveCurrentToPrevious();
                    break;
                case "N":
                    if (!(view.CurrentPosition==CollecBD.Count()-1))
                        view.MoveCurrentToNext();
                    break;
                case "L":
                    view.MoveCurrentToLast();
                    break;
            }

        }
    }
}
