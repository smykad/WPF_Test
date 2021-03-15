using System;
using System.Collections.Generic;
namespace WPF_Test.Models
{
    public class GameItem
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UseMessage { get; set; }
        public string Information => InformationString();

        #endregion

        #region Constructors        
        public GameItem(int id, string name, string description, string useMessage = ".")
        {
            Id = id;
            Name = name;
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
