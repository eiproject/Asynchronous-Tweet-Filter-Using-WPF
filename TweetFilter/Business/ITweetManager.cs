using System.Collections.Generic;
using TweetFilter.Models;

namespace TweetFilter.Business {
  interface ITweetManager {
    List<Tweet> GetTweets();
    void LoadTweetFromCSV(string filePath);
    void Dispose();
  }
}
