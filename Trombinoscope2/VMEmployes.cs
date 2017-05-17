using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trombinoscope
{
    class VMEmployes : ViewModelBase
    {
        public ContexteEmploye Contexte { get; set; }

        public VMEmployes()
        {
            Contexte = new ContexteEmploye();
        }
    }
}
