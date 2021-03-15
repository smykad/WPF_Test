namespace WPF_Test.Models
{
    public class Armor: GameItem
    {
        #region Enums
        public enum ArmorType
        {
            Leather,
            Cloth,
            Plate
        }
        #endregion

        #region Properties
        public ArmorType Type { get; set; }
        public int HealthChange { get; set; }
        public int XpGain { get; set; }

        #endregion

        #region Constructors

        public Armor(int id, string name, ArmorType type, int healthChange,
            int xpGain, string description, string onUseMessage): base(id, name, description, onUseMessage)
        {
            Type = type;
            HealthChange = healthChange;
            XpGain = xpGain;
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
