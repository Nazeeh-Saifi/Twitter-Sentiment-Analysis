using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSentimentService
{
    [Serializable]
    public class Tweets
    {
        public int tweet_id { get; set; }
        public string text { get; set; }
        public int sentiment { get; set; }
        public string date { get; set; }

        public int case_id { get; set; }
    }
    [Serializable]
    public class Cases
    {
        public int case_id { get; set; }
        public string text { get; set; }
        public string dol { get; set; }
        public int overall_sentiment { get; set; }

    }
    [Serializable]
    public class Users
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string dol { get; set; }
    }
}
