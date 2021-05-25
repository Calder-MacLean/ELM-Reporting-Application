using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MessageHandler
{
    public class smsMessage : Message
    {
        public string phoneNumber { get; set; }

        public void formatSMS()
        {
            string[] textLines = Regex.Split(this.messageTxt, "\r");

            this.phoneNumber = textLines[0];
            int charToRemove = textLines[0].Length;
           
            this.messageTxt = this.messageTxt.Remove(0, charToRemove);
            /*
            for (int j = 1; j < textLines.Length - 3; j++)
            {
                this.messageTxt += textLines[j] + "\r";
            }
            */    
        }

        public void textTalk(Dictionary<string, string> tTalk)
        {
            string abbToAdd = "";
            Regex regex = new Regex(@"\b[\s,\.-:;]*");
            /*
            if (this.messageTxt.Length >= 3)
            {
                foreach (string word in this.messageTxt.Split(' '))
                {
                    if (tTalk.ContainsKey(word))
                    {
                        messageTxt +=  " <" + tTalk[word] + "> ";
                    }
                }
            }
            */
           
            var messageWords = regex.Split(this.messageTxt).Where(x => !string.IsNullOrEmpty(x));

            foreach (string word in messageWords)
            {
                //count = count + word.Length;
                if (tTalk.ContainsKey(word))   
                {
                    abbToAdd +=  word + " <" + tTalk[word] + "> ";
                    //this.messageTxt.Insert(count, abbToAdd);
                    this.messageTxt = this.messageTxt.Replace(word, word + " <" + tTalk[word] + "> ");
                }
            }
            /*
            //this.messageTxt.Insert(count, abbToAdd);
            if (abbToAdd.Length > 0)
            {
                this.messageTxt = this.messageTxt + abbToAdd.Substring(0, abbToAdd.Length - 1);
            }
            */

        }

        public bool validBody()
        {
            bool validBody = true;
            string[] textLines = Regex.Split(this.messageTxt, "\r");
            if (textLines.Length >= 2)
            {
                if (textLines[1] == "" || textLines[1] == "\r\n")
                {
                    validBody = false;
                }
            }

            return validBody;
        }

        public bool validNumber()
        {
            int correct = 0;
            if(this.phoneNumber.Length > 0)
            {
                if(this.phoneNumber.Length >= 11 && this.phoneNumber.Length <= 15)
                {
                    for (int i = 1; i < this.phoneNumber.Length; i++)
                    {
                        if (Char.IsDigit(this.phoneNumber, i))
                        {
                            correct++;
                        }
                    }
                }

            }

            if(correct >= 11 && correct <= 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
