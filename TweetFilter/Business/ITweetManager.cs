using System;
using System.Collections.Generic;
using TweetFilter.Models;

namespace TweetFilter.Business {
  public interface ITweetManager : IDisposable {
    List<Tweet> GetTweets();
    void LoadTweetFromCSV(string filePath);
    // void Dispose();
    int ProgressPercentage { get; }
  }
}
