using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_tp_1___calabozos_y_dragones
{
    public class Experto : Intermedio
    {
        public Calabozo[] calabozos = new Calabozo[3];
        public Experto(string nombre, int cantJugadores) : base(nombre, cantJugadores) { }

        override protected void IniciarTablero(string nombre, int cantJugadores)
        {
            base.IniciarTablero(nombre, cantJugadores);

            for(int i = 0; i < 3; i++)
            {
                calabozos[i] = new Calabozo();
            }
        }
    }
}
