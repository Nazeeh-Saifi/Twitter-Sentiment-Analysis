using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminModels
{
    public class AddUser_Data
    {
        
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string  dob{ get; set; }
    }

    public class RemoveUser_Data
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
    public class RetrieveUser_Data
    {
        public int id { get; set; }
    }
    public class RetrieveTweets_Data
    {
        public int user_id { get; set; }
        public int tweet_id { get; set; }
        public string text { get; set; }
        public int overall_sentiment { get; set; }
        public string date { get; set; }

        public int case_id { get; set; }
    }
    public class AddCase_Data
    {
        public string case_text { get; set; }
    }
    public class RemoveCase_Data
    {
        public int case_id { get; set; }
    }
    public class RetrieveCases_user_Data
    {
        public int user_id { get; set; }
        public int case_id { get; set; }
        public string text { get; set; }
        public string dol { get; set; }

    }
    public class RetrieveUsers_case_Data
    {
        public int case_id { get; set; }
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string dol { get; set; }

    }
    public class AddSentimentforTweet
    {
        public int tweet_id { get; set; }
        public int sentiment { get; set; }
        public int case_id { get; set; }
    }

}
