using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using TwitterSentimentService;

namespace ITwitterSentimentService
{

    public interface ITwitterSentimentService
    {
        //admin methods
        string AddUser(string first_name, string last_name, string DOB);
        void RemoveUser(int id, string first_name, string last_name);
        ArrayList RetrieveUser(int id);
        ArrayList RetrieveTweets(int user_id);
        void AddCase(string case_text);
        void RemoveCase(int case_id);
        ArrayList RetrieveCases_user(int user_id);
        ArrayList RetrieveUsers_case(int case_id);
        void AddSentimentforTweet(int tweet_id, int sentiment,int case_id);
        ArrayList RetreiveAllTweetsforSentiment();
        ArrayList RetrieveOverallSentiments();
        //User methods
        string CheckUser(string first_name, string last_name);
        void AddTweet(string tweet_text,string first_name,string last_name);
        void ParticipateCase(string tweet_text, string first_name, string last_name, int case_id);
        ArrayList RetrieveCases();
    }
}
