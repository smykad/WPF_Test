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
