using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Schacchi_v1._1
{
    public class Re : Pezzo
    {
        public Re()
        {

        }

        public Re( Grid gs,  Grid gp, int r, int c, string nome, string tipo)
        {
            this.Gp = gp;
            this.Gs = gs;
            this.R = r;
            this.C = c;
            this.Tipo = tipo;
            App.pezzi[r, c] = this;
            this.B = new Button();
            this.B.Name = nome;
            Image img = new Image();
            if (this.Tipo == "b")
            {
                img.Source = new BitmapImage(new Uri("Pezzi/reBianco.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                img.Source = new BitmapImage(new Uri("Pezzi/reNero.png", UriKind.RelativeOrAbsolute));
            }
            this.B.Content = img;
            this.B.Click += B_Click;
            this.Gs.Children.Add(B);
            Grid.SetRow(B, this.R);
            Grid.SetColumn(B, this.C);
            this.isMoved = false;
        }

        private void B_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pulisci();
            ReMove();   
            //Controllo per arrocco
            if(!this.isMoved)
            {
                //Controllo per arrocco destro
                int count = 0;
                int index = 0;
                for(int i = this.C + 1; i <= 7; i++)
                {
                    if(GetChildrenPezzi(this.R, i) != null)
                    {
                        count++;
                        index = i;
                    }
                }
                if(count == 1)
                {
                    if(!App.pezzi[this.R, index].isMoved)
                    {
                        Torre t = new Torre();
                        if(App.pezzi[this.R, index].GetType() == t.GetType())
                        {
                            generaPosArrocco(this.R, this.C + 2);
                        }
                    }
                }
                //Controllo per arrocco sinistro
                count = 0;
                index = 0;
                for (int i = this.C - 1; i >= 0; i--)
                {
                    if (GetChildrenPezzi(this.R, i) != null)
                    {
                        count++;
                        index = i;
                    }
                }
                if (count == 1)
                {
                    if (!App.pezzi[this.R, index].isMoved)
                    {
                        Torre t = new Torre();
                        if (App.pezzi[this.R, index].GetType() == t.GetType())
                        {
                            generaPosArrocco(this.R, this.C - 2);
                        }

                    }
                }
            }
        }

        private void ReMove()
        {
            int c = Grid.GetColumn(this.B), r = Grid.GetRow(this.B);
            //Alto 
            if (r - 1 > -1)
            {
                generaPos(r - 1, c);
            }
            //Alto sinistra
            if (c - 1 > -1 && r - 1 > -1)
            {
                generaPos(r - 1, c - 1);
            }
            //Alto destra
            if (c + 1 < 8 && r - 1 > -1)
            {
                generaPos(r - 1, c + 1);
            }
            //Basso
            if (r + 1 < 8)
            {
                generaPos(r + 1, c);
            }
            //Basso sinistra
            if (c - 1 > -1 && r + 1 < 8)
            {
                generaPos(r + 1, c - 1);
            }
            //Basso destra
            if (c + 1 < 8 && r + 1 < 8)
            {
                generaPos(r + 1, c + 1);
            }
            //Sinistra
            if (c - 1 > -1)
            {
                generaPos(r, c - 1);
            }
            //Destra
            if (c + 1 < 8)
            {
                generaPos(r, c + 1);
            }
        }
    }
}