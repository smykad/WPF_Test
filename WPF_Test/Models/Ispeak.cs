using System.Collections.Generic;

namespace WPF_Test.Models
{
    interface ISpeak
    {
        List<string> Messages { get; set; }

        string Speak();
    }
}