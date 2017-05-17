using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trombinoscope
{
    class VMTrombi : ViewModelBase
    {
        public List<EmployéPhoto> LstEmp { get; set; }

        public VMTrombi()
        {
            LstEmp = DAL.GetPictureFromDataReader();
        }
    }
}
