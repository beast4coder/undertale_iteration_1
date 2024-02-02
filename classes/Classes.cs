namespace undertale_iteration_1
{
    internal class Sprite_Handler
    {
        protected Bitmap Sheet;
        protected Bitmap Sprite;
        protected PointF Size;
        protected PointF Ttl_Rows_Cols;
        protected PointF Offset;
        protected PointF Padding;
        protected PointF Location;
        protected PointF Center;
        protected PointF Crnt_Row_Col;
        protected Rectangle SpriteArea;
        protected float Scale = 1f;

        //constructor for sprite handles with more than one sprite
        public Sprite_Handler(Bitmap pSheet, Color pBackground_Colour, PointF pSize, PointF pRows_Cols, PointF pOffset, PointF pPadding, PointF pLoc)
        {
            Sheet = Process_SpriteSheet(pSheet, pBackground_Colour);
            Size = pSize;
            Ttl_Rows_Cols = pRows_Cols;
            Offset = pOffset;
            Padding = pPadding;
            Location = pLoc;
            Crnt_Row_Col = new PointF(0, 0);
            Center = new PointF(Location.X + Size.X / 2, Location.Y + Size.Y / 2);
            Update_SpriteArea();
        }
        //constructor for sprite handles with more than one sprite and a transparent background
        public Sprite_Handler(Bitmap pSheet, PointF pSize, PointF pRows_Cols, PointF pOffset, PointF pPadding, PointF pLoc)
        {
            Sheet = pSheet;
            Size = pSize;
            Ttl_Rows_Cols = pRows_Cols;
            Offset = pOffset;
            Padding = pPadding;
            Location = pLoc;
            Crnt_Row_Col = new PointF(0, 0);
            Center = new PointF(Location.X + Size.X / 2, Location.Y + Size.Y / 2);
            Update_SpriteArea();
        }
        //constructor for sprite handles with only one sprite
        public Sprite_Handler(Bitmap pSheet, Color pBackground_Colour, PointF pSize, PointF pOffset, PointF pLoc)
        {
            Sheet = Process_SpriteSheet(pSheet, pBackground_Colour);
            Size = pSize;
            Ttl_Rows_Cols = new PointF(1, 1);
            Offset = pOffset;
            Padding = new PointF(0, 0);
            Location = pLoc;
            Crnt_Row_Col = new PointF(0, 0);
            Center = new PointF(Location.X + Size.X / 2, Location.Y + Size.Y / 2);
            Update_SpriteArea();
        }
        //constructor for sprite handles with only one sprite and a transparent background
        public Sprite_Handler(Bitmap pSheet, PointF pSize, PointF pOffset, PointF pLoc)
        {
            Sheet = pSheet;
            Size = pSize;
            Ttl_Rows_Cols = new PointF(1, 1);
            Offset = pOffset;
            Padding = new PointF(0, 0);
            Location = pLoc;
            Crnt_Row_Col = new PointF(0, 0);
            Center = new PointF(Location.X + Size.X / 2, Location.Y + Size.Y / 2);
            Update_SpriteArea();
        }
        public void Draw(Graphics g)
        {
            //scale the sprite
            int width = (int)(Size.X * Scale);
            int height = (int)(Size.Y * Scale);
            //draw the sprite
            g.DrawImage(Sprite, new Rectangle((int)Location.X, (int)Location.Y, width, height));
        }
        #region Sprite Sheet Manipulation
        private Bitmap Process_SpriteSheet(Bitmap pSheet, Color pTarget_Colour)
        {
            //checks for pixels in the sheet that are supposed to be transparent and makes them transparent
            //loops through every pixel in the sheet
            for (int x = 0; x < pSheet.Width; x++)
            {
                for (int y = 0; y < pSheet.Height; y++)
                {
                    //if the pixel colour matches the hex code, make it transparent
                    if (pSheet.GetPixel(x, y) == pTarget_Colour)
                    {
                        pSheet.SetPixel(x, y, Color.Transparent);
                    }
                }
            }
            return pSheet;
        }

        private void Update_SpriteArea()
        {
            //calculate total offset for x
            //use current column as that moves it across
            int Ttl_Offset_X = (int)Offset.X + (int)((Padding.X+Size.X)*Crnt_Row_Col.Y);

            //calculate total offset for y
            //use current row as that moves it down
            int Ttl_Offset_Y = (int)Offset.Y + (int)((Padding.Y+Size.Y)*Crnt_Row_Col.X);

            //calculate new sprite area and redefine the sprite
            SpriteArea = new Rectangle(Ttl_Offset_X, Ttl_Offset_Y, (int)Size.X, (int)Size.Y);
            Sprite = Sheet.Clone(SpriteArea, Sheet.PixelFormat);
        }
       
        public void Next()
        {
            //if not at the end of the a row, go to the next column
            if(Crnt_Row_Col.Y < Ttl_Rows_Cols.Y - 1) Crnt_Row_Col.Y += 1;
            //if not at the end of the column, go to the next row
            else if (Crnt_Row_Col.X < Ttl_Rows_Cols.X - 1) Crnt_Row_Col = new PointF (Crnt_Row_Col.X + 1, 0);
            //go to back to the start
            else Crnt_Row_Col = new PointF(0, 0);
            //redfine the sprite
            Update_SpriteArea();
        }
        
        public void Set_Row_Col(PointF new_Row_Col)
        {
            Crnt_Row_Col = new_Row_Col;
            Update_SpriteArea();
        }


        #endregion

        #region Get/Set methods
        #region Size
        //get size
        public PointF Get_Size()
        {
            return Size;
        }
        //set size
        public void Set_Size(PointF pSize)
        {
            Size = pSize;
            Update_SpriteArea();
        }
        //note: be very careful using set size, will aboslutely screw with the sprite sheet cloning
        #endregion
        #region Location
        //get location
        public PointF Get_Location()
        {
            return Location;
        }
        //set location
        public void Set_Location(PointF pLoc)
        {
            Location = pLoc;
            Update_Center();
        }
        //move location
        public void Move(float x, float y)
        {
            Location = new PointF(Location.X + x, Location.Y + y);
            Update_Center();
        }
        //update location
        public void Update_Location()
        {
            Location = new PointF(Center.X - (Size.X / 2), Center.Y - (Size.Y / 2));
        }
        #endregion
        #region Center
        //get center
        public PointF Get_Center()
        {
            return Center;
        }
        //set center
        public void Set_Center(PointF pCenter)
        {
            Center = pCenter;
            Update_Location();
        }
        //move center
        public void Move_Center(float x, float y)
        {
            Center = new PointF(Center.X + x, Center.Y + y);
            Update_Location();
        }
        //update center
        public void Update_Center()
        {
            Center = new PointF(Location.X + (Size.X / 2), Location.Y + (Size.Y / 2));
        }
        #endregion
        #region Scale
        //get scale
        public float Get_Scale()
        {
            return Scale;
        }
        //set scale
        public void Set_Scale(float pScale)
        {
            Scale = pScale;
        }
        #endregion
        #endregion
    }

    internal class Player: Sprite_Handler
    {
        private string Name;
        private int Health;
        private int MaxHealth;
        private int Box_Position;
        private int Option_Position;
        private int Selected_Option;
        private int Attack;
        private int Defense;
        public bool Wait_For_Input = false;
        private List<Item> Inventory = new List<Item>();

        public Player(Bitmap pSheet, Color pBackground_Colour, PointF pSize, PointF pRows_Cols, PointF pOffset, PointF pPadding, PointF pLoc, string pName)
        : base(pSheet, pBackground_Colour, pSize, pRows_Cols, pOffset, pPadding, pLoc)
        {
            Name = pName;
            Health = 20;
            MaxHealth = 20;
            Box_Position = 0;
            Option_Position = 1;
            Attack = 10;
            Defense = 5;
        }

        #region Get/Set methods
        #region Name
        //get name
        public string Get_Name()
        {
            return Name;
        }
        //set name
        public void Set_Name(string pName)
        {
            Name = pName;
        }
        #endregion
        #region Health
        //get health
        public int Get_Health()
        {
            return Health;
        }
        //set health
        public void Set_Health(int pHealth)
        {
            if (pHealth > MaxHealth) Health = MaxHealth;
            else if (pHealth <= 0) Health = 0;
            else if (Health > 0) Health = pHealth;
        }
        #endregion
        #region MaxHealth
        //get max health
        public int Get_MaxHealth()
        {
            return MaxHealth;
        }
        //set max health
        public void Set_MaxHealth(int pMaxHealth)
        {
            MaxHealth = pMaxHealth;
        }
        #endregion
        #region Box Position
        //get box position
        public int Get_Box_Position()
        {
            return Box_Position;
        }
        //set box position
        public void Set_Box_Position(int pBox_Position)
        {
            Box_Position = pBox_Position;
        }
        #endregion
        #region Option Position
        //get option position
        public int Get_Option_Position()
        {
            return Option_Position;
        }
        //set option position
        public void Set_Option_Position(int pOption_Position)
        {
            Option_Position = pOption_Position;
        }
        #endregion
        #region Selected Option
        //get selected option
        public int Get_Selected_Option()
        {
            return Selected_Option;
        }
        //set selected option
        public void Set_Selected_Option()
        {
            Selected_Option = Option_Position;
        }
        //reset selected option
        public void Reset_Selected_Option()
        {
            Selected_Option = 0;
        }
        #endregion
        #region Attack
        //get attack
        public int Get_Attack()
        {
            return Attack;
        }
        //set attack
        public void Set_Attack(int pAttack)
        {
            Attack = pAttack;
        }
        #endregion
        #region Defense
        //get defense
        public int Get_Defense()
        {
            return Defense;
        }
        //set defense
        public void Set_Defense(int pDefense)
        {
            Defense = pDefense;
        }
        #endregion
        #endregion
    
        #region Inventory
        //get inventory
        public List<Item> Get_Inventory()
        {
            return Inventory;
        }
        //add item
        public void Add_Item(Item_ID pItem_ID)
        {
            Item item = null;
            switch(pItem_ID)
            {
                case Item_ID.Monster_Candy:
                    item = new Monster_Candy();
                    break;
                case Item_ID.Spider_Cider:
                    item = new Spider_Cider();
                    break;
                case Item_ID.Temmie_Flakes:
                    item = new Temmie_Flakes();
                    break;
                case Item_ID.ButterScotch_Pie:
                    item = new ButterScotch_Pie();
                    break;
                default:
                    break;
            }
            if (item != null && Inventory.Count < 4) Inventory.Add(item);
        }
        //get the text that appears when the item is used
        public string Get_Use_Item_Text(int pItem_Index)
        {
            string output_text = "";
            if (Inventory.Count > 0)
            {
                int heal = Inventory[pItem_Index].Get_Heal();
                output_text = Inventory[pItem_Index].Get_Flavour_Text();
                if (Health + heal >= MaxHealth) output_text += "\nYour HP was maxed out!";
                else output_text += "\nYou restored " + heal + "HP.";
                Inventory.RemoveAt(pItem_Index);
            }
            return output_text;
        }
        //use the item
        public void Use_Item(int pItem_Index)
        {
            if (Inventory.Count > pItem_Index)
            {
                Set_Health(Health + Inventory[pItem_Index].Get_Heal());
                Inventory.RemoveAt(pItem_Index);
            }
        }
        #endregion
    }

    internal class Projectile : Sprite_Handler
    {
        private int Damage;

        public Projectile(Bitmap pSheet, Color pBackground_Colour, PointF pSize, PointF pRows_Cols, PointF pOffset, PointF pPadding, PointF pLoc, int pDamage)
        : base(pSheet, pBackground_Colour, pSize, pRows_Cols, pOffset, pPadding, pLoc)
        {
            Damage = pDamage;
        }

        public int Get_Damage()
        {
            return Damage;
        }
    }

    internal class Enemy
    {
        protected string Name;
        protected int Health;
        protected int Damage;
        protected int Defense;
        protected Sprite_Handler[] Sprites;
        protected List<Projectile> Projectiles;
        protected Thread Arena_Text_Thread;
        protected string[] Arena_Text;
        protected int Turn_Selector;
        protected string[] Actions;
        protected string Flavour_Text;

        #region Get/Set methods
        #region Name
        //get name
        public string Get_Name()
        {
            return Name;
        }
        #endregion
        #region Health
        //get health
        public int Get_Health()
        {
            return Health;
        }
        //set health
        public void Set_Health(int pHealth)
        {
            if(pHealth > 0) Health = pHealth;
            else Health = 0;
        }
        #endregion
        #region Damage
        //get damage
        public int Get_Damage()
        {
            return Damage;
        }
        //set damage
        public void Set_Damage(int pDamage)
        {
            Damage = pDamage;
        }
        #endregion
        #region Defense
        //get defense
        public int Get_Defense()
        {
            return Defense;
        }
        //set defense
        public void Set_Defense(int pDefense)
        {
            Defense = pDefense;
        }
        #endregion
        #region Sprites
        //get sprites
        public Sprite_Handler[] Get_Sprites()
        {
            return Sprites;
        }
        #endregion
        #region Projectiles
        //get projectiles
        public List<Projectile> Get_Projectiles()
        {
            return Projectiles;
        }
        #endregion
        #endregion

        #region Misc Methods
        public virtual int Get_Height()
        {
            if(Sprites.Length > 0) return (int)Sprites[Sprites.Length - 1].Get_Location().Y + (int)Sprites[Sprites.Length - 1].Get_Size().Y - (int)Sprites[0].Get_Location().Y;
            else return (int)Sprites[0].Get_Size().Y;
        }
        #endregion
        //empty methods that will be overloaded by child classes
        //virtual methods return nothing and will always be overloaded by child classes
        public virtual void Select_Turn() {}
        public virtual string Choose_Arena_Text() {return "";}
        //just returns Actions[] attribute
        public string[] Get_Actions() 
        {
            return Actions;
        }
        public virtual string Select_Action(int pAction) {return "";}
        //all enemies will have the check act
        public string Check_Action()
        {
            return Name + "\nATK: " + Damage + "      DEF: " + Defense + "\n" + Flavour_Text;
        }
    }

    internal class Item
    {
        protected string Name;
        protected int Heal;
        protected string Flavour_Text;
        
        //no sets here :O
        #region Get methods
        //get name
        public string Get_Name()
        {
            return Name;
        }
        //get heal
        public int Get_Heal()
        {
            return Heal;
        }
        //get flavour text
        public string Get_Flavour_Text()
        {
            return Flavour_Text;
        }
        #endregion
    }
}