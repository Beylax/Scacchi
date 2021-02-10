using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Schacchi_v1._1
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Pezzo[,] pezzi = new Pezzo[8, 8];
        public static DispatcherTimer timerB;
        public static DispatcherTimer timerN;
    }
}