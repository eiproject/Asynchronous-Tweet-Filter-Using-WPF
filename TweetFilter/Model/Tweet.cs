using System;

namespace TweetFilter.Models {
  public class Tweet {
    private string _id;
    private string _lang;
    private string _date;
    private string _source;
    private string _len;
    private string _fulltext;
    private string _likes;
    private string _rts;
    private string _hashtags;
    private string _usermentionnames;
    private string _usermentionid;
    private string _name;
    private string _place;
    private string _followers;
    private string _friends;

    private bool disposedValue;

    public string ID { get { return _id; } }
    public string Lang { get { return _lang; } }
    public string Date { get { return _date; } }
    public string Source { get { return _source; } }
    public string Len { get { return _len; } }
    public string FullText { get { return _fulltext; } }
    public string Likes { get { return _likes; } }
    public string RTs { get { return _rts; } }
    public string Hashtags { get { return _hashtags; } }
    public string UserMentionNames { get { return _usermentionnames; } }
    public string UserMentionID { get { return _usermentionid; } }
    public string Name { get { return _name; } }
    public string Place { get { return _place; } }
    public string Followers { get { return _followers; } }
    public string Friends { get { return _friends; } }

    public Tweet(string[] inputArray) {
      _id = inputArray[0];
      _lang = inputArray[1];
      _date = inputArray[2];
      _source = inputArray[3];
      _len = inputArray[4];
      _fulltext = inputArray[5];
      _likes = inputArray[6];
      _rts = inputArray[7];
      _hashtags = inputArray[8];
      _usermentionnames = inputArray[9];
      _usermentionid = inputArray[10];
      _name = inputArray[11];
      _place = inputArray[12];
      _followers = inputArray[13];
      _friends = inputArray[14];
    }
  }
}
