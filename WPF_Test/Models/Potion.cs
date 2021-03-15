namespace WPF_Test.Models
{
    public class Potion: GameItem
    {
        #region Properties
        
        public int HealthChange { get; set; }
        public int LivesChange { get; set; }
        public int XpGain { get; set; }

        #endregion

        #region Constructor
        public Potion(int id, string name, int healthChange, int livesChange, int xpGain, string description, string useMessage)
            : base(id, name, description, useMessage)
        {
            HealthChange = healthChange;
            LivesChange = livesChange;
            XpGain = xpGain;
        }
        #endregion

        #region Methods
        
        public override string InformationString()
        {
            if(HealthChange != 0)
            {
                return $"{Name}: {Description}\nHealth: {HealthChange}";
            }
            else if (LivesChange !=0)
            {
                return $"{Name}: {Description}\nLives: {LivesChange}";
            }
            else
            {
                return $"{Name}: {Description}";
            }
        }
        #endregion

    }
}
