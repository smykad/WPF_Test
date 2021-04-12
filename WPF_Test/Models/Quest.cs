using System.Collections.ObjectModel;

namespace WPF_Test.Models
{
    public class Quest
    {
        #region ENUMS
        public enum QuestStatus
        {
            Incomplete,
            Complete
        }

        #endregion

        #region PROPERTIES
        public int Id { get; set; }

        public int GoldToGive { get; set; }

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

        public Quest(int id, int goldToGive, string name, QuestStatus status)
        {
            Id = id;
            GoldToGive = goldToGive;
            Name = name;
            Status = status;
        }

        #endregion
    }
}
