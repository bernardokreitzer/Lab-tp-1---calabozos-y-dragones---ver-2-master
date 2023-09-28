using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_tp_1___calabozos_y_dragones
{
    public class Intermedio:Basico
    {
        protected ArrayList piezas = new ArrayList();

        public int CantidadDePiezas { get { return piezas.Count; } }

        public Intermedio(string nombre, int cantJugadores):base(nombre, cantJugadores) { }

        override protected void IniciarTablero(string nombre, int cantJugadores)
        {
            base.IniciarTablero(nombre, cantJugadores);

            foreach(Jugador jugador in jugadores)
            {
                Pieza dragon1 = new Dragon(jugador.Nombre);
                piezas.Add(dragon1);
                Pieza dragon2 = new Dragon(jugador.Nombre);
                piezas.Add(dragon2);
            }
        }
        override public void Jugar()
        {
            foreach (Jugador jugador in jugadores)
            {
                jugador.Avanzar();

                foreach (Pieza pieza in piezas)
                    pieza.Evaluar(jugador);
            }
            foreach(Dragon dragon in piezas)
                MoverDragon(dragon);
        }

        public void MoverDragon(Dragon dragon)
        {
            dragon.Posicion = Juego.rdm.Next(1, 51);
        }
        public Pieza VerPieza(int idx)
        {
            return (Pieza)piezas[idx];
        }
    }
}
