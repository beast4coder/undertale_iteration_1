using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace undertale_iteration_1
{
    internal class ImageSprite
    {
        public Bitmap Sheet;
        public Bitmap Sprite;
        public PointF Size;
        public PointF Ttl_Rows_Cols;
        public PointF Offset;
        public PointF Padding;
        public PointF Location;
        public PointF Center;
        public PointF Crnt_Row_Col;
        public Rectangle SpriteArea;

        public ImageSprite(Bitmap pSheet, PointF pSize, PointF pRows_Cols, PointF pOffset, PointF pPadding, PointF pLoc)
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
        public void Draw(Graphics g)
        {
            g.DrawImage(Sprite, Location);
        }
        public void Update_SpriteArea()
        {
            //calculate total offset for x
            //use current column which as that moves it across
            int Ttl_Offset_X = (int)Offset.X + (int)((Padding.X+Size.X)*Crnt_Row_Col.Y);
            //calculate total offset for y
            //use current row which as that moves it across
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

        public void Move(float x, float y)
        {
            Location = new PointF(Location.X + x, Location.Y + y);
            Center = new PointF(Location.X + Size.X / 2, Location.Y + Size.Y / 2);
        }
    }

    internal class Player: ImageSprite
    {
        private int Health;
        private int MaxHealth;

        public Player(Bitmap pSheet, PointF pSize, PointF pRows_Cols, PointF pOffset, PointF pPadding, PointF pLoc, int pHealth, int pMaxHealth)
        : base(pSheet, pSize, pRows_Cols, pOffset, pPadding, pLoc)
        {
            Health = pHealth;
            MaxHealth = pHealth;
        }

        public int GetHealth()
        {
            return Health;
        }

        public void Set_Health(int pHealth)
        {
            if (pHealth > MaxHealth) Health = MaxHealth;
            else if (pHealth <= 0) Health = 0;
            else if (Health > 0) Health = pHealth;
        }
    }

    internal class Projectile : ImageSprite
    {
        private int Damage;

        public Projectile(Bitmap pSheet, PointF pSize, PointF pRows_Cols, PointF pOffset, PointF pPadding, PointF pLoc, int pDamage)
        : base(pSheet, pSize, pRows_Cols, pOffset, pPadding, pLoc)
        {
            Damage = pDamage;
        }

        public int Get_Damage()
        {
            return Damage;
        }
    }
}
