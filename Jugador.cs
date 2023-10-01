using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_tp_1___calabozos_y_dragones
{
    public class Jugador
    {
        static Random dado = new Random();

        private string nombre;
        public string Nombre
        {
            get
            {
                if (this.EnCalabozo)
                {
                    return nombre + " (está en el Calabozo)";
                }
                return nombre;
            }
            private set { nombre = value; }
        }

        private int posicion;
        public int Posicion 
        {
            get
            {
                return posicion;
            }
            set
            {
                if (value > 50)
                    posicion = 50;
                else if (value < 1)
                    posicion = 1;
                else posicion = value;
            }
        }
        public int PosicionAnterior { get; private set; }
        public int Avance { get; private set; }
        public bool Ganador
        {
            get
            {
                return Posicion == 50;
            }
        }
        private bool enCalabozo = false;
        public bool EnCalabozo { get { return enCalabozo; } set { enCalabozo = value; } }

        public Jugador(string nombre) 
        {
            Nombre = nombre;
            Posicion = 1;
        }

        public virtual void Avanzar()
        {
            Avance = dado.Next(1, 7);

            PosicionAnterior = Posicion;

            if (!EnCalabozo)
            {
                Posicion += Avance;
            }
        }
    }
}
