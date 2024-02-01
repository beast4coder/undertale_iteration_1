namespace undertale_iteration_1
{
    internal class Test_Enemy : Enemy
    {
        public Test_Enemy()
        {
            Name = "Test_Enemy";
            Health = 100;
            Damage = 5;
            Defense = 5;
            Sprites = new Sprite_Handler[1];
            Turn_Selector = 0;
            
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
            switch(Turn_Selector)
            {
                case 0:
                    Filler_Turn();
                    break;
                default:
                    break;
            }
            GameForm.Turn_Ended = true;
            GameForm.Player_Turn = true;
        }
        public void Filler_Turn()
        {
            Thread.Sleep(1000);
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
                default:
                    break;
            }
            return output_text;
        }
        public string Filler_Action()
        {
            return "Filler text goes here";
        }
        #endregion
    }
}

