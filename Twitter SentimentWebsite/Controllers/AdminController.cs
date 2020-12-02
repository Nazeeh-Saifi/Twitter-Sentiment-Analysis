using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using AdminModels;

namespace Twitter_SentimentWebsite.Controllers
{
    [Serializable]
    public class AdminController : Controller
    {
        ITwitterSentimentService.ITwitterSentimentService client;
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult AddUser()
        {
            return View();
        }
        public ActionResult RemoveUser()
        {
            return View();
        }
        public ActionResult RetrieveUser()
        {
            ViewBag.check = false;
            return View();
        }
        public ActionResult RetrieveTweets()
        {
            ViewBag.check = false;
            return View();
        }
        public ActionResult AddCase()
        {
            return View();
        }
        public ActionResult RemoveCase()
        {
            return View();
        }
        public ActionResult RetrieveCases_user()
        {
            ViewBag.check = false;
            return View();
        }
        public ActionResult RetrieveUsers_case()
        {
            ViewBag.check = false;
            return View();
        }
  
        [Obsolete]
        [HttpPost]
        public ActionResult Verify_adduser(AddUser_Data add)
        {
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/AddUser");

            string user_check = client.AddUser(add.first_name, add.last_name, add.dob);
            if(user_check == "exsist")
            {
                ViewBag.user_check = 1;
            }
            else
            {
                ViewBag.user_check = 0;
            }
            ChannelServices.UnregisterChannel(channel);

            return View("AddUser");
        }

        [Obsolete]
        [HttpPost]
        public ActionResult Verify_removeuser(RemoveUser_Data rem)
        {
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RemoveUser");

            client.RemoveUser(rem.id, rem.first_name, rem.last_name);
            ChannelServices.UnregisterChannel(channel);

            return View("RemoveUser");
        }
        [Obsolete]
        [HttpPost]
        public ActionResult Verify_retrieveuser(RetrieveUser_Data ret)
        {
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RetrieveUser");

            ArrayList user_retrieve = client.RetrieveUser(ret.id);
            if (user_retrieve != null)
            {
                ViewBag.user = user_retrieve;
            }

            else
            {
                ViewBag.user = null;
            }
            ChannelServices.UnregisterChannel(channel);
            return View("RetrieveUser");

        }
        [HttpPost]
        [Obsolete]
        public ActionResult Verify_retrievetweets(RetrieveTweets_Data ret)
        {
            ViewBag.check = true;

            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RetrieveTweets");

            ArrayList tweet_retrieve = client.RetrieveTweets(ret.user_id);
            if (tweet_retrieve != null)
            {
                ViewBag.tweet = tweet_retrieve;  
            }

            else
            {
                ViewBag.tweet = null;
            }
            ChannelServices.UnregisterChannel(channel);
            return View("RetrieveTweets");
        }

        [Obsolete]
        [HttpPost]
        public ActionResult Verify_addcase(AddCase_Data add)
        {
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/AddCase");
           
            client.AddCase(add.case_text);
            ChannelServices.UnregisterChannel(channel);

            return View("AddCase");
        }

        [Obsolete]
        [HttpPost]
        public ActionResult Verify_removecase(RemoveCase_Data rem)
        {
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RemoveCase");

            client.RemoveCase(rem.case_id);
            ChannelServices.UnregisterChannel(channel);

            return View("RemoveCase");
        }

        [HttpPost]
        [Obsolete]
        public ActionResult Verify_retrievecases_user(RetrieveCases_user_Data ret)
        {
            ViewBag.check = true;

            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RetrieveCases_user");

            ArrayList case_retrieve = client.RetrieveCases_user(ret.user_id);
            if (case_retrieve != null)
            {
                ViewBag.cases = case_retrieve;
            }

            else
            {
                ViewBag.cases = null;
            }
            ChannelServices.UnregisterChannel(channel);
            return View("RetrieveCases_user");
        }
        [Obsolete]
        [HttpPost]
        public ActionResult Verify_retrieveusers_case(RetrieveUsers_case_Data ret)
        {
            ViewBag.check = true;

            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RetrieveUsers_case");

            ArrayList user_retrieve = client.RetrieveUsers_case(ret.case_id);
            if (user_retrieve != null)
            {
                ViewBag.users = user_retrieve;
            }

            else
            {
                ViewBag.users = null;
            }
            ChannelServices.UnregisterChannel(channel);
            return View("RetrieveUsers_case");
        }

      [Obsolete]
        public ActionResult AddTweetSentiment()
        { 

        HttpChannel channel = new HttpChannel();
        ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService) Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RetreiveAllTweetsforSentiment");

        ArrayList tweets_retrieve = client.RetreiveAllTweetsforSentiment();
            if (tweets_retrieve != null)
            {
                ViewBag.tweet = tweets_retrieve;
            }

            else
            {
                ViewBag.tweet = null;
            }
            ChannelServices.UnregisterChannel(channel);
            return View();

        }
        [HttpPost]
        [Obsolete]
        public ActionResult AddSentimentforTweet(AddSentimentforTweet add)
        {

            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/AddSentimentforTweet");

            client.AddSentimentforTweet(add.tweet_id,add.sentiment,add.case_id);
            ChannelServices.UnregisterChannel(channel);
            
            return View("AddTweetSentiment",AddTweetSentiment());
        }
        [Obsolete]
        public ActionResult RetrieveOverallSentiments()
        {
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RetrieveOverallSentiments");

            ArrayList cases_retrieve = client.RetrieveOverallSentiments();
            if (cases_retrieve != null)
            {
                ViewBag.cases = cases_retrieve;
            }

            else
            {
                ViewBag.cases = null;
            }
            ChannelServices.UnregisterChannel(channel);
            return View();
        }
    }
}
       