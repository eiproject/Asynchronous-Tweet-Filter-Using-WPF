using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Models;

namespace TweetFilter.Business {
  public class TweetManager : ITweetManager {
    private List<Tweet> _tweets;
    private double _currentProgress = 0;
    private int _progressPercentage = 0;
    public int ProgressPercentage {
      get {
        if (_progressPercentage < 50) {
          _progressPercentage = (int)(50 * _currentProgress);
        }
        else {
          _progressPercentage = 50;
        }

        return _progressPercentage;
      }
    }

    public TweetManager() { }

    public List<Tweet> GetTweets() {
      return _tweets;
    }

    public void Dispose() {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }

    public void LoadTweetFromCSV(string filePath) {
      try {
        using (StreamReader csvStreamer = File.OpenText(filePath))
        using (CsvReader csvReader = new CsvReader(csvStreamer, new CultureInfo("en-US", false))) {
          _tweets = new List<Tweet>();
          while (csvReader.Read()) {
            dynamic obj = csvReader.GetRecord<object>();
            string[] queryArray = DynamicObjectToArray(obj);
            AddTweetToTweets(queryArray);
            _currentProgress += 0.000001;
          }
        }
      }
      catch (ArgumentException argumentError) {
        Console.WriteLine(argumentError.Message);
      }
    }

    public void WriteTweetToCSV(string filename, List<Tweet> tweets) {
      using (var writer = new StreamWriter(filename))
      using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
        csv.WriteField("ID");
        csv.WriteField("Lang");
        csv.WriteField("Date");
        csv.WriteField("Source");
        csv.WriteField("Len");
        csv.WriteField("FullText");
        csv.WriteField("Likes");
        csv.WriteField("RTs");
        csv.WriteField("Hashtags");
        csv.WriteField("UserMentionNames");
        csv.WriteField("UserMentionID");
        csv.WriteField("Name");
        csv.WriteField("Place");
        csv.WriteField("Followers");
        csv.WriteField("Friends");
        csv.NextRecord();
        foreach (Tweet tweet in tweets) {
          csv.WriteField(tweet.ID);
          csv.WriteField(tweet.Lang);
          csv.WriteField(tweet.Date);
          csv.WriteField(tweet.Source);
          csv.WriteField(tweet.Len);
          csv.WriteField(tweet.FullText);
          csv.WriteField(tweet.Likes);
          csv.WriteField(tweet.RTs);
          csv.WriteField(tweet.Hashtags);
          csv.WriteField(tweet.UserMentionNames);
          csv.WriteField(tweet.UserMentionID);
          csv.WriteField(tweet.Name);
          csv.WriteField(tweet.Place);
          csv.WriteField(tweet.Followers);
          csv.WriteField(tweet.Friends);
          csv.NextRecord();
        }
      }
    }

    private string[] DynamicObjectToArray(dynamic obj) {
      string[] queryArray = new string[] {
        obj.ID, obj.lang, obj.Date, obj.Source, obj.len, obj.Tweet,
        obj.Likes, obj.RTs, obj.Hashtags, obj.UserMentionNames,
        obj.UserMentionID, obj.Name, obj.Place, obj.Followers, obj.Friends
      };
      return queryArray;
    }

    private void AddTweetToTweets(string[] queryArray) {
      Tweet tweet = new Tweet(queryArray);
      _tweets.Add(tweet);
    }

    private void Dispose(bool disposing) {
      if (disposing) {
        if (_tweets != null) { _tweets.Clear(); }
        GC.Collect();
      }
    }
  }
}
