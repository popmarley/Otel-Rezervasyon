using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel.CommonClass
{
    public class Oda
    {
        public byte YatakSayisi { get; set; }
        public int Numarasi { get; set; }

        public double Fiyat { get; set; }
        public OdaDurumu OdaDurumu { get; set; }

        //public Musteri Musteri { get; set; }
    }
}
