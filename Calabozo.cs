using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_tp_1___calabozos_y_dragones
{
    public class Calabozo : Pieza
    {
        public int Posicion { get; private set; }
        
        public Calabozo()
        {
            Posicion = Juego.rdm.Next(2, 50);
        }

        public override void Evaluar(Jugador jugador)
        {
            if (jugador.Posicion == Posicion)
            {
                if(!jugador.EnCalabozo)
                    jugador.EnCalabozo = true;
                else
                    jugador.EnCalabozo = false;
            }
        }

        public override string VerDescripcion()
        {
            return $"Calabozo en {Posicion}";
        }
    }
}
