using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Models;

namespace TweetFilter.Business {
  class TweetFilterManager : ITweetFilterManager {
    ITweetManager _tweetManager;
    internal TweetFilterManager(ITweetManager twtManager) {
      _tweetManager = twtManager;
    }

    public List<Tweet> FilterByMinimumFollower(int minimumFollower) {
      List<Tweet> filteredTweet = new List<Tweet>();
      try {
        List<Tweet> tweetData = _tweetManager.GetTweets()
          ?? throw new NullReferenceException("Tweets not yet loaded");

        filteredTweet = tweetData.Where(
         n => ParseIntegerToString(n.Followers) > minimumFollower).ToList();
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
  }
}
