using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Windows.Forms.PropertyGridInternal;

namespace undertale_iteration_1
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        ImageSprite player;
        float fltPlayerSpeed = 1f;

        private void GameForm_Load(object sender, EventArgs e)
        {
            Point StartPosition = new Point(490, 297);
            player = new ImageSprite(Resource1.red_heart, StartPosition);
        }


        private void Update_Sprites(object sender, PaintEventArgs e)
        {
            player.Draw(e.Graphics);
        }

        #region Key Presses

        //corresponding bools for each key
        bool D = false;
        bool U = false;
        bool L = false;
        bool R = false;

        //keeps track of what buttons are held by setting a corresponding bool to true when they go down and setting it false when they go up
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) D = true;
            if (e.KeyCode == Keys.Up) U = true;
            if (e.KeyCode == Keys.Left) L = true;
            if (e.KeyCode == Keys.Right) R = true;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) D = false;
            if (e.KeyCode == Keys.Up) U = false;
            if (e.KeyCode == Keys.Left) L = false;
            if (e.KeyCode == Keys.Right) R = false;
        }
        #endregion

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            //x and y track final displacement of player
            float x = 0;
            float y = 0;

            //checks which keys are helds and alters x and y accordingly
            if (D) y += fltPlayerSpeed;
            if (U) y -= fltPlayerSpeed;
            if (L) x -= fltPlayerSpeed;
            if (R) x += fltPlayerSpeed;

            //checks boundaries against the picturebox
            //picture box position hard coded for iteration 1, will be changed later
            /*if (player.Center.X < 365 + player.Picture.Width) x = 0;
            if (player.Center.X > 365 + 250 - player.Picture.Width) x = 0;

            if (player.Center.Y < 173 + player.Picture.Height) y = 0;
            if (player.Center.Y > 173 + 250 - player.Picture.Height) y = 0;*/

            //moves player final x and y values
            player.Location = new PointF(player.Location.X + x, player.Location.Y + y);

            //refresh the picture box to update the sprite's position
            pbArena.Refresh();
        }
    }
}