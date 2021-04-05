namespace WPF_Test.Models
{
    public class Money: GameItem
    {
        public enum MoneyType
        {
            GOLD,
            SILVER
        }

        public MoneyType Type { get; set; }

        public Money(int id, string name, int experiencePoints, int value, string description, string useMessage, MoneyType moneyType)
            : base(id, name, experiencePoints, value, description, useMessage)
        {
            Type = moneyType;
        }
        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }

    }
}
