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

        //arena
        Rectangle Arena_Hitbox;
        const int Arena_Width = 4;

        //objects
        Player player;
        Sprite_Handler FightBox;
        Sprite_Handler ActBox;
        Sprite_Handler ItemBox;
        Sprite_Handler MercyBox;

        //controls
        Label lblPlayerHealth;

        //consts
        const float flt_PLAYER_SPEED = 1f;
        const float flt_FORM_WIDTH = 640f;
        const float flt_FORM_HEIGHT = 480f;

        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
            Arena_Setup();
        }

        private void Arena_Setup()
        {
            //spawn player
            #region Spawn Player
            string background_colour = "#FFFF66FF";
            PointF size = new PointF(16,16);
            PointF rows_cols = new PointF(1, 1);
            PointF offset = new PointF(7, 6);
            PointF padding = new PointF(0, 0);
            PointF loc = new PointF((flt_FORM_WIDTH - size.X)/2, (flt_FORM_HEIGHT - size.Y)/2);
            player = new Player(Resource1.Souls_Sprite_Sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc, "TEST1", 20, 20, false);
            #endregion

            //spawn option boxes
            #region Spawn FightBox
            background_colour = "#FFC386FF";
            size = new PointF(112, 44);
            rows_cols = new PointF(2, 1);
            offset = new PointF(7, 110);
            padding = new PointF(0, 3);
            loc = new PointF(39, flt_FORM_HEIGHT - size.Y - 1);
            FightBox = new Sprite_Handler(Resource1.Battle_Menu_Sprite_Sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            #region Spawn ActBox
            background_colour = "#FFC386FF";
            size = new PointF(112, 44);
            rows_cols = new PointF(2, 1);
            offset = new PointF(7, 8);
            padding = new PointF(0, 3);
            loc = new PointF(189, flt_FORM_HEIGHT - size.Y - 1);
            ActBox = new Sprite_Handler(Resource1.Battle_Menu_Sprite_Sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            #region Spawn ItemBox
            background_colour = "#FFC386FF";
            size = new PointF(112, 44);
            rows_cols = new PointF(2, 1);
            offset = new PointF(7, 211);
            padding = new PointF(0, 3);
            loc = new PointF(339, flt_FORM_HEIGHT - size.Y - 1);
            ItemBox = new Sprite_Handler(Resource1.Battle_Menu_Sprite_Sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            #region Spawn MercyBox
            background_colour = "#FFC386FF";
            size = new PointF(112, 44);
            rows_cols = new PointF(2, 1);
            offset = new PointF(7, 312);
            padding = new PointF(0, 3);
            loc = new PointF(489, flt_FORM_HEIGHT - size.Y - 1);
            MercyBox = new Sprite_Handler(Resource1.Battle_Menu_Sprite_Sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            //define arena box
            #region Define Arena
            size = new PointF(200, 200);
            loc = new PointF((flt_FORM_WIDTH - size.X)/2, (flt_FORM_HEIGHT - size.Y)/2);
            Arena_Hitbox = new Rectangle((int)loc.X, (int)loc.Y, (int)size.X, (int)size.Y);
            #endregion

            //spawn all controls required
            #region lblPlayerHealth
            //lblPlayerHealth = new Label
            //{
            //    AutoSize = true,
            //    ForeColor = Color.White,
            //    Location = new Point(411, 363),
            //    Name = "lblPlayerHealth",
            //    Size = new Size(36, 15),
            //    TabIndex = 1,
            //    Text = player.GetHealth() + "/20",
            //};
            //this.Controls.Add(lblPlayerHealth);
            //lblPlayerHealth.BringToFront();

            #endregion

        }

        private void Update_Sprites(object sender, PaintEventArgs e)
        {
            //Draw the arena box
            e.Graphics.DrawRectangle(new Pen(Color.White, Arena_Width), Arena_Hitbox);

            //Draw the sprites
            player.Draw(e.Graphics);
            FightBox.Draw(e.Graphics);
            ActBox.Draw(e.Graphics);
            ItemBox.Draw(e.Graphics);
            MercyBox.Draw(e.Graphics);
        }

        //handles all key presses and releases
        #region Key Presses

        //global variables for all keys, tracks whether they are pressed wor not
        bool D = false;
        bool U = false;
        bool L = false;
        bool R = false;
        bool Z = false;
        bool X = false;

        //keeps track of what buttons are held by setting a corresponding bool to true when they go down and setting it false when they go up
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) D = true;
            if (e.KeyCode == Keys.Up) U = true;
            if (e.KeyCode == Keys.Left) L = true;
            if (e.KeyCode == Keys.Right) R = true;
            if (e.KeyCode == Keys.Z) Z = true;
            if (e.KeyCode == Keys.X) X = true;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) D = false;
            if (e.KeyCode == Keys.Up) U = false;
            if (e.KeyCode == Keys.Left) L = false;
            if (e.KeyCode == Keys.Right) R = false;
            if (e.KeyCode == Keys.Z) Z = false;
            if (e.KeyCode == Keys.X) X = false;
        }
        #endregion

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            Movement_System();
            Update_System();
            //Damage_System();
        }

        private void Movement_System()
        {
            //x and y track final displacement of player
            float x = 0;
            float y = 0;

            //checks which keys are helds and alters x and y accordingly
            if (D) y += flt_PLAYER_SPEED;
            if (U) y -= flt_PLAYER_SPEED;
            if (L) x -= flt_PLAYER_SPEED;
            if (R) x += flt_PLAYER_SPEED;

            //checks boundaries against the arena, if moving into the boundary, sets x and y to the distance to the boundary
            //when against the boundary this will set x and y to 0
            if (player.Get_Location().X + x < Arena_Hitbox.Left + (Arena_Width/2)) x = Arena_Hitbox.Left - player.Get_Location().X + (Arena_Width/2);
            if (player.Get_Location().X + player.Get_Size().X + x > Arena_Hitbox.Right - (Arena_Width/2)) x = Arena_Hitbox.Right - player.Get_Location().X - player.Get_Size().X - (Arena_Width/2);
            if (player.Get_Location().Y + y < Arena_Hitbox.Top + (Arena_Width/2)) y = Arena_Hitbox.Top - player.Get_Location().Y + (Arena_Width/2);
            if (player.Get_Location().Y + player.Get_Size().Y + y > Arena_Hitbox.Bottom - (Arena_Width/2)) y = Arena_Hitbox.Bottom - player.Get_Location().Y - player.Get_Size().Y - (Arena_Width/2);

            //moves player final x and y values
            player.Move(x, y);
        }

        private void Update_System()
        {
            //refresh the picture box to update the sprite's position
            pbBackground.Refresh();
            //lblPlayerHealth.Text = player.GetHealth() + "/20";
        }

        /*
        private void Damage_System()
        {
            //checks if the player is touching the box in x-axis
            if (player.Location.X + player.Size.X > box.Location.X && player.Location.X < box.Location.X + box.Size.X)
            {
                //checks if the player is touching the box in y-axis
                if (player.Location.Y + player.Size.Y > box.Location.Y && player.Location.Y < box.Location.Y + box.Size.Y)
                {
                    //if the player is touching the box, the player takes damage
                    player.Set_Health(player.GetHealth() - box.Get_Damage());
                }
            }
        }
        */
    }
}