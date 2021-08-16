using CsvHelper;
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
    private StreamReader _csvStreamer;
    protected CsvReader _csvReader;
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

    private void Dispose(bool disposing) {
      if (disposing) {
        if (_tweets != null) { _tweets.Clear(); }
        GC.Collect();
      }
    }

    public void LoadTweetFromCSV(string filePath) {
        try {
          _csvStreamer = File.OpenText(filePath);
          _csvReader = new CsvReader(_csvStreamer, new CultureInfo("en-US", false));
          _tweets = new List<Tweet>();
          while (_csvReader.Read()) {
            dynamic obj = _csvReader.GetRecord<object>();
            string[] queryArray = DynamicObjectToArray(obj);
            AddTweetToTweets(queryArray);
            _currentProgress += 0.000001;
          }
        }
        catch (ArgumentException argumentError) {
          Console.WriteLine(argumentError.Message);
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
  }
}
