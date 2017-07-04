using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
    [Serializable]
  public  class DZ_USER
    {
      public string Userid { get; set; }
      public string Userpwd { get; set; }
      public string Username { get; set; }
      public int state { get; set; }
      public string Groupname { get; set; }
      public int Objectid { get; set; }
      public int Userlevel { get; set; }
      public int Integral { get; set; }
      public int EXP { get; set; }
      public string TEL { get; set; }
      public string ZFB { get; set; }
      public string ZQL { get; set; }
      public int Roleid { get; set; }
    }
}
