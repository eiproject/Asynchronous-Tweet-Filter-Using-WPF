using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Models;

namespace TweetFilter.Business {
  public class TweetFilterManager : ITweetFilterManager {
    ITweetManager _tweetManager;
    public TweetFilterManager(ITweetManager twtManager) {
      _tweetManager = twtManager;
    }

    public List<Tweet> FilterByMinimumFollower(int minimumFollower) {
      List<Tweet> filteredTweet = new List<Tweet>();
      try {
        List<Tweet> tweetData = _tweetManager.GetTweets()
          ?? throw new NullReferenceException("Tweets not yet loaded");

        /*filteredTweet = tweetData.Where(
         n => ParseIntegerToString(n.Followers) > minimumFollower).ToList();*/

        IEnumerable<Tweet> filtered =
          from n in tweetData.AsParallel()
          where ProgressBarUpdate()
          where ParseIntegerToString(n.Followers) > minimumFollower
          select n;

        filteredTweet = filtered.ToList();
      }
      catch (NullReferenceException nullReference) {
        Console.WriteLine(nullReference.Message);
      }
      return filteredTweet;
    }

    int ParseIntegerToString(string strNumber) {
      int.TryParse(strNumber, out int result);
      return result;
    }
    private bool ProgressBarUpdate() {

      return true;
    }
  }
}
