using PCheckers.Classes;
using System;
using System.Media;
using System.Drawing;
using System.Windows.Forms;

namespace PCheckers
{
    public partial class PCheckers : Form
    {
        SoundPlayer spBgMusic;

        int BLACKS = 12; //Tiene il conto delle nere in campo
        int WHITES = 12;//Tiene il conto delle bianche in campo
        int lastSelected = -1; //Tiene in memoria l'ultima pedina selezionata

        bool eat = false;

        Pedina[] pieces;    //Array dove sono tutte le pedine
        PictureBox[] possibleMoves; //Picturebox che appariranno nelle posizioni possibili

        Color turn = Color.White; //Il turno di chi
        //Immagini delle pedine
        #region Images ===========================================================================
        //p = Pedina d = Dama
        Bitmap pD;
        Bitmap pW;
        Bitmap dB;
        Bitmap dW;
        #endregion ===========================================================================
        public PCheckers()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creo il contorno alla scacchiera con un rettangolo disegnato agli estremi
        /// </summary>
        private void DamaGame_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(//Disegna un rettangolo
                new Pen(Color.Fuchsia, 10),//Di colore AlphaRGB e con spessore 10 
                new Rectangle(new Point(picChessBoard.Location.X, picChessBoard.Location.Y),picChessBoard.Size));//Questo rettangolo
        }

        private void DamaGame_Load(object sender, EventArgs e)
        {
            LoadGame();
            
        }

        #region Loaders===========================================================================

        /// <summary>
        /// Chiama le funzione necessarie al primo avvio del gioco per potersi caricare
        /// </summary>
        private void LoadGame()
        {
            LoadImages();//Carica le immagini
            LoadSounds();
            CreateArrays();//Istanzia gli array
            FillArrays();//Riempie gli array
            PutPiecesOnForm(); //Aggiunge le padine alla form
            DisposePiecesOnBoard();//Posiziona le pedine sulla tavola
            spBgMusic.PlayLooping();
            picChessBoard.SendToBack(); //Spinge indietro la tavola così non c'è rischio che copra le pedine
            timWalk.Start();
        }

        private void LoadSounds()
        {
            spBgMusic = new SoundPlayer(@"assets\Checker_Dance_Deltarune.wav");
        }

