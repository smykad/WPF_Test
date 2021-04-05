using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Test.Models
{
    public class Vendor : Npc, ISpeak
    {
        public List<string> Messages { get; set; }

        public string Speak()
        {
            return this.Messages != null ? GetMessage() : "";
        }

        private string GetMessage()
        {
            var random = new Random();
            var index = random.Next(0, Messages.Count);
            return Messages[index];
        }
        protected override string InformationText()
        {
            return $"{Name} - {Race} -  {Description}";
        }
    }
}
