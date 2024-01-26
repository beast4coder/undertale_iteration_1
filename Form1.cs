//using statements go here

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
        const int int_ARENA_WALL_SIZE = 5;
        Sprite_Handler FightBox;
        Sprite_Handler ActBox;
        Sprite_Handler ItemBox;
        Sprite_Handler MercyBox;
        Sprite_Handler Target_Sprite;

        //turn bool
        bool Player_Turn = false;

        //player
        Player player;
        Sprite_Handler Player_Projectile_Sprite;

        //enemies
        Test_Enemy test_enemy;

        //controls
        Label lblPlayerHealth;
        Label lblArenaText;

        Label debug_label;

        //consts
        public const float flt_PLAYER_SPEED = 1f;
        public const float flt_FORM_WIDTH = 640f;
        public const float flt_FORM_HEIGHT = 480f;

        //time counter
        int int_time_counter = 0;

        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
            Arena_Setup();
            Thread Turn = new Thread(Intro_Turn);
            Turn.Start();
        }

        private void Arena_Setup()
        {
            //define player objects
            #region Spawn Player
            Bitmap sheet = Resource1.Souls_Sprite_Sheet;
            string background_colour = "#FFFF66FF";
            PointF size = new PointF(16, 16);
            PointF rows_cols = new PointF(1, 1);
            PointF offset = new PointF(7, 6);
            PointF padding = new PointF(0, 0);
            PointF loc = new PointF((flt_FORM_WIDTH - size.X) / 2, (flt_FORM_HEIGHT - size.Y) / 2);
            player = new Player(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc, "TEST1", 20, 20);
            #endregion

            #region Define Player Projectile Sprite
            sheet = Resource1.Attack_Effects_Sprite_Sheet;
            size = new PointF(14, 128);
            rows_cols = new PointF(1, 2);
            offset = new PointF(1139, 23);
            padding = new PointF(5, 0);
            loc = new PointF(0, 0);
            Player_Projectile_Sprite = new Sprite_Handler(sheet, size, offset, loc);
            #endregion

            //define arena size, objects and controls
            #region Set Arena Size
            size = new PointF(180, 185);
            loc = new PointF((flt_FORM_WIDTH - size.X) / 2, (flt_FORM_HEIGHT - size.Y) / 2);
            Arena_Hitbox = new Rectangle((int)loc.X, (int)loc.Y, (int)size.X, (int)size.Y);
            #endregion

            #region Spawn Arena Text Box
            //make text appear in the top left corner of the arena when the arena is largest
            loc = new PointF(23, loc.Y + 23);
            Size font_size = new Size(20, 20);
            lblArenaText = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point((int)loc.X + 5, (int)loc.Y),
                Name = "lblArenaText",
                Size = font_size,
                TabIndex = 1,
                Text = "",
                Font = new Font("Pixelated MS Sans Serif", 15, FontStyle.Regular)
            };
            Controls.Add(lblArenaText);
            lblArenaText.BringToFront();
            #endregion

            #region Spawn FightBox
            sheet = Resource1.Battle_Menu_Sprite_Sheet;
            background_colour = "#FFC386FF";
            size = new PointF(112, 44);
            rows_cols = new PointF(2, 1);
            offset = new PointF(7, 110);
            padding = new PointF(0, 3);
            loc = new PointF(39, flt_FORM_HEIGHT - size.Y - 5);
            FightBox = new Sprite_Handler(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            #region Spawn ActBox
            size = new PointF(112, 44);
            rows_cols = new PointF(2, 1);
            offset = new PointF(7, 8);
            padding = new PointF(0, 3);
            loc = new PointF(189, flt_FORM_HEIGHT - size.Y - 5);
            ActBox = new Sprite_Handler(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            #region Spawn ItemBox
            size = new PointF(112, 44);
            rows_cols = new PointF(2, 1);
            offset = new PointF(7, 211);
            padding = new PointF(0, 3);
            loc = new PointF(339, flt_FORM_HEIGHT - size.Y - 5);
            ItemBox = new Sprite_Handler(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            #region Spawn MercyBox
            size = new PointF(112, 44);
            rows_cols = new PointF(2, 1);
            offset = new PointF(7, 312);
            padding = new PointF(0, 3);
            loc = new PointF(489, flt_FORM_HEIGHT - size.Y - 5);
            MercyBox = new Sprite_Handler(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            #region Spawn Target Sprite
            sheet = Resource1.Attack_Effects_Sprite_Sheet;
            size = new PointF(562, 128);
            offset = new PointF(5, 23);
            loc = new PointF(0, 0);
            Target_Sprite = new Sprite_Handler(sheet, size, offset, loc);
            #endregion

            //spawn enemy
            #region Spawn Enemy
            test_enemy = new Test_Enemy();
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

        private void Intro_Turn()
        {
            //wait 1s, start player turn
            Thread.Sleep(1000);
            Player_Turn_Start();
        }

        private void Update_Sprites(object sender, PaintEventArgs e)
        {
            //Draw the arena box
            e.Graphics.DrawRectangle(new Pen(Color.White, int_ARENA_WALL_SIZE), Arena_Hitbox);

            //Draw enemy sprites
            foreach (Sprite_Handler sprite in test_enemy.Get_Sprites())
            {
                sprite.Draw(e.Graphics, 1f);
            }

            //Draw objects
            //***the last one drawn will be on top***
            FightBox.Draw(e.Graphics, 1f);
            ActBox.Draw(e.Graphics, 1f);
            ItemBox.Draw(e.Graphics, 1f);
            MercyBox.Draw(e.Graphics, 1f);

            player.Draw(e.Graphics, 1f);
        }

        //handles management of all inputs
        #region Key Inputs

        //global variables tracks whether relevant keys are held or not
        bool Down_Held = false;
        bool Up_Held = false;
        bool Left_Held = false;
        bool Right_Held = false;
        bool Z_Held = false;
        bool X_Held = false;

        //global variables tracks whether relevant keys are just pressed or not with help of the JustPressed_System() method
        bool Z_Pressed = false;
        bool X_Pressed = false;
        bool Down_Pressed = false;
        bool Up_Pressed = false;
        bool Left_Pressed = false;
        bool Right_Pressed = false;

        //global variables tracks whether relevant keys are still held or not as an intermediary step to the JustPressed_System() method
        bool Z_Still_Held = false;
        bool X_Still_Held = false;
        bool Down_Still_Held = false;
        bool Up_Still_Held = false;
        bool Left_Still_Held = false;
        bool Right_Still_Held = false;

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
            if (e.KeyCode == Keys.Down)
            {
                Down_Held = false;
                Down_Still_Held = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                Up_Held = false;
                Up_Still_Held = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                Left_Held = false;
                Left_Still_Held = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                Right_Held = false;
                Right_Still_Held = false;
            }
            if (e.KeyCode == Keys.Z)
            {
                Z_Held = false;
                Z_Still_Held = false;
            }
            if (e.KeyCode == Keys.X)
            {
                X_Held = false;
                X_Still_Held = false;
            }
        }

        private void JustPressed_System()
        {
            //if a key is held and it wasn't held last tick, sets the just pressed still held value to true
            //if it was held last tick, set the just pressed value to false
            if (Z_Held && !Z_Still_Held)
            {
                Z_Pressed = true;
                Z_Still_Held = true;
            }
            else Z_Pressed = false;
            if (X_Held && !X_Still_Held)
            {
                X_Pressed = true;
                X_Still_Held = true;
            }
            else X_Pressed = false;
            if (Down_Held && !Down_Still_Held)
            {
                Down_Pressed = true;
                Down_Still_Held = true;
            }
            else Down_Pressed = false;
            if (Up_Held && !Up_Still_Held)
            {
                Up_Pressed = true;
                Up_Still_Held = true;
            }
            else Up_Pressed = false;
            if (Left_Held && !Left_Still_Held)
            {
                Left_Pressed = true;
                Left_Still_Held = true;
            }
            else Left_Pressed = false;
            if (Right_Held && !Right_Still_Held)
            {
                Right_Pressed = true;
                Right_Still_Held = true;
            }
            else Right_Pressed = false;
        }
        #endregion

        

        #region Player Turn Logic
        public void Player_Turn_Option_System()
        {
            int player_box_pos = player.Get_Box_Position();
            if (player_box_pos > -1 && Z_Pressed)
            {
                player.Set_Box_Position(player_box_pos - 4);
            }
            else if (player_box_pos > -5)
            {
                //at -4, -3, -1 the player must select an enemy to fight/act/mercy respectively
                if (player_box_pos == -4 || player_box_pos == -3 || player_box_pos == -1)
                {
                    //if z is pressed then select the box
                    if (Z_Pressed)
                    {
                        switch (player_box_pos)
                        {
                            case -4:
                                player.Set_Box_Position(player_box_pos - 4);
                                Fight_Logic();
                                break;
                            case -3:
                                player.Set_Box_Position(player_box_pos - 4);
                                Act_Logic();
                                break;
                            case -1:
                                player.Set_Box_Position(player_box_pos - 4);
                                Mercy_Logic();
                                break;
                            default:
                                break;
                        }
                    }
                    else if (X_Pressed)
                    {
                        player.Set_Box_Position(player_box_pos + 4);
                    }
                }
            }
        }

        private void Fight_Logic()
        {
            //get the player off the screen in the fight spot
        }

        private void Act_Logic()
        {
            //get the player off the screen in the act spot
            player.Set_Box_Position(-2);
        }

        private void Item_Logic()
        {
            //get the player off the screen in the item spot
            player.Set_Box_Position(-3);
        }

        private void Mercy_Logic()
        {
            //get the player off the screen in the mercy spot
            player.Set_Box_Position(-4);
        }
        #endregion

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            //refresh the picture box to update the sprite's position
            pbBackground.Refresh();

            //lblPlayerHealth.Text = player.GetHealth() + "/20";

            //run the systems
            JustPressed_System();
            Player_Movement_System();

            //player turn exclusives ;)
            if (Player_Turn) Player_Turn_Systems();
            /*Damage_System();*/
            int_time_counter++;
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

        //all funcitons that update something on the form
        #region Updating Functions
        private void Update_Arena_Hitbox()
        {
            //redefines the arena box
            if (Player_Turn)
            {
                Arena_Hitbox.X = 18;
                Arena_Hitbox.Width = (int)flt_FORM_WIDTH - 36;
            }
            else
            {
                Arena_Hitbox.X = (int)(flt_FORM_WIDTH - player.Get_Size().X) / 2;
                Arena_Hitbox.Width = 180;
            }
        }
        private void Update_Arena_Text()
        {
            if (Player_Turn)
            {
                float pos = player.Get_Box_Position();
                if (pos > -1 && pos < 4) lblArenaText.Text = "* " + test_enemy.Choose_Arena_Text();
                else if (pos == -1 || pos == -2 || pos == -4) lblArenaText.Text = "* " + test_enemy.Get_Name();
                else if (pos == -3) ;//implemented later
            }
            else lblArenaText.Text = "";
        }
        private void Update_Option_Boxes()
        {
            //resets all the boxes
            PointF Box_Default = new PointF(0, 0);
            FightBox.Set_Row_Col(Box_Default);
            ActBox.Set_Row_Col(Box_Default);
            ItemBox.Set_Row_Col(Box_Default);
            MercyBox.Set_Row_Col(Box_Default);
            //makes the box yellow if player is there
            int pos = player.Get_Box_Position();
            if (pos > -1)
                switch (pos)
                {
                    case 0:
                        FightBox.Next();
                        break;
                    case 1:
                        ActBox.Next();
                        break;
                    case 2:
                        ItemBox.Next();
                        break;
                    case 3:
                        MercyBox.Next();
                        break;
                    default:
                        break;
                }
        }
        #endregion

        //all systems that run per tick
        #region Systems
        private void Player_Turn_Systems()
        {
            //update option boxes shouldn't be here for efficiency reasons but itll do for now
            Update_Option_Boxes();
            Player_Turn_Option_System();

        }

        #region Player Movement
        public void Player_Movement_System()
        {
            int player_box_pos = player.Get_Box_Position();
            PointF loc = player.Get_Location();
            PointF size = player.Get_Size();
            if (Player_Turn)
            {
                //checks if player box position is positieve
                if (player_box_pos > -1)
                {
                    //if left arrow key is pressed move left, if right arrow key is pressed move right
                    //if left is pressed on the leftmost box, move to the rightmost box, vice versa
                    if (Left_Pressed)
                    {
                        if (player_box_pos > 0) player.Set_Box_Position(player_box_pos - 1);
                        else player.Set_Box_Position(3);
                    }
                    if (Right_Pressed)
                    {
                        if (player_box_pos < 3) player.Set_Box_Position(player_box_pos + 1);
                        else player.Set_Box_Position(0);
                    }
                    //sets new player box position before moving player off the value
                    player_box_pos = player.Get_Box_Position();
                    player.Set_Location(new PointF(49 + (player_box_pos * 150), loc.Y)); //boxes are 112 wide, with 38 pixels between -- guessed 50 pxiels, seems to have nailed it
                    player.Set_Center(new PointF(player.Get_Center().X, flt_FORM_HEIGHT - 27)); //boxes are 5 off the floor and 44 high
                }
                else if (player_box_pos > -5)
                {
                    //if the box pos is negative but above -5, the player has selected a box
                    int player_option_pos = player.Get_Option_Position();
                    //check how many options the player has
                    //for current iteration, only one enemy is implemented and no items for now, a foreach will be implemented later
                    int num_options = 1;
                    if (Left_Pressed)
                    {
                        if (player_option_pos == 3 && 1 <= num_options) player.Set_Option_Position(1);
                        else if (player_option_pos == 4 && 2 <= num_options) player.Set_Option_Position(2);
                    }
                    if (Right_Pressed)
                    {
                        if (player_option_pos == 1 && 3 <= num_options) player.Set_Option_Position(3);
                        else if (player_option_pos == 2 && 4 <= num_options) player.Set_Option_Position(4);
                    }
                    if (Up_Pressed)
                    {
                        if (player_option_pos == 2 && 1 <= num_options) player.Set_Option_Position(1);
                        else if (player_option_pos == 4 && 3 <= num_options) player.Set_Option_Position(3);
                    }
                    if (Down_Pressed)
                    {
                        if (player_option_pos == 1 && 2 <= num_options) player.Set_Option_Position(2);
                        else if (player_option_pos == 3 && 4 <= num_options) player.Set_Option_Position(4);
                    }

                    //update the player's option position
                    player_option_pos = player.Get_Option_Position();

                    //move player to the selected option
                    switch (player_option_pos)
                    {
                        case 1:
                            player.Set_Location(new PointF(Arena_Hitbox.X + 7, Arena_Hitbox.Y + 7));
                            break;
                        case 2:
                            player.Set_Location(new PointF(Arena_Hitbox.X + 7, Arena_Hitbox.Y + Arena_Hitbox.Height / 2 + 7));
                            break;
                        case 3:
                            player.Set_Location(new PointF(Arena_Hitbox.X + Arena_Hitbox.Width / 2 + 7, Arena_Hitbox.Y + 7));
                            break;
                        case 4:
                            player.Set_Location(new PointF(Arena_Hitbox.X + Arena_Hitbox.Width / 2 + 7, Arena_Hitbox.Y + Arena_Hitbox.Height / 2 + 7));
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //if the player's box pos is this negative, the player has selected an option
                    //move them off the screen
                    player.Set_Location(new PointF(-100, -100));
                }
            }
            else
            {
                //x and y track final displacement of player
                float x = 0;
                float y = 0;

                //checks which keys are helds and alters x and y accordingly
                if (Down_Held) y += flt_PLAYER_SPEED;
                if (Up_Held) y -= flt_PLAYER_SPEED;
                if (Left_Held) x -= flt_PLAYER_SPEED;
                if (Right_Held) x += flt_PLAYER_SPEED;

                //checks boundaries against the arena

                //adjuments are different for different sides as the length is 5 so halfing it gives rounding errors
                //Math.Floor/Ceiling corrects for this
                int Adjustment_C = (int)Math.Ceiling((float)int_ARENA_WALL_SIZE / 2);
                int Adjustment_F = (int)Math.Floor((float)int_ARENA_WALL_SIZE / 2);
                if (loc.X + x < Arena_Hitbox.Left + Adjustment_C) x = Arena_Hitbox.Left - loc.X + Adjustment_C;
                if (loc.X + size.X + x > Arena_Hitbox.Right - Adjustment_F) x = Arena_Hitbox.Right - loc.X - size.X - Adjustment_F;
                if (loc.Y + y < Arena_Hitbox.Top + Adjustment_C) y = Arena_Hitbox.Top - loc.Y + Adjustment_C;
                if (loc.Y + size.Y + y > Arena_Hitbox.Bottom - Adjustment_F) y = Arena_Hitbox.Bottom - loc.Y - size.Y - Adjustment_F;


                //moves player final x and y values
                player.Move(x, y);
            }
        }
        #endregion
        #endregion

        private void Player_Turn_Start()
        {
            Player_Turn = true;
            Update_Arena_Hitbox();
            //Update_Arena_Text();
        }
    }
}