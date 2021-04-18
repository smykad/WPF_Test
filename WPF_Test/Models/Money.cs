namespace WPF_Test.Models
{
    public class Money: GameItem
    {
        #region ENUMS
        public enum MoneyType
        {
            GOLD,
            SILVER
        }
        #endregion

        #region PROPERTIES

        public MoneyType Type { get; set; }

        #endregion

        #region CONSTRUCTORS

        public Money(int id, string name, int experiencePoints, int value, string description, string useMessage, MoneyType moneyType)
            : base(id, name, experiencePoints, value, description, useMessage)
        {
            Type = moneyType;
        }

        #endregion

        #region METHODS
        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
        #endregion

    }
}
