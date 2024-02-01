//using statements go here

using System.Drawing.Text;
using System.Threading;
using System.Diagnostics;
using System.Media;

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
        const int int_PLAYER_TURN_ARENA_X = 38;
        const int int_PLAYER_TURN_ARENA_Y = 254;
        const int int_PLAYER_TURN_ARENA_WIDTH = 565;
        const int int_PLAYER_TURN_ARENA_HEIGHT = 130;
        const int int_DEFAULT_ARENA_X = 230;
        const int int_DEFAULT_ARENA_Y = 199;
        const int int_DEFAULT_ARENA_WIDTH = 180;
        const int int_DEFAULT_ARENA_HEIGHT = 185;
        Sprite_Handler FightBox;
        Sprite_Handler ActBox;
        Sprite_Handler ItemBox;
        Sprite_Handler MercyBox;
        Sprite_Handler Target_Sprite;

        //turn variables
        public static bool Player_Turn = false;
        public static bool Turn_Ended = true;

        //player
        Player player;
        Sprite_Handler Player_Attack_Sprite;
        Sprite_Handler Slash_Sprite;
        bool Draw_Slash_Sprite = false;
        List<Sprite_Handler> Attack_Numbers = new List<Sprite_Handler>();
        bool Draw_Attack_Numbers = false;
        

        //enemies
        List<Enemy> Enemies = new List<Enemy>();

        //controls
        Label lblPlayerHealth;
        Label lblArenaText;

        Label debug_label;

        //consts
        public const float flt_PLAYER_SPEED = 3f;
        public const float flt_FORM_WIDTH = 640f;
        public const float flt_FORM_HEIGHT = 480f;

        //threads
        Thread Fight_Logic_Thread;
        Thread Act_Logic_Thread;
        Thread Item_Logic_Thread;
        Thread Mercy_Logic_Thread;

        //time counter
        //int int_time_counter = 0;

        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
            Arena_Setup();
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
            player = new Player(sheet, ColorTranslator.FromHtml(background_colour), size, rows_cols, offset, padding, loc, "TEST1");
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

            //define arena size, objects and controls
            #region Set Arena Size
            size = new PointF(int_DEFAULT_ARENA_WIDTH, int_DEFAULT_ARENA_HEIGHT);
            loc = new PointF(int_DEFAULT_ARENA_X, int_DEFAULT_ARENA_Y);
            Arena_Hitbox = new Rectangle((int)loc.X, (int)loc.Y, (int)size.X, (int)size.Y);
            #endregion

            #region Spawn Arena Text Box
            //make text appear in the top left corner of the arena
            Size font_size = new Size(20, 20);

            //c# doesn;t let you do fonts nicely, cannot add file straight to font family
            //font file added to a 'private font collection', font pulled from that as a font family type
            //only then usable
            PrivateFontCollection my_fonts = new PrivateFontCollection();
            my_fonts.AddFontFile("Resources/8bitoperator_jve.ttf");
            FontFamily pixel_font = my_fonts.Families[0];

            lblArenaText = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(65, 270),
                Name = "lblArenaText",
                Size = font_size,
                TabIndex = 1,
                Text = "",
                Font = new Font(pixel_font, 18, FontStyle.Regular)
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
            loc = new PointF(int_PLAYER_TURN_ARENA_X + 1, int_PLAYER_TURN_ARENA_Y + 1);
            Target_Sprite = new Sprite_Handler(sheet, size, offset, loc);
            #endregion

            //spawn enemy
            #region Spawn Enemy
            Enemies.Add(new Test_Enemy());
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
            Rectangle arena_wall = new Rectangle(
                Arena_Hitbox.X - (int)Math.Ceiling((float)int_ARENA_WALL_SIZE/2),
                Arena_Hitbox.Y - (int)Math.Ceiling((float)int_ARENA_WALL_SIZE/2),
                Arena_Hitbox.Width + int_ARENA_WALL_SIZE,
                Arena_Hitbox.Height + int_ARENA_WALL_SIZE
            );
            e.Graphics.DrawRectangle(new Pen(Color.White, int_ARENA_WALL_SIZE), arena_wall);

            //Draw the option boxes
            FightBox.Draw(e.Graphics);
            ActBox.Draw(e.Graphics);
            ItemBox.Draw(e.Graphics);
            MercyBox.Draw(e.Graphics);

            //Draw enemy sprites
            foreach (Enemy enemy in Enemies)
            {
                foreach (Sprite_Handler sprite in enemy.Get_Sprites())
                {
                    sprite.Draw(e.Graphics);
                }
            }

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

        #region Player Turn Logic
        public void Player_Turn_Option_System()
        {
            int player_box_pos = player.Get_Box_Position();
            if (player_box_pos > -1 && Z_Pressed)
            {
                Play_Sound_Effect("snd_select");
                player.Set_Box_Position(player_box_pos - 4);
                Update_Arena_Text();
            }
            else if (player_box_pos > -5)
            {
                //if z is pressed then select the box
                if (Z_Pressed)
                {
                    switch (player_box_pos)
                    {
                        case -4:
                            Play_Sound_Effect("snd_select");
                            player.Set_Box_Position(player_box_pos - 4);
                            Update_Arena_Text();
                            Fight_Logic_Thread = new Thread (Fight_Logic);
                            Fight_Logic_Thread.Start();
                            break;
                        case -3:
                            Play_Sound_Effect("snd_select");
                            player.Set_Box_Position(player_box_pos - 4);
                            Update_Arena_Text();
                            Act_Logic_Thread = new Thread(Act_Logic);
                            Act_Logic_Thread.Start();
                            break;
                        case -2:
                            Play_Sound_Effect("snd_select");
                            player.Set_Box_Position(player_box_pos - 4);
                            Update_Arena_Text();
                            Item_Logic_Thread = new Thread(Item_Logic);
                            Item_Logic_Thread.Start();
                            break;
                        case -1:
                            Play_Sound_Effect("snd_select");
                            player.Set_Box_Position(player_box_pos - 4);
                            Update_Arena_Text();
                            Mercy_Logic_Thread = new Thread(Mercy_Logic);
                            Mercy_Logic_Thread.Start();
                            break;
                        default:
                            break;
                    }
                }
                else if (X_Pressed)
                {
                    player.Set_Box_Position(player_box_pos + 4);
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
                        int projectile_center = (int)Player_Attack_Sprite.Get_Location().X + (int)(Player_Attack_Sprite.Get_Size().X / 2);
                        int distance = Math.Abs(projectile_center - (int)(Target_Sprite.Get_Size().X /2) + (int)Target_Sprite.Get_Location().X);
                        Random rand = new Random();
                        if(distance <= 12)
                        {
                            damage = (int)Math.Round((player.Get_Attack() - Enemies[0].Get_Defense() + rand.Next(3)) * 2.2);
                        }
                        else
                        {
                            damage = (int)Math.Round((player.Get_Attack() - Enemies[0].Get_Defense() + rand.Next(3)) * (1 - distance/Target_Sprite.Get_Size().X) * 2);
                        }
                    }
                    //if player didn't attack, set damage to -1 as an indicator that player didn't attack
                    else
                    {
                        damage = -1;
                    }
                    //apply damage
                    if (damage > 0) Enemies[0].Set_Health(Enemies[0].Get_Health() - damage);
                    debug_label.Text = Enemies[0].Get_Health().ToString();

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
            player.Set_Box_Position(1);
            Player_Turn = false;
            Turn_Ended = true;
        }

        private void Item_Logic()
        {
            //not implemented yet, probs not this iteration, display text in the meantime
            Thread.Sleep(800);
            player.Set_Box_Position(2);
            Player_Turn = true;
            Turn_Ended = true;
        }

        private void Mercy_Logic()
        {
            player.Set_Box_Position(3);
            Player_Turn = false;
            Turn_Ended = true;
        }
        #endregion
        #endregion

        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {
            //refresh the controls
            pbBackground.Refresh();
            lblArenaText.Refresh();

            //lblPlayerHealth.Text = player.GetHealth() + "/20";

            //player turn exclusives ;)
            if (Player_Turn) Player_Turn_Systems();

            //run the systems
            JustPressed_System();
            Turn_System();
            Player_Movement_System();
            /*Damage_System();*/

            //increment the timer
            //int_time_counter++;
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
            if (Player_Turn)
            {
                float pos = player.Get_Box_Position();
                if (pos > -1 && pos < 4) 
                {
                    //implement foreach later
                    lblArenaText.Location = new Point(65, 270);
                    lblArenaText.Text = "* " + Enemies[0].Choose_Arena_Text();
                }
                else if (pos == -4 || pos == -3 || pos == -1)
                {
                    //implement foreach later
                    lblArenaText.Location = new Point(101, 270);
                    lblArenaText.Text = "* " + Enemies[0].Get_Name();
                }
                else if (pos == -2) lblArenaText.Text = "* CONGRATULATIONS!!! \n* YOU'VE FOUND A BUG!!!";//implement later
                else if (pos == -8) lblArenaText.Text = "";
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
                else if (player_box_pos > -5)
                {
                    //if the box pos is negative but above -5, the player has selected a box
                    int player_option_pos = player.Get_Option_Position();
                    //check how many options the player has
                    //for current iteration, only one enemy is implemented and no items for now, a foreach will be implemented later
                    int num_options = 1;
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
                if (loc.X + x < Arena_Hitbox.Left ) x = Arena_Hitbox.Left - loc.X;
                if (loc.X + size.X + x > Arena_Hitbox.Right ) x = Arena_Hitbox.Right - loc.X - size.X;
                if (loc.Y + y < Arena_Hitbox.Top) y = Arena_Hitbox.Top - loc.Y;
                if (loc.Y + size.Y + y > Arena_Hitbox.Bottom) y = Arena_Hitbox.Bottom - loc.Y - size.Y;


                //moves player final x and y values
                player.Move(x, y);
            }
        }        

        private void Turn_System()
        {
            //handles calling new turns so there are no issues with threads
            if (Turn_Ended)
            {
                if(Player_Turn)
                {
                    Player_Turn_Start();
                }
                else
                {
                    Update_Arena_Text();
                    Update_Arena_Hitbox();
                    player.Set_Center(new PointF(Arena_Hitbox.X + Arena_Hitbox.Width / 2, Arena_Hitbox.Y + Arena_Hitbox.Height / 2));
                    //foreach later
                    Thread turn_thread = new Thread(Enemies[0].Select_Turn);
                    turn_thread.Start();
                }
                Turn_Ended = false;
            }
        }
        #endregion

        private void Player_Turn_Start()
        {
            Update_Arena_Hitbox();
            Update_Arena_Text();
        }

        #region Animations
        private void Animate_Player_Projectile()
        {
            //fight thread waits 1500ms before continuing
            //increments sprite map 15 times with 100ms intervals and then defaults the value
            for(int i = 0; i < 15; i++)
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
                    Thread.Sleep(150);
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
            //play the sound
            Play_Sound_Effect("snd_damage");
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