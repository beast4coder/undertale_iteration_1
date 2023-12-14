using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace undertale_iteration_1
{
    internal class ImageSprite
    {
        //class worked on in class project
        public Bitmap Picture;
        public PointF Location;
        public PointF Center;

        public ImageSprite(Bitmap pImg, Point pLoc)
        {
            Picture = pImg;
            Location = pLoc;
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(Picture, Location);
        }
    }

    internal class Player: ImageSprite
    {
        private int Health;
        private int MaxHealth;

        public Player(Bitmap pImg, Point pLoc, int pHealth) : base(pImg, pLoc)
        {
            Picture = pImg;
            Location = pLoc;
            Health = pHealth;
            MaxHealth = pHealth;
        }

        public int GetHealth()
        {
            return Health;
        }

        public void Set_Health(int pHealth)
        {
            if (pHealth > MaxHealth)
            {
                Health = MaxHealth;
            }
            else if (pHealth <= 0)
            {
                Health = 0;
            }
            else if (Health > 0) 
            {
                Health = pHealth;
            }
        }
    }

    internal class Projectile : ImageSprite
    {
        private int Damage;

        public Projectile(Bitmap pImg, Point pLoc, int pDamage) : base(pImg, pLoc)
        {
            Picture = pImg;
            Location = pLoc;
            Damage = pDamage;
        }

        public int Get_Damage()
        {
            return Damage;
        }
    }
}
