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

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Spielbrett spielbrett;

        private DispatcherTimer _timer;
        private int _seconds = 0;


        public MainWindow()
        {
            InitializeComponent();
          
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Point point = (Point)button.Tag;

            // Öffne das Spielfeld
            int x = (int)point.X;
            int y = (int)point.Y;
            spielbrett.OeffneFeld(x, y);

            for (int i = 0; i < spielbrett.breite; i++)
            {
                for (int j = 0; j < spielbrett.hoehe; j++)
                {
                    Spielfeld spielfeld = spielbrett.spielfelder[i, j];

                    button.Content = spielfeld.Wert;
                    button.IsEnabled = !spielfeld.IstOffen();
                }
            }

            // Überprüfe, ob das Spiel gewonnen wurde
            if (spielbrett.istGewonnen)
            {
                MessageBox.Show("Glückwunsch, du hast das Spiel gewonnen!");
            }

            if (spielbrett.istVerloren)
            {
                MessageBox.Show("Sie haben verloren");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _seconds++;
            TimerLabel.Content = "Timer: " + _seconds.ToString();
        }

        private void NeuesSpielButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int breite = int.Parse(BreiteTextBox.Text);
            int hoehe = int.Parse(HoeheTextBox.Text);

            spielbrett = new Spielbrett(breite, hoehe , 3);
            FlagsAvailable.Content = "Flags: "+ (spielbrett.anzahlMinen - spielbrett.gesetzteFlaggen).ToString();

            List<Spielfeld> spielfeldList = new List<Spielfeld>();
            for (int y = 0; y < spielbrett.hoehe; y++)
            {
                for (int x = 0; x < spielbrett.breite; x++)
                {
                    spielfeldList.Add(spielbrett.spielfelder[x, y]);
                }
            }

            spielfeldPanel.ItemsSource = spielfeldList;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            button.IsEnabled = false;
            

        }
    }
}