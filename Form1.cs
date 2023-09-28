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

        int caballeroRojoX = 10;
        int caballeroRojoY = 5;



        int dragonRojoY = 161;
        int dragonRojoX = 4;

        int dragonVerdeY = 205;
        int dragonVerdeX = 2;

        int cantidadMaxCaballeros = 4;

        PictureBox[] caballeros = new PictureBox[4];
        PictureBox[] dragones;
        int cantidadJugadores;
        SoundPlayer soundPlayer;

        public Form1()
        {
            InitializeComponent();

            

            // posicionar picturebox inicial

            // Calcula la nueva posición del PictureBox
            //int caballeroRojoX = 3;
            // int caballeroRojoY = 5;

            // Point [] caballeros = new Point[4];

            dragones = new PictureBox[cantidadMaxCaballeros * 2];

            caballeros[0] = pbCaballeroRojo;
            caballeros[1] = pBCaballeroVerde;
            caballeros[2] = pbCaballeroAzul;
            caballeros[3] = pbCaballeroNegro;

            for (int i = 0; i < cantidadMaxCaballeros; i++ )
            {
                caballeros[i].Visible = false;
            }

            // Asigna un título a la ventana de la aplicación
            this.Text = "Calabozos y Dragones";


            // Establece la nueva posición
            caballeros[0].Location = new Point(caballeroRojoX, caballeroRojoY);
            pbCaballeroRojo.BackColor = Color.Transparent;
            pbDragonRojo.Location = new Point(dragonRojoX, dragonRojoY);

            // Ocultar dragones 
            pbDragonRojo.Visible = false;
            pbDragonRojo2.Visible = false;
            pbDragonVerde.Visible = false;
            pbDragonVerde2.Visible = false;

            caballeros[1].Location = new Point(caballeroVerdeX, caballeroVerdeY);
            pBCaballeroVerde.BackColor = Color.Transparent;
            pbDragonVerde.Location = new Point(dragonVerdeX, dragonVerdeY);

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


        private void btnAvanzar_Click(object sender, EventArgs e)
        {
            // Este metodo mueve al caballero horizontalmente, sumandole 50 a nuevaX

            // Calcula la nueva posición del PictureBox
            caballeroRojoX = pbCaballeroRojo.Left + (50);
            caballeroRojoY = pbCaballeroRojo.Top;

            dragonRojoX = caballeroRojoX;

            if (caballeroRojoX >= 450) caballeroRojoX = 450;

            // Establece la nueva posición
            pbCaballeroRojo.Location = new Point(caballeroRojoX, caballeroRojoY);
            pbDragonRojo.Location = new Point(dragonRojoX, dragonRojoY);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Este metodo mueve al caballero horizontalmente, sumandole 50 a nuevaX

            // Calcula la nueva posición del PictureBox
            caballeroVerdeX = pBCaballeroVerde.Left + (50);
            dragonVerdeX = caballeroVerdeX;


            if (caballeroVerdeX >= 450) caballeroVerdeX = 450;

            // Establece la nueva posición
            pBCaballeroVerde.Location = new Point(caballeroVerdeX, caballeroVerdeY);
            pbDragonVerde.Location = new Point(dragonVerdeX, dragonVerdeY);

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

                for (int i = 0; i < cantidadJugadores + 1; i++)
                {
                    caballeros[i].Visible = true;
                }
                nuevo.IniciarJuego(jugador, cantidadJugadores, nivel);

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

                    caballeroRojoX = ancho * columna;
                    caballeroRojoY = alto * fila;

                    // object creation and path selection
                    SoundPlayer horses = new SoundPlayer(@"C:\Users\barni\Google Drive\UTN\Tecnicatura en Programacion\Cursado\Laboratorio II\Trabajo Practico 1\Lab-tp-1---calabozos-y-dragones---ver-2-master\resources\horse-fast-gallop - short2.wav");

                    // apply the method to play sound
                    horses.Play();

                    //Thread.Sleep(500);
                    // esta linea mueve a los jugadores, loopea el arreglo de picturebox, donde estan los caballeros
                    caballeros[n].Location = new Point(caballeroRojoX , caballeroRojoY);
                   // SystemSounds.Beep.Play(); 
                    //SystemSounds.Asterisk.Play();
                   

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
    }
}

