using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Models;

namespace TweetFilter.Business {
  class TweetManager : IDisposable, ITweetManager {
    private ArrayList _tweets;
    private int _numberOfColumn = 0;
    private bool _isDisposable;

    internal TweetManager() {
      _tweets = new ArrayList();
    }

    public ArrayList GetTweets() {
      return _tweets;
    }

    public void Dispose() {
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing) {
      if (_isDisposable) {
        if (disposing) {
          if (_tweets != null) { _tweets.Clear(); }
          GC.Collect();
        }
        _isDisposable = false;
      }
    }

    public void LoadTweetFromCSV(string filePath) {
      IEnumerable<string> stream = File.ReadLines(filePath);
      GetValidNumberOfColumn(stream.Take(1));
      foreach (string row in stream.Skip(1)) {
        try {
          TryToCreateTweet(row);
        }
        catch (ArgumentException invalidQuery){
          Console.WriteLine(invalidQuery.Message);
        }
      }
      _isDisposable = true;
    }

    private void TryToCreateTweet(string row) {
      string[] query = ParseStringToArray(row);
      Tweet newTweet = CreateTweetObject(query);
      _tweets.Add(newTweet);
    }

    private void GetValidNumberOfColumn(IEnumerable<string> header) {
      foreach (string h in header) {
        _numberOfColumn = ParseStringToArray(h).Length;
        Console.WriteLine("header: " + h);
      }
    }

    private string[] ParseStringToArray(string text) {
      text.Replace(",,", ", ,");
      string[] queryArray = text.Split(',');
      return queryArray;
    }

    private Tweet CreateTweetObject(string[] query) {
      if (query.Length != _numberOfColumn) {
        throw new ArgumentException("Invalid query, Query have difference number of column");
      }
      return new Tweet(query);
    }

  }
}
