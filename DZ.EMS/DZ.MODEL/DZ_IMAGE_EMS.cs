using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.MODEL
{
  public  class DZ_IMAGE_EMS
    {
      public int IMAGEID { get; set; }
      public string BOXNO { get; set; }
      public string IMAGEDATE { get; set; }
      public int IMAGESTATE{ get; set; }
      public string IMAGETYPE { get; set; }
      public string IMAGEFTP { get; set; }
      public string FILEDTYPE { get; set; }
      public string CHECKUSER { get; set; }
      public int PAGENUM { get; set; }
      public int OUTSTATE { get; set; }
      public string FILENAME { get; set; }


      public int BOXID { get; set; }
      public DateTime CREATETIME { get; set; }
      public int CHECKSTATE { get; set; }
      public string BATCHNAM { get; set; }

      public string BARCODE { get; set; }
      public string GUID { get; set; }
      public string USERNAME { get; set; }
    }
}
