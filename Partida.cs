using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_tp_1___calabozos_y_dragones
{
    public class Partida
    {
        public string Ganador { get; set; }
        public int Ganadas { get; set; }

        public Partida(string Ganador, int Ganadas)
        {
            this.Ganador = Ganador;
            this.Ganadas = Ganadas;
        }
    }
}