        /// <summary>
        /// Chiama le funzioni addette a riempire i rispettivi array
        /// </summary>
        private void FillArrays()
        {
            CreatePieces();//Crea le pedine
            CreatePossibleMoves();//Crea le mosse possibili
        }
        /// <summary>
        /// Istanzia le picturebox che mostreranno le possibili mosse di una pedina
        /// </summary>
        private void CreatePossibleMoves()
        {
            for (int i = 0; i < possibleMoves.Length; i++)
            {
                possibleMoves[i] = new PictureBox
                {
                    BackColor = Color.FromArgb(200, 50, 255, 0),
                    Size = new Size(picChessBoard.Width / 8, picChessBoard.Height / 8)
                };
                possibleMoves[i].Hide();
                possibleMoves[i].Click += PossibleMoves_Click;
                this.Controls.Add(possibleMoves[i]);
            }
        }
        /// <summary>
        /// Istanzia gli array delle pedine e mosse possibili
        /// </summary>
        private void CreateArrays()
        {
            pieces = new Pedina[24];
            possibleMoves = new PictureBox[4];
        }
        /// <summary>
        /// Carica le immagini delle dame e pedine
        /// </summary>
        private void LoadImages()
        {
            this.BackgroundImage = new Bitmap(@"assets\deltarune_battle_bg.jpg");
            picChessBoard.BackgroundImage = new Bitmap(@"assets\scacchiera.png");
            picKing.ImageLocation = @"assets\checker2.gif";
            pD = new Bitmap(@"assets\pedina_nera.png");//Pedina nera
            pW = new Bitmap(@"assets\pedina_bianca.png");//Pedina Bianca
            dB = new Bitmap(@"assets\checkerBlack.png"); //new Bitmap(@"assets\dama_nera.png"); //Dama Nera
            dW = new Bitmap(@"assets\checkerWhite.png"); //new Bitmap(@"assets\dama_bianca.png");//Dama Bianca
        }
        /// <summary>
        /// Aggiunge alla form le pedine e le porta in avanti
        /// </summary>
        private void PutPiecesOnForm()
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                this.Controls.Add(pieces[i].Body);
                pieces[i].Body.BringToFront();
            }
        }

        /// <summary>
        /// Posiziona sulla tavola le pedine
        /// </summary>
        private void DisposePiecesOnBoard()
        {
            TopRow();//Posiziona le pedine della fila sopra
            BotRow();//Posiziona le pedine della fila sotto
        }

        private void TopRow()
        {
            int xoff = 0; //Offset di quanto sarà spostata una pedina(diventa 1 nel caso inizi una casella dopo)
            int counter = 0; //Da dove iniziare a contare dall'array

            for (int i = 0; i < 3; i++)//Per il numero di righe da completare
            {
                for (int j = 0; j < 4; j++)//Posiziono 4 pedine
                {
                    /*
                      A cui do il nome della poszione di dove si trovano formato "ColonnaRiga" (XY)
                      ('A' + (j * 2 + xoff)):
                        partendo da A che la posizione base, aggiungo la colonna (j),
                        moltiplicata per 2 perchè le pedine saranno a 2 caselle di distanza,
                        e aggiungo l'offset.
                     */
                    pieces[counter].Body.Name = $"{(char)('A' + (j * 2 + xoff))}{i}";
                    pieces[counter].PieceColor = Color.Black;//Poi so che la colonna di sopra ha le pedine nere
                    pieces[counter].Body.Image = pD; //Gli assegno l'immagine della pedina nera
                    /*
                     E calcolo la posizone che sarà:
                     X: origine + (larghezzaCasella * (colonna * 2))       + offset, nel caso della seconda riga sfalsata
                                                       1 casella di spazio
                     */
                    pieces[counter].Body.Location = new Point(
                        picChessBoard.Location.X + pieces[counter].Body.Size.Width * (j * 2 + xoff),
                        picChessBoard.Location.Y + pieces[counter].Body.Size.Height * i
                        );
                    counter++;//Vado alla prossima pedina
                }
                //Quando ho finito una riga,vedo se ho messo una fila che inizia da 0 o 1 casella in la
                if (i % 2 == 0)
                    xoff++;
                else
                    xoff--;
            }
        }

        private void BotRow()
        {
            int xoff = 1;//La riga un basso inizia spostata di una casella
            int yoff = 5; //Le pedine inizieranno ad essere dalla 5 riga
            int counter = 12;//Devo iniziare a contare dalla 12esima pedina nell'array

            for (int i = 0; i < 3; i++)//Per ogni riga
            {
                for (int j = 0; j < 4; j++)//Posiziono 4 pedine
                {
                    //Stesso succo di prima, ma stavolta devo aggiungere l'offset anche alla riga perchè inizio dalla 5
                    pieces[counter].Body.Name = $"{(char)('A' + (j * 2 + xoff))}{i + yoff}";
                    pieces[counter].PieceColor = Color.White;
                    pieces[counter].Body.Image = pW;
                    pieces[counter].Body.Location = new Point(
                        picChessBoard.Location.X + pieces[counter].Body.Size.Width * (j * 2 + xoff),
                        picChessBoard.Location.Y + pieces[counter].Body.Size.Height * (i + yoff)
                        );
                    counter++;
                }

                if (i % 2 == 1)
                    xoff++;
                else
                    xoff--;
            }
        }

        #endregion ===========================================================================

        
        /// <summary>
        /// Mangia la pedina con coordinate where
        /// </summary>
        /// <param name="where">Le coordinate della pedina da mangiare</param>
        void Eat(string where)
        {
            //Prendo l'indice della pedina da mangiare
            int pos = Pedina.TrovaPedina(where, pieces);

            //Controllo il colore della pedina mangiata
            if (pieces[pos].PieceColor == Color.White)
            {
                WHITES--;
                lblW.Text = $"Bianchi in campo: {WHITES}";
            }
            else
            {
                BLACKS--;
                lblB.Text = $"Neri in campo: {BLACKS}";
            }

            //La pedina si mangia da sola
            pieces[pos].Eaten();
            eat = true;

            //Visto che la pedina ha mangiato, si deve muovere due volte,mantenendo la stessa direzione
            int dirX = where[0] - pieces[lastSelected].Body.Name[0];
            int dirY = where[1] - pieces[lastSelected].Body.Name[1];
            //La muovo due volte
            pieces[lastSelected].Move(where, picChessBoard.Location);
            pieces[lastSelected].Move($"{(Char)(where[0] + dirX)}{(Char)(where[1] + dirY)}",
                picChessBoard.Location);
        }
        /// <summary>
        /// Passa al prossimo turno
        /// </summary>
        private void NextTurn()
        {
            turn = (turn == Color.White ? Color.Black : Color.White);
            lblTurn.Text = $"Turno Dei{Environment.NewLine}{(turn == Color.White ? "Bianchi" : "Neri")}";
            eat = false;
            if (lastSelected != -1)
            {
                pieces[lastSelected].Selected = false;
                HidePossibleMoves();
                lastSelected = -1;
            }
        }
        /// <summary>
        /// Mostro il messaggio di vittoria
        /// </summary>
        private void EndGame()
        {
            if (BLACKS == 0)
                MessageBox.Show("WHITES WIN!");
            if (WHITES == 0)
                MessageBox.Show("BLACKS WIN!");
        }
        /// <summary>
        /// Nasconde le picturebox delle mosse possibili
        /// </summary>
        private void HidePossibleMoves()
        {
            for (int i = 0; i < possibleMoves.Length; i++)
            {
                possibleMoves[i].Hide();
            }
        }
        /// <summary>
        /// Istanzia le pedine
        /// </summary>
        private void CreatePieces()
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = new Pedina() {
                    Body = new PictureBox {
                        BackColor = Color.Black,
                        Size = new Size(picChessBoard.Size.Width / 8, picChessBoard.Size.Height / 8),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Location = new Point(-100,-100)
                    },
                    
                };
                pieces[i].Body.Click += Pedina_Click;
            }
        }
        
        /// <summary>
        /// Crea le picturebox che andranno a possizionarsi dove la dama può spostarse
        /// </summary>
        private void ShowPossibleMoves()
        {
            //Calcolo le posizioni
            CalcPossiblePositions();
            //Controllo che le posizioni calcolate siano accessibili
            ShowPossibles();
        }
        /// <summary>
        /// Dopo aver controllato che la posizione sia libera o si possa mangiare, mostro la posizione possible
        /// </summary>
        private void ShowPossibles()
        {
            for (int i = 0; i < possibleMoves.Length; i++)
            {
                ACTION action = pieces[lastSelected].CanMoveOrEat(possibleMoves[i].Name, pieces);
                if ( action != ACTION.DENY)//Se in quella posizione è permesso muoversi
                {
                    possibleMoves[i].Name = Pedina.GetCoordsFromLocation(possibleMoves[i].Location, possibleMoves[i].Size, picChessBoard.Location);
                    possibleMoves[i].BringToFront();
                    possibleMoves[i].BackColor = (action == ACTION.MOVE ? Color.FromArgb(200, 50, 255, 0) : Color.DarkRed);
                    possibleMoves[i].Show();
                }
                else
                    possibleMoves[i].Hide();
            }
        }
        /// <summary>
        /// Calcolo la posizione in avnti-destra avanti-sinistra dietro sinistra-dietro-destra
        /// </summary>
        private void CalcPossiblePositions()
        {
            Point location = Pedina.GetLocationFromCoords(pieces[lastSelected].Body.Name);
            possibleMoves[0].Location = new Point(
                picChessBoard.Location.X + (location.X - 1) * (possibleMoves[0].Size.Height),
                picChessBoard.Location.Y + (location.Y - 1) * (possibleMoves[0].Size.Height)
                );
            possibleMoves[0].Name = Pedina.GetCoordsFromLocation(possibleMoves[0].Location, possibleMoves[0].Size, picChessBoard.Location);
            possibleMoves[1].Location = new Point(
                picChessBoard.Location.X + (location.X + 1) * (possibleMoves[1].Width),
                picChessBoard.Location.Y + (location.Y - 1) * (possibleMoves[1].Height)
                );
            possibleMoves[1].Name = Pedina.GetCoordsFromLocation(possibleMoves[1].Location, possibleMoves[1].Size, picChessBoard.Location);
            possibleMoves[2].Location = new Point(
                picChessBoard.Location.X + (location.X + 1) * (possibleMoves[2].Width),
                picChessBoard.Location.Y + (location.Y + 1) * (possibleMoves[2].Height)
                );
            possibleMoves[2].Name = Pedina.GetCoordsFromLocation(possibleMoves[2].Location, possibleMoves[2].Size, picChessBoard.Location);
            possibleMoves[3].Location = new Point(
                picChessBoard.Location.X + (location.X - 1) * (possibleMoves[3].Width),
                picChessBoard.Location.Y + (location.Y + 1) * (possibleMoves[3].Height)
                );
            possibleMoves[3].Name = Pedina.GetCoordsFromLocation(possibleMoves[3].Location, possibleMoves[3].Size, picChessBoard.Location);
        }

        /// <summary>
        /// Resetta il gioco
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            DisposePiecesOnBoard();//Rimette le pedine
            ShowPieces();//Le mostra
            WHITES = 12;//Resetta i punteggi
            BLACKS = 12;
            lblB.Text = $"Neri in campo: {BLACKS}";
            lblW.Text = $"Bianchi in campo: {WHITES}";
            if (this.turn == Color.Black)
                NextTurn();
        }

        /// <summary>
        /// Mostra tutte le pedine
        /// </summary>
        private void ShowPieces()
        {
            foreach (var item in pieces)
            {
                item.Body.Show();
            }
        }
        #region Events =====================================================================
        private void btnSkip_Click(object sender, EventArgs e)
        {
            NextTurn();
        }

        /// <summary>
        /// Azioni da fare quano una pedina viene cliccata
        /// </summary>
        private void Pedina_Click(object sender, EventArgs e)
        {
            //Prendo la pedina cliccata
            PictureBox temp = sender as PictureBox;
            //La trovo nell'array
            int last = Pedina.TrovaPedina(temp.Name, pieces);
            if (eat)
                if (last != lastSelected)
                    return; 

            if (pieces[last].PieceColor == turn)//Se il colore della pedina corrisponde a quello del turno
            {
                if (lastSelected != -1)//Deseleziono l'ultima pedina se c'è
                    pieces[lastSelected].Selected = false;

                lastSelected = last;//Memorizzo la posizione della pedina
                pieces[lastSelected].Selected = true;//Seleziono la pedina
                ShowPossibleMoves();//Mostro le sue possibili mosse
            }
        }
       
        /// <summary>
        /// Elabora l'azione da fare quando viene selezionata una mossa possibile
        /// </summary>
        private void PossibleMoves_Click(object sender, EventArgs e)
        {
            PictureBox temp = sender as PictureBox; //Leggo la picturebox che è stata cliccata
            ACTION action = pieces[lastSelected].CanMoveOrEat(temp.Name, pieces); //Vedo che azione posso fare in quella posizione
            //Se la dama può fare qualcosa
            if (action != ACTION.DENY)
            {
                int last = lastSelected;
                if (action == ACTION.MOVE)
                {
                    pieces[lastSelected].Move(temp.Name, picChessBoard.Location);
                    NextTurn();
                }
                else if (action == ACTION.EAT)
                {
                    Eat(temp.Name);
                }

                //'Promuovo' la pedina (dentro controllo se può essere promossa)
                pieces[last].Promote((pieces[last].PieceColor == Color.White ? dW : dB));
                pieces[last].Selected = false;

                HidePossibleMoves();

                //Controllo se la partita sia finita
                if (WHITES == 0 || BLACKS == 0)
                    EndGame();
            }
        }
        #endregion =====================================================================

        private void rdbBgMusic_Click(object sender, EventArgs e)
        {

            rdbBgMusic.Checked = !rdbBgMusic.Checked;

            if (rdbBgMusic.Checked)
                spBgMusic.PlayLooping();
            else
                spBgMusic.Stop();
        }

        int vel = 10;

        private void timWalk_Tick(object sender, EventArgs e)
        {
            if (picKing.Location.X + picKing.Width >= picChessBoard.Location.X - 10 ||
                picKing.Location.X <= 0)
            {
                vel *= -1;
                picKing.ImageLocation = (vel < 0 ? @"assets\checker1.gif" : @"assets\checker2.gif");
                GC.Collect();//Stabilizza l'uso ram sennò aumenta a perchè il GC non elimina subito le immagini dalla ram
            }

            picKing.Left += vel;
            
        }
    }
}
