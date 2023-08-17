using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Uygulaması
{
    internal class Unite
    {
        public int uniteNumarasi;
        public List<Kaset> kasetler = new List<Kaset>();
        public Kaset _200, _100, _50, _20, _10, _5;

        public Unite(int uniteNo, Kaset kaset1, Kaset kaset2, Kaset kaset3, Kaset kaset4, Kaset kaset5, Kaset kaset6)
        { 
            uniteNumarasi = uniteNo;
            kasetler.Add(kaset1);
            kasetler.Add(kaset2);
            kasetler.Add(kaset3);
            kasetler.Add(kaset4);
            kasetler.Add(kaset5);
            kasetler.Add(kaset6);
            _200= kaset1;
            _100= kaset2;
            _50= kaset3;
            _20= kaset4;
            _10= kaset5;
            _5= kaset6;
        }
    }
}
