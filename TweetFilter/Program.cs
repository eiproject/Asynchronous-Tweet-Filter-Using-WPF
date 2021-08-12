using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetFilter.Business;
using TweetFilter.Models;

namespace TweetFilter {
  class Program {
    static int minFollower = 1000;
    static string filePath = @"C:\Lab Formulatrix\TweetFilterWPF\TweetFilter\Assets\covid_tweet.csv";
    static void Main(string[] args) {
      ITweetManager manager = new TweetManager();
      manager.LoadTweetFromCSV(filePath);

      ITweetFilterManager filter = new TweetFilterManager(manager);
      List<Tweet> filtered = filter.FilterByMinimumFollower(minFollower);

      Console.WriteLine($"Result: { filtered.Count } is more that { minFollower } Followers");
      Console.ReadKey();
    }
  }
}
