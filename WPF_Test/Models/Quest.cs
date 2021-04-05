namespace WPF_Test.Models
{
    public class Quest
    {
        #region ENUMS

        public enum QuestStatus
        {
            INCOMPLETE,
            COMPLETE
        }

        #endregion

        #region PROPERTIES
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public QuestStatus Status { get; set; }
        public string StatusDetail { get; set; }
        public int ExperienceGain { get; set; }
        #endregion

        #region CONSTRUCTOR

        public Quest()
        {

        }

        public Quest(int id, string name, QuestStatus status)
        {
            Id = id;
            Name = name;
            Status = status;
        }
        #endregion
    }
}
