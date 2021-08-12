using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Business;

namespace UserInterface.Business {
  class ButtonManager : IButtonManager {
    ITweetManager _twtManager;
    ITweetFilterManager _filterManager;
    internal ButtonManager() {
      _twtManager = new TweetManager();
      _filterManager = new TweetFilterManager(_twtManager);
    }

    public void FilterTweetByFollower(string filePath, int minFollower) {
      _twtManager.LoadTweetFromCSV(filePath);
      _filterManager.FilterByMinimumFollower(minFollower);
    }
  }
}
