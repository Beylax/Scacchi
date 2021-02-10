using Scacchi_V2._0.Classi;
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

namespace Scacchi_V2._0
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

        public static List<Pezzo> pezzi;
        public static List<Pezzo> pezziMangiati;
        public static Grid scacchiera;
        public static Grid sMosse;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pezzi = new List<Pezzo>();
            pezziMangiati = new List<Pezzo>();
            scacchiera = GScacchiera;
            sMosse = GMosse;
            creaPezzi();
        }

        void creaPezzi()
        {
            //Pedoni bianchi
            for (int i = 0; i < 8; i++)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("/Immagini/pedoneBianco.png", UriKind.Relative));
                Pezzo pedone = new Pedone(6, i, "p", "b", img, GScacchiera);
                pezzi.Add(pedone);
            }

            //Pedoni neri
            for (int i = 0; i < 8; i++)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("/Immagini/pedoneNero.png", UriKind.Relative));
                Pezzo pedone = new Pedone(1, i, "p", "n", img, GScacchiera);
                pezzi.Add(pedone);
            }

            //Pezzi bianchi
            Image immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/torreBianca.png", UriKind.Relative));
            pezzi.Add(new Torre(7, 0, "t", "b", immagine, GScacchiera));
            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/torreBianca.png", UriKind.Relative));
            pezzi.Add(new Torre(7, 7, "t", "b", immagine, GScacchiera));

            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/cavalloBianco.png", UriKind.Relative));
            pezzi.Add(new Cavallo(7, 1, "c", "b", immagine, GScacchiera));
            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/cavalloBianco.png", UriKind.Relative));
            pezzi.Add(new Cavallo(7, 6, "c", "b", immagine, GScacchiera));

            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/alfiereBianco.png", UriKind.Relative));
            pezzi.Add(new Alfiere(7, 2, "a", "b", immagine, GScacchiera));
            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/alfiereBianco.png", UriKind.Relative));
            pezzi.Add(new Alfiere(7, 5, "a", "b", immagine, GScacchiera));

            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/reBianco.png", UriKind.Relative));
            pezzi.Add(new Re(7, 4, "R", "b", immagine, GScacchiera));

            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/reginaBianca.png", UriKind.Relative));
            pezzi.Add(new Regina(7, 3, "r", "b", immagine, GScacchiera));

            //Pezzi neri
            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/torreNera.png", UriKind.Relative));
            pezzi.Add(new Torre(0, 0, "t", "n", immagine, GScacchiera));
            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/torreNera.png", UriKind.Relative));
            pezzi.Add(new Torre(0, 7, "t", "n", immagine, GScacchiera));

            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/cavalloNero.png", UriKind.Relative));
            pezzi.Add(new Cavallo(0, 1, "c", "n", immagine, GScacchiera));
            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/cavalloNero.png", UriKind.Relative));
            pezzi.Add(new Cavallo(0, 6, "c", "n", immagine, GScacchiera));

            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/alfiereNero.png", UriKind.Relative));
            pezzi.Add(new Alfiere(0, 2, "a", "n", immagine, GScacchiera));
            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/alfiereNero.png", UriKind.Relative));
            pezzi.Add(new Alfiere(0, 5, "a", "n", immagine, GScacchiera));

            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/reNero.png", UriKind.Relative));
            pezzi.Add(new Re(0, 4, "R", "n", immagine, GScacchiera));

            immagine = new Image();
            immagine.Source = new BitmapImage(new Uri("/Immagini/reginaNera.png", UriKind.Relative));
            pezzi.Add(new Regina(0, 3, "r", "n", immagine, GScacchiera));

            foreach (Pezzo p in pezzi)
            {
                p.aggiornaMossePossibili();
            }
        }
    }
}
