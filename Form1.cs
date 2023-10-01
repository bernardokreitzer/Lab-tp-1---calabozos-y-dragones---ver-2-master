using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;



namespace Lab_tp_1___calabozos_y_dragones
{
    public partial class Form1 : Form
    {
        Juego nuevo = new Juego();

        int caballeroVerdeX = 5;
        int caballeroVerdeY = 8;

        int caballeroX = 10;
        int caballeroY = 5;



        int dragonRojoY = 161;
        int dragonRojoX = 4;

        int dragonVerdeY = 205;
        int dragonVerdeX = 2;

        int cantidadMaxCaballeros = 4;
        int cantidadMaxDragones = 4;

        PictureBox[] caballeros = new PictureBox[4];
        PictureBox[] dragones = new PictureBox[4];
        PictureBox[] calabozos = new PictureBox[3];
        int cantidadJugadores;
        int cantidadDragones;
        SoundPlayer soundPlayer;

        public Form1()
        {
            InitializeComponent();

            

            // posicionar picturebox inicial

            // Calcula la nueva posición del PictureBox
            //int caballeroRojoX = 3;
            // int caballeroRojoY = 5;

            // Point [] caballeros = new Point[4];

            //dragones = new PictureBox[cantidadMaxCaballeros * 2];

            caballeros[0] = pbCaballeroRojo;
            caballeros[1] = pBCaballeroVerde;
            caballeros[2] = pbCaballeroAzul;
            caballeros[3] = pbCaballeroNegro;

            for (int i = 0; i < cantidadMaxCaballeros; i++ )
            {
                caballeros[i].Visible = false;
            }

            dragones[0] = pbDragonRojo1;
            dragones[1] = pbDragonRojo2;
            dragones[2] = pbDragonVerde1;
            dragones[3] = pbDragonVerde2;

            for (int i = 0; i < cantidadMaxDragones; i++)
            {
                dragones[i].Visible = false;
            }

            calabozos[0] = pbCalabozo1;
            calabozos[1] = pbCalabozo2;
            calabozos[2] = pbCalabozo3;

            for (int i = 0; i < 3; i++)
            {
                calabozos[i].Visible = false;
            }

            // Asigna un título a la ventana de la aplicación
            this.Text = "Calabozos y Dragones";


            // Establece la nueva posición
            caballeros[0].Location = new Point(caballeroX, caballeroY);
            pbCaballeroRojo.BackColor = Color.Transparent;
            pbDragonRojo1.Location = new Point(dragonRojoX, dragonRojoY);

            // Ocultar dragones 
            pbDragonRojo1.Visible = false;
            pbDragonRojo2.Visible = false;
            pbDragonVerde1.Visible = false;
            pbDragonVerde2.Visible = false;

            caballeros[1].Location = new Point(caballeroVerdeX, caballeroVerdeY);
            pBCaballeroVerde.BackColor = Color.Transparent;
            pbDragonVerde1.Location = new Point(dragonVerdeX, dragonVerdeY);

            // Tamaño de cada cuadrícula
            int gridSize = 50;
      
                    // Número de cuadrículas por fila y columna
            int numCols = 10;
            int numRows = 5;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    // Crear una nueva cuadrícula como un control Panel
                    Panel cuadricula = new Panel
                    {

                        Size = new Size(gridSize, gridSize),
                        Location = new Point(col * gridSize, row * gridSize),
                        BorderStyle = BorderStyle.FixedSingle, // Agregar un borde,
                    };

                    // Puedes personalizar la apariencia de cada cuadrícula aquí

                    // Agregar la cuadrícula al panel del tablero
                    panelTablero.Controls.Add(cuadricula);
                }
            } // fin Cuadricula
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FormDatos fDato = new FormDatos();

