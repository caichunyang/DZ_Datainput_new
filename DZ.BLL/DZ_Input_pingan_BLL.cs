using DZ.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ.DLL;

namespace DZ.BLL
{
    public class DZ_Input_pingan_BLL
    {
        DZ_Input_pingan_Dal dal = new DZ_Input_pingan_Dal();
        public bool Add(dz_input_pingan model)
        {
            return dal.Add(model);
        }
    }
}
