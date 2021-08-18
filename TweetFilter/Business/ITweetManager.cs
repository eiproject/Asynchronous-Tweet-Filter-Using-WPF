using System;
using System.Collections.Generic;
using TweetFilter.Models;

namespace TweetFilter.Business {
  public interface ITweetManager : IDisposable {
    List<Tweet> GetTweets();
    void LoadTweetFromCSV(string filePath);
    void WriteTweetToCSV(string filename, List<Tweet> tweet);
    int ProgressPercentage { get; }
  }
}
