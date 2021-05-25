using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MessageHandler
{
    public class emailMessage : Message
    {
        public string emailAddress { get; set; }
        public string emailSubject { get; set; }

        public bool validSubject()
        {
            if(this.emailSubject.Length > 20)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool validAddress()
        {
            if(Regex.IsMatch(this.emailAddress, @"(@)(.+)$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isSIRAndValid()
        {
            if(this.emailSubject.Contains("SIR") && Regex.IsMatch(this.emailSubject, @"(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/]\d{2}"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validSIRContent(List<string> list)
        {
            bool result = false;  
            //{
            string[] textLines = Regex.Split(this.messageTxt, "\r\n");
            if(textLines.Length >= 1)
            {
                if (list.Count > 0)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        for (int i = 0; i < textLines.Length; i++)
                        {
                            if (textLines[i].Contains(list[j]))
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            /*
            if (list.Count > 0)
                {
                    foreach (string item in list)
                    {
                        if (this.messageTxt.Contains(item))
                        {
                            result = true;
                        }
                    }
                }
           // }
           */
            return result;
        }

        public void formatEmail()
        {
            string[] textLines = Regex.Split(this.messageTxt, "\r\n");
            this.emailAddress = textLines[0];
            this.emailSubject = textLines[1];
            int line1 = textLines[0].Length;
            int line2 = textLines[1].Length;

            int charsToRemove = line1 + line2 + 2;
            /*
            for(int j = 2; j < textLines.Length; j++)
            {
                this.messageTxt += textLines[j] + "\r";
            }
            */
            this.messageTxt = this.messageTxt.Remove(0, charsToRemove);

        }

        public void quarantineURL(List<string> quarantine)
        {
            if(this.messageTxt.Length > 4)
            {
                foreach(string word in this.messageTxt.Split(' '))
                {
                    if (Regex.IsMatch(word, @"www."))
                    {
                        quarantine.Add(word);
                        //this.messageTxt -= word;
                        //this.messageTxt += "<URL QUARANTINE>";
                        this.messageTxt = this.messageTxt.Replace(word, "<URL QUARANTINE>");
                        //this.messageTxt = Regex.Replace(this.messageTxt, word, "<URL QUARANTINE>");
                       // messageTxt.Replace(word, "<URL QUARANTINE>");
                    }
                }
            }
        }

        public bool validSIRCode()
        {
            if(this.messageTxt.Length > 2)
            {
                string[] code = this.messageTxt.Split(' ');
                code[0] = code[0].Replace("\r\n", string.Empty);
                
                if(code[0].Length == 9)
                {
                    if (Char.IsNumber(code[0], 0) == true && Char.IsNumber(code[0], 1) == true)
                    {
                        if (code[0].Substring(2, 1) == "-")
                        {
                            if (Char.IsNumber(code[0], 3) == true && Char.IsNumber(code[0], 4) == true && Char.IsNumber(code[0], 5) == true)
                            {
                                if (code[0].Substring(6, 1) == "-")
                                {
                                    if (Char.IsNumber(code[0], 7) == true && Char.IsNumber(code[0], 8) == true)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

