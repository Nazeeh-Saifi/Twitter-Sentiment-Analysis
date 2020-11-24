﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TwitterSentimentService;


namespace TwitterSentimentServices
{
    [Serializable]
    public class TwitterSentimentServices : MarshalByRefObject,
        ITwitterSentimentService.ITwitterSentimentService
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        void sqlconnection()
        {
            con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TwitterSentiment;Integrated Security=true";
            con.Open();
            com.Connection = con;
        }

        //____Admin Services_____

        public void AddUser(string first_name, string last_name, string DOB)
        {
            sqlconnection();

            com.CommandText = "insert into users(first_name,last_name,dob) Values('" + first_name + "','"
            + last_name + "','"
            + DOB + "');";

            dr = com.ExecuteReader();
            con.Close();

        }

        public void RemoveUser(int id, string first_name, string last_name)
        {
            sqlconnection();

            com.CommandText = "Delete from twits where user_id =" + id + ";";

            dr = com.ExecuteReader();
            con.Close();

            sqlconnection();

            com.CommandText = "Delete from users where id =" + id + " or " +
                "(first_name = '" + first_name + "' and " +
                "last_name = '" + last_name + "');";

            dr = com.ExecuteReader();
            con.Close();

        }
        [STAThread]
        public ArrayList RetrieveUser(int id)
        {
            Users[] allRecords = { };
            sqlconnection();

            com.CommandText = "select first_name,last_name,dob from users where id =" + id + ";";

            dr = com.ExecuteReader();
            List<Users> users = new List<Users>();
            var ui = new ArrayList();
            while (dr.Read())
            {
                users.Add(new Users
                {
                    first_name = dr.GetString(0),
                    last_name = dr.GetString(1),
                    dol = dr.GetString(2)
                });

                allRecords = users.ToArray();
            }
            ui.AddRange(allRecords);
            con.Close();
            return ui;

        }

        public ArrayList RetrieveTwits(int user_id)
        {
            Twits[] allRecords = { };
            sqlconnection();
            com.CommandText = "select id,text,sentiment,date,case_id from twits where user_id =" + user_id + ";";

            dr = com.ExecuteReader();
            List<Twits> twits = new List<Twits>();
            var li = new ArrayList();

            while (dr.Read())
            {
                twits.Add(new Twits
                {
                    twit_id = dr.GetInt32(0),
                    text = dr.GetString(1),
                    sentiment = dr.GetInt32(2),
                    date = dr.GetString(3),
                    case_id = dr.GetInt32(4)
                });
     
                allRecords = twits.ToArray();

            }
            li.AddRange(allRecords);
            con.Close();
            return li;
        }



        public void AddCase(string case_text)
        {
            sqlconnection();
            string dol = DateTime.Now.ToString();
            com.CommandText = "insert into cases (text,dol) Values('" + case_text + "','"
           + dol + "');";

            dr = com.ExecuteReader();
            con.Close();
        }

        public void RemoveCase(int case_id)
        {
            sqlconnection();

            com.CommandText = "Delete from twits where case_id =" + case_id + ";";
            dr = com.ExecuteReader();
            con.Close();

            sqlconnection();
            com.CommandText = "Delete from cases where id = " + case_id + ";";
            dr = com.ExecuteReader();

            con.Close();

        }


        public ArrayList RetrieveCases_user(int user_id)
        {
            Cases[] allRecords = { };
            sqlconnection();
            com.CommandText = "select id,text,dol from cases where id IN (select case_id from twits where user_id=" + user_id + ");";
            dr = com.ExecuteReader();
            List<Cases> cases = new List<Cases>();
            var ci = new ArrayList();

            while (dr.Read())
            {
                cases.Add(new Cases
                {
                    case_id = dr.GetInt32(0),
                    text = dr.GetString(1),
                    dol = dr.GetString(2)
                });

                allRecords = cases.ToArray();
            }

            ci.AddRange(allRecords);
            con.Close();
            return ci;
        }


