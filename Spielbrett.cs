using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    public class Spielbrett
    {
        public int breite;
        public int hoehe;
        public int anzahlMinen;
        public int gesetzteFlaggen = 0;
        public Spielfeld[,] spielfelder;
        public bool istGewonnen;
        public bool istVerloren;

        public Spielbrett(int breite, int hoehe, int anzahlMinen)
        {
            this.breite = breite;
            this.hoehe = hoehe;
            this.anzahlMinen = anzahlMinen;
            spielfelder = new Spielfeld[breite, hoehe];
            istGewonnen = false;
            istVerloren = false;
            InitialisiereSpielbrett();
        }

        private void InitialisiereSpielbrett()
        {
            for (int x = 0; x < breite; x++)
            {
                for (int y = 0; y < hoehe; y++)
                {
                    spielfelder[x, y] = new Spielfeld();
                }
            }

            int anzahlGesetzterMinen = 0;
            Random random = new Random();
            while (anzahlGesetzterMinen < anzahlMinen)
            {
                int x = random.Next(breite);
                int y = random.Next(hoehe);

                if (!spielfelder[x, y].HatMine())
                {
                    spielfelder[x, y].SetzeMine();
                    anzahlGesetzterMinen++;
                }
            }


            for (int x = 0; x < breite; x++)
            {
                for (int y = 0; y < hoehe; y++)
                {
                    if (!spielfelder[x, y].HatMine())
                    {
                        int anzahlMinenInNachbarschaft = 0;

                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                int nx = x + dx;
                                int ny = y + dy;

                                if (nx >= 0 && nx < breite && ny >= 0 && ny < hoehe && spielfelder[nx, ny].HatMine())
                                {
                                    anzahlMinenInNachbarschaft++;
                                }
                            }
                        }

                        spielfelder[x, y].Wert = anzahlMinenInNachbarschaft;
                    }
                }
            }
        }

        public void OeffneFeld(int x, int y)
        {
            if (spielfelder[x, y].IstOffen() || spielfelder[x, y].IstMarkiert())
            {
                return;
            }

            spielfelder[x, y].Oeffne();

            if (spielfelder[x, y].HatMine())
            {
                istVerloren = true;
              
            }
            else if (spielfelder[x, y].Wert == 0)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int nx = x + dx;
                        int ny = y + dy;

                        if (nx >= 0 && nx < breite && ny >= 0 && ny < hoehe)
                        {
                            OeffneFeld(nx, ny);
                        }
                    }
                }
            }

            if (HatGewonnen())
            {
                istGewonnen = true;
            }
        }

        public bool HatGewonnen()
        {
            return false;
        }

        


    }
}
