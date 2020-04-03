using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fagschlunger_Camping.Models
{
    public class Message
    {
        public string Header { get; set; }
        public string AdditionalHeader { get; set; }
        public string MessageText { get; set; }
        public string Solution { get; set; }

        public Message() : this("", "", "", "") { }

        public Message(string header, string messageText) : this(header, "", messageText, "") { }
        public Message(string header, string addHeader, string messageText, string solution)
        {
            this.Header = header;
            this.AdditionalHeader = addHeader;
            this.MessageText = messageText;
            this.Solution = solution;
        }

        //ToString()
    }
}