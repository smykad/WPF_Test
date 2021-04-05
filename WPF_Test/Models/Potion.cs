namespace WPF_Test.Models
{
    public class Potion: GameItem
    {
        #region Properties
        
        public int HealthChange { get; set; }
        public int LivesChange { get; set; }

        #endregion

        #region Constructor
        public Potion(int id, string name, int xpGain, int value, int healthChange, int livesChange, string description, string useMessage)
            : base(id, name, value, xpGain, description, useMessage)
        {
            HealthChange = healthChange;
            LivesChange = livesChange;
        }
        #endregion

        #region Methods
        
        public override string InformationString()
        {
            return $"{Name}: {Description}";
        }
        #endregion

    }
}
