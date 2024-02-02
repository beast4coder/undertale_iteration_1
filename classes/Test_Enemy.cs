using System.Xml;

namespace undertale_iteration_1
{
    internal class Test_Enemy : Enemy
    {
        private int Happiness = 0;
        public Test_Enemy()
        {
            Name = "Test_Enemy";
            Health = 100;
            Damage = 5;
            Defense = 5;
            Sprites = new Sprite_Handler[1];
            Turn_Selector = 0;
            Projectiles = new List<Projectile>();
            
            //define dummy sprite
            #region Dummy Sprite
            Bitmap sheet = Resource1.Napstablook_and_Dummies_Sheet;
            Color background_col = ColorTranslator.FromHtml("#FFC386FF");
            PointF size = new PointF(39, 52);
            PointF rows_cols = new PointF(1, 2);
            PointF offset = new PointF(305, 21);
            PointF padding = new PointF(1, 0);
            PointF loc = new PointF((GameForm.flt_FORM_WIDTH - size.X - 16) / 2, 90);
            Sprites[0] = new Sprite_Handler(sheet, background_col, size, rows_cols, offset, padding, loc);
            Sprites[0].Set_Scale(1.5f);
            #endregion
            //define arena text messages           
            #region Arena Text Messages
            Arena_Text = new string[] {
                "THIS IS FILLER TEXT",
                "EVEN MORE FILLER TEXT",
                "Regular case filler text.",
                "Your mum",
            };
            #endregion
        
            //define actions
            #region Actions
            Actions = new string[] {
                "Check",
                "Filler",
                "Compliment"
            };
            #endregion
        
            //f l a v o u r
            Flavour_Text = "This is the test enemy. \nTHE test enemy. Yep. That's right.";
        }

        public override string Choose_Arena_Text()
        {
            //selects arena text
            //this enemy just chooses randomly from predefined options
            Random rand = new Random();
            int msg_num = rand.Next(Arena_Text.Length);
            return Arena_Text[msg_num];
        }
    
        #region Turns
        public override void Select_Turn()
        {
            //implement code here to select between multiple turns
            //only one turn in current iteration, defined in constructor
            Turn_Running = Turn_Selector;
        }

        public override void Run_Turn(int turn_clock)
        {
            switch(Turn_Running)
            {
                case 0:
                    Test_Turn(turn_clock);
                    break;
                default:
                    break;
            }
        }

        public void End_Turn()
        {
            Turn_Running = -1;
            GameForm.Turn_Ended = true;
            GameForm.Player_Turn = true;
        }
        public void Test_Turn(int turn_clock)
        {
            //spawn projectile at start of turn
            if(turn_clock == 0)
            {
                Random rand = new Random();
                Projectiles.Add(new Test_Projectile(Damage, new PointF(GameForm.int_DEFAULT_ARENA_X - 33, GameForm.int_DEFAULT_ARENA_Y + rand.Next(GameForm.int_DEFAULT_ARENA_HEIGHT -30))));
            }
            //tick every 20ms, move the projectile every tick once 500ms has passed
            if(Projectiles.Count > 0 && turn_clock > 25)
            {
                Projectiles[0].Move();
            }
            //if projectile is offscreen, remove it and end turn
            if(Projectiles.Count > 0 && Projectiles[0].Get_Location().X - Projectiles[0].Get_Size().X > GameForm.int_DEFAULT_ARENA_X + GameForm.int_DEFAULT_ARENA_WIDTH)
            {
                Projectiles.RemoveAt(0);
            }
            if(Projectiles.Count == 0)
            {
                End_Turn();
            }
        }
        #endregion
    
        #region Actions
        public override string Select_Action(int pAction)
        {
            string output_text = "";
            switch(pAction)
            {
                case 1:
                    output_text = Check_Action();
                    break;
                case 2:
                    output_text = Filler_Action();
                    break;
                case 3:
                    output_text = Compliment_Action();
                    break;
                default:
                    break;
            }
            return output_text;
        }
        private string Filler_Action()
        {
            return "* Filler text goes here";
        }
        private string Compliment_Action()
        {
            string output_text;
            if(Happiness <= 0) output_text = "* You complimented the Test Enemy. \n* It didn't respond.";
            else output_text = "* You vocally marvel at this genius implementation \n* Deep in the dev's soul... \n* You feel something resonate within him.";
            Happiness += 1;
            if(Happiness >= 2) Mercy = true;
            return output_text;
        }
        #endregion
    }

    #region Projectiles
    internal class Test_Projectile : Projectile
    {
        private int Speed = 5;
        public Test_Projectile(int pDamage, PointF pLoc)
        : base(
            //define the projectile
            Resource1.Napstablook_and_Dummies_Sheet,
            ColorTranslator.FromHtml("#FFC386FF"),
            new PointF(30, 10),
            new PointF(1, 1),
            new PointF(142, 302),
            new PointF(0, 0),
            pLoc,
            pDamage
        ) {}
        public override void Move()
        {
            Location.X += Speed;
        }
    }

    #endregion
}

