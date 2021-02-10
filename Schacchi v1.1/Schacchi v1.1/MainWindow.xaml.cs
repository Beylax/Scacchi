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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Schacchi_v1._1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static TextBlock txtN, txtB;
        int timerBM = 5, timerNM = 5, timerBS = 0, timerNS = 0;

        public static StackPanel STKbianchi;
        public static StackPanel STKPedoniBianchi;
        public static StackPanel STKNeri;
        public static StackPanel STKPedoniNeri;

        public static List<Pezzo> pezziMangiati;
        public static List<Image> immaginiPezziMangiati;
        public static List<string> mosse;

        public static bool visualizza = true;

        public static Grid GSchacchiera;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Creazione di GScacchiera
            creaScacchiera();

            //Creazione timers
            creazioneTimers();

            //Creazione pezzi
            creazionePezzi();

            //Disattivo tutti i pezzi neri
            onoff(false, "n");

            //Creo le liste
            pezziMangiati = new List<Pezzo>();
            mosse = new List<string>();
            immaginiPezziMangiati = new List<Image>();

            //Creo gli stack panels per i pezzi mangiati
            creazioneStackPanels();
        }

        public static void onoff(bool onoff, string tipo)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (App.pezzi[i, k] != null)
                    {
                        if (App.pezzi[i, k].Tipo == tipo)
                        {
                            App.pezzi[i, k].B.IsHitTestVisible = onoff;
                        }
                        else
                        {
                            App.pezzi[i, k].B.IsHitTestVisible = !onoff;
                        }
                    }
                }
            }
        }

        private void timerB_Tick(object sender, EventArgs e)
        {
            if (timerBS == 0)
            {
                timerBS = 60;
                timerBM--;
            }
            timerBS--;
            MainWindow.txtB.Text = string.Format("{0}:{1:00}", timerBM, timerBS);
            if (timerBM == 0 && timerBS == 0)
            {
                //Quando Nero vince
            }
        }

        private void timerN_Tick(object sender, EventArgs e)
        {
            if (timerNS == 0)
            {
                timerNS = 60;
                timerNM--;
            }
            timerNS--;
            MainWindow.txtN.Text = string.Format("{0}:{1:00}", timerNM, timerNS);
            if (timerNM == 0 && timerNS == 0)
            {
                //Quando Bianco vince
            }
        }

        private void Home_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.R)
            {
                if(mosse.Count > 0)
                {
                    if (visualizza)
                    {
                        Window1 w = new Window1();
                        w.Show();
                    }
                    else
                    {
                        Window1.indietro();
                    }
                }
            }
        }

        private void creaScacchiera()
        {
            GSchacchiera = GScacchiera;
        }

        //Da implementare
        //private void BTNReset_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow w = new MainWindow();
        //    w.Show();
        //    this.Close();
        //}

        private void creazioneTimers()
        {
            //CCreazione timer
            App.timerB = new DispatcherTimer();
            App.timerB.Stop();
            App.timerB.Interval = new TimeSpan(0, 0, 1);
            App.timerB.Tick += new EventHandler(timerB_Tick);
            App.timerN = new DispatcherTimer();
            App.timerN.Stop();
            App.timerN.Interval = new TimeSpan(0, 0, 1);
            App.timerN.Tick += new EventHandler(timerN_Tick);
            //Creo la text block per il tempo del nero
            txtN = new TextBlock();
            txtN.Margin = new Thickness(40, 100, 40, 100);
            txtN.Text = "5:00";
            txtN.FontSize = 90;
            txtN.FontWeight = FontWeights.Black;
            txtN.TextAlignment = TextAlignment.Center;
            GDX.Children.Add(txtN);
            //Creo la text block per il tempo del bianco
            txtB = new TextBlock();
            txtB.Margin = new Thickness(40, 100, 40, 100);
            txtB.Text = "5:00";
            txtB.FontSize = 90;
            txtB.FontWeight = FontWeights.Black;
            txtB.TextAlignment = TextAlignment.Center;
            GDX.Children.Add(txtB);
            Grid.SetRow(txtB, 2);
        }
         
        private void creazionePezzi()
        {
            //Creazioni pedoni bianchi
            for (int i = 0; i < 8; i++)
            {
                Pedone p = new Pedone( GSchacchiera,  GPosizioni, 6, i, $"bPedone{i}", "b");
            }
            //Creazione pedoni neri
            for (int i = 0; i < 8; i++)
            {
                Pedone p = new Pedone( GSchacchiera,  GPosizioni, 1, i, $"nPedone{i}", "n");
            }
            //Creazioni pezzi bianchi
            Torre btL = new Torre( GSchacchiera,  GPosizioni, 7, 0, "bTorreL", "b");
            Torre btR = new Torre( GSchacchiera,  GPosizioni, 7, 7, "bTorreR", "b");
            Cavallo bcL = new Cavallo( GSchacchiera,  GPosizioni, 7, 1, "bCavalloL", "b");
            Cavallo bcR = new Cavallo( GSchacchiera,  GPosizioni, 7, 6, "bCavalloR", "b");
            Alfiere baL = new Alfiere( GSchacchiera,  GPosizioni, 7, 2, "bAlfiereL", "b");
            Alfiere baR = new Alfiere( GSchacchiera,  GPosizioni, 7, 5, "bAlfiereR", "b");
            Regina bregina = new Regina( GSchacchiera,  GPosizioni, 7, 3, "bRegina", "b");
            Re bre = new Re( GSchacchiera,  GPosizioni, 7, 4, "bRe", "b");
            //Crezione pezzi neri
            Torre ntL = new Torre( GSchacchiera,  GPosizioni, 0, 0, "nTorreL", "n");
            Torre ntR = new Torre( GSchacchiera,  GPosizioni, 0, 7, "nTorreR", "n");
            Cavallo ncL = new Cavallo( GSchacchiera,  GPosizioni, 0, 1, "nCavalloL", "n");
            Cavallo ncR = new Cavallo( GSchacchiera,  GPosizioni, 0, 6, "nCavalloR", "n");
            Alfiere naL = new Alfiere( GSchacchiera,  GPosizioni, 0, 2, "nAlfiereL", "n");
            Alfiere naR = new Alfiere( GSchacchiera,  GPosizioni, 0, 5, "nAlfiereR", "n");
            Regina nregina = new Regina( GSchacchiera,  GPosizioni, 0, 3, "nRegina", "n");
            Re nre = new Re( GSchacchiera,  GPosizioni, 0, 4, "bRe", "n");
        }

        private void creazioneStackPanels()
        {
            STKbianchi = new StackPanel();
            STKbianchi.Margin = new Thickness(10, 250, 10, 50);
            STKbianchi.Orientation = Orientation.Horizontal;

            STKPedoniBianchi = new StackPanel();
            STKPedoniBianchi.Margin = new Thickness(10, 290, 10, 10);
            STKPedoniBianchi.Orientation = Orientation.Horizontal;

            STKNeri = new StackPanel();
            STKNeri.Margin = new Thickness(10, 250, 10, 50);
            STKNeri.Orientation = Orientation.Horizontal;

            STKPedoniNeri = new StackPanel();
            STKPedoniNeri.Margin = new Thickness(10, 290, 10, 10);
            STKPedoniNeri.Orientation = Orientation.Horizontal;

            GDX.Children.Add(STKbianchi);
            GDX.Children.Add(STKPedoniBianchi);
            GDX.Children.Add(STKNeri);
            Grid.SetRow(STKNeri, 2);
            GDX.Children.Add(STKPedoniNeri);
            Grid.SetRow(STKPedoniNeri, 2);
        }

        private void BtnIndietro_Click(object sender, RoutedEventArgs e)
        {
            if(mosse.Count > 0)
            {
                if (visualizza)
                {
                    Window1 w = new Window1();
                    w.Show();
                }
                else
                {
                    Window1.indietro();
                }
            }
        }
    }
}