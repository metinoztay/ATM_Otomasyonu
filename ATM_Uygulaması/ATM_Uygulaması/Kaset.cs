using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_Uygulaması
{
    public class Kaset
    {
        public int banknotDegeri;
        public int adetYedek;
        public int adet { 
            get { return _adet; }
            set {
                if ( Math.Abs(value-_adet)!=1 )
                {
                    adetYedek = value;
                }
                _adet = value;
                textBox.Text = _adet.ToString();   
            }
        }

        private int _adet=0;
        public TextBox textBox;

        public Kaset(int deger, TextBox textBox)
        {
            this.banknotDegeri = deger;
            this.textBox = textBox;
        }

        public void AdetGuncelle()
        {
            if (textBox.Text.Trim() == string.Empty) //textbox boşsa
            {
                adet = 0;
            }
            else
            {
                adet = int.Parse(textBox.Text.Trim());
            }            
        }

        public void YedekGeriYukle()
        {
            adet = adetYedek;
        }

    }
}
