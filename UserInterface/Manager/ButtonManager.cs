using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Business;
using TweetFilter.Models;

namespace UserInterface.Business {
  class ButtonManager : IButtonManager {
    ITweetManager _twtManager;
    ITweetFilterManager _filterManager;
    internal ButtonManager(ITweetManager twtManager, ITweetFilterManager filterManager) {
      _twtManager = twtManager;
      _filterManager = filterManager;
    }

    public List<Tweet> FilterTweetByFollower(string filePath, int minFollower) {
      _twtManager.LoadTweetFromCSV(filePath);
      List<Tweet> result = _filterManager.FilterByMinimumFollower(minFollower);
      Console.WriteLine($"Result:  { result.Count} tweets.");
      _twtManager.Dispose();

      return result;
    }
  }
}
