using System;
using System.Collections.Generic;
using TweetFilter.Models;

namespace TweetFilter.Business {
  public interface ITweetFilterManager : IDisposable {
    List<Tweet> FilterByMinimumFollower(int minimumFollower);
    int ProgressPercentage { get; }
  }
}
