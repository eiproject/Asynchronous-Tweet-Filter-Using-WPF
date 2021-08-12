using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Models;

namespace UserInterface.Business {
  interface IButtonManager {
    List<Tweet> FilterTweetByFollower(string filePath, int minFollower);
  }
}
