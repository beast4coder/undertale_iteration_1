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
            Flavour_Text = "* You ate the Monster Candy. \n* Very un-licorice-like.";
        }
    }
    internal class Spider_Cider : Item
    {
        public Spider_Cider()
        {
            Name = "Spider Cider";
            Heal = 24;
            Flavour_Text = "* You drank the Spider Cider. \n* Crunchy...";
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
                "An OrIgInAl BrEaKfAsT",
                "It'S sO gOoD yOu CaN't TaStE iT",
                "DoN't FoRgEt To DiGeSt It",
                "TeMmIe FlAkEs In YoUr MoUtH",
                "This completed none of my breakfast."
            };
            Flavour_Text = "* You ate the Temmie Flakes. \n* " + Extra_Flavour[rand.Next(Extra_Flavour.Length)];
        }
    }
    internal class ButterScotch_Pie : Item
    {
        public ButterScotch_Pie()
        {
            Name = "ButterScotch Pie";
            Heal = 99;
            Flavour_Text = "* You ate the Butterscotch Pie. \n* Reminds you of home...";
        }
    }
}