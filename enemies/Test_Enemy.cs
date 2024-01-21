namespace undertale_iteration_1
{
    internal class Test_Enemy : Enemy
    {
        public Test_Enemy()
        {
            Name = "Test Enemy";
            Health = 100;
            Damage = 5;
            Sprites = new Sprite_Handler[4];
            //define head sprite
            #region Head Sprite
            Bitmap sheet = Resource1.Napstablook_and_Dummies_Sheet;
            Color background_col = ColorTranslator.FromHtml("#C386FF");
            PointF size = new PointF(34, 18);
            PointF rows_cols = new PointF(1, 4);
            PointF offset = new PointF(142, 20);
            PointF padding = new PointF(1, 0);
            PointF loc = new PointF((GameForm.flt_FORM_WIDTH - size.X) / 2, 20);
            Sprites[0] = new Sprite_Handler(sheet, background_col, size, rows_cols, offset, padding, loc);
            #endregion
            //define torso sprite
            #region Torso Sprite
            size = new PointF(28, 21);
            rows_cols = new PointF(1, 1);
            offset = new PointF(142, 39);
            padding = new PointF(0, 0);
            loc = new PointF((GameForm.flt_FORM_WIDTH - size.X) / 2, 55);
            Sprites[1] = new Sprite_Handler(sheet, background_col, size, rows_cols, offset, padding, loc);
            #endregion
            //define waist sprite
            #region Waist Sprite
            size = new PointF(27, 14);
            rows_cols = new PointF(1, 1);
            offset = new PointF(171, 39);
            padding = new PointF(0, 0);
            loc = new PointF((GameForm.flt_FORM_WIDTH - size.X) / 2, 96);
            Sprites[2] = new Sprite_Handler(sheet, background_col, size, rows_cols, offset, padding, loc);
            #endregion
            //define stool sprite
            #region Stool Sprite
            size = new PointF(23, 14);
            rows_cols = new PointF(1, 1);
            offset = new PointF(199, 39);
            padding = new PointF(0, 0);
            loc = new PointF((GameForm.flt_FORM_WIDTH - size.X) / 2, 124);
            Sprites[3] = new Sprite_Handler(sheet, background_col, size, rows_cols, offset, padding, loc);
            #endregion
        }
    }
}

