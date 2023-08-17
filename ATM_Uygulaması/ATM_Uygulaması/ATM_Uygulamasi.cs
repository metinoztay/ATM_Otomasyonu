using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace ATM_Uygulaması
{
    public partial class ATM_Uygulamasi : Form
    {
        List<Unite> uniteler = new List<Unite>();
        int seciliBanknotIndex = -1,seciliBanknotDeger;
        int tutar;
        int bozulacakTutar;
        List<int> anapara = new List<int>();
        List<int> kullanilanPara = new List<int>();
        VerilebiliyorMuSonuc sonuc;
        public ATM_Uygulamasi()
        {
            InitializeComponent();
        }

        private void ATM_Uygulamasi_Load(object sender, EventArgs e)
        {
            KasetleriTanimla();    
        }

        private void KasetleriTanimla()
        {
            Kaset unite1_200 = new Kaset(200, tbx_unite1_200);
            Kaset unite1_100 = new Kaset(100, tbx_unite1_100);
            Kaset unite1_50 = new Kaset(50, tbx_unite1_50);
            Kaset unite1_20 = new Kaset(20, tbx_unite1_20);
            Kaset unite1_10 = new Kaset(10, tbx_unite1_10);
            Kaset unite1_5 = new Kaset(5, tbx_unite1_5);
            Unite unite1 = new Unite(1, unite1_200, unite1_100, unite1_50, unite1_20, unite1_10, unite1_5);
            uniteler.Add(unite1);
            Kaset unite2_200 = new Kaset(200, tbx_unite2_200);
            Kaset unite2_100 = new Kaset(100, tbx_unite2_100);
            Kaset unite2_50 = new Kaset(50, tbx_unite2_50);
            Kaset unite2_20 = new Kaset(20, tbx_unite2_20);
            Kaset unite2_10 = new Kaset(10, tbx_unite2_10);
            Kaset unite2_5 = new Kaset(5, tbx_unite2_5);
            Unite unite2 = new Unite(2, unite2_200, unite2_100, unite2_50, unite2_20, unite2_10, unite2_5);
            uniteler.Add(unite2);
        } //Kasetleri tanımla listeye ekle
        private void btn_unite1Kaydet_Click(object sender, EventArgs e)
        {
            int doluKasetSayisi;
            lbl_Unite1GirisUyari.Text = string.Empty;
            tbx_SonucEkrani.Text = string.Empty;
            tbx_AltLimit.Text = "10";
            tbx_UstLimit.Text = "1000";
            
                        
                doluKasetSayisi = 0;
                foreach (var kaset in uniteler[0].kasetler)
                {
                    kaset.AdetGuncelle();
                    if (kaset.adet == 0)
                        continue;
                    doluKasetSayisi++;
                }

                if (doluKasetSayisi > 4)
                {
                    lbl_Unite1GirisUyari.Text = "En fazla 4 adet kaset girişi yapabilirsiniz!";
                }
                else if (doluKasetSayisi == 0)
                {
                    lbl_Unite1GirisUyari.Text = "En az 1 adet kaset girişi yapınız!";
                }
                else // Kaset girişi başarıyla yapıldı.
                {
                    foreach (var kaset in uniteler[0].kasetler)
                    {
                        kaset.textBox.Enabled = false;
                    }
                    btn_unite1Sifirla.Enabled = true;
                    grp_ParaCekmeEkrani.Enabled = true;

                    VerilebilirTutarListele();
                }
        }

        private void btn_unite1Sifirla_Click(object sender, EventArgs e)
        {
            grp_ParaCekmeEkrani.Enabled = false;
            tbx_SonucEkrani.Text = string.Empty;
            btn_unite1Kaydet.Enabled = true;
            tbx_tutar.Text = string.Empty;
            //grp_VerilebilirTutarListesi.Enabled=false;
            list_VerilebilirTutar.Items.Clear();
            tbx_AltLimit.Text = string.Empty;
            tbx_UstLimit.Text = string.Empty;
            foreach (var unite in uniteler)
            {
                foreach (var kaset in unite.kasetler)
                {
                    kaset.textBox.Enabled = true;
                    kaset.textBox.Text = string.Empty;
                    kaset.AdetGuncelle();
                }
            }
            btn_unite1Sifirla.Enabled = false;
            grp_DokumanOkumaEkrani.Enabled = true;
        }

        //private void btn_ParaCek_Click(object sender, EventArgs e)
        //{
        //    anapara.Clear();
        //    kullanilanPara.Clear();
        //    seciliBanknotIndex = -1;
        //    if (int.TryParse(tbx_tutar.Text.Trim(),out tutar))
        //    {
        //        anapara.Add(tutar);
        //        while (ParaBozuluyorMu())
        //        {
        //            //
        //        }
        //        if (tutar == ToplamHesapla() && (bozulacakTutar == 0 || bozulacakTutar == anapara[anapara.Count-1]) && anapara.Count != 1)
        //        {
        //            EkranaYaz();
        //        }
        //        else
        //        {
        //            ParalariYerineKoy();
        //            foreach (var unite in uniteler)
        //            {
                        
        //            }

        //            if (ParaVerilebiliyorMu(tutar))
        //            {
        //                ParayiAktar();
        //                EkranaYaz();
        //            }
        //            else
        //            {
        //                tbx_SonucEkrani.Text = "İstediginiz tutar verilemedi. \r\nTavsiye edilen tutar: " + (tutar - bozulacakTutar).ToString() + " TL";
        //            }                    
        //            bozulacakTutar = 0;
        //        }
        //    }

        //    VerilebilirTutarListele();
        //}

        //private bool ParaBozuluyorMu()
        //{
        //    bozulacakTutar = anapara[anapara.Count - 1];
        //    while(BanknotSecildiMi(bozulacakTutar))
        //    {
        //        if (kasetler[seciliBanknotIndex].adet>0 && bozulacakTutar >= seciliBanknotDeger)
        //        {
        //            bozulacakTutar -= seciliBanknotDeger;
        //            kullanilanPara.Add(seciliBanknotDeger);
        //            kasetler[seciliBanknotIndex].adet--;
        //        }
                
        //        if (bozulacakTutar == 0)
        //        {
        //            ParayiAktar();
        //            return true;
        //        }
        //    }           

        //    return false;
        //}

        //private bool BanknotSecildiMi(int para)
        //{
        //    if (seciliBanknotIndex == -1) // banknot seçilmemişse
        //    {
        //        if (_200.adet >= _100.adet)
        //        {
        //            seciliBanknotIndex = 0;
        //            seciliBanknotDeger = 200;
        //        }
        //        else
        //        {
        //            seciliBanknotIndex = 1;
        //            seciliBanknotDeger = 100;
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        if (kasetler[seciliBanknotIndex].adet>0 && bozulacakTutar >= seciliBanknotDeger)
        //        {
        //            if (seciliBanknotDeger == anapara[anapara.Count-1] && seciliBanknotIndex < kasetler.Count - 1)
        //            {
        //                seciliBanknotIndex++;
        //                seciliBanknotDeger = kasetler[seciliBanknotIndex].banknotDegeri;
        //                return true;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //        else if (seciliBanknotIndex < kasetler.Count-1) //son kaset değilse
        //        {
        //            seciliBanknotIndex++;
        //            seciliBanknotDeger = kasetler[seciliBanknotIndex].banknotDegeri;
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }            
        //}

        private void btn_VerilebilirTutarListele_Click(object sender, EventArgs e)
        {
            VerilebilirTutarListele();
        }

        //private void ParayiAktar()
        //{
        //    anapara.RemoveAt(anapara.Count-1);
        //    foreach (var para in kullanilanPara)
        //    {
        //        anapara.Add(para);
        //    }
        //    kullanilanPara.Clear();
        //}

        //private int ToplamHesapla()
        //{
        //    int toplam = 0;
        //    foreach (var para in anapara)
        //    {
        //        toplam += para;
        //    }
        //    return toplam;
        //}

        //private void EkranaYaz()
        //{
        //    int adet;
        //    tbx_SonucEkrani.Text = "İstediginiz tutar \r\n\r\n";
        //    foreach (var kaset in kasetler)
        //    {
        //        adet = 0;
        //        foreach (var para in anapara)
        //        {
        //            if (kaset.banknotDegeri == para)
        //            {
        //                adet++;
        //            }
        //        }

        //        if (adet > 0)
        //        {
        //            tbx_SonucEkrani.Text += kaset.banknotDegeri.ToString() + "x" + adet.ToString() + "\r\n";
        //        }            
        //    }
        //    tbx_SonucEkrani.Text += "\r\n olarak verildi.";
        //}

        //private void ParalariYerineKoy()
        //{
            
        //    while (bozulacakTutar!=0 && kullanilanPara.Count>=1) 
        //    {
        //        foreach (var kaset in kasetler)
        //        {
        //            if (kaset.banknotDegeri == kullanilanPara[kullanilanPara.Count - 1])
        //            {
        //                kaset.adet++;
        //                break;
        //            }
        //        }
        //        kullanilanPara.RemoveAt(kullanilanPara.Count-1);
        //        seciliBanknotIndex = -1;
        //        BanknotSecildiMi(bozulacakTutar);
        //    }

        //    foreach (var para in kullanilanPara)
        //    {
        //        foreach (var kaset in kasetler)
        //        {
        //            if (kaset.banknotDegeri == para)
        //            {
        //                kaset.adet++;
        //            }
        //        }
        //    }            
        //}

        //private VerilebiliyorMuSonuc ParaVerilebiliyorMuHesapla1(int kontrolEdilecekTutar, Unite kontrolEdilecekUnite ,bool paraVerilecekMi=false) // :)
        //{            
        //    int paraToplam;
        //    for (int x = 0; x <= kontrolEdilecekUnite._5.adet; x++)
        //    {
        //        for (int y = 0; y <= kontrolEdilecekUnite._10.adet; y++)
        //        {
        //            for (int z = 0; z <= kontrolEdilecekUnite._20.adet; z++)
        //            {
        //                for (int t = 0; t <= kontrolEdilecekUnite._50.adet; t++)
        //                {
        //                    for (int u = 0; u <= kontrolEdilecekUnite._100.adet; u++)
        //                    {
        //                        for (int v = 0; v <= kontrolEdilecekUnite._200.adet; v++)
        //                        {
        //                            paraToplam = (5 * x) + (10 * y) + (20 * z) + (50 * t) + (100 * u) + (200 * v);
        //                            if (paraToplam > kontrolEdilecekTutar)
        //                            {
        //                                break;
        //                            }

        //                            if (kontrolEdilecekTutar == paraToplam) //istenen tutar verilebiliyor
        //                            {
        //                                if (kontrolEdilecekUnite._100.adet>kontrolEdilecekUnite._200.adet && (kontrolEdilecekUnite._100.adet >= u + (v * 2))) //100lük sayısı fazla ise kullanıyor
        //                                {
        //                                    u += v * 2;
        //                                    v = 0;
        //                                } 

        //                                if (paraVerilecekMi) //Eğer kontrol için değilse paraları taşı
        //                                {
        //                                    kullanilanPara.Clear();
        //                                    int index = 0;
        //                                    int deger = 0;
        //                                    int[] dizi = { v, u, t, z, y, x }; //200 - 100 - 50 - 20 - 10 - 5
        //                                    foreach (var item in dizi)
        //                                    {
        //                                        deger = kontrolEdilecekUnite.kasetler[index].banknotDegeri;
        //                                        for (int i = 0; i < item; i++)
        //                                        {
        //                                            kullanilanPara.Add(deger);
        //                                            kontrolEdilecekUnite.kasetler[index].adet--;
        //                                        }
        //                                        index++;
        //                                    }
        //                                }

                                        
        //                                VerilebiliyorMuSonuc sonuc1 = new VerilebiliyorMuSonuc(true, kontrolEdilecekUnite,x,y,z,t,u,v);
        //                                SonKupurBozulduMu(sonuc1);                                        

        //                                return sonuc1;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    VerilebiliyorMuSonuc sonuc2 = new VerilebiliyorMuSonuc(false, kontrolEdilecekUnite, 0, 0, 0, 0, 0, 0);
        //    return sonuc2; //istenen tutar verilemiyor
        //}

        private VerilebiliyorMuSonuc ParaVerilebiliyorMuHesapla(int kontrolEdilecekTutar, Unite kontrolEdilecekUnite, bool paraVerilecekMi = false) // :)
        {
            int paraToplam;            
            int xmax = Math.Min((kontrolEdilecekTutar / 200), kontrolEdilecekUnite._200.adet);
            for (int x = xmax; x >= 0; x--)
            {
                
                int xtemp = kontrolEdilecekTutar;                
                if (xtemp >= x * 200)
                {
                    xtemp -= x * 200;
                }
                int ymax = Math.Min((xtemp / 100), kontrolEdilecekUnite._100.adet);
                for (int y = ymax; y >= 0 ; y--)
                {
                    int ytemp = xtemp;
                    if (ytemp >= y * 100)
                    {
                        ytemp -= y * 100;
                    }
                    int zmax = Math.Min((ytemp / 50), kontrolEdilecekUnite._50.adet);
                    for (int z = zmax; z >= 0; z--)
                    {
                        int ztemp = ytemp;
                        if (ztemp >= z * 50)
                        {
                            ztemp -= z * 50;
                        }
                        int tmax = Math.Min((ztemp / 20), kontrolEdilecekUnite._20.adet);
                        for (int t = tmax; t >= 0; t--)
                        {
                            int ttemp = ztemp;
                            if (ttemp >= t * 20)
                            {
                                ttemp -= t * 20;
                            }
                            int umax = Math.Min((ttemp / 10), kontrolEdilecekUnite._10.adet);
                            for (int u = umax; u >= 0; u--)
                            {
                                int utemp = ttemp;
                                if (utemp >= u * 10)
                                {
                                    utemp -= u * 10;
                                }
                                int vmax = Math.Min((utemp / 5), kontrolEdilecekUnite._5.adet);
                                for (int v = vmax; v >= 0; v--)
                                {
                                    int vtemp = utemp;
                                    if (vtemp >= v * 5)
                                    {
                                        vtemp -= v * 5;
                                    }

                                    if (vtemp == 0) // para verilebiliyor
                                    {
                                        
                                        if (kontrolEdilecekUnite._100.adet > kontrolEdilecekUnite._200.adet && (kontrolEdilecekUnite._100.adet >= y + (x * 2))) //100lük sayısı fazla ise kullanıyor
                                        {
                                            y += x * 2;
                                            x = 0;
                                        }
                                        
                                        VerilebiliyorMuSonuc verilebiliyor = new VerilebiliyorMuSonuc(true, kontrolEdilecekUnite, v, u, t, z, y, x);
                                        while (SonKupurBozulduMu(verilebiliyor) == true)
                                        {
                                            SonKupurBozulduMu(verilebiliyor);
                                        }
                                        if (paraVerilecekMi) //Eğer kontrol için değilse para boz ve taşı
                                        {
                                            
                                            int index = 0;
                                            int deger = 0;
                                            
                                            foreach (var para in kullanilanPara)
                                            {
                                                foreach (var kaset in kontrolEdilecekUnite.kasetler)
                                                {
                                                    if (kaset.banknotDegeri==para)
                                                    {
                                                        kaset.adet--;
                                                    }
                                                }
                                            }
                                        }
                                        return verilebiliyor;
                                    }                                    
                                }
                            }
                        }
                    }
                }
            }

            VerilebiliyorMuSonuc verilemiyor = new VerilebiliyorMuSonuc(false, kontrolEdilecekUnite, 0, 0, 0, 0, 0, 0);
            return verilemiyor; //istenen tutar verilemiyor
        }
        private bool SonKupurBozulduMu(VerilebiliyorMuSonuc verilenPara)
        {
            kullanilanPara.Clear();
            int index = 0;
            int deger = 0;
            int[] dizi = { 
                verilenPara.kullanilanParaListesi[0][1],
                verilenPara.kullanilanParaListesi[1][1],
                verilenPara.kullanilanParaListesi[2][1],
                verilenPara.kullanilanParaListesi[3][1],
                verilenPara.kullanilanParaListesi[4][1],
                verilenPara.kullanilanParaListesi[5][1] 
            }; //200 - 100 - 50 - 20 - 10 - 5
            foreach (var item in dizi)
            {
                deger = verilenPara.verilenUnite.kasetler[index].banknotDegeri;
                for (int i = 0; i < item; i++)
                {
                    kullanilanPara.Add(deger);;
                }
                index++;
            }

            if (kullanilanPara.Count == 0)
            {
                return false;
            }

            int bozulacakTutar = kullanilanPara[kullanilanPara.Count-1];
            List<int[]> kucukParaListesi = new List<int[]>(); 

            foreach (var kaset in verilenPara.verilenUnite.kasetler)
            {
                if (kaset.banknotDegeri < bozulacakTutar && kaset.adet > 0)
                {
                    int[] banknot = new int[2]; // banknot değeri - adet
                    banknot[0] = kaset.banknotDegeri;
                    banknot[1] = kaset.adet;
                    kucukParaListesi.Add(banknot);
                }
            }

            if (kucukParaListesi.Count == 0)
            {
                return false;
            }
            

            int hesaplananTutar;
            for (int x = 0; x <= verilenPara.verilenUnite._5.adet; x++)
            {
                for (int y = 0; y <= verilenPara.verilenUnite._10.adet; y++)
                {
                    if (y == 1 && bozulacakTutar == 10)
                    {
                        break;
                    }
                    for (int z = 0; z <= verilenPara.verilenUnite._20.adet; z++)
                    {

                        if (z == 1 && bozulacakTutar == 20)
                        {
                            break;
                        }

                        for (int t = 0; t <= verilenPara.verilenUnite._50.adet; t++)
                        {
                            if (t == 1 && bozulacakTutar == 50)
                            {
                                break;
                            }

                            for (int u = 0; u <= verilenPara.verilenUnite._100.adet; u++)
                            {
                                if (u == 1 && bozulacakTutar == 100)
                                {
                                    break;
                                }

                                hesaplananTutar = (5 * x) + (10 * y) + (20 * z) + (50 * t) + (100 * u);
                                if (bozulacakTutar == hesaplananTutar)
                                {
                                    kullanilanPara.RemoveAt(kullanilanPara.Count- 1);

                                    index = 1; //100 lük kasetin indexinden başlayacak
                                    deger = 0;
                                    int[] dizi1 = { u, t, z, y, x }; //100 - 50 - 20 - 10 - 5
                                    foreach (var item in dizi1)
                                    {
                                        deger = verilenPara.verilenUnite.kasetler[index].banknotDegeri;
                                        for (int i = 0; i < item; i++)
                                        {
                                            kullanilanPara.Add(deger);;
                                        }
                                        index++;
                                    }

                                    verilenPara.KullanilanParaGuncelle(kullanilanPara);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }


            return false;
        }


        private void btn_unite2Kaydet_Click(object sender, EventArgs e)
        {
            int doluKasetSayisi;
            lbl_Unite2GirisUyari.Text = string.Empty;
            tbx_SonucEkrani.Text = string.Empty;
            tbx_AltLimit.Text = "10";
            tbx_UstLimit.Text = "1000";

            doluKasetSayisi = 0;
            foreach (var kaset in uniteler[1].kasetler)
            {
                kaset.AdetGuncelle();
                if (kaset.adet == 0)
                    continue;
                doluKasetSayisi++;
            }

            if (doluKasetSayisi > 4)
            {
                lbl_Unite2GirisUyari.Text = "En fazla 4 adet kaset girişi yapabilirsiniz!";
            }
            else if (doluKasetSayisi == 0)
            {
                lbl_Unite2GirisUyari.Text = "En az 1 adet kaset girişi yapınız!";
            }
            else // Kaset girişi başarıyla yapıldı.
            {
                foreach (var kaset in uniteler[1].kasetler)
                {
                    kaset.textBox.Enabled = false;
                }
                btn_unite2Sifirla.Enabled = true;
                grp_ParaCekmeEkrani.Enabled = true;
                VerilebilirTutarListele();
            }
        }

        private void btn_unite2Sifirla_Click(object sender, EventArgs e)
        {
            grp_ParaCekmeEkrani.Enabled = false;
            tbx_SonucEkrani.Text = string.Empty;
            btn_unite2Kaydet.Enabled = true;
            tbx_tutar.Text = string.Empty;
            //grp_VerilebilirTutarListesi.Enabled=false;
            list_VerilebilirTutar.Items.Clear();
            tbx_AltLimit.Text = string.Empty;
            tbx_UstLimit.Text = string.Empty;
            foreach (var unite in uniteler)
            {
                foreach (var kaset in unite.kasetler)
                {
                    kaset.textBox.Enabled = true;
                    kaset.textBox.Text = string.Empty;
                    kaset.AdetGuncelle();
                }
            }
            btn_unite2Sifirla.Enabled = false;
            grp_DokumanOkumaEkrani.Enabled = true;
        }

        private void VerilebilirTutarListele()
        {
            int altLimit = int.Parse(tbx_AltLimit.Text.Trim());
            int ustLimit = int.Parse(tbx_UstLimit.Text.Trim());
            list_VerilebilirTutar.Items.Clear();
            lbl_ListeUyari.Text = string.Empty;
            
            if (altLimit <0 ||ustLimit<altLimit)
            {
                lbl_ListeUyari.Text = "Lutfen Gecerli Bir Tutar Giriniz.";
            }
            else 
            {
                int[] verilebilirTutar = {0,-1}; // tutar - kasa no
                
                for (int kontrolEdilenTutar = altLimit; kontrolEdilenTutar <= ustLimit; kontrolEdilenTutar+=10)
                {
                    string durum = "";
                    VerilebiliyorMuSonuc[] unitelerinSonuclari = new VerilebiliyorMuSonuc[2]; //ünite 1 - 2
                    foreach (var unite in uniteler)
                    {
                        VerilebiliyorMuSonuc sonuc = ParaVerilebiliyorMuHesapla(kontrolEdilenTutar, unite);
                        unitelerinSonuclari[unite.uniteNumarasi - 1] = sonuc;
                        if (sonuc.verilebiliyorMu)
                        {
                            verilebilirTutar[0] = kontrolEdilenTutar;
                            verilebilirTutar[1] = unite.uniteNumarasi;
                        }
                    }

                    if (unitelerinSonuclari[0].verilebiliyorMu == false && unitelerinSonuclari[1].verilebiliyorMu == false) // iki ünite de veremiyor
                    {
                        if (verilebilirTutar[1] == -1)
                        {
                            durum = "Tutar verilemiyor";
                            string[] satir = { kontrolEdilenTutar.ToString() + "TL", durum, "" };
                            ListViewItem listViewItem = new ListViewItem(satir);
                            list_VerilebilirTutar.Items.Add(listViewItem);
                        }
                        else
                        {
                            durum = "Verilebilecek Tutar: " + verilebilirTutar[0].ToString() + "TL";
                            string[] satir = { kontrolEdilenTutar.ToString() + "TL", durum, "Ünite " + verilebilirTutar[1].ToString() };
                            ListViewItem listViewItem = new ListViewItem(satir);
                            list_VerilebilirTutar.Items.Add(listViewItem);
                        }
                    }
                    else if(unitelerinSonuclari[0].verilebiliyorMu == true && unitelerinSonuclari[1].verilebiliyorMu == true)
                    {
                        int verilenUniteNo = UniteSec(unitelerinSonuclari);

                        foreach (var unite in unitelerinSonuclari)
                        {
                            if (unite.verilenUnite.uniteNumarasi == verilenUniteNo)
                            {
                                durum = "";
                                foreach (var para in unite.kullanilanParaListesi)
                                {
                                    if (para[1] > 0)
                                    {
                                        durum += para[0].ToString() + "x" + para[1].ToString() + " ";
                                    }
                                }
                                string[] satir = { kontrolEdilenTutar.ToString() + "TL", durum, "Ünite " + unite.verilenUnite.uniteNumarasi.ToString() };
                                ListViewItem listViewItem = new ListViewItem(satir);
                                list_VerilebilirTutar.Items.Add(listViewItem);
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var unite in unitelerinSonuclari)
                        {
                            if (unite.verilebiliyorMu == true)
                            {
                                durum = "";
                                foreach (var para in unite.kullanilanParaListesi)
                                {
                                    if (para[1]>0)
                                    {
                                        durum += para[0].ToString() + "x" + para[1].ToString() + " ";
                                    }
                                }
                                string[] satir = { kontrolEdilenTutar.ToString() + "TL", durum, "Ünite " + unite.verilenUnite.uniteNumarasi.ToString() };
                                ListViewItem listViewItem = new ListViewItem(satir);
                                list_VerilebilirTutar.Items.Add(listViewItem);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btn_ParaCek_Click(object sender, EventArgs e)
        {
            grp_DokumanOkumaEkrani.Enabled= false;
            ParaCek();
            VerilebilirTutarListele();
        }

        private int UniteSec(VerilebiliyorMuSonuc[] sonuclar)
        {
            List<int> kontrolEdilecekBanknotlar = new List<int>();
            int[] gerekenBanknotSayilari = { 0, 0 }; //1. - 2. 
            foreach (var sonuc in sonuclar)
            {
                foreach (var para in sonuc.kullanilanParaListesi)
                {
                    if (para[1]>0)
                    {
                        kontrolEdilecekBanknotlar.Add(para[0]);
                        gerekenBanknotSayilari[sonuc.verilenUnite.uniteNumarasi-1] += para[1];
                    }
                }
            }

            List<int> toplamBanknotSayilari = new List<int>(); // unite 1 - 2 
            foreach (var unite in uniteler)
            {
                int toplam = 0;
                foreach (var kaset in unite.kasetler)
                {
                    foreach (var banknot in kontrolEdilecekBanknotlar)
                    {
                        if (banknot == kaset.banknotDegeri)
                        {
                            toplam += kaset.adet;
                        }
                    }
                }
                toplamBanknotSayilari.Add(toplam);
            }

            if (toplamBanknotSayilari[0] > toplamBanknotSayilari[1])
            {
                return sonuclar[0].verilenUnite.uniteNumarasi;
            }
            else if (toplamBanknotSayilari[0] < toplamBanknotSayilari[1])
            {
                return sonuclar[1].verilenUnite.uniteNumarasi;
            }
            else
            {
                return gerekenBanknotSayilari[0] < gerekenBanknotSayilari[1] ? 1 : 2; // hangi ünitede daha az para veriliyor
            }            
        }

        private void ParaCek()
        {
            string durum = string.Empty;
            int tutar;
            int.TryParse(tbx_tutar.Text, out tutar);
            if (tutar <= 0)
            {
                lbl_ListeUyari.Text = "Lutfen Gecerli Bir Tutar Giriniz.";
            }
            else
            {
                VerilebiliyorMuSonuc[] unitedenVerilebiliyorMu = new VerilebiliyorMuSonuc[2]; //ünite 1 - 2
                foreach (var unite in uniteler)
                {
                        VerilebiliyorMuSonuc sonuc = ParaVerilebiliyorMuHesapla(tutar, unite);
                        unitedenVerilebiliyorMu[unite.uniteNumarasi - 1] = sonuc;                        
                }

                if (unitedenVerilebiliyorMu[0].verilebiliyorMu == false && unitedenVerilebiliyorMu[1].verilebiliyorMu == false) // iki ünite de veremiyor
                {  
                    int[] verilebilirTutar = { 0, -1 }; // tutar - kasa no
                    for (int kontrolEdilenTutar = tutar; kontrolEdilenTutar > 0; kontrolEdilenTutar -= 5)
                    {
                        VerilebiliyorMuSonuc[] tempUnitedenVerilebiliyorMu = new VerilebiliyorMuSonuc[2];
                        foreach (var unite in uniteler)
                        {
                            VerilebiliyorMuSonuc tempSonuc = ParaVerilebiliyorMuHesapla(kontrolEdilenTutar, unite);
                            tempUnitedenVerilebiliyorMu[unite.uniteNumarasi - 1] = tempSonuc;
                            if (tempSonuc.verilebiliyorMu)
                            {
                                verilebilirTutar[0] = kontrolEdilenTutar;
                                verilebilirTutar[1] = unite.uniteNumarasi;
                            }
                        }

                        if (tempUnitedenVerilebiliyorMu[0].verilebiliyorMu == false && tempUnitedenVerilebiliyorMu[1].verilebiliyorMu == false) // iki ünite de veremiyor
                        {
                            continue;
                        }
                        else if (tempUnitedenVerilebiliyorMu[0].verilebiliyorMu)
                        {
                            durum = "İstenilen tutar verilemiyor. \r\nVerilebilecek tutar: ";
                            durum += verilebilirTutar[0] + "TL -- Ünite " + verilebilirTutar[1].ToString();
                            tbx_SonucEkrani.Text = durum;
                            break;
                        }
                        else if (tempUnitedenVerilebiliyorMu[1].verilebiliyorMu)
                        {
                            durum = "İstenilen tutar verilemiyor. \r\nVerilebilecek tutar: ";
                            durum += verilebilirTutar[0] + "TL -- Ünite " + verilebilirTutar[1].ToString();
                            tbx_SonucEkrani.Text = durum;
                            break;
                        }
                    }

                    if (verilebilirTutar[0] == 0)
                    {
                        durum = "İstenilen tutar verilemiyor. \r\nDaha Yüksek bir Tutar giriniz.";
                        tbx_SonucEkrani.Text = durum;
                    }
                }
                else if (unitedenVerilebiliyorMu[0].verilebiliyorMu == true && unitedenVerilebiliyorMu[1].verilebiliyorMu == true)
                {
                    int verilenUniteNo = UniteSec(unitedenVerilebiliyorMu);

                    foreach (var unite in unitedenVerilebiliyorMu)
                    {
                        if (unite.verilenUnite.uniteNumarasi == verilenUniteNo)
                        {
                            durum += "İstenilen tutar \r\n";
                            foreach (var para in unite.kullanilanParaListesi)
                            {
                                if (para[1] > 0)
                                {
                                    durum += para[0].ToString() + "x" + para[1].ToString() + "\r\n";                                    
                                }
                            }
                            durum += "şeklinde Ünite" + unite.verilenUnite.uniteNumarasi.ToString() + " den verilmiştir.";
                            tbx_SonucEkrani.Text = durum;
                            ParaVerilebiliyorMuHesapla(tutar, unite.verilenUnite, true);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var unite in unitedenVerilebiliyorMu)
                    {
                        if (unite.verilebiliyorMu == true)
                        {
                            VerilebiliyorMuSonuc sonuc = ParaVerilebiliyorMuHesapla(tutar, unite.verilenUnite, true);
                            durum += "İstenilen tutar \r\n";
                            foreach (var para in sonuc.kullanilanParaListesi)
                            {
                                if (para[1] > 0)
                                {
                                    durum += para[0].ToString() + "x" + para[1].ToString() + "\r\n";
                                }
                            }
                            durum += "şeklinde Ünite" + unite.verilenUnite.uniteNumarasi.ToString() + " den verilmiştir.";
                            tbx_SonucEkrani.Text = durum;                            
                            break;
                        }
                    }
                }
            }
        }

        private void btn_ExcelOku_Click(object sender, EventArgs e)
        {
            ExcelOku();
        }
        private void ExcelOku()
        {
            ParaYatirmaDokumanOku();
            ParaCekmeDokumanOku();
            
            foreach (var unite in uniteler) //uniteleri başlangıç konumuna geri getir
            {
                foreach (var kaset in unite.kasetler)
                {
                    kaset.YedekGeriYukle();
                }
            }
        }

        private void ParaYatirmaDokumanOku()
        {
            int islemSayisi = 0;
            int toplamTutar = 0;
            int[,] kullanilanBanknotSayilari = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }; // 200 -100 -50 -20 -10 -5
            string mevcutKonum = Directory.GetCurrentDirectory();
            string dosyaYolu = Path.Combine(mevcutKonum, "ATM_ParaYatirma_Islemler.xlsx");
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook calismaKitabi;
            Worksheet calismaSayfasi;

            calismaKitabi = excel.Workbooks.Open(dosyaYolu, Editable: true);
            calismaSayfasi = calismaKitabi.Worksheets[1];

            Range hucreler = calismaSayfasi.Range["B2:B400"];

            int kasetIndex;
            foreach (var hucre in hucreler.Value)
            {
                if (hucre != null)
                {
                    Unite paraEklenecekUnite = HangiUnitedeDahaAzParaVar();
                    kasetIndex = 0;
                    foreach (var kaset in paraEklenecekUnite.kasetler)
                    {
                        if (kaset.banknotDegeri == (int)hucre)
                        {
                            kullanilanBanknotSayilari[paraEklenecekUnite.uniteNumarasi - 1, kasetIndex]++;
                            kaset.adet++;
                        }
                        toplamTutar += hucre;
                        kasetIndex++;
                    }
                }
            }

            Range islemHucreler = calismaSayfasi.Range["A2:A400"];
            foreach (var hucre in islemHucreler.Value)
            {
                if (hucre != null)
                {
                    islemSayisi = (int)hucre;
                }
            }
            tbx_ParaYatirmaIslemSayisi.Text = islemSayisi.ToString();
            tbx_ParaYatirmaToplamTutar.Text = toplamTutar.ToString();


            calismaKitabi.Close();


            tbx_unite1_yatirilan_200Adedi.Text = kullanilanBanknotSayilari[0, 0].ToString();
            tbx_unite1_yatirilan_100Adedi.Text = kullanilanBanknotSayilari[0, 1].ToString();
            tbx_unite1_yatirilan_50Adedi.Text = kullanilanBanknotSayilari[0, 2].ToString();
            tbx_unite1_yatirilan_20Adedi.Text = kullanilanBanknotSayilari[0, 3].ToString();
            tbx_unite1_yatirilan_10Adedi.Text = kullanilanBanknotSayilari[0, 4].ToString();
            tbx_unite1_yatirilan_5Adedi.Text = kullanilanBanknotSayilari[0, 5].ToString();
            tbx_unite2_yatirilan_200Adedi.Text = kullanilanBanknotSayilari[1, 0].ToString();
            tbx_unite2_yatirilan_100Adedi.Text = kullanilanBanknotSayilari[1, 1].ToString();
            tbx_unite2_yatirilan_50Adedi.Text = kullanilanBanknotSayilari[1, 2].ToString();
            tbx_unite2_yatirilan_20Adedi.Text = kullanilanBanknotSayilari[1, 3].ToString();
            tbx_unite2_yatirilan_10Adedi.Text = kullanilanBanknotSayilari[1, 4].ToString();
            tbx_unite2_yatirilan_5Adedi.Text = kullanilanBanknotSayilari[1, 5].ToString();
        }
        private void ParaCekmeDokumanOku()
        {
            int islemSayisi = 0;
            int toplamTutar = 0;
            int[,] kullanilanBanknotSayilari = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } }; // 200 -100 -50 -20 -10 -5
            string mevcutKonum = Directory.GetCurrentDirectory();
            string dosyaYolu = Path.Combine(mevcutKonum, "ATM_ParaCekme_Islemler.xlsx");
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook calismaKitabi;
            Worksheet calismaSayfasi;

            calismaKitabi = excel.Workbooks.Open(dosyaYolu, Editable: true);
            calismaSayfasi = calismaKitabi.Worksheets[1];

            Range hucreler = calismaSayfasi.Range["B2:B400"];


            foreach (var hucre in hucreler.Value)
            {
                if (hucre != null)
                {
                    VerilebiliyorMuSonuc sonuc1 = ParaVerilebiliyorMuHesapla((int)hucre, uniteler[0], true);
                    VerilebiliyorMuSonuc sonuc2 = ParaVerilebiliyorMuHesapla((int)hucre, uniteler[1], true);
                    VerilebiliyorMuSonuc[] sonuclar = { sonuc1, sonuc2 };
                    int secilenUniteNo = UniteSec(sonuclar);
                    VerilebiliyorMuSonuc sonuc = (secilenUniteNo == 1) ? sonuc1 : sonuc2;
                    if (sonuc.verilebiliyorMu)
                    {
                        for (int i = 0; i < sonuc.kullanilanParaListesi.Count; i++)
                        {
                            kullanilanBanknotSayilari[secilenUniteNo - 1, i] += sonuc.kullanilanParaListesi[i][1];
                        }
                        islemSayisi++;
                        toplamTutar += hucre;
                    }
                }
            }
            
            calismaKitabi.Close();

            tbx_unite1_200Adedi.Text = kullanilanBanknotSayilari[0, 0].ToString();
            tbx_unite1_100Adedi.Text = kullanilanBanknotSayilari[0, 1].ToString();
            tbx_unite1_50Adedi.Text = kullanilanBanknotSayilari[0, 2].ToString();
            tbx_unite1_20Adedi.Text = kullanilanBanknotSayilari[0, 3].ToString();
            tbx_unite1_10Adedi.Text = kullanilanBanknotSayilari[0, 4].ToString();
            tbx_unite1_5Adedi.Text = kullanilanBanknotSayilari[0, 5].ToString();
            tbx_unite2_200Adedi.Text = kullanilanBanknotSayilari[1, 0].ToString();
            tbx_unite2_100Adedi.Text = kullanilanBanknotSayilari[1, 1].ToString();
            tbx_unite2_50Adedi.Text = kullanilanBanknotSayilari[1, 2].ToString();
            tbx_unite2_20Adedi.Text = kullanilanBanknotSayilari[1, 3].ToString();
            tbx_unite2_10Adedi.Text = kullanilanBanknotSayilari[1, 4].ToString();
            tbx_unite2_5Adedi.Text = kullanilanBanknotSayilari[1, 5].ToString();
            tbx_IslemSayisi.Text = islemSayisi.ToString();
            tbx_ToplamTutar.Text = toplamTutar.ToString();

            
        }

        private void btn_ParaYatir_Click(object sender, EventArgs e)
        {
            int[,] yatirilanBanknotAdetleri = { { 200,0 }, { 100, 0 }, { 50, 0 }, { 20, 0 }, { 10, 0 }, { 5, 0 } };
            yatirilanBanknotAdetleri[0,1] += (tbx_yatirilan200adet.Text.Trim() == string.Empty) ? 0 : int.Parse(tbx_yatirilan200adet.Text);
            yatirilanBanknotAdetleri[1,1] += (tbx_yatirilan100adet.Text.Trim() == string.Empty) ? 0 : int.Parse(tbx_yatirilan100adet.Text);
            yatirilanBanknotAdetleri[2,1] += (tbx_yatirilan50adet.Text.Trim() == string.Empty) ? 0 : int.Parse(tbx_yatirilan50adet.Text);
            yatirilanBanknotAdetleri[3,1] += (tbx_yatirilan20adet.Text.Trim() == string.Empty) ? 0 : int.Parse(tbx_yatirilan20adet.Text);
            yatirilanBanknotAdetleri[4,1] += (tbx_yatirilan10adet.Text.Trim() == string.Empty) ? 0 : int.Parse(tbx_yatirilan10adet.Text);
            yatirilanBanknotAdetleri[5,1] += (tbx_yatirilan5adet.Text.Trim() == string.Empty) ? 0 : int.Parse(tbx_yatirilan5adet.Text);


            Unite kullanilacakUnite = HangiUnitedeDahaAzParaVar();
            foreach (var kaset in kullanilacakUnite.kasetler)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (yatirilanBanknotAdetleri[i,0] == kaset.banknotDegeri)
                    {
                        kaset.adet += yatirilanBanknotAdetleri[i, 1];
                    }
                }
            }

            lbl_yatirmaSonuc.Visible = true;
        }

        private Unite HangiUnitedeDahaAzParaVar()
        {
            int unite1banknotAdedi = 0;
            int unite2banknotAdedi = 0;
            foreach (var unite in uniteler) //uniteleri başlangıç konumuna geri getir
            {
                foreach (var kaset in unite.kasetler)
                {
                    if (unite.uniteNumarasi == 1)
                    {
                        unite1banknotAdedi += kaset.adet;
                    }
                    else if (unite.uniteNumarasi == 2)
                    {
                        unite2banknotAdedi += kaset.adet;
                    }
                }
            }

            if (unite1banknotAdedi >= unite2banknotAdedi)
            {
                return uniteler[1];
            }
            else
            {
                return uniteler[0];
            }
        }
    }
}






