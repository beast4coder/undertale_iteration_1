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

        #region Global Variables

        //objects
        Player player;
        Projectile box;

        Label lblPlayerHealth;

        //consts (idc its not in caps i dont like it and no one else is using the code lol)
        float fltPlayerSpeed = 1f;

        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
            Setup();
        }

        private void Setup()
        {
            //spawn all objects required
            Point StartPosition = new Point(490, 297);
            player = new Player(Resource1.red_heart, StartPosition, 20);
            StartPosition = new Point(590, 257);
            box = new Projectile(Resource1.projectile_box, StartPosition, 1);

            //spawn all controls required
            #region lblPlayerHealth
            lblPlayerHealth = new Label
            {
                AutoSize = true,
                ForeColor =  Color.White,
                Location = new Point(411, 363),
                Name = "lblPlayerHealth",
                Size = new Size(36, 15),
                TabIndex = 1,
                Text = "20/20",
            }; 
            this.Controls.Add(lblPlayerHealth);
            lblPlayerHealth.BringToFront();
            #endregion
        }

        private void Update_Sprites(object sender, PaintEventArgs e)
        {
            player.Draw(e.Graphics);
            box.Draw(e.Graphics);
        }

        //handles all key presses and releases
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
            Movement_System();
            Update_System();
            Damage_System();
        }

        private void Movement_System()
        {
            //x and y track final displacement of player
            float x = 0;
            float y = 0;

            //checks which keys are helds and alters x and y accordingly
            if (D) y += fltPlayerSpeed;
            if (U) y -= fltPlayerSpeed;
            if (L) x -= fltPlayerSpeed;
            if (R) x += fltPlayerSpeed;

            //checks boundaries against the arena
            //arena position hard coded for iteration 1, will be changed later

            if (player.Center.X < (265 + (player.Picture.Width / 2)) && x < 0) x = 0;
            if (player.Center.X > (715 - (player.Picture.Width / 2)) && x > 0) x = 0;

            if (player.Center.Y < (73 + (player.Picture.Height / 2)) && y < 0) y = 0;
            if (player.Center.Y > (523 - (player.Picture.Height / 2)) && y > 0)y = 0;

            //moves player final x and y values
            player.MOVE(x , y);
        }

        private void Update_System()
        {
            //refresh the picture box to update the sprite's position
            pbArena.Refresh();

            lblPlayerHealth.Text = player.GetHealth() + "/20";
        }

        private void Damage_System()
        {
            //checks if the player is touching the box in x-axis
            if (player.Location.X + player.Picture.Width > box.Location.X && player.Location.X < box.Location.X + box.Picture.Width)
            {
                //checks if the player is touching the box in y-axis
                if (player.Location.Y + player.Picture.Height > box.Location.Y && player.Location.Y < box.Location.Y + box.Picture.Height)
                {
                    //if the player is touching the box, the player takes damage
                    player.Set_Health(player.GetHealth() - box.Get_Damage());
                }
            }
        }
    }
}