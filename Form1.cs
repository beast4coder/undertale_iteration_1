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



        #region ImageSprite Class
        //class worked on in class project
        class ImageSprite
        {
            public float Angle;
            public PointF Center;
            public Bitmap Picture;
            public float Radius;

            public ImageSprite(float angle, PointF center, Bitmap picture)
            {
                Angle = angle;
                Center = center;
                Picture = picture;
                Radius = Math.Min(Picture.Width, picture.Height) / 2;
            }

            public void Draw(Graphics gr)
            {
                GraphicsState state = gr.Save();
                gr.ResetTransform();
                gr.RotateTransform(Angle);
                gr.TranslateTransform(Center.X, Center.Y, MatrixOrder.Append);
                gr.DrawImage(Picture, new PointF(-Radius, -Radius));
                gr.Restore(state);
            }
        }
        #endregion

        ImageSprite player;

        private void GameForm_Load(object sender, EventArgs e)
        {
            PointF StartPosition = new PointF(490, 297);
            player = new ImageSprite(0, StartPosition, new Bitmap (Resource1.red_heart));
        }
        private void Update_Sprites(object sender, PaintEventArgs e)
        {
            player.Draw(e.Graphics);
        }

        #region Key Presses

        //corresponding bools for each key
        bool Down = false;
        bool Up = false;
        bool Left = false;
        bool Right = false;

        //keeps track of what buttons are held by setting a corresponding bool to true when they go down and setting it false when they go up
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) Down = true;
            if (e.KeyCode == Keys.Up) Up = true;
            if (e.KeyCode == Keys.Left) Left = true;
            if (e.KeyCode == Keys.Right) Right = true;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) Down = false;
            if (e.KeyCode == Keys.Up) Up = false;
            if (e.KeyCode == Keys.Left) Left = false;
            if (e.KeyCode == Keys.Right) Right = false;
        }
        #endregion

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            //x and y track final displacement of player
            float x = 0;
            float y = 0;

            //checks which keys are helds and alters x and y accordingly
            if (Down) y += 5;
            if (Up) y -= 5;
            if (Left) x -= 5;
            if (Right) x += 5;

            //checks boundaries against the picturebox
            //picture box position hard coded for iteration 1, will be changed later
            if (player.Center.X < 365 + player.Picture.Width) x = 0;
            if (player.Center.X > 365 + 250 - player.Picture.Width) x = 0;

            if (player.Center.Y < 173 + player.Picture.Height) y = 0;
            if (player.Center.Y > 173 + 250 - player.Picture.Height) y = 0;

            //moves player final x and y values
            player.Center = new PointF(player.Center.X + x, player.Center.Y + y);

            //refresh the picture box to update the sprite's position
            pbArena.Refresh();
        }
    }
}