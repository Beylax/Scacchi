using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Schacchi_v1._1
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void BTNSi_Click(object sender, RoutedEventArgs e)
        {
            indietro();
            this.Close();
        }

        private void BTNNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Indietro non funziona con arrocco
        public static void indietro()
        {
            if (MainWindow.mosse.Count > 0)
            {
                int r = int.Parse((MainWindow.mosse[MainWindow.mosse.Count - 1][0]).ToString());
                int c = int.Parse((MainWindow.mosse[MainWindow.mosse.Count - 1][1]).ToString());
                int rD = int.Parse((MainWindow.mosse[MainWindow.mosse.Count - 1][2]).ToString());
                int cD = int.Parse((MainWindow.mosse[MainWindow.mosse.Count - 1][3]).ToString());

                //Aggiorno app.pezzi
                App.pezzi[r, c] = App.pezzi[rD, cD];
                App.pezzi[r, c].R = r;
                App.pezzi[r, c].C = c;
                App.pezzi[rD, cD] = null;

                //Aggiorno gs
                Grid.SetRow(App.pezzi[r, c].B, App.pezzi[r, c].R);
                Grid.SetColumn(App.pezzi[r, c].B, App.pezzi[r, c].C);

                //Ripristo i pezzi
                if (MainWindow.mosse[MainWindow.mosse.Count - 1][4] == 't')
                {
                    //Aggiorno app.pezzi
                    App.pezzi[rD, cD] = MainWindow.pezziMangiati[MainWindow.pezziMangiati.Count - 1];

                    //Aggiorno gs
                    MainWindow.GSchacchiera.Children.Add(App.pezzi[rD, cD].B);
                    Grid.SetRow(App.pezzi[rD, cD].B, App.pezzi[rD, cD].R);
                    Grid.SetColumn(App.pezzi[rD, cD].B, App.pezzi[rD, cD].C);
                    //Aggiungere il fatto che quando ripristino un pezzo devo togliere l'immagine che è stato mangiato
                    MainWindow.pezziMangiati.RemoveAt(MainWindow.pezziMangiati.Count - 1);

                    //Tolgo che il pezzo è stato mangiato
                    for (int i = 0; i < MainWindow.STKbianchi.Children.Count; i++)
                    {
                        if (((Image)MainWindow.STKbianchi.Children[i]).Name == MainWindow.immaginiPezziMangiati[MainWindow.immaginiPezziMangiati.Count - 1].Name)
                        {
                            MainWindow.STKbianchi.Children.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < MainWindow.STKPedoniBianchi.Children.Count; i++)
                    {
                        if (((Image)MainWindow.STKPedoniBianchi.Children[i]).Name == MainWindow.immaginiPezziMangiati[MainWindow.immaginiPezziMangiati.Count - 1].Name)
                        {
                            MainWindow.STKPedoniBianchi.Children.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < MainWindow.STKNeri.Children.Count; i++)
                    {
                        if (((Image)MainWindow.STKNeri.Children[i]).Name == MainWindow.immaginiPezziMangiati[MainWindow.immaginiPezziMangiati.Count - 1].Name)
                        {
                            MainWindow.STKNeri.Children.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < MainWindow.STKPedoniNeri.Children.Count; i++)
                    {
                        if (((Image)MainWindow.STKPedoniNeri.Children[i]).Name == MainWindow.immaginiPezziMangiati[MainWindow.immaginiPezziMangiati.Count - 1].Name)
                        {
                            MainWindow.STKPedoniNeri.Children.RemoveAt(i);
                        }
                    }
                    MainWindow.immaginiPezziMangiati.RemoveAt(MainWindow.immaginiPezziMangiati.Count - 1);
                }

                //Attivo i pezzi del mio stesso tipo
                MainWindow.onoff(true, App.pezzi[r, c].Tipo);
                if (App.pezzi[r, c].Tipo == "b")
                {
                    App.timerN.Stop();
                    App.timerB.Start();
                }
                else
                {
                    App.timerN.Start();
                    App.timerB.Stop();
                }

                MainWindow.mosse.RemoveAt(MainWindow.mosse.Count - 1);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if((bool)CKBVisualizzare.IsChecked)
            {
                MainWindow.visualizza = false;
            }
            else
            {
                MainWindow.visualizza = true;
            }
            App.Current.MainWindow.IsHitTestVisible = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
            App.Current.MainWindow.IsHitTestVisible = false;
        }
    }
}
