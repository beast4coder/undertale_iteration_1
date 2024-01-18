﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Draw(Graphics g)
        {
            g.DrawImage(Sprite, Location);
        }
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
        #region Sprite Sheet Manipulation 
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
            Location = new PointF(Center.X - (Size.X / 2), Center.Y - (Size.Y / 2));
        }
        //update center
        public void Update_Center()
        {
            Center = new PointF(Location.X + (Size.X / 2), Location.Y + (Size.Y / 2));
        }
        #endregion
        #endregion
    }

    internal class Player: Sprite_Handler
    {
        private string Name;
        private int Health;
        private int MaxHealth;
        private bool TurnState;

        public Player(Bitmap pSheet, Color pBackground_Colour, PointF pSize, PointF pRows_Cols, PointF pOffset, PointF pPadding, PointF pLoc, string pName, int pHealth, int pMaxHealth, bool  pTurn)
        : base(pSheet, pBackground_Colour, pSize, pRows_Cols, pOffset, pPadding, pLoc)
        {
            Name = pName;
            Health = pHealth;
            MaxHealth = pMaxHealth;
            TurnState = pTurn;
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
        public int GetHealth()
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
        #region TurnState
        //get turn state
        public bool Get_TurnState()
        {
            return TurnState;
        }
        //set turn state
        public void Change_Turn()
        {
            TurnState = !TurnState;
        }
        #endregion
        #endregion

        #region Movement
        public void Movement_System()
        {
            if (TurnState)
            {
                
            }
            else
            {
                //x and y track final displacement of player
                float x = 0;
                float y = 0;

                //checks which keys are helds and alters x and y accordingly
                if (GameForm.Down_Held) y += GameForm.flt_PLAYER_SPEED;
                if (GameForm.Up_Held) y -= GameForm.flt_PLAYER_SPEED;
                if (GameForm.Left_Held) x -= GameForm.flt_PLAYER_SPEED;
                if (GameForm.Right_Held) x += GameForm.flt_PLAYER_SPEED;

                //checks boundaries against the arena
                
                
                //moves player final x and y values
                Location.X += x;
                Location.Y += y;
            }
            Update_Center();
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
}