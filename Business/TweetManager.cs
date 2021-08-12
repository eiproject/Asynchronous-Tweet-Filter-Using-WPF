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
  class TweetManager : IDisposable {
    private ArrayList _tweets;
    private Stopwatch _timer;
    private List<string> _csvRead;
    private string _filePath;
    private int _numberOfColumn = 0;

    private bool disposedValue;

    internal TweetManager(Stopwatch timer, string filePath) {
      _filePath = filePath;
      _tweets = new ArrayList();
      _timer = timer;
      StreamFile();
    }

    private void StreamFile() {
      _timer.Start();
      IEnumerable<string> stream = File.ReadLines(_filePath);
      foreach (string row in stream) {
        string[] queryArray = ParseStringToArray(row);
        if (_numberOfColumn == 0) { _numberOfColumn += queryArray.Length; }
        if (queryArray.Length == _numberOfColumn) {
          Tweet tweet = new Tweet(queryArray);
          _tweets.Add(tweet);
        }
      }
      _timer.Stop();
      Console.WriteLine(stream.Count() + " " + _timer.Elapsed);
    }

    private string[] ParseStringToArray(string text) {
      text.Replace(",,", ", ,");
      string[] queryArray = text.Split(',');
      return queryArray;
    }

    private string[] DynamicObjectToArray(dynamic obj) {
      string[] queryArray = new string[] {
        obj.ID, obj.lang, obj.Date, obj.Source, obj.len, obj.Tweet, obj.Likes,
        obj.RTs, obj.Hashtags, obj.UserMentionNames, obj.UserMentionID,
        obj.Name, obj.Place, obj.Followers, obj.Friends
      };
      return queryArray;
    }

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          // TODO: dispose managed state (managed objects)
          if (_tweets != null) { _tweets.Clear(); }
          if (_csvRead != null) { _csvRead.Clear(); }

          Console.WriteLine("Disposed _tweets: " + _tweets.Count); // 0
          GC.Collect(); // is always need this to Dispose?
        }

        // TODO: free unmanaged resources (unmanaged objects) and override finalizer
        // TODO: set large fields to null
        disposedValue = true;
      }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~CSVManager()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose() {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
}
