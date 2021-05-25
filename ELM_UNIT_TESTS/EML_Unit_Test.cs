using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageHandler;
using ELM;
using System.Collections.Generic;

namespace ELM_UNIT_TESTS
{
    [TestClass]
    public class EML_Unit_Test
    {

        [TestMethod]
        public void validEmail()
        {
            string messageHeader = "E123123123";
            string messagetxt = "bob@gmail.com\r\n test subject\r\nthis www.google.com";
            emailMessage msg = new emailMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatEmail();
            if(msg.validAddress() == true && msg.validSubject() == true)
            {
                Console.WriteLine("Test Passed");
            }
        }

        [TestMethod]
        public void validText()
        {
            string messageHeader = "S123123123";
            string messagetxt = "+447484876521\r\ntest text";
            smsMessage msg = new smsMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatSMS();
            if (msg.validNumber() == true && msg.validBody() == true)
            {
                Console.WriteLine("Test Passed");
            }
        }

        [TestMethod]
        public void validTweet()
        {
            string messageHeader = "T123123123";
            string messagetxt = "@bob\r\ntest text";
            twitterMessage msg = new twitterMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatTweet();
            if (msg.validID(msg.twitterName) == true)
            {
                Console.WriteLine("Test Passed");
            }
        }

        [TestMethod]
        public void validSIREmail()
        {
            List<string> incident = new List<string>();
            incident.Add("Raid");
            string messageHeader = "E123123123";
            string messagetxt = "bob@gmail.com\r\n SIR 26/11/19\r\n99-999-99 Raid";
            emailMessage msg = new emailMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatEmail();
            if (msg.validSIRCode() == true && msg.isSIRAndValid() == true && msg.validSIRContent(incident))
            {
                Console.WriteLine("Test Passed");
            }
        }

        [TestMethod]
        public void tweetmention()
        {
            List<string> mentions = new List<string>();
            string messageHeader = "T123123123";
            string messagetxt = "@bob\r\ntest text @jim";
            twitterMessage msg = new twitterMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatTweet();
            if (mentions.Count == 0)
            {
                msg.twitterMentions(mentions);
                if(mentions.Count != 0)
                {
                    Console.WriteLine("Test Passed");
                }
            }
        }

        [TestMethod]
        public void tweethashtag()
        {
            List<string> trending = new List<string>();
            string messageHeader = "T123123123";
            string messagetxt = "@bob\r\ntest text @jim #cool";
            twitterMessage msg = new twitterMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatTweet();
            if (trending.Count == 0)
            {
                msg.trendingList(trending);
                if (trending.Count != 0)
                {
                    Console.WriteLine("Test Passed");
                }
            }
        }

        [TestMethod]
        public void urlQuarantine()
        {
            List<string> url = new List<string>();
            string messageHeader = "E123123123";
            string messagetxt = "bob@gmail.com\r\n test subject\r\nthis www.google.com";
            emailMessage msg = new emailMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatEmail();
            msg.quarantineURL(url);
            if (url.Count > 0)
            {
                Console.WriteLine("Test Passed");
            }
        }

        [TestMethod]
        public void tweetAbbrev()
        {
            Dictionary<string, string> words = new Dictionary<string, string>();
            words.Add("AAP", "Always A Pleasure");
            string messageHeader = "T123123123";
            string messagetxt = "@bob\r\ntest text @jim #cool AAP";
            twitterMessage msg = new twitterMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatTweet();
            msg.textTalk(words);
            Console.WriteLine(msg.messageTxt);
        }

        [TestMethod]
        public void smsAbbrev()
        {
            Dictionary<string, string> words = new Dictionary<string, string>();
            words.Add("AAP", "Always A Pleasure");
            string messageHeader = "S123123123";
            string messagetxt = "+447484876521\r\nAAP";
            twitterMessage msg = new twitterMessage();
            msg.messageHeader = messageHeader;
            msg.messageTxt = messagetxt;
            msg.formatTweet();
            msg.textTalk(words);
            Console.WriteLine(msg.messageTxt);
        }
    }
}
