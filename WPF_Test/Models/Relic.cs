using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Test.Models
{
    public class Relic : GameItem
    {
        public enum UseActionType
        {
            OPENLOCATION,
            KILLRABBIT
        }

        public UseActionType UseAction { get; set; }

        public Relic(int id, string name, int experiencePoints, int value, string description, string useMessage, UseActionType useAction)
            :base(id, name, experiencePoints, value, description, useMessage)
        {
            UseAction = useAction;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}
