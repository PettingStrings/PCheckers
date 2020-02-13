using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PCheckers.Classes
{
    public enum PIECE_TYPE { NORMALE, DAMA };//Dice il tipo di pedina
    public enum ACTION { EAT, MOVE, DENY };//Elenco azioni che può fare una pedina
    public partial class Pedina
    {
        #region Variables =====================================================================

        private PictureBox body; //La picturebox che mostra la pedina
        private Color pieceColor; //Il colore a cui appartene la pedina
        private bool selected; //True se la pedina è selezionata
        private PIECE_TYPE tipo; //Tipo di pedina

        #endregion =====================================================================

        #region Properties =====================================================================

        public PictureBox Body { get => body; set => body = value; }
        public Color PieceColor
        {
            get => pieceColor;
            set
            {
                if (value == Color.White || value == Color.Black)
                    pieceColor = value;
            }
        }

        public bool Selected
        {
            get => selected; set
            {
                selected = value;
                if (this.selected)
                {
                    Select();
                }
                else
                {
                    Unselect();
                }
            }
        }

        public PIECE_TYPE Tipo
        {
            get => tipo; set
            {

                if (value == PIECE_TYPE.DAMA || value == PIECE_TYPE.NORMALE)
                    tipo = value;
            }
        }

        #endregion =====================================================================

        #region Methods =====================================================================

        public Pedina()
        {
            body = new PictureBox();
            this.Selected = false;
        }

        private void Unselect()
        {
            this.body.BackColor = Color.Black;
        }

        private void Select()
        {
            this.body.BackColor = Color.Yellow;
        }
        /// <summary>
        /// Semplicemente muove la pedina nelle coordinate date
        /// </summary>
        /// <param name="coords">Dove muoversi</param>
        /// <param name="origin">Punto di origine della scacchiera</param>
        public void Move(string coords, Point origin)
        {
            Point newLocation = Pedina.GetLocationFromCoords(coords);
            this.Body.Name = coords;
            this.Body.Location = new Point(
                origin.X + this.Body.Size.Width * newLocation.X,
                origin.Y + this.Body.Size.Height * newLocation.Y
                );
        }
        /// <summary>
        /// Dice l'azione che può fare la pedina nella posizione data
        /// </summary>
        /// <param name="destination">Posizione dove può muoversi</param>
        /// <param name="pedine">Array delle pedine da prendere come riferimento</param>
        /// <returns></returns>
        public ACTION CanMoveOrEat(string destination, Pedina[] pedine)
        {
            //Se le coordinate non sono valide(fuori dal sistema di riferimento o non ha i permessi per muoversi)
            if (!ValidateCoords(destination))
                return ACTION.DENY;
            else
                return EmptySpaceOrEat(destination, pedine);
        }

        /// <summary>
        /// Vede se nella posizione data c'è una pedina da mangiare o lo spazio è vuoto
        /// </summary>
        /// <param name="destination">Dove muoversi</param>
        /// <param name="pedine">Array di pedine</param>
        private ACTION EmptySpaceOrEat(string destination, Pedina[] pedine)
        {
            int i = 0;
            while (i < pedine.Length)
            {
                if (pedine[i].body.Name == destination)
                {
                    if (pedine[i].pieceColor != this.pieceColor)
                    {

                        if (this.tipo != pedine[i].tipo)
                            if (this.tipo == PIECE_TYPE.NORMALE)
                                return ACTION.DENY;

                        return pedine[i].Eatable(
                            new Point((int)(destination[0] - this.body.Name[0]), (int)(destination[1] - this.body.Name[1])),
                            pedine);
                    }
                    else
                        return ACTION.DENY;
                }
                i++;
            }
            return ACTION.MOVE;
        }
        /// <summary>
        /// Dice se la pedina può essere mangiata,
        /// quindi controlla se lo spazio è libero e dentro la tavola
        /// </summary>
        /// <param name="dirs">Tiene in mente la direzione</param>
        /// <param name="pedine"></param>
        /// <returns></returns>
        private ACTION Eatable(Point dirs,Pedina []pedine)
        {
            if (dirs.X != -1 && dirs.X != 1)
                return ACTION.DENY;

            string dest = $"{(char)(this.body.Name[0] + dirs.X)}{(char)(this.body.Name[1] + dirs.Y)}";

            if (!Pedina.DoCoordExixst(dest))
                return ACTION.DENY;

            for (int i = 0; i < pedine.Length; i++)
            {
                if (pedine[i].body.Name == dest)
                    return ACTION.DENY;
            }
            return ACTION.EAT;
        }
        /// <summary>
        /// Promuove la pedina a dama, se ha raggiunto la fine della tavola
        /// </summary>
        /// <param name="img"></param>
        public void Promote(Bitmap img)
        {
            if (this.body.Name[1] == '0' || this.body.Name[1] == '7')
            {
                this.tipo = PIECE_TYPE.DAMA;
                this.body.Image = img;
            }
        }
        
        /// <summary>
        /// Vede se le coordinate siano valide(non fuori dalla tavola) e 
        /// la pedina può muoversi
        /// </summary>
        /// <param name="coords">dove muoversi</param>
        /// <returns></returns>
        public bool ValidateCoords(string coords)
        {

            if (!((coords[0]) == (this.Body.Name[0] - 1)||
                (coords[0]) == (this.Body.Name[0] + 1)) || 
                !Pedina.DoCoordExixst(coords))
                return false;

            int diff = coords[1] - this.Body.Name[1];
            
            if (diff == -1 || diff == 1)
            {
                if (this.Tipo == PIECE_TYPE.DAMA)
                    return true;
                if (this.PieceColor == Color.White && diff == -1)
                    return true;
                if (this.PieceColor == Color.Black && diff == 1)
                    return true;
            }

            return false;
        }

        public bool Eat(ref Pedina pedina)
        {
            if (this.Body.Name == pedina.Body.Name && pedina.PieceColor != this.PieceColor)
            {
                pedina.Eaten();
                return true;
            }

            return false;
        }

        public void Eaten()
        {
            this.body.Name = "iten";
            this.body.Location = new Point(-1, -1);
            this.body.Hide();
        }

        static public Point GetLocationFromCoords(string coord)
        {
            int x, y;

            if (!int.TryParse((coord[0] - 'A').ToString(), out x) || !int.TryParse((coord[1]).ToString(), out y))
                return new Point(-1, -1);

            if (x < 0 || x > 7 || y < 0 || y > 7)
                return new Point(-1, -1);

            return new Point(x,y);
        }

        static public string GetCoordsFromLocation(Point location, Size size, Point origin)
        {
            return $"{(char)('A' + ((location.X - origin.X) / size.Width))}{(location.Y - origin.Y) / size.Width}";
        }
        static public bool ValidateCoords(string start,string end,PIECE_TYPE type,Color color)
        {
            if (end.Length != 2 || start.Length != 2 || type != PIECE_TYPE.DAMA || type != PIECE_TYPE.NORMALE || 
                (color != Color.White && color != Color.Black))
                return false;
            if (
                !((end[0]) == (start[0] - 1)
                || (end[0]) == (start[0] + 1)) ||
                end[0] < 'A' || end[0] > ('A' + 7)
                )
                return false;

            if (end[1] < '0' || end[1] > '7')
                return false;

            int diff = end[1] - start[1];

            if (diff == -1 || diff == 1)
            {
                if (type== PIECE_TYPE.DAMA)
                    return true;
                if (color == Color.White && diff == -1)
                    return true;
                if (color == Color.Black && diff == 1)
                    return true;
            }

            return false;
        }

        static public int TrovaPedina(string Name, Pedina[] here)
        {
            int i = 0;
            while (i < here.Length)
            {
                if (Name == here[i].body.Name)
                    return i;
                i++;
            }
            return -1;
        }

        public static bool DoCoordExixst(string coords)
        {
            if (coords.Length != 2)
                return false;
            if (coords[1] < '0' || coords[1] > '7')
                return false;

            if (coords[0] < 'A' || coords[0] > ('A' + 7))
                return false;

            return true;
        }
        #endregion =====================================================================

    }
}
