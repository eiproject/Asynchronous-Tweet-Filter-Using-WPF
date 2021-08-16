using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetFilter.Business {
  public class ProgressManager {
    ITweetFilterManager _filterManager;
    public ProgressManager(ITweetFilterManager filterManager) {
      _filterManager = filterManager;
    }
    public void UpdateProgress() {
      
    }
  }
}
