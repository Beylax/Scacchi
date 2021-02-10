using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Schacchi_v1._1
{
    public class Regina : Pezzo
    {
        public Regina( Grid gs,  Grid gp, int r, int c, string nome, string tipo)
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
                img.Source = new BitmapImage(new Uri("Pezzi/reginaBianca.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                img.Source = new BitmapImage(new Uri("Pezzi/reginaNera.png", UriKind.RelativeOrAbsolute));
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
            AlfiereMove();
            TorreMove();
        }
    }
}