using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Media;

namespace Schacchi_v1._1
{
    public class Pezzo
    {
        public string Tipo { get; set; }
        public Button B { get; set; }
        public string Nome { get; set; }
        public Grid Gs { get; set; }
        public Grid Gp { get; set; }
        public int R { get; set; }
        public int C { get; set; }
        public bool isMoved { get; set; }

        public Pezzo GetChildrenPezzi(int row, int column)
        {
            if(row >= 0 && row <= 7 && column >= 0 && column <= 7)
            {
                return App.pezzi[row, column];
            }
            else
            {
                return null;
            }
        }

        public Button GetChildren(int row, int column, Grid g)
        {
            for (int i = 0; i < g.Children.Count; i++)
            {
                if (Grid.GetRow(g.Children[i]) == row && Grid.GetColumn(g.Children[i]) == column)
                {
                    return (Button)g.Children[i];
                }
            }
            return null;
        }

        public void RemoveChildren(int row, int column, Grid g)
        {
            for (int i = 0; i < g.Children.Count; i++)
            {
                if (Grid.GetRow(g.Children[i]) == row && Grid.GetColumn(g.Children[i]) == column)
                {
                    g.Children.RemoveAt(i);
                }
            }
        }

        public void generaPos(int r, int c)
        {
            Button pos = new Button();
            if (GetChildrenPezzi(r, c) == null)
            {
                Ellipse cerchio = new Ellipse();
                cerchio.Height = 20;
                cerchio.Width = 20;
                cerchio.Fill = Brushes.Green;
                pos.Content = cerchio;
                pos.Name = "vuoto";
                pos.Click += Pos_Click;
                this.Gp.Children.Add(pos);
                Grid.SetRow(pos, r);
                Grid.SetColumn(pos, c);
            }
            else
            {
                if (GetChildrenPezzi(r, c).Tipo != this.Tipo)
                { 
                    pos.BorderThickness = new Thickness(4);
                    pos.BorderBrush = Brushes.Red;
                    pos.Name = "vuoto";
                    pos.Click += Pos_Click;
                    this.Gp.Children.Add(pos);
                    Grid.SetRow(pos, r);
                    Grid.SetColumn(pos, c);
                }
            }            
        }

        public void generaPosArrocco(int r, int c)
        {
            Button pos = new Button();
            if (GetChildrenPezzi(r, c) == null)
            {
                Ellipse cerchio = new Ellipse();
                cerchio.Height = 20;
                cerchio.Width = 20;
                cerchio.Fill = Brushes.Green;
                pos.Content = cerchio;
                pos.Name = "vuoto";
                pos.Click += arrocco_Click;
                this.Gp.Children.Add(pos);
                Grid.SetRow(pos, r);
                Grid.SetColumn(pos, c);
            }
        }

        public void generaPosPedone(int r, int c)
        {
            Button pos = new Button();
            if (GetChildrenPezzi(r, c) == null)
            {
                Ellipse cerchio = new Ellipse();
                cerchio.Height = 20;
                cerchio.Width = 20;
                cerchio.Fill = Brushes.Green;
                pos.Content = cerchio;
                pos.Name = "vuoto";
                if (this.Tipo == "b")
                {
                    pos.Click += PosPedone_Click;
                }
                else
                {
                    pos.Click += PosPedoneNero_Click;
                }
                this.Gp.Children.Add(pos);
                Grid.SetRow(pos, r);
                Grid.SetColumn(pos, c);
            }
            else
            {
                if (GetChildrenPezzi(r, c).Tipo != this.Tipo)
                {
                    pos.BorderThickness = new Thickness(4);
                    pos.BorderBrush = Brushes.Red;
                    pos.Name = "vuoto";
                    if (this.Tipo == "b")
                    {
                        pos.Click += PosPedone_Click;
                    }
                    else
                    {
                        pos.Click += PosPedoneNero_Click;
                    }
                    this.Gp.Children.Add(pos);
                    Grid.SetRow(pos, r);
                    Grid.SetColumn(pos, c);
                }
            }
        }

        private void Pos_Click(object sender, RoutedEventArgs e)
        {
            //Attivo l'immagine dei pezzi mangiati
            if (App.pezzi[Grid.GetRow(((Button)sender)), Grid.GetColumn(((Button)sender))] != null)
            {
                MainWindow.pezziMangiati.Add(App.pezzi[Grid.GetRow(((Button)sender)), Grid.GetColumn(((Button)sender))]);
                AttivaImmagine(App.pezzi[Grid.GetRow(((Button)sender)), Grid.GetColumn(((Button)sender))]);

                //aggiorno le mosse
                MainWindow.mosse.Add($"{this.R}{this.C}{Grid.GetRow(((Button)sender))}{Grid.GetColumn(((Button)sender))}t");
            }
            else
            {
                //aggiorno le mosse
                MainWindow.mosse.Add($"{this.R}{this.C}{Grid.GetRow(((Button)sender))}{Grid.GetColumn(((Button)sender))}f");
            }

            //Aggiorno App.pezzi
            App.pezzi[this.R, this.C] = null;
            this.R = Grid.GetRow(((Button)sender));
            this.C = Grid.GetColumn(((Button)sender));
            App.pezzi[this.R, this.C] = this;

            //Aggiorno Gs
            RemoveChildren(Grid.GetRow(((Button)sender)), Grid.GetColumn(((Button)sender)), this.Gs);
            Grid.SetRow(this.B, this.R);
            Grid.SetColumn(this.B, this.C);

            //Aggiorno Gp
            pulisci();

            //Attivo i pezzi del tipo contrario al mio e disattivo i pezzi del mio stesso tipo
            MainWindow.onoff(false, this.Tipo);
            if(this.Tipo == "b")
            {
                App.timerN.Start();
                App.timerB.Stop();
            }
            else
            {
                App.timerN.Stop();
                App.timerB.Start();
            }
            this.isMoved = true;
            SoundPlayer sp = new SoundPlayer("mossaPezzo.wav");
            sp.Play();
        }

        private void arrocco_Click(object sender, RoutedEventArgs e)
        {
            if(Grid.GetColumn((Button)sender) > 4)
            {
                //Aggiorno App.pezzi a destra
                //Aggiorno Gs
                //Re
                Grid.SetColumn(this.B, this.C + 2);
                //Torre
                Grid.SetColumn(App.pezzi[this.R, this.C + 3].B, this.C + 1);
                //Aggiorno la torre
                App.pezzi[this.R, this.C + 1] = App.pezzi[this.R, this.C + 3];
                App.pezzi[this.R, this.C + 3] = null;
                App.pezzi[this.R, this.C + 1].C = this.C + 1;
                //Aggiorno il re
                App.pezzi[this.R, this.C] = null;
                this.R = Grid.GetRow(((Button)sender));
                this.C = Grid.GetColumn(((Button)sender));
                App.pezzi[this.R, this.C] = this;
            }
            else
            {
                //Aggiorno App.pezzi a sinistra
                //Aggiorno Gs
                //Re
                Grid.SetColumn(this.B, this.C - 2);
                //Torre
                Grid.SetColumn(App.pezzi[this.R, this.C - 4].B, this.C - 1);
                //Aggiorno la torre
                App.pezzi[this.R, this.C - 1] = App.pezzi[this.R, this.C - 4];
                App.pezzi[this.R, this.C - 4] = null;
                App.pezzi[this.R, this.C - 1].C = this.C - 1;
                //Aggiorno il re
                App.pezzi[this.R, this.C] = null;
                this.R = Grid.GetRow(((Button)sender));
                this.C = Grid.GetColumn(((Button)sender));
                App.pezzi[this.R, this.C] = this;
            }
            //Aggiorno Gp
            pulisci();
            //Attivo i pezzi del tipo contrario al mio e disattivo i pezzi del mio stesso tipo
            MainWindow.onoff(false, this.Tipo);
            if (this.Tipo == "b")
            {
                App.timerN.Start();
                App.timerB.Stop();
            }
            else
            {
                App.timerN.Stop();
                App.timerB.Start();
            }
            this.isMoved = true;
            SoundPlayer sp = new SoundPlayer("mossaPezzo.wav");
            sp.Play();
        }

        private void PosPedone_Click(object sender, RoutedEventArgs e)
        {
            //Faccio scegliere all'utente che pezzo vuol fare diventare
            pulisci();
            Button regina = new Button();
            regina.Name = "regina";
            regina.Background = Brushes.White;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("Pezzi/reginaBianca.png", UriKind.RelativeOrAbsolute));
            regina.Content = img;
            regina.Click += scelta_Click;
            this.Gp.Children.Add(regina);
            Grid.SetColumn(regina, Grid.GetColumn((Button)sender));
            Grid.SetRow(regina, this.R - 1);

            Button torre = new Button();
            torre.Name = "torre";
            torre.Background = Brushes.White;
            img = new Image();
            img.Source = new BitmapImage(new Uri("Pezzi/torreBianca.png", UriKind.RelativeOrAbsolute));
            torre.Content = img;
            torre.Click += scelta_Click;
            this.Gp.Children.Add(torre);
            Grid.SetColumn(torre, Grid.GetColumn((Button)sender));
            Grid.SetRow(torre, this.R);

            Button cavallo = new Button();
            cavallo.Name = "cavallo";
            cavallo.Background = Brushes.White;
            img = new Image();
            img.Source = new BitmapImage(new Uri("Pezzi/cavalloBianco.png", UriKind.RelativeOrAbsolute));
            cavallo.Content = img;
            cavallo.Click += scelta_Click;
            this.Gp.Children.Add(cavallo);
            Grid.SetColumn(cavallo, Grid.GetColumn((Button)sender));
            Grid.SetRow(cavallo, this.R + 1);

            Button alfiere = new Button();
            alfiere.Name = "alfiere";
            alfiere.Background = Brushes.White;
            img = new Image();
            img.Source = new BitmapImage(new Uri("Pezzi/alfiereBianco.png", UriKind.RelativeOrAbsolute));
            alfiere.Content = img;
            alfiere.Click += scelta_Click;
            this.Gp.Children.Add(alfiere);
            Grid.SetColumn(alfiere, Grid.GetColumn((Button)sender));
            Grid.SetRow(alfiere, this.R + 2);


        }

        private void scelta_Click(object sender, RoutedEventArgs e)
        {
            RemoveChildren(this.R - 1, Grid.GetColumn((Button)sender), this.Gs);
            switch (((Button)sender).Name)
            {
                case "regina":
                    {
                        Regina bregina = new Regina(this.Gs, this.Gp, this.R - 1, Grid.GetColumn((Button)sender), "Regina2", "b");
                        App.pezzi[this.R - 1, Grid.GetColumn((Button)sender)] = bregina;
                        break;
                    }
                case "torre":
                    {
                        Torre btR = new Torre(this.Gs, this.Gp, this.R - 1, Grid.GetColumn((Button)sender), "Torre2", "b");
                        App.pezzi[this.R - 1, Grid.GetColumn((Button)sender)] = btR;
                        break;
                    }
                case "cavallo":
                    {
                        Cavallo btC = new Cavallo(this.Gs, this.Gp, this.R - 1, Grid.GetColumn((Button)sender), "Cavallo2", "b");
                        App.pezzi[this.R - 1, Grid.GetColumn((Button)sender)] = btC;
                        break;
                    }
                case "alfiere":
                    {
                        Alfiere btA = new Alfiere(this.Gs, this.Gp, this.R - 1, Grid.GetColumn((Button)sender), "Alfiere2", "b");
                        App.pezzi[this.R - 1, Grid.GetColumn((Button)sender)] = btA;
                        break;
                    }
            }
            App.pezzi[this.R, this.C] = null;
            RemoveChildren(this.R, this.C, this.Gs);
            pulisci();
            MainWindow.onoff(false, this.Tipo);
            if (this.Tipo == "b")
            {
                App.timerN.Start();
                App.timerB.Stop();
            }
            else
            {
                App.timerN.Stop();
                App.timerB.Start();
            }
            this.isMoved = true;
            SoundPlayer sp = new SoundPlayer("mossaPezzo.wav");
            sp.Play();
        }

        private void PosPedoneNero_Click(object sender, RoutedEventArgs e)
        {
            //Faccio scegliere all'utente che pezzo vuol fare diventare
            pulisci();
            Button regina = new Button();
            regina.Name = "regina";
            regina.Background = Brushes.White;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("Pezzi/reginaNera.png", UriKind.RelativeOrAbsolute));
            regina.Content = img;
            regina.Click += sceltaNero_Click;
            this.Gp.Children.Add(regina);
            Grid.SetColumn(regina, Grid.GetColumn((Button)sender));
            Grid.SetRow(regina, this.R + 1);

            Button torre = new Button();
            torre.Name = "torre";
            torre.Background = Brushes.White;
            img = new Image();
            img.Source = new BitmapImage(new Uri("Pezzi/torreNera.png", UriKind.RelativeOrAbsolute));
            torre.Content = img;
            torre.Click += sceltaNero_Click;
            this.Gp.Children.Add(torre);
            Grid.SetColumn(torre, Grid.GetColumn((Button)sender));
            Grid.SetRow(torre, this.R);

            Button cavallo = new Button();
            cavallo.Name = "cavallo";
            cavallo.Background = Brushes.White;
            img = new Image();
            img.Source = new BitmapImage(new Uri("Pezzi/cavalloNero.png", UriKind.RelativeOrAbsolute));
            cavallo.Content = img;
            cavallo.Click += sceltaNero_Click;
            this.Gp.Children.Add(cavallo);
            Grid.SetColumn(cavallo, Grid.GetColumn((Button)sender));
            Grid.SetRow(cavallo, this.R - 1);

            Button alfiere = new Button();
            alfiere.Name = "alfiere";
            alfiere.Background = Brushes.White;
            img = new Image();
            img.Source = new BitmapImage(new Uri("Pezzi/alfiereNero.png", UriKind.RelativeOrAbsolute));
            alfiere.Content = img;
            alfiere.Click += sceltaNero_Click;
            this.Gp.Children.Add(alfiere);
            Grid.SetColumn(alfiere, Grid.GetColumn((Button)sender));
            Grid.SetRow(alfiere, this.R - 2);

        }

        private void sceltaNero_Click(object sender, RoutedEventArgs e)
        {
            RemoveChildren(this.R + 1, Grid.GetColumn((Button)sender), this.Gs);
            switch (((Button)sender).Name)
            {
                case "regina":
                    {
                        Regina bregina = new Regina(this.Gs, this.Gp, this.R + 1, Grid.GetColumn((Button)sender), "Regina2", "n");
                        App.pezzi[this.R + 1, Grid.GetColumn((Button)sender)] = bregina;
                        break;
                    }
                case "torre":
                    {
                        Torre btR = new Torre(this.Gs, this.Gp, this.R + 1, Grid.GetColumn((Button)sender), "TorreR2", "n");
                        App.pezzi[this.R + 1, Grid.GetColumn((Button)sender)] = btR;
                        break;
                    }
                case "cavallo":
                    {
                        Cavallo btC = new Cavallo(this.Gs, this.Gp, this.R + 1, Grid.GetColumn((Button)sender), "Cavallo2", "n");
                        App.pezzi[this.R + 1, Grid.GetColumn((Button)sender)] = btC;
                        break;
                    }
                case "alfiere":
                    {
                        Alfiere btA = new Alfiere(this.Gs, this.Gp, this.R + 1, Grid.GetColumn((Button)sender), "Alfiere2", "n");
                        App.pezzi[this.R + 1, Grid.GetColumn((Button)sender)] = btA;
                        break;
                    }
            }
            App.pezzi[this.R, this.C] = null;
            RemoveChildren(this.R, this.C, this.Gs);
            pulisci();
            MainWindow.onoff(false, this.Tipo);
            if (this.Tipo == "b")
            {
                App.timerN.Start();
                App.timerB.Stop();
            }
            else
            {
                App.timerN.Stop();
                App.timerB.Start();
            }
            this.isMoved = true;
            SoundPlayer sp = new SoundPlayer("mossaPezzo.wav");
            sp.Play();
        }

        public void pulisci()
        {
            Gp.Children.Clear();
        }
        public void TorreMove()
        {
            int c = Grid.GetColumn(this.B), r = Grid.GetRow(this.B);
            int i = 1;
            if (r + i != 8)
            {
                while (GetChildrenPezzi(r + i, c) == null && r + i != 8)
                {
                    generaPos(r + i, c);
                    i++;
                }
                if (r + i != 8)
                {
                    generaPos(r + i, c);
                }
            }
            i = 1;
            if (r - i != -1)
            {
                while (GetChildrenPezzi(r - i, c) == null && r - i != -1)
                {
                    generaPos(r - i, c);
                    i++;
                }
                if (r - i != -1)
                {
                    generaPos(r - i, c);
                }
            }
            i = 1;
            if (c - i != -1)
            {
                while (GetChildrenPezzi(r, c - i) == null && c - i != -1)
                {
                    generaPos(r, c - i);
                    i++;
                }
                if (c - i != -1)
                {
                    generaPos(r, c - i);
                }
            }
            i = 1;
            if (c + i != 8)
            {
                while (GetChildrenPezzi(r, c + i) == null && c + i != 8)
                {
                    generaPos(r, c + i);
                    i++;
                }
                if (c + i != 8)
                {
                    generaPos(r, c + i);
                }
            }
        }
        public void AlfiereMove()
        {
            int c = Grid.GetColumn(this.B), r = Grid.GetRow(this.B);
            //Basso destra
            int i = 1;
            if (r + i != 8 && c + i != 8)
            {
                while (GetChildrenPezzi(r + i, c + i) == null && r + i != 8 && c + i != 8)
                {
                    generaPos(r + i, c + i);
                    i++;
                }
                if (r + i != 8 && c + i != 8)
                {
                    generaPos(r + i, c + i);
                }
            }
            //Alto destra
            i = 1;
            if (r - i != -1 && c + i != 8)
            {
                while (GetChildrenPezzi(r - i, c + i) == null && r - i != -1 && c + i != 8)
                {
                    generaPos(r - i, c + i);
                    i++;
                }
                if (r - i != -1 && c + i != 8)
                {
                    generaPos(r - i, c + i);
                }
            }
            //Basso sinistra
            i = 1;
            if (c - i != -1 && r + i != 8)
            {
                while (GetChildrenPezzi(r + i, c - i) == null && c - i != -1 && r + i != 8)
                {
                    generaPos(r + i, c - i);
                    i++;
                }
                if (c - i != -1 && r + i != 8)
                {
                    generaPos(r + i, c - i);
                }
            }
            i = 1;
            //Alto sinistra
            if (c - i != -1 && r - i != -1)
            {
                while (GetChildrenPezzi(r - i, c - i) == null && c - i != -1 && r - i != -1)
                {
                    generaPos(r - i, c - i);
                    i++;
                }
                if (c - i != -1 && r - i != -1)
                {
                    generaPos(r - i, c - i);
                }
            }
        }
        public void AttivaImmagine(Pezzo p)
        {
            if(p.Tipo == "b")
            {
                switch (p.B.Name[1])
                {
                    case 'P':
                        {
                            Image img = new Image();
                            img.Name = "pedone";
                            img.Source = new BitmapImage(new Uri("Pezzi/pedoneBianco.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKPedoniBianchi.Children.Add(img);
                            break;
                        }
                    case 'T':
                        {
                            Image img = new Image();
                            img.Name = "torre";
                            img.Source = new BitmapImage(new Uri("Pezzi/torreBianca.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKbianchi.Children.Add(img);
                            break;
                        }
                    case 'C':
                        {
                            Image img = new Image();
                            img.Name = "cavallo";
                            img.Source = new BitmapImage(new Uri("Pezzi/cavalloBianco.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKbianchi.Children.Add(img);
                            break;
                        }
                    case 'A':
                        {
                            Image img = new Image();
                            img.Name = "alfiere";
                            img.Source = new BitmapImage(new Uri("Pezzi/alfiereBianco.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKbianchi.Children.Add(img);
                            break;
                        }
                    case 'R':
                        {
                            Image img = new Image();
                            img.Name = "regina";
                            img.Source = new BitmapImage(new Uri("Pezzi/reginaBianca.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKbianchi.Children.Add(img);
                            break;
                        }
                }
            }
            else
            {
                switch (p.B.Name[1])
                {
                    case 'P':
                        {
                            Image img = new Image();
                            img.Name = "npedone";
                            img.Source = new BitmapImage(new Uri("Pezzi/pedoneNero.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKPedoniNeri.Children.Add(img);
                            break;
                        }
                    case 'T':
                        {
                            Image img = new Image();
                            img.Name = "ntorre";
                            img.Source = new BitmapImage(new Uri("Pezzi/torreNera.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKNeri.Children.Add(img);
                            break;
                        }
                    case 'C':
                        {
                            Image img = new Image();
                            img.Name = "ncavallo";
                            img.Source = new BitmapImage(new Uri("Pezzi/cavalloNero.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKNeri.Children.Add(img);
                            break;
                        }
                    case 'A':
                        {
                            Image img = new Image();
                            img.Name = "nalfiere";
                            img.Source = new BitmapImage(new Uri("Pezzi/alfiereNero.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKNeri.Children.Add(img);
                            break;
                        }
                    case 'R':
                        {
                            Image img = new Image();
                            img.Name = "nregina";
                            img.Source = new BitmapImage(new Uri("Pezzi/reginaNera.png", UriKind.RelativeOrAbsolute));
                            MainWindow.immaginiPezziMangiati.Add(img);
                            MainWindow.STKNeri.Children.Add(img);
                            break;
                        }
                }
            }
        }

    }
}