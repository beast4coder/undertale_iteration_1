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
        Rectangle Arena_Rectangle;

        //objects
        Player player;
        Sprite_Handler FightBox;
        Sprite_Handler ActBox;
        Sprite_Handler ItemBox;
        Sprite_Handler MercyBox;

        //controls
        Label lblPlayerHealth;

        Label debug_label;

        //consts
        public const float flt_PLAYER_SPEED = 1f;
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
            #region Spawn Arena
            size = new PointF(200, 200);
            loc = new PointF((flt_FORM_WIDTH - size.X)/2, (flt_FORM_HEIGHT - size.Y)/2);
            Arena_Rectangle = new Rectangle((int)loc.X, (int)loc.Y, (int)size.X, (int)size.Y);
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

            //spawn debug label
            #region debug_label
            debug_label = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                Location = new Point(411, 363),
                Name = "debug_label",
                Size = new Size(36, 15),
                TabIndex = 1,
                Text = "",
            };
            Controls.Add(debug_label);
            debug_label.BringToFront();
            #endregion
        }

        private void Update_Sprites(object sender, PaintEventArgs e)
        {
            //Draw the arena box
            e.Graphics.DrawRectangle(new Pen(Color.White, 4), Arena_Rectangle);

            //Draw the sprites
            player.Draw(e.Graphics);
            FightBox.Draw(e.Graphics);
            ActBox.Draw(e.Graphics);
            ItemBox.Draw(e.Graphics);
            MercyBox.Draw(e.Graphics);
        }

        //handles all key presses and releases
        #region Key Presses

        //global variables tracks whether relevant keys are held or not
        public static bool Down_Held = false;
        public static bool Up_Held = false;
        public static bool Left_Held = false;
        public static bool Right_Held = false;
        public static bool Z_Held = false;
        public static bool X_Held = false;

        //global variables tracks whether relevant keys are just pressed or not with help of the JustPressed_System() method
        public static bool Z_Pressed = false;
        public static bool X_Pressed = false;
        public static bool Down_Pressed = false;
        public static bool Up_Pressed = false;
        public static bool Left_Pressed = false;
        public static bool Right_Pressed = false;

        //keeps track of what buttons are held by setting a corresponding bool to true when they go down and setting it false when they go up
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) Down_Held = true;
            if (e.KeyCode == Keys.Up) Up_Held = true;
            if (e.KeyCode == Keys.Left) Left_Held = true;
            if (e.KeyCode == Keys.Right) Right_Held = true;
            if (e.KeyCode == Keys.Z) Z_Held = true;
            if (e.KeyCode == Keys.X) X_Held = true;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) Down_Held = false;
            if (e.KeyCode == Keys.Up) Up_Held = false;
            if (e.KeyCode == Keys.Left) Left_Held = false;
            if (e.KeyCode == Keys.Right) Right_Held = false;
            if (e.KeyCode == Keys.Z) Z_Held = false;
            if (e.KeyCode == Keys.X) X_Held = false;
        }

        private void JustPressed_System()
        {
            if (Z_Held && !Z_Pressed) Z_Pressed = true;
            else Z_Pressed = false;
            if (X_Held && !X_Pressed) X_Pressed = true;
            else X_Pressed = false;
            if (Down_Held && !Down_Pressed) Down_Pressed = true;
            else Down_Pressed = false;
            if (Up_Held && !Up_Pressed) Up_Pressed = true;
            else Up_Pressed = false;
            if (Left_Held && !Left_Pressed) Left_Pressed = true;
            else Left_Pressed = false;
            if (Right_Held && !Right_Pressed) Right_Pressed = true;
            else Right_Pressed = false;
        }
        #endregion

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            JustPressed_System();
            player.Movement_System();
            Update_System();
            //Damage_System();
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

        private void Player_Turn_Start()
        {
            player.Change_Turn();

        }
    }
}