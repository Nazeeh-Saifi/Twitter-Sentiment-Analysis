using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersModels
{
        public class Users_Data
        {

            public string first_name { get; set; }
            public string last_name { get; set; }
            public string dol { get; set; }
            public int case_id { get; set; }
            public string tweet_text { get; set; }
    }
    public class AddTweet_Data
    {
        
        public string first_name { get; set; }
        public string last_name { get; set; }
        
    }
}