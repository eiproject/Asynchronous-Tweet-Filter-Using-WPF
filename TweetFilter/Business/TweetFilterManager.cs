using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Models;

namespace TweetFilter.Business {
  public class TweetFilterManager : IDisposable, ITweetFilterManager {
    private ITweetManager _tweetManager;
    private int _numOfTweet = 0;
    private int _currentProgress = 0;
    private int _progressPercentage = 0;
    private List<Tweet> _filteredTweet;
    public int ProgressPercentage {
      get {
        if (_numOfTweet != 0) {
          _progressPercentage = 50 * _currentProgress / _numOfTweet;
        }
        return _progressPercentage;
      }
    }

    public TweetFilterManager(ITweetManager twtManager) {
      _tweetManager = twtManager;
    }

    public List<Tweet> FilterByMinimumFollower(int minimumFollower) {
      try {
        _filteredTweet = new List<Tweet>();
        List<Tweet> tweetData = _tweetManager.GetTweets()
          ?? throw new NullReferenceException("Tweets not yet loaded");
        _numOfTweet = tweetData.Count;

        IEnumerable<Tweet> filtered =
          from n in tweetData.AsParallel()
          where ProgressBarUpdate()
          where ParseIntegerToString(n.Followers) > minimumFollower
          select n;

        _filteredTweet = filtered.ToList();

        Console.WriteLine($"{_numOfTweet}  {_currentProgress}");
      }
      catch (NullReferenceException nullReference) {
        Console.WriteLine(nullReference.Message);
      }
      return _filteredTweet;
    }

    int ParseIntegerToString(string strNumber) {
      int.TryParse(strNumber, out int result);
      return result;
    }
    private bool ProgressBarUpdate() {
      _currentProgress += 1;
      return true;
    }

    public void Dispose() {
      if (_filteredTweet != null) { _filteredTweet.Clear(); }
      GC.Collect();
    }
  }
}
