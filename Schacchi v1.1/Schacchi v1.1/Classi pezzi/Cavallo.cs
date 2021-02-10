using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Schacchi_v1._1
{
    public class Cavallo : Pezzo
    {
        public Cavallo( Grid gs,  Grid gp, int r, int c, string nome, string tipo)
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
                img.Source = new BitmapImage(new Uri("Pezzi/cavalloBianco.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                img.Source = new BitmapImage(new Uri("Pezzi/cavalloNero.png", UriKind.RelativeOrAbsolute));
            }
            this.B.Content = img;
            this.B.Click += B_Click;
            this.Gs.Children.Add(B);
            Grid.SetRow(B, this.R);
            Grid.SetColumn(B, this.C);
        }

        private void B_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pulisci();
            CavalloMove();
        }

        private void CavalloMove()
        {
            int c = Grid.GetColumn(this.B), r = Grid.GetRow(this.B);
            //Alto 
            //Alto sinistra
            if (c - 1 > -1 && r - 2 > -1)
            {
                generaPos(r - 2, c - 1);
            }
            //Alto destra
            if (c + 1 < 8 && r - 2 > -1)
            {
                generaPos(r - 2, c + 1);
            }
            //Basso
            //Basso sinistra
            if (c - 1 > -1 && r + 2 < 8)
            {
                generaPos(r + 2, c - 1);
            }
            //Basso destra
            if (c + 1 < 8 && r + 2 < 8)
            {
                generaPos(r + 2, c + 1);
            }
            //Sinistra
            //Sinistra alto
            if (c - 2 > -1 && r - 1 > -1)
            {
                generaPos(r - 1, c - 2);
            }
            //Sinistra basso
            if (c - 2 > -1 && r + 1 < 8)
            {
                generaPos(r + 1, c - 2);
            }
            //Destra
            //Destra alto
            if (c + 2 < 8 && r - 1 > -1)
            {
                generaPos(r - 1, c + 2);
            }
            //Destra basso
            if (c + 2 < 8 && r + 1 < 8)
            {
                generaPos(r + 1, c + 2);
            }
        }
    }
}