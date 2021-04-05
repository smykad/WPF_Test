namespace WPF_Test.Models
{
    public abstract class Npc : Character
    {
        public string Description { get; set; }

        //public int XpToGain { get; set; }
        //public ObservableCollection<GameItemQuantity> GameItems { get; set; }
        public string Information => InformationText();

        

        public Npc()
        {
            //GameItems = new ObservableCollection<GameItemQuantity>();
        }

        public Npc(int id, string name, RaceType race, string description)
            : base(name, race, id)
        {
            Id = id;
            Name = name;
            Race = race;
            Description = description;
            //GameItems = new ObservableCollection<GameItemQuantity>();
        }

        protected abstract string InformationText();
    }
}
