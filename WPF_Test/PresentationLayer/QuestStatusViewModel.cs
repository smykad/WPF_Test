using WPF_Test.Models;
using System.Collections.Generic;

namespace WPF_Test.PresentationLayer
{
    public class QuestStatusViewModel : ObservableObject
    {
        #region Fields
        private string _questInformation;
        #endregion
        #region Properites

        public List<Quest> Quests {get; set;}
        #endregion
        #region Methods        
        public string QuestInformation
        {
            get => _questInformation;
            set
            {
                _questInformation = value;
                OnPropertyChanged(nameof(QuestInformation));
            }
        }
        #endregion
    }
}
