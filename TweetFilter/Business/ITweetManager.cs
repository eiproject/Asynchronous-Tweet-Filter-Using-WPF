using System.Collections;

namespace TweetFilter.Business {
  interface ITweetManager {
    ArrayList GetTweets();
    void LoadTweetFromCSV(string filePath);
    void Dispose();
  }
}
