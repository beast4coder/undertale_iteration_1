namespace undertale_iteration_1
{
    internal class Test_Enemy : Enemy
    {
        public Test_Enemy()
        {
            Name = "Test Enemy";
            Health = 100;
            Damage = 5;
            Sprites = new Sprite_Handler[1];
            //define dummy sprite
            #region Dummy Sprite
            Bitmap sheet = Resource1.Monsters_Ruins_Sheet;
            Color background_col = ColorTranslator.FromHtml("#C386FF");
            PointF size = new PointF(34, 18);
            PointF rows_cols = new PointF(1, 4);
            PointF offset = new PointF(142, 20);
            PointF padding = new PointF(1, 0);
            PointF loc = new PointF((GameForm.flt_FORM_WIDTH - size.X) / 2, 20);
            Sprites[0] = new Sprite_Handler(sheet, background_col, size, rows_cols, offset, padding, loc);
            #endregion
        }
    }
}

