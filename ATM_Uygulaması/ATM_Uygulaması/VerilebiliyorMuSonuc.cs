using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Uygulaması
{
    internal class VerilebiliyorMuSonuc
    {
        public bool verilebiliyorMu;
        public List<int[]> kullanilanParaListesi = new List<int[]>();
        public Unite verilenUnite;
        public VerilebiliyorMuSonuc(bool verilebiliyorMu, Unite unit ,int _5, int _10, int _20, int _50, int _100, int _200)
        {
            
            this.verilebiliyorMu = verilebiliyorMu;
            verilenUnite = unit;
            int[] _5adet = { 5, _5 };
            int[] _10adet = { 10, _10 };
            int[] _20adet = { 20, _20 };
            int[] _50adet = { 50, _50 };
            int[] _100adet = { 100, _100 };
            int[] _200adet = { 200, _200 };
            kullanilanParaListesi.Add(_200adet);
            kullanilanParaListesi.Add(_100adet);
            kullanilanParaListesi.Add(_50adet);
            kullanilanParaListesi.Add(_20adet);
            kullanilanParaListesi.Add(_10adet);
            kullanilanParaListesi.Add(_5adet);            
        }

        public void KullanilanParaGuncelle(List<int> kullanilanParalar)
        {
            int banknotDegeri;
            kullanilanParaListesi.Clear();
            BanknotlariTanimla();
            foreach (var banknot in kullanilanParaListesi)
            {
                banknotDegeri = banknot[0];
                foreach (var para in kullanilanParalar)
                {
                    if (para==banknotDegeri)
                    {
                        banknot[1]++;
                    }
                }
            }
        }

        public void BanknotlariTanimla()
        {
            int[] _5adet = { 5, 0 };
            int[] _10adet = { 10, 0 };
            int[] _20adet = { 20, 0 };
            int[] _50adet = { 50, 0 };
            int[] _100adet = { 100, 0 };
            int[] _200adet = { 200, 0};
            kullanilanParaListesi.Add(_200adet);
            kullanilanParaListesi.Add(_100adet);
            kullanilanParaListesi.Add(_50adet);
            kullanilanParaListesi.Add(_20adet);
            kullanilanParaListesi.Add(_10adet);
            kullanilanParaListesi.Add(_5adet);
        }
            
    }
}