        public ArrayList RetrieveUsers_case(int case_id)
        {
            Users[] allRecords = { };
            sqlconnection();
            com.CommandText = "select DISTINCT u.id,u.first_name,u.last_name from users u Full Outer Join twits t ON u.id = t.user_id where t.case_id =" + case_id + ";";
            dr = com.ExecuteReader();
            List<Users> users = new List<Users>();
            var ui = new ArrayList();
            while (dr.Read())
            {
                users.Add(new Users {
                    user_id = dr.GetInt32(0),
                    first_name = dr.GetString(1),
                    last_name = dr.GetString(2)
                });

                allRecords = users.ToArray();
            }
            ui.AddRange(allRecords);
            con.Close();
            return ui;

        }
        public ArrayList RetreiveAllTwitsforSentiment()
        {
            Twits[] allRecords = { };
            sqlconnection();
            com.CommandText = "select id,text,date,case_id from twits where sentiment = 0;";

            dr = com.ExecuteReader();
            List<Twits> twits = new List<Twits>();
            var li = new ArrayList();

            while (dr.Read())
            {
                twits.Add(new Twits
                {
                    twit_id = dr.GetInt32(0),
                    text = dr.GetString(1),
                    date = dr.GetString(2),
                    case_id = dr.GetInt32(3)
                });

                allRecords = twits.ToArray();

            }
            li.AddRange(allRecords);

            con.Close();
            return li;
        }

        public void AddSentimentforTwit(int twit_id, int sentiment,int case_id)
        {
            sqlconnection();

            com.CommandText = "Update twits SET sentiment = '" + sentiment + "' where id = " + twit_id + ";";

            dr = com.ExecuteReader();
            con.Close();


            //Calculate overall sentiment for a Case
            int Count = 0;
            int allsen = 0;
            sqlconnection();
            com.CommandText = "select sentiment from twits where case_id = " + case_id +";";

            dr = com.ExecuteReader();
            while (dr.Read()){
                Count++;
                allsen += dr.GetInt32(0);
            }
            
            int overall_sentiment = allsen / Count;
            con.Close();
            
            sqlconnection();
            com.CommandText = "Update cases SET overall_sentiment = '" + overall_sentiment + "' where id = " + case_id + ";";

            dr = com.ExecuteReader();
            con.Close();
        }
        public ArrayList RetrieveOverallSentiments()
        {
            Cases[] allRecords = null;
            sqlconnection();
            com.CommandText = "select id,text,dol,overall_sentiment from cases;";
            dr = com.ExecuteReader();
            List<Cases> cases = new List<Cases>();
            var ci = new ArrayList();
            while (dr.Read())
            {
                cases.Add(new Cases
                {
                    case_id = dr.GetInt32(0),
                    text = dr.GetString(1),
                    dol = dr.GetString(2),
                    overall_sentiment = dr.GetInt32(3)
                });
                allRecords = cases.ToArray();
            }
            ci.AddRange(allRecords);
            con.Close();
            return ci;
        }

    //_____User Services_______

    public string AddTwit(string twit_text,string username)
        {
            sqlconnection();
            string date = DateTime.Now.ToString();
            com.CommandText = "insert into twits(text,user_id,date) Values('" + twit_text + "'," +
                "(SELECT id from users where username = "+ username + "),'"+ date +"');";
            //return com.CommandText;

            dr = com.ExecuteReader();
            if (dr.Read() == true)
            {
                con.Close();
                return "Error!!! your name not found";
            }
            else
            {
                con.Close();
                return "your twit added Successfully.";
            }
        }

        public string ParticipateCase(int case_id,string twit_text,string username)
        {
            sqlconnection();
            AddTwit(twit_text,username);

            com.CommandText = "insert into twits (case_id) Values (" + case_id + ");";
            dr = com.ExecuteReader();

            return "user partcipate in a case number :" + case_id;
        }

    }
 
}
