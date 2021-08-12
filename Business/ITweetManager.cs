using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetFilter.Business {
  interface ITweetManager {
    ArrayList GetTweets();
    void LoadTweetFromCSV(string filePath);
    void Dispose();
  }
}
