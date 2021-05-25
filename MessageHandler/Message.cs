using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandler
{
    public class Message
    {
        public string messageHeader { get; set; }
        public string messageTxt { get; set; }

    }
    /*
    private bool IsValidHeader(string header, List<string> headers, int matches)
    {
        if (headers.Count > 0 && header != "")
        {
            foreach (string headerName in headers)
            {
                if (header == headerName)
                {
                    matches++;
                }
            }
        }
        else
        {
            return true;
        }
       
        if (matches > 0)
        {
            return false;
        }

    }
    */
}
