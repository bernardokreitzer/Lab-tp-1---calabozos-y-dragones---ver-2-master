using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Lab_tp_1___calabozos_y_dragones
{
    public class Juego
    {
        public static Random rdm = new Random();

        Basico tablero;
        public Basico Tablero {get { return tablero;}}

        public ArrayList partidas = new ArrayList();

        public Juego()
        {

        }

        public void IniciarJuego(string jugador, int cantJugadores, int nivel)
        {
            switch(nivel)
            {
                case 1:
                    tablero = new Basico(jugador, cantJugadores);
                    break;

                case 2:
                    tablero = new Intermedio(jugador, cantJugadores);
                    break; 
                
                case 3:
                    tablero = new Experto(jugador, cantJugadores);
                    break;
            }

        }

        public void AgregarPartida(string nombre)
        {
            #region buscar el registro primero
            Partida buscado = null;
            for (int n = 0; n < partidas.Count && buscado == null; n++)
            {
                Partida p = (Partida)partidas[n];
                if (p.Ganador.Trim().ToUpper() == nombre.Trim().ToUpper())
                    buscado = p;
            }
            #endregion

            #region lo actualizo si lo encuentro sino lo agrego 
            if (buscado != null)
                buscado.Ganadas++;
            else
                partidas.Add(new Partida(nombre, 1));
            #endregion
        }

        public ArrayList ListarPartidas()
        {
            for (int n = 0; n < partidas.Count - 1; n++)
            {
                for (int m = n + 1; m < partidas.Count; m++)
                {
                    Partida p = (Partida)partidas[n];
                    Partida q = (Partida)partidas[m];

                    if (p.Ganadas < q.Ganadas)
                    {
                        object aux = partidas[n];
                        partidas[n] = partidas[m];
                        partidas[m] = aux;
                    }
                }
            }
            return partidas;
        }

    }
}
