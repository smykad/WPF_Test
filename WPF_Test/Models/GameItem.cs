using System;
using System.Collections.Generic;
namespace WPF_Test.Models
{
    public class GameItem
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int XpGain { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public string UseMessage { get; set; }
        public string Information => InformationString();

        #endregion

        #region Constructors        
        public GameItem(int id, string name, int xpGain, int value, 
            string description, string useMessage = ".")
        {
            Id = id;
            Name = name;
            XpGain = xpGain;
            Value = value;
            Description = description;
            UseMessage = useMessage;
        }
        #endregion

        #region Methods

        public virtual string InformationString()
        {
            return $"{Name}: {Description}";
        }


        #endregion
    }
}
