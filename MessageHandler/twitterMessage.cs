using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MessageHandler
{
    public class twitterMessage : Message
    {
        public string twitterName { get; set; }

        public bool validID(string name)
        {
            if (name.Length > 16 && name.Substring(0,1) != "@" || name.Contains(" "))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void formatTweet()
        {
            string[] textLines = Regex.Split(this.messageTxt, "\r");
            int removeId = textLines[0].Length;
            this.twitterName = textLines[0];
            /*
            for (int j = 1; j < textLines.Length; j++)
            {
                this.messageTxt += textLines[j] + "\r";
            }
            */

            this.messageTxt = this.messageTxt.Remove(0, removeId);
        }

        public void trendingList(List<string> trending)
        {
            if (this.messageTxt.Length > 0)
            {
                foreach (string word in this.messageTxt.Split(' '))
                {
                    if (word.Substring(0,1) == "#")
                    {
                        trending.Add(word);
                    }
                }
            }
        }

        public void twitterMentions(List<string> mentions)
        {
            if (this.messageTxt.Length > 0)
            {
                foreach (string word in this.messageTxt.Split(' '))
                {
                    if (Regex.IsMatch(word, @"@"))
                    {
                        mentions.Add(word);
                    }
                }
            }
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

            // int count = 0;
            foreach (string word in messageWords)
            {
                //count = count + word.Length;
                if (tTalk.ContainsKey(word))
                {
                    abbToAdd += word + " <" + tTalk[word] + "> ";
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
    }
}
