using System.Collections.Generic;
using TweetFilter.Models;

namespace TweetFilter.Business {
  public interface ITweetFilterManager {
    List<Tweet> FilterByMinimumFollower(int minimumFollower);
  }
}
