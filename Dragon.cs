using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_tp_1___calabozos_y_dragones
{
    public class Dragon : Pieza
    {
        //public string Nombre { get; private set; }
        public int Posicion { get; set; }
        public string Dueño { get; private set; }

        public Dragon(string dueño)
        {
            Dueño = dueño;
            Posicion = Juego.rdm.Next(2, 50);
        }
        public override void Evaluar(Jugador jugador)
        {
            if(jugador.Posicion ==  Posicion)
            {
                if(jugador.Nombre == Dueño)
                {
                    jugador.Posicion += 5;
                    System.Windows.Forms.MessageBox.Show($"{jugador.Nombre} se encontró con su dragón");
                }
                else
                {
                    jugador.Posicion -= 5;
                    System.Windows.Forms.MessageBox.Show($"{jugador.Nombre} se encontró el dragón de {Dueño}");
                }
            }
        }

        public override string VerDescripcion()
        {
            return $"Dragón de {Dueño} - Posición: {Posicion}";
        }

    }
}