            if (fDato.ShowDialog() == DialogResult.OK)
            {
                lbResultados.Items.Clear();

                string jugador = fDato.tbNombre.Text;
                cantidadJugadores = Convert.ToInt32(fDato.nudCantidad.Value);
                int nivel = fDato.cbNivel.SelectedIndex + 1;

                nuevo.IniciarJuego(jugador, cantidadJugadores, nivel);

                for (int i = 0; i <= cantidadJugadores; i++)
                {
                    caballeros[i].Visible = true;
                }

                if (nuevo.Tablero is Intermedio)
                {
                    Intermedio tableroIntermedio = (Intermedio)nuevo.Tablero;

                    for (int n = 0; n < tableroIntermedio.CantidadJugadores*2; n++)
                    {
                        dragones[n].Visible = true;
                    }
                }

                if (nuevo.Tablero is Experto)
                {
                    Experto tableroExperto = (Experto)nuevo.Tablero;

                    for (int n = 0; n < tableroExperto.calabozos.Length; n++)
                    {
                        calabozos[n].Visible = true;
                    }
                }

                btnJugar.Enabled = true;
            }
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            if (nuevo.Tablero.Termino() == false)
            {
                if (nuevo.Tablero is Experto)
                {
                    Experto tableroExperto = (Experto)nuevo.Tablero;

                    for (int n = 0; n < tableroExperto.calabozos.Length; n++)
                    {
                        Calabozo calabozo = tableroExperto.calabozos[n];
                        lbResultados.Items.Add(calabozo.VerDescripcion());
                        lbResultados.SelectedIndex = lbResultados.Items.Count - 1;

                        int cel = calabozo.Posicion;
                        int Columnas = 10;
                        int ancho = 50;
                        int alto = 50;

                        int fila = (cel - 1) / Columnas;
                        int columna = (cel - 1) % Columnas;

                        caballeroX = ancho * columna;
                        caballeroY = alto * fila;

                        calabozos[n].Location = new Point(caballeroX, caballeroY);
                    }
                }

                if (nuevo.Tablero is Intermedio)
                {
                    Intermedio tableroIntermedio = (Intermedio)nuevo.Tablero;

                    for (int n = 0; n < tableroIntermedio.CantidadDePiezas; n++)
                    {
                        if(tableroIntermedio.VerPieza(n) is Dragon)
                        {
                            Dragon dragon = (Dragon)tableroIntermedio.VerPieza(n);
                            lbResultados.Items.Add(dragon.VerDescripcion());
                            lbResultados.SelectedIndex = lbResultados.Items.Count - 1;

                            int cel = dragon.Posicion;
                            int Columnas = 10;
                            int ancho = 50;
                            int alto = 50;

                            int fila = (cel - 1) / Columnas;
                            int columna = (cel - 1) % Columnas;

                            caballeroX = ancho * columna;
                            caballeroY = alto * fila;

                            dragones[n].Location = new Point(caballeroX, caballeroY);
                        }
                    }
                }

                nuevo.Tablero.Jugar();

                for (int n = 0; n < nuevo.Tablero.CantidadJugadores; n++)
                {
                    Jugador jugador = nuevo.Tablero.VerJugador(n);

                    //INICIO GRAFICOS

                    int cel = jugador.Posicion;
                    int Columnas = 10;
                    int ancho = 50;
                    int alto = 50;

                    int fila = (cel - 1) / Columnas;
                    int columna = (cel - 1) % Columnas;

                    caballeroX = ancho * columna;
                    caballeroY = alto * fila;

                    // object creation and path selection
                    SoundPlayer horses = new SoundPlayer(@"C:\Users\barni\Google Drive\UTN\Tecnicatura en Programacion\Cursado\Laboratorio II\Trabajo Practico 1\Lab-tp-1---calabozos-y-dragones---ver-2-master\resources\horse-fast-gallop - short2.wav");

                    // apply the method to play sound
                    horses.Play();

                    
                    // esta linea mueve a los jugadores, loopea el arreglo de picturebox, donde estan los caballeros
                    caballeros[n].Location = new Point(caballeroX , caballeroY);
                   

                    //FIN GRAFICOS

                    string linea = $">{jugador.Nombre} se movió desde la posición: {jugador.PosicionAnterior}" +
                                    $" a la posición {jugador.Posicion} ({jugador.Avance})";

                    lbResultados.Items.Add(linea);
                    lbResultados.SelectedIndex = lbResultados.Items.Count - 1;
                }
                lbResultados.Items.Add("------");
            }
            else
            {
                SoundPlayer winner = new SoundPlayer(@"C:\Users\barni\Google Drive\UTN\Tecnicatura en Programacion\Cursado\Laboratorio II\Trabajo Practico 1\Lab-tp-1---calabozos-y-dragones---ver-2-master\resources\Ta Da-.wav");

                winner.Play();
                MessageBox.Show("Finalizó!");

                for (int n = 0; n < nuevo.Tablero.CantidadJugadores; n++)
                {
                    Jugador jug = (Jugador)(nuevo.Tablero.VerJugador(n));
                    if (jug.Ganador)
                        nuevo.AgregarPartida(jug.Nombre);
                }

                btnJugar.Enabled = false;
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            FormHistorial fHistorial = new FormHistorial();

            foreach (Partida p in nuevo.ListarPartidas())
                fHistorial.lbHistorial.Items.Add($"{p.Ganador}  {p.Ganadas}");

            fHistorial.ShowDialog();

            fHistorial.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (nuevo.Tablero.Termino() == false)
            {
                btnJugar.PerformClick();
            }        
        }
    }
}

