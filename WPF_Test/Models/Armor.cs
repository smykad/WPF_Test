namespace WPF_Test.Models
{
    public class Armor: GameItem
    {
        #region Enums
        public enum ArmorType
        {
            Helmet,
            Chest,
            Cloak
        }
        #endregion

        #region Properties
        public ArmorType Type { get; set; }
        public int HealthChange { get; set; }

        #endregion

        #region Constructors

        public Armor(int id, string name, int xpGain, int value, ArmorType type, int healthChange,
            string description, string onUseMessage): base(id, name, xpGain, value, description, onUseMessage)
        {
            Type = type;
            HealthChange = healthChange;
        }
        #endregion

        #region Methods
        public override string InformationString()
        {
            if (HealthChange != 0)
            {
                return $"{Name}: {Description}/nHealth: {HealthChange}";
            }
            else
            {
                return $"{Name}: {Description}";
            }
        }
        #endregion






    }
}
