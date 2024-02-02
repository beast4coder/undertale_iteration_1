namespace undertale_iteration_1
{
    internal enum Item_ID
    {
        Monster_Candy,
        Spider_Cider,
        Temmie_Flakes,
        ButterScotch_Pie,
    }
    internal class Monster_Candy : Item
    {
        public Monster_Candy()
        {
            Name = "Monster Candy";
            Heal = 10;
            Flavour_Text = "You ate the Monster Candy. \nVery un-licorice-like.";
        }
    }
    internal class Spider_Cider : Item
    {
        public Spider_Cider()
        {
            Name = "Spider Cider";
            Heal = 24;
            Flavour_Text = "You drank the Spider Cider. \nCrunchy...";
        }
    }
    internal class Temmie_Flakes : Item
    {
        public Temmie_Flakes()
        {
            Name = "Temmie Flakes";
            Heal = 2;
            Random rand = new Random();
            string[] Extra_Flavour = new string[] {
                "aN oRiGiNaL bReAkFaSt",
                "iT's sO gOoD yOu cAn'T tAsTe iT",
                "dOn'T fOrGeT tO dIgEsT iT",
                "tEmMiE fLaKeS iN yOuR mOuTh",
                "This completed none of my breakfast."
            };
            Flavour_Text = "You ate the Temmie Flakes. \n" + Extra_Flavour[rand.Next(Extra_Flavour.Length)];
        }
    }
    internal class ButterScotch_Pie : Item
    {
        public ButterScotch_Pie()
        {
            Name = "ButterScotch Pie";
            Heal = 99;
            Flavour_Text = "You ate the Butterscotch Pie. \nReminds you of home...";
        }
    }
}