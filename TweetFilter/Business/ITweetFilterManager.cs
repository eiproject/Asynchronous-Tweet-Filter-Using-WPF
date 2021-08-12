using System.Collections.Generic;
using TweetFilter.Models;

namespace TweetFilter.Business {
  interface ITweetFilterManager {
    List<Tweet> FilterByMinimumFollower(int minimumFollower);
  }
}
