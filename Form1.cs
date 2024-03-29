//using statements go here

using System.Drawing.Text;
using System.Threading;
using System.Diagnostics;
using System.Media;
using System.Diagnostics.Eventing.Reader;

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
        public const int int_ARENA_WALL_SIZE = 5;
        public const int int_PLAYER_TURN_ARENA_X = 38;
        public const int int_PLAYER_TURN_ARENA_Y = 254;
        public const int int_PLAYER_TURN_ARENA_WIDTH = 565;
        public const int int_PLAYER_TURN_ARENA_HEIGHT = 130;
        public const int int_DEFAULT_ARENA_X = 230;
        public const int int_DEFAULT_ARENA_Y = 199;
        public const int int_DEFAULT_ARENA_WIDTH = 180;
        public const int int_DEFAULT_ARENA_HEIGHT = 185;
        Sprite_Handler FightBox;
        Sprite_Handler ActBox;
        Sprite_Handler ItemBox;
        Sprite_Handler MercyBox;
        Sprite_Handler Target_Sprite;
        Label lblArenaGeneral;
        Label lblArenaOpt1;
        Label lblArenaOpt2;
        Label lblArenaOpt3;
        Label lblArenaOpt4;
        PictureBox pbProjectileCover_Left;
        PictureBox pbProjectileCover_Right;

        //turn variables
        public static bool Player_Turn = true;
        public static bool Turn_Ended = true;

        //player
        Player player;
        Sprite_Handler Player_Attack_Sprite;
        Sprite_Handler Slash_Sprite;
        bool Draw_Slash_Sprite = false;
        List<Sprite_Handler> Attack_Numbers = new List<Sprite_Handler>();
        bool Draw_Attack_Numbers = false;
        Label lblPlayerName;
        Label lblPlayerLevel;
        Label lblPlayerHPText;
        Label lblPlayerHealth;
        Rectangle Player_Remaining_Health;
        Rectangle Player_Lost_Health;

        

        //enemies
        List<Enemy> Enemies = new List<Enemy>();
        int Enemy_Turn_Timer = 0;
        
        //IT'S TIME! TO D- D- D- D- D- D- D- D- DEBUG
        Label debug_label;

        //consts
        public const float flt_PLAYER_SPEED = 3f;
        public const float flt_FORM_WIDTH = 640f;
        public const float flt_FORM_HEIGHT = 480f;

        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
            Arena_Setup();
            Player_Setup();
            Spawn_Test_Enemy();
        }

        #region Setup
        private void Arena_Setup()
        {
            //define arena size, objects and controls
            #region Set Arena Size
            PointF size = new PointF(int_DEFAULT_ARENA_WIDTH, int_DEFAULT_ARENA_HEIGHT);
            PointF loc = new PointF(int_DEFAULT_ARENA_X, int_DEFAULT_ARENA_Y);
            Arena_Hitbox = new Rectangle((int)loc.X, (int)loc.Y, (int)size.X, (int)size.Y);
            #endregion

            #region Spawn Arena Text Box
            //make text appear in the top left corner of the arena

            //c# doesn;t let you do fonts nicely, cannot add file straight to font family
            //font file added to a 'private font collection', font pulled from that as a font family type
            //only then usable
            PrivateFontCollection my_fonts = new PrivateFontCollection();
            my_fonts.AddFontFile("Resources/8bitoperator_jve.ttf");
            FontFamily pixel_font = my_fonts.Families[0];

            lblArenaGeneral = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(65, 270),
                Name = "lblArenaText",
                TabIndex = 1,
                Text = "",
                Font = new Font(pixel_font, 18, FontStyle.Regular)
            };
            Controls.Add(lblArenaGeneral);
            lblArenaGeneral.BringToFront();
            #endregion

            #region Spawn Option Text Boxes

            //option box 1, top left
            lblArenaOpt1 = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(100, 270),
                Name = "lblArenaOpt1",
                TabIndex = 1,
                Text = "",
                Font = new Font(pixel_font, 18, FontStyle.Regular)
            };
            Controls.Add(lblArenaOpt1);
            lblArenaOpt1.BringToFront();

            //option box 2, bottom left
            lblArenaOpt2 = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(100, 325), //345, 332
                Name = "lblArenaOpt2",
                TabIndex = 1,
                Text = "",
                Font = new Font(pixel_font, 18, FontStyle.Regular)
            };
            Controls.Add(lblArenaOpt2);
            lblArenaOpt2.BringToFront();

            //option box 3, top right
            lblArenaOpt3 = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(380, 270),
                Name = "lblArenaOpt3",
                TabIndex = 1,
                Text = "",
                Font = new Font(pixel_font, 18, FontStyle.Regular)
            };
            Controls.Add(lblArenaOpt3);
            lblArenaOpt3.BringToFront();

            //option box 4, bottom right
            lblArenaOpt4 = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(380, 325),
                Name = "lblArenaOpt4",
                TabIndex = 1,
                Text = "",
                Font = new Font(pixel_font, 18, FontStyle.Regular)
            };
            Controls.Add(lblArenaOpt4);
            lblArenaOpt4.BringToFront();
            #endregion

            #region Spawn FightBox
            Bitmap sheet = Resource1.Battle_Menu_Sprite_Sheet;
            string background_colour = "#FFC386FF";
            size = new PointF(112, 44);
            PointF rows_cols = new PointF(2, 1);
            PointF offset = new PointF(7, 110);
            PointF padding = new PointF(0, 3);
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
            loc = new PointF(int_PLAYER_TURN_ARENA_X + 1, int_PLAYER_TURN_ARENA_Y + 1);
            Target_Sprite = new Sprite_Handler(sheet, size, offset, loc);
            #endregion

            #region Spawn Covers
            pbProjectileCover_Left = new PictureBox
            {
                BackColor = Color.Black,
                Location = new Point(0, int_DEFAULT_ARENA_Y - 3),
                Name = "pbProjectileCover_Left",
                Size = new Size(int_DEFAULT_ARENA_X - 5, int_DEFAULT_ARENA_HEIGHT + 5),
                TabIndex = 1,
                TabStop = false,
            };
            Controls.Add(pbProjectileCover_Left);
            pbProjectileCover_Left.BringToFront();

            pbProjectileCover_Right = new PictureBox
            {
                BackColor = Color.Black,
                Location = new Point(int_DEFAULT_ARENA_X + int_DEFAULT_ARENA_WIDTH + 5, int_DEFAULT_ARENA_Y),
                Name = "pbProjectileCover_Right",
                Size = new Size((int)flt_FORM_WIDTH - int_DEFAULT_ARENA_X - int_DEFAULT_ARENA_WIDTH, int_DEFAULT_ARENA_HEIGHT),
                TabIndex = 1,
                TabStop = false,
            };
            Controls.Add(pbProjectileCover_Right);
            pbProjectileCover_Right.BringToFront();
            #endregion



            #region debug_label
            debug_label = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                Location = new Point(411, 50),
                Name = "debug_label",
                Size = new Size(36, 15),
                TabIndex = 1,
                Text = "",
            };
            Controls.Add(debug_label);
            debug_label.BringToFront();
            #endregion


        }

        private void Player_Setup()
        {
            //define player objects
            #region Spawn Player
            Bitmap sheet = Resource1.Souls_Sprite_Sheet;
            string background_colour = "#FFFF66FF";
            PointF size = new PointF(16, 16);
            PointF rows_cols = new PointF(2, 1);
            PointF offset = new PointF(7, 6);
            PointF padding = new PointF(0, 30);
            PointF loc = new PointF((flt_FORM_WIDTH - size.X) / 2, (flt_FORM_HEIGHT - size.Y) / 2);
            player = new Player(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc, "TEST1", 1);
            #endregion

            #region Define Player Projectile Sprite
            sheet = Resource1.Attack_Effects_Sprite_Sheet;
            size = new PointF(14, 128);
            rows_cols = new PointF(1, 2);
            offset = new PointF(1139, 23);
            padding = new PointF(5, 0);
            loc = new PointF(0, 0);
            Player_Attack_Sprite = new Sprite_Handler(sheet, size, rows_cols, offset, padding, loc);
            #endregion

            #region Define Slash Sprite
            sheet = Resource1.Attack_Effects_Sprite_Sheet;
            background_colour = "#FFC386FF";
            size = new PointF(26, 110);
            rows_cols = new PointF(1, 6);
            offset = new PointF(910, 235);
            padding = new PointF(5, 0);
            loc = new PointF(0, 0);
            Slash_Sprite = new Sprite_Handler(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc);
            #endregion

            #region Add Items
            player.Add_Item(Item_ID.Monster_Candy);
            player.Add_Item(Item_ID.Spider_Cider);
            player.Add_Item(Item_ID.Temmie_Flakes);
            player.Add_Item(Item_ID.ButterScotch_Pie);
            #endregion
        
            #region Display Player Stats
            PrivateFontCollection my_fonts = new PrivateFontCollection();
            my_fonts.AddFontFile("Resources/8bitoperator_jve.ttf");
            FontFamily pixel_font = my_fonts.Families[0];
            
            //labale locations eyeballed
            lblPlayerName = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                Location = new Point(38, (int)flt_FORM_HEIGHT - 81),
                Name = "lblPlayerName",
                Size = new Size(16, 16),
                TabIndex = 1,
                Text = player.Get_Name(),
                Font = new Font(pixel_font, 16, FontStyle.Bold)
            };
            Controls.Add(lblPlayerName);
            lblPlayerName.BringToFront();

            lblPlayerLevel = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                Location = new Point(124, (int)flt_FORM_HEIGHT - 81),
                Name = "lblPlayerLevel",
                Size = new Size(16, 16),
                TabIndex = 1,
                Text = "LV " + player.Get_Level(),
                Font = new Font(pixel_font, 16, FontStyle.Bold)
            };
            Controls.Add(lblPlayerLevel);
            lblPlayerLevel.BringToFront();

            lblPlayerHPText = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                Location = new Point(231, (int)flt_FORM_HEIGHT - 78),
                Name = "lblPlayerHPText",
                Size = new Size(16, 16),
                TabIndex = 1,
                Text = "HP",
                Font = new Font(pixel_font, 14, FontStyle.Bold)
            };
            Controls.Add(lblPlayerHPText);
            lblPlayerHPText.BringToFront();

            Player_Remaining_Health = new Rectangle(266, (int)flt_FORM_HEIGHT - 76, player.Get_Health() + 1, 16);
            Player_Lost_Health = new Rectangle(266 + player.Get_Health() + 1, (int)flt_FORM_HEIGHT - 76, player.Get_MaxHealth() - player.Get_Health() + 1, 16);
            
            lblPlayerHealth = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                Location = new Point(Player_Lost_Health.X + Player_Lost_Health.Width + 10, (int)flt_FORM_HEIGHT - 81),
                Name = "lblPlayerHealth",
                Size = new Size(16, 16),
                TabIndex = 1,
                Text = player.Get_Health() + " / " + player.Get_MaxHealth(),
                Font = new Font(pixel_font, 16, FontStyle.Regular)
            };
            Controls.Add(lblPlayerHealth);
            lblPlayerHealth.BringToFront();

            #endregion
        }

        private void Spawn_Test_Enemy()
        {
            //spawn enemy
            #region Spawn Enemy
            Enemies.Add(new Test_Enemy());
            #endregion
        }
        #endregion

        private void Update_Sprites(object sender, PaintEventArgs e)
        {
            //Draw the turn boxes
            FightBox.Draw(e.Graphics);
            ActBox.Draw(e.Graphics);
            ItemBox.Draw(e.Graphics);
            MercyBox.Draw(e.Graphics);

            //Draw enemy sprites and projectiles
            foreach (Enemy enemy in Enemies)
            {
                foreach (Sprite_Handler sprite in enemy.Get_Sprites())
                {
                    sprite.Draw(e.Graphics);
                }

                List<Projectile> projectiles = enemy.Get_Projectiles();
                foreach (Projectile projectile in projectiles)
                {
                    projectile.Draw(e.Graphics);
                }
            }

            //refresh the covers
            pbProjectileCover_Left.Refresh();
            pbProjectileCover_Right.Refresh();

            //Draw the arena box
            Rectangle arena_wall = new Rectangle(
                Arena_Hitbox.X - (int)Math.Ceiling((float)int_ARENA_WALL_SIZE/2),
                Arena_Hitbox.Y - (int)Math.Ceiling((float)int_ARENA_WALL_SIZE/2),
                Arena_Hitbox.Width + int_ARENA_WALL_SIZE,
                Arena_Hitbox.Height + int_ARENA_WALL_SIZE
            );
            e.Graphics.DrawRectangle(new Pen(Color.White, int_ARENA_WALL_SIZE), arena_wall);

            //Ony draws if the player is attacking
            if (player.Get_Box_Position() == -8)
            {
                //Draws the target and player attack sprites while player is attacking
                Target_Sprite.Draw(e.Graphics);
                Player_Attack_Sprite.Draw(e.Graphics);
                //Draws the slash sprite once player has attacked
                if (Draw_Slash_Sprite) Slash_Sprite.Draw(e.Graphics);
                if (Draw_Attack_Numbers)
                {
                    foreach (Sprite_Handler sprite in Attack_Numbers)
                    {
                        sprite.Draw(e.Graphics);
                    }
                }
            }

            //Draw the player health bar
            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), Player_Remaining_Health);
            e.Graphics.FillRectangle(new SolidBrush(Color.Red), Player_Lost_Health);

            //Draw player sprite last so it is on top
            player.Draw(e.Graphics);
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

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            //refresh the controls
            pbBackground.Refresh();

            //player turn dependant
            if (Player_Turn) Player_Turn_Systems();
            else Enemy_Turn_System();

            //run the systems
            JustPressed_System();
            Player_Movement_System();
            Damage_System();
            player.Immunity_System();
            Change_Turn_System();

            //debug label
            debug_label.Text = "Just Moved: " + player.Get_Just_Moved().ToString(); 
        }

        #region Player Turn Logic
        public void Player_Turn_Option_System()
        {
            int player_box_pos = player.Get_Box_Position();
            if (player_box_pos > -1)
            {
                if(Z_Pressed)
                {
                    Play_Sound_Effect("snd_select");
                    if(player_box_pos != 2 || player.Get_Inventory().Count > 0)
                    {
                        player.Set_Box_Position(player_box_pos - 4);
                        Update_Arena_Text();
                    }
                }
            }
            else if (player_box_pos > -5)
            {
                //if z is pressed then select the box
                if (Z_Pressed)
                {
                    if (player_box_pos == -4)
                    {
                        Play_Sound_Effect("snd_select");
                        player.Set_Box_Position(player_box_pos - 4);
                        Update_Arena_Text();
                        Thread Fight_Logic_Thread = new Thread (Fight_Logic);
                        Fight_Logic_Thread.Start();
                    }
                    else if (player_box_pos == -2 && !player.Wait_For_Input)
                    {
                        Play_Sound_Effect("snd_select");
                        player.Set_Box_Position(player_box_pos - 4);
                        Item_Logic();
                    }
                    else if (player_box_pos != -1 || player.Get_Option_Position() != 2)
                    {
                        player.Set_Selected_Option();
                        Play_Sound_Effect("snd_select");
                        player.Set_Box_Position(player_box_pos - 4);
                        Update_Arena_Text();
                    }
                    else
                    {
                        Flee_Logic();
                    }
                }
                else if (X_Pressed)
                {
                    player.Set_Box_Position(player_box_pos + 4);
                    player.Set_Option_Position(1);
                    player.Reset_Selected_Option();
                    Update_Arena_Text();
                }
            }
            else if (player_box_pos == -7)
            {
                if (Z_Pressed)
                {
                    //run act logic
                    Act_Logic();
                }
                else if (X_Pressed && !player.Wait_For_Input)
                {
                    player.Set_Option_Position(1);
                    player.Set_Box_Position(player_box_pos + 4);
                    player.Reset_Selected_Option();
                    Update_Arena_Text();
                }
            }
            else if (player_box_pos == -6 && Z_Pressed && player.Wait_For_Input)
            {
                //more item logic
                Item_Logic();
            }
            else if(player_box_pos == -5)
            {
                if(Z_Pressed)
                {
                    //run spare logic
                    Spare_Logic();
                }
                else if (X_Pressed)
                {
                    player.Set_Box_Position(player_box_pos + 4);
                    player.Set_Option_Position(1);
                    player.Reset_Selected_Option();
                    Update_Arena_Text();
                }
            }
        }

        #region Thread Logic
        private void Fight_Logic()
        {
            Player_Attack_Sprite.Set_Location(new PointF(Arena_Hitbox.X, Arena_Hitbox.Y));
            //if statement is needed for the actual logic, however there are multiple break conditions
            bool break_condition = false;
            Stopwatch stopwatch = new Stopwatch();
            //slower pcs sometimes attack immediately, this should fix it, allows z_pressed to be set false again
            Thread.Sleep(100);            
            while(!break_condition)
            {
                stopwatch.Restart();
                if(!Z_Pressed && Player_Attack_Sprite.Get_Location().X < Arena_Hitbox.X + Arena_Hitbox.Width - Player_Attack_Sprite.Get_Size().X)
                {
                    Player_Attack_Sprite.Move(1, 0);
                    while (stopwatch.ElapsedMilliseconds < 1.5) ;
                }
                else
                {
                    int damage;
                    Thread animate_projectile = new Thread(Animate_Player_Projectile);
                    animate_projectile.Start();
                    //if player attacked, calculate damage
                    if(Z_Pressed)
                    {
                        //apply undertale damage algorithm
                        //borrowed from u/spiceytomato at https://www.reddit.com/r/Underminers/comments/56xm7x/damage_calculation/
                        int projectile_center = (int)Player_Attack_Sprite.Get_Location().X + (int)(Player_Attack_Sprite.Get_Size().X / 2) - (int)Target_Sprite.Get_Location().X;
                        int distance = Math.Abs(projectile_center - (int)(Target_Sprite.Get_Size().X /2));
                        Random rand = new Random();
                        if(distance <= 12)
                        {
                            damage = (int)Math.Round((player.Get_Attack() - Enemies[0].Get_Defense() + rand.Next(2)) * 2.2);
                        }
                        else
                        {
                            damage = (int)Math.Round((player.Get_Attack() - Enemies[0].Get_Defense() + rand.Next(2)) * (1 - distance/(Target_Sprite.Get_Size().X / 2)) * 2);
                        }
                    }
                    //if player didn't attack, set damage to -1 as an indicator that player didn't attack
                    else
                    {
                        damage = -1;
                    }
                    //apply damage
                    if (damage > 0) Enemies[0].Set_Health(Enemies[0].Get_Health() - damage);

                    //animate the slash and damage numbers
                    Animate_Slash_Damage(damage);
                    
                    break_condition = true;
                }
            }
            stopwatch.Stop();
            player.Set_Box_Position(0);
            Player_Turn = false;
            Turn_Ended = true;
        }

        private void Act_Logic()
        {
            if(!player.Wait_For_Input)
            {
                Play_Sound_Effect("snd_select");
                player.Wait_For_Input = true;
                Update_Arena_Text();
            }
            else
            {
                player.Wait_For_Input = false;
                player.Set_Box_Position(1);
                Player_Turn = false;
                Turn_Ended = true;
            }
        }

        private void Item_Logic()
        {
            if(!player.Wait_For_Input)
            {
                Update_Arena_Text();
                player.Use_Item(player.Get_Option_Position() - 1);
                Play_Sound_Effect("snd_select");
                player.Wait_For_Input = true;
                Update_Player_Health_Stats();
            }
            else
            {
                player.Wait_For_Input = false;
                player.Set_Box_Position(2);
                Player_Turn = false;
                Turn_Ended = true;
            }
        }

        private void Spare_Logic()
        {
            if(Enemies[player.Get_Option_Position() - 1].Get_Mercy())
            {
                Play_Sound_Effect("snd_dumbvictory");
                Enemies.RemoveAt(player.Get_Option_Position() - 1);
            }
            else
            {
                Play_Sound_Effect("snd_select");
            }
            player.Set_Box_Position(3);
            Player_Turn = false;
            Turn_Ended = true;
        }

        private void Flee_Logic()
        {
            player.Set_Box_Position(-9);
            Update_Arena_Text();
            Play_Sound_Effect("snd_escaped");
            Thread flee_thread = new Thread(Animate_Fleeing);
            flee_thread.Start();
        }

        #endregion
        #endregion

        //all systems that run per tick
        #region Systems
        private void Player_Turn_Systems()
        {
            //update option boxes shouldn't be here for efficiency reasons but itll do for now
            Update_Turn_Boxes();
            Player_Turn_Option_System();
        }

        private void Player_Movement_System()
        {
            int player_box_pos = player.Get_Box_Position();
            PointF loc = player.Get_Location();
            PointF size = player.Get_Size();
            if (Player_Turn)
            {
                //checks if player box position is positieve
                if (player_box_pos > -1)
                {
                    //if positive, the player is navigating the option boxes
                    //if left arrow key is pressed move left, if right arrow key is pressed move right
                    //if left is pressed on the leftmost box, move to the rightmost box, vice versa
                    if (Left_Pressed)
                    {
                        Play_Sound_Effect("snd_movemenu");
                        if (player_box_pos > 0) player.Set_Box_Position(player_box_pos - 1);
                        else player.Set_Box_Position(3);
                    }
                    if (Right_Pressed)
                    {
                        Play_Sound_Effect("snd_movemenu");
                        if (player_box_pos < 3) player.Set_Box_Position(player_box_pos + 1);
                        else player.Set_Box_Position(0);
                    }
                    //sets new player box position before moving player off the value
                    player_box_pos = player.Get_Box_Position();
                    player.Set_Location(new PointF(49 + (player_box_pos * 150), loc.Y)); //boxes are 112 wide, with 38 pixels between -- guessed 50 pxiels, seems to have nailed it
                    player.Set_Center(new PointF(player.Get_Center().X, flt_FORM_HEIGHT - 27)); //boxes are 5 off the floor and 44 high
                }
                //attack occuring/item consumed, leave the screen
                else if (player_box_pos == -6 || player_box_pos == -8)
                {
                    //move them off the screen
                    player.Set_Location(new PointF(-100, -100));
                }
                else if (player_box_pos > -8 && !player.Wait_For_Input)
                {
                    //if the box pos is negative but above -5, the player has selected a box
                    int player_option_pos = player.Get_Option_Position();
                    //check how many options the player has
                    int num_options = 1;
                    if (Enemies.Count == 0)
                    {
                        num_options = 1;
                    }
                    else if (player_box_pos > -6 && player_box_pos < -2)
                    {
                        //display enemy list for fight, act or spare
                        num_options = Enemies.Count;
                    }
                    else if (player_box_pos == -2)
                    {
                        //pick an item
                        num_options = player.Get_Inventory().Count;
                    }
                    else if (player_box_pos == -1)
                    {
                        //spare or flee
                        num_options = 2;
                    }
                    else if (player_box_pos == -7)
                    {
                        //enemy selected to act on, select action
                        num_options = Enemies[player.Get_Selected_Option() - 1].Get_Actions().Length;
                    }
                    
                    if (Left_Pressed)
                    {
                        if (player_option_pos == 3 && 1 <= num_options) {player.Set_Option_Position(1); Play_Sound_Effect("snd_movemenu");}
                        else if (player_option_pos == 4 && 2 <= num_options) {player.Set_Option_Position(2); Play_Sound_Effect("snd_movemenu");}
                    }
                    if (Right_Pressed)
                    {
                        if (player_option_pos == 1 && 3 <= num_options) {player.Set_Option_Position(3); Play_Sound_Effect("snd_movemenu");}
                        else if (player_option_pos == 2 && 4 <= num_options) {player.Set_Option_Position(4); Play_Sound_Effect("snd_movemenu");}
                    }
                    if (Up_Pressed)
                    {
                        if (player_option_pos == 2 && 1 <= num_options) {player.Set_Option_Position(1); Play_Sound_Effect("snd_movemenu");}
                        else if (player_option_pos == 4 && 3 <= num_options) {player.Set_Option_Position(3); Play_Sound_Effect("snd_movemenu");}
                    }
                    if (Down_Pressed)
                    {
                        if (player_option_pos == 1 && 2 <= num_options) { player.Set_Option_Position(2); Play_Sound_Effect("snd_movemenu"); }
                        else if (player_option_pos == 3 && 4 <= num_options) { player.Set_Option_Position(4); Play_Sound_Effect("snd_movemenu"); }
                    }

                    //update the player's option position
                    player_option_pos = player.Get_Option_Position();

                    //move player to the selected option
                    switch (player_option_pos)
                    {
                        case 1:
                            //the real location of this has been mined, the rest will be updated later, just old text for now that wont actually get used until items at least
                            player.Set_Location(new PointF(65, 277));
                            break;
                        case 2:
                            //other values haven't been mined but are calculated based on the mined location and suitable for the project
                            player.Set_Location(new PointF(65, 332));
                            break;
                        case 3:
                            player.Set_Location(new PointF(345, 277));
                            break;
                        case 4:
                            player.Set_Location(new PointF(345, 332));
                            break;
                        default:
                            break;
                    }
                }
                //exclude fleeing from the moving system
                else if (player_box_pos == -9) ;
                else
                {
                    //no idea what happened if it gets this bad, but leave to screen to tell me
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
                if (loc.X + x < Arena_Hitbox.Left ) x = Arena_Hitbox.Left - loc.X;
                if (loc.X + size.X + x > Arena_Hitbox.Right ) x = Arena_Hitbox.Right - loc.X - size.X;
                if (loc.Y + y < Arena_Hitbox.Top) y = Arena_Hitbox.Top - loc.Y;
                if (loc.Y + size.Y + y > Arena_Hitbox.Bottom) y = Arena_Hitbox.Bottom - loc.Y - size.Y;


                //moves player final x and y values if not and manages just pressed attribute
                if(x != 0 || y != 0)
                {
                    player.Move(x, y);
                    player.Set_Just_Moved(true);
                }
                else
                {
                    player.Set_Just_Moved(false);
                }
            }
        }        

        private void Change_Turn_System()
        {
            //handles calling new turns so there are no issues with threads
            if (Turn_Ended)
            {
                Check_Enemies();
                if(Enemies.Count == 0)
                {
                    if(player.Get_Box_Position() == 3)
                    {
                        Thread box_thread = new Thread(() => MessageBox.Show("You won! \nYou spared the dummy!"));
                        box_thread.Start();
                    }
                    else if(player.Get_Box_Position() == 0)
                    {
                        Thread box_thread = new Thread(() => MessageBox.Show("You won! \nYou killed the dummy!"));
                        box_thread.Start();
                    }
                    Close();
                }
                else
                {
                    if(Player_Turn)
                    {
                        Player_Turn_Start();
                    }
                    else
                    {
                        Enemy_Turn_Start();
                    }
                    Turn_Ended = false;
                }
            }
        }
        private void Enemy_Turn_System()
        {
            Enemies[0].Run_Turn(Enemy_Turn_Timer);
            Enemy_Turn_Timer++;
        }
        
        private void Damage_System()
        {
            if (player.Get_Immunity() == 0)
            {
                //runs all the time in case for some cheeky player turn attacks ;)
                Rectangle player_hitbox = player.Get_Hitbox();
                foreach (Enemy enemy in Enemies)
                {
                    //game shits itself if you foreach the real list and modify it part way through
                    List<Projectile> projectiles_clone = new List<Projectile>(enemy.Get_Projectiles());
                    foreach (Projectile projectile in projectiles_clone)
                    {
                        if(player_hitbox.IntersectsWith(projectile.Get_Hitbox()))
                        {
                            switch (projectile.Get_Colour())
                            {
                                case Projectile_Colour.White:
                                    Projectile_Hit(enemy, projectile);
                                    break;
                                case Projectile_Colour.Blue:
                                    if (player.Get_Just_Moved()) Projectile_Hit(enemy, projectile);
                                    break;
                                case Projectile_Colour.Orange:
                                    if (!player.Get_Just_Moved()) Projectile_Hit(enemy, projectile);
                                    break;
                                default:
                                    break;
                            }
                            
                        }
                    }
                }
            }
        }

        #endregion

        //all funcitons that update something on the form
        #region Updating Functions
        private void Projectile_Hit(Enemy enemy, Projectile projectile)
        {
            Play_Sound_Effect("snd_hurt1");
            player.Hit();
            player.Set_Health(player.Get_Health() - projectile.Get_Damage());
            Update_Player_Health_Stats();
            Check_Player();
            enemy.Get_Projectiles().Remove(projectile);
        }
        private void Update_Arena_Hitbox()
        {
            //redefines the arena box
            if (Player_Turn)
            {
                Arena_Hitbox.X = int_PLAYER_TURN_ARENA_X;
                Arena_Hitbox.Width = int_PLAYER_TURN_ARENA_WIDTH;
                Arena_Hitbox.Y = int_PLAYER_TURN_ARENA_Y;
                Arena_Hitbox.Height = int_PLAYER_TURN_ARENA_HEIGHT;
            }
            else
            {
                Arena_Hitbox.X = int_DEFAULT_ARENA_X;
                Arena_Hitbox.Width = int_DEFAULT_ARENA_WIDTH;
                Arena_Hitbox.Y = int_DEFAULT_ARENA_Y;
                Arena_Hitbox.Height = int_DEFAULT_ARENA_HEIGHT;
            }
        }
        public void Update_Arena_Text()
        {
            //clear all
            lblArenaGeneral.Text = "";
            lblArenaOpt1.Text = "";
            lblArenaOpt2.Text = "";
            lblArenaOpt3.Text = "";
            lblArenaOpt4.Text = "";
            lblArenaOpt1.ForeColor = Color.White;
            lblArenaOpt2.ForeColor = Color.White;
            lblArenaOpt3.ForeColor = Color.White;
            lblArenaOpt4.ForeColor = Color.White;
            //then update
            if (Player_Turn && Enemies.Count != 0)
            {
                float box_pos = player.Get_Box_Position();
                if (box_pos > -1) 
                {
                    //implement foreach later
                    lblArenaGeneral.Location = new Point(65, 270);
                    lblArenaGeneral.Text = "* " + Enemies[0].Choose_Arena_Text();
                }
                else if (box_pos == -4 || box_pos == -3 || box_pos == -5)
                {
                    lblArenaOpt1.Text = "* " + Enemies[0].Get_Name();
                    if (Enemies[0].Get_Mercy()) lblArenaOpt1.ForeColor = Color.Yellow;
                    if (Enemies.Count > 1)
                    {
                        lblArenaOpt2.Text = "* " + Enemies[1].Get_Name();
                        if (Enemies[1].Get_Mercy()) lblArenaOpt2.ForeColor = Color.Yellow;
                    }
                    if (Enemies.Count > 2)
                    {
                        lblArenaOpt3.Text = "* " + Enemies[2].Get_Name();
                        if (Enemies[2].Get_Mercy()) lblArenaOpt3.ForeColor = Color.Yellow;
                    }
                    if (Enemies.Count > 3)
                    {
                        lblArenaOpt4.Text = "* " + Enemies[3].Get_Name();
                        if (Enemies[3].Get_Mercy()) lblArenaOpt4.ForeColor = Color.Yellow;
                    }
                }
                else if (box_pos == -2)
                {
                    List<Item> items = player.Get_Inventory();
                    if(items.Count > 0)
                    {
                        lblArenaOpt1.Text = "* " + items[0].Get_Name();
                        if (items.Count > 1) lblArenaOpt2.Text = "* " + items[1].Get_Name();
                        if (items.Count > 2) lblArenaOpt3.Text = "* " + items[2].Get_Name();
                        if (items.Count > 3) lblArenaOpt4.Text = "* " + items[3].Get_Name();
                    }
                }
                else if (box_pos == -1)
                {
                    lblArenaOpt1.Text = "* Spare";
                    lblArenaOpt2.Text = "* Flee";
                }
                //enemy selected to fight
                else if (box_pos == -8) lblArenaGeneral.Text = "";
                //enemy selected to act
                else if (box_pos == -7)
                {
                    if (!player.Wait_For_Input)
                    {
                        string[] actions = Enemies[player.Get_Selected_Option() - 1].Get_Actions();
                        lblArenaOpt1.Text = "* " + actions[0];
                        if (actions.Length > 1) lblArenaOpt2.Text = "* " + actions[1];
                        if (actions.Length > 2) lblArenaOpt3.Text = "* " + actions[2];
                        if (actions.Length > 3) lblArenaOpt4.Text = "* " + actions[3];
                    }
                    else
                    {
                        lblArenaGeneral.Text = Enemies[player.Get_Selected_Option() - 1].Select_Action(player.Get_Option_Position());
                    }
                }
                //item selected to consume
                else if (box_pos == -6)
                {
                    lblArenaGeneral.Text = player.Get_Use_Item_Text(player.Get_Option_Position() - 1);
                }
                else if (box_pos == -9)
                {
                    Random rand = new Random();
                    string[] flee_text = new string[] { 
                        "* Escaped...", 
                        "* Coward.", 
                        "* Really?",
                        "* Run Forest Run",
                        "* What did I even make this project for...",
                        "* You feel like you're going to have a bad time... \n* Not playing this sick nasty cool game!"};
                    lblArenaGeneral.Text = flee_text[rand.Next(flee_text.Length)];
                }
            }
        }
        private void Update_Turn_Boxes()
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

        private void Update_Player_Health_Stats()
        {
            Player_Remaining_Health = new Rectangle(266, (int)flt_FORM_HEIGHT - 76, player.Get_Health() + 1, 16);
            Player_Lost_Health = new Rectangle(266 + player.Get_Health() + 1, (int)flt_FORM_HEIGHT - 76, player.Get_MaxHealth() - player.Get_Health() + 1, 16);
            lblPlayerHealth.Text = player.Get_Health() + " / " + player.Get_MaxHealth();        
        }

        private void Player_Turn_Start()
        {
            Update_Arena_Hitbox();
            Update_Arena_Text();
            player.Reset_Selected_Option();
            player.Set_Option_Position(1);
            pbProjectileCover_Left.SendToBack();
            pbProjectileCover_Right.SendToBack();
            player.Set_Just_Moved(false);
        }

        private void Enemy_Turn_Start()
        {
            Update_Arena_Text();
            Update_Arena_Hitbox();
            player.Set_Center(new PointF(Arena_Hitbox.X + Arena_Hitbox.Width / 2, Arena_Hitbox.Y + Arena_Hitbox.Height / 2));
            //foreach later
            Enemies[0].Select_Turn();
            Enemy_Turn_Timer = 0;
            pbProjectileCover_Left.BringToFront();
            pbProjectileCover_Right.BringToFront();
            lblPlayerName.BringToFront();
            lblPlayerLevel.BringToFront();
            lblPlayerHPText.BringToFront();
            lblPlayerHealth.BringToFront();
            player.Set_Just_Moved(false);
        }

        private void Check_Enemies()
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.Get_Health() == 0)
                {
                    Play_Sound_Effect("snd_vaporized");
                    Enemies.Remove(enemy);
                }
            }
        }

        private void Check_Player()
        {
            //if player dead, game over man
            if (player.Get_Health() == 0)
            {
                Animate_Player_Death();
                Thread box_thread = new Thread(() => MessageBox.Show("You died!"));
                box_thread.Start();
                Close();
            }
        }
        
        #endregion

        #region Animations
        private void Animate_Player_Projectile()
        {
            //fight thread waits 1500ms before continuing
            //increments sprite map 15 times with 100ms intervals and then defaults the value
            for(int i = 0; i < 20; i++)
            {
                Thread.Sleep(100);
                Player_Attack_Sprite.Next();
            }
            Player_Attack_Sprite.Set_Row_Col(new PointF(0, 0));
        }

        private void Animate_Slash_Damage(int damage)
        {
            if(damage > -1)
            {
                //play the slash noise
                Play_Sound_Effect("snd_laz");
                //use offset to center the slash on the enemy
                int offest_x = (int)(Enemies[0].Get_Sprites()[0].Get_Size().X - Slash_Sprite.Get_Size().X) / 2;
                int offest_y = (int)(Enemies[0].Get_Height() - Slash_Sprite.Get_Size().Y) / 2;
                float loc_x = Enemies[0].Get_Sprites()[0].Get_Location().X + offest_x;
                float loc_y = Enemies[0].Get_Sprites()[0].Get_Location().Y + offest_y;
                Slash_Sprite.Set_Location(new PointF(loc_x, loc_y));

                //draw sprite, increment sprite map every 150ms to complete animation
                Draw_Slash_Sprite = true;
                for (int i = 0; i < 6; i++)
                {
                    Thread.Sleep(100);
                    Slash_Sprite.Next();
                }
                Draw_Slash_Sprite = false;
            }

            //define the damage number sprites
            if(damage > 0)
            {
                //define the damage number sprites
                Bitmap sheet = Resource1.Attack_Effects_Sprite_Sheet;
                string background_colour = "#FFC386FF";
                PointF size = new PointF(36, 38);
                PointF rows_cols = new PointF(1, 10);
                PointF offset = new PointF(415, 174);
                PointF padding = new PointF(5, 0);
                int offest_y = (int)(Enemies[0].Get_Height() - size.Y) / 2;
                PointF loc = new PointF(0,Enemies[0].Get_Sprites()[0].Get_Location().Y + offest_y);

                //adds each digit of the damage number to the list and set the Row_Col to the correct digit
                int List_Index = 0;
                foreach(char c in damage.ToString())
                {
                    Attack_Numbers.Add(new Sprite_Handler(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc));
                    Attack_Numbers[List_Index].Set_Row_Col(new PointF(0, (int)Char.GetNumericValue(c)));
                    List_Index += 1;
                }

                //set x location of each digit
                int num_offset_x = (int)(Enemies[0].Get_Sprites()[0].Get_Size().X - Attack_Numbers[0].Get_Size().X * Attack_Numbers.Count);
                foreach (Sprite_Handler sprite in Attack_Numbers)
                {
                    sprite.Set_Location(new PointF(Enemies[0].Get_Sprites()[0].Get_Location().X + num_offset_x + Enemies[0].Get_Sprites()[0].Get_Size().X / 2, sprite.Get_Location().Y));
                    num_offset_x += (int)sprite.Get_Size().X;
                }

                //play the sound
                Play_Sound_Effect("snd_damage");
            }
            else
            {
                //define the miss sprite
                Bitmap sheet = Resource1.Attack_Effects_Sprite_Sheet;
                string background_colour = "#FFC386FF";
                PointF size = new PointF(124, 38);
                PointF offset = new PointF(825, 174);

                //center the miss sprite above the enemy
                int num_offset_x = (int)(Enemies[0].Get_Sprites()[0].Get_Size().X - (int)size.X )/ 2;
                PointF loc = new PointF(Enemies[0].Get_Sprites()[0].Get_Location().X + num_offset_x, Enemies[0].Get_Sprites()[0].Get_Location().Y - size.Y - 10);
                Attack_Numbers.Add(new Sprite_Handler(sheet, ColorTranslator.FromHtml(background_colour), size, offset, loc));
            }

            //draw the sprites
            Draw_Attack_Numbers = true;

            //busy loop until the player is done attacking
            Thread.Sleep(800);

            Draw_Attack_Numbers = false;

            //default values
            Slash_Sprite.Set_Row_Col(new PointF(0, 0));
            //check if null first, if not it shits itself
            if (Attack_Numbers != null) Attack_Numbers.Clear();
        }

        private void Animate_Fleeing()
        {
            //redfine player sprite to walk
            player.New_Offset(new PointF(7, 79));
            player.Set_Size(new PointF(16, 24));
            player.New_Rows_Cols(new PointF(1, 2));
            player.New_Padding(new PointF(8, 0));
            while (player.Get_Location().X > -16)
            {
                Thread.Sleep(50);
                //walk
                player.Move(-5, 0);
                player.Next();
            }
            //close the form
            Close();
        }

        private void Animate_Player_Death()
        {
            //break it
            player.Set_Size(new PointF(20, 16));
            player.New_Offset(new PointF(7, 114));
            //paint it
            this.Refresh();
            Play_Sound_Effect("snd_break1");
            Thread.Sleep(1000);
            Play_Sound_Effect("snd_break2");
        }
        #endregion

        #region Sound
        private void Play_Sound_Effect(string sound_name)
        {
            SoundPlayer sound = new SoundPlayer("Resources/Sound_Effects/" + sound_name + ".wav");
            sound.Play();
        }
        #endregion
    }
}