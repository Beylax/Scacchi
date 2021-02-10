using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Schacchi_v1._1
{
    public class Pedone : Pezzo
    {
        public Pedone(Grid gs, Grid gp, int r, int c, string nome, string tipo)
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
                img.Source = new BitmapImage(new Uri("Pezzi/pedoneBianco.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                img.Source = new BitmapImage(new Uri("Pezzi/pedoneNero.png", UriKind.RelativeOrAbsolute));
            }
            this.B.Content = img;
            this.B.Click += this.B_Click;
            this.Gs.Children.Add(B);
            Grid.SetRow(B, this.R);
            Grid.SetColumn(B, this.C);
        }

        private void B_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            pulisci();
            PedoneMove();
        }

        private void PedoneMove()
        {
            int c = Grid.GetColumn(this.B), r = Grid.GetRow(this.B);
            //Controllo se il pedone è bianco o nero
            if (this.Tipo.ToLower() == "b")
            {
                if (GetChildrenPezzi(r - 1, c) == null)
                {
                    if (r - 1 == 0)
                    {
                        generaPosPedone(r - 1, c);
                    }
                    else
                    {
                        generaPos(r - 1, c);
                    }
                    if (r == 6 && GetChildrenPezzi(r - 2, c) == null)
                    {
                        generaPos(r - 2, c);
                    }
                }
                if (c - 1 != -1)
                {
                    if (GetChildrenPezzi(r - 1, c - 1) != null)
                    {
                        if (GetChildrenPezzi(r - 1, c - 1).Tipo == "n")
                        {
                            if (r - 1 == 0)
                            {
                                generaPosPedone(r - 1, c - 1);
                            }
                            else
                            {
                                generaPos(r - 1, c - 1);
                            }
                        }
                    }
                }
                if (c + 1 != 8)
                {
                    if (GetChildrenPezzi(r - 1, c + 1) != null)
                    {
                        if (GetChildrenPezzi(r - 1, c + 1).Tipo == "n")
                        {
                            if (r - 1 == 0)
                            {
                                generaPosPedone(r - 1, c + 1);
                            }
                            else
                            {
                                generaPos(r - 1, c + 1);
                            }
                        }
                    }
                }

            }
            else
            {
                if (GetChildrenPezzi(r + 1, c) == null)
                {
                    if (r + 1 == 7)
                    {
                        generaPosPedone(r + 1, c);
                    }
                    else
                    {
                        generaPos(r + 1, c);
                    }
                    if (r == 1 && GetChildrenPezzi(r + 2, c) == null)
                    {
                        generaPos(r + 2, c);
                    }
                }
                if (c - 1 != -1)
                {
                    if (GetChildrenPezzi(r + 1, c - 1) != null)
                    {
                        if (GetChildrenPezzi(r + 1, c - 1).Tipo == "b")
                        {
                            if (r + 1 == 7)
                            {
                                generaPosPedone(r + 1, c - 1);
                            }
                            else
                            {
                                generaPos(r + 1, c - 1);
                            }
                        }
                    }
                }
                if (c + 1 != 8)
                {
                    if (GetChildrenPezzi(r + 1, c + 1) != null)
                    {
                        if (GetChildrenPezzi(r + 1, c + 1).Tipo == "b")
                        {
                            if (r + 1 == 7)
                            {
                                generaPosPedone(r + 1, c + 1);
                            }
                            else
                            {
                                generaPos(r + 1, c + 1);
                            }
                        }
                    }
                }
            }
        }
    }
}