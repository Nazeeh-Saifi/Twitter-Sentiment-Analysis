using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using UsersModels;

namespace Twitter_SentimentWebsite.Controllers
{
    public class UsersController : Controller
    {
        ITwitterSentimentService.ITwitterSentimentService client;

        // GET: Users
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        [Obsolete]
        public ActionResult Home(Users_Data user, int check)
        {
            ViewBag.check = check;
            ViewBag.username = user; 
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
                (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/RetrieveCases");
            ViewBag.cases = client.RetrieveCases();
            ChannelServices.UnregisterChannel(channel);
            return View("Home"); 
        }
        [Route]
        [HttpPost]
        [Obsolete]
        public ActionResult Verify_Login(Users_Data user_data)
        {

            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/CheckUser");


            string check_user = client.CheckUser(user_data.first_name, user_data.last_name);
            if (check_user == "Registered")
            {
                ChannelServices.UnregisterChannel(channel);
                int check = 1;
                return this.Home(user_data, check);
            }
            else
            {
                ViewBag.check = 0;
                ChannelServices.UnregisterChannel(channel);
                return View("Login");
            }
        }
        [HttpPost]
        [Obsolete]
        public ActionResult Verify_addtweet(Users_Data add)
        {
            int check = 0;
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/AddTweet");

            client.AddTweet(add.tweet_text, add.first_name, add.last_name);
            ChannelServices.UnregisterChannel(channel);
           
            return this.Home(add,check);
        }
        [HttpPost]
        [Obsolete]
        public ActionResult Verify_ParticipateInCase(Users_Data add)
        {
            int check = 0;
            HttpChannel channel = new HttpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (ITwitterSentimentService.ITwitterSentimentService)Activator.GetObject
              (typeof(ITwitterSentimentService.ITwitterSentimentService), "http://localhost:8080/ParticipateCase");

            client.ParticipateCase(add.tweet_text, add.first_name, add.last_name,add.case_id);
            ChannelServices.UnregisterChannel(channel);

            return this.Home(add, check);
        }
    }
}