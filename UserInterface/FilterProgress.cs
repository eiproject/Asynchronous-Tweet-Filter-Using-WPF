using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface {
  public class FilterProgress : INotifyPropertyChanged {
    private string _fileName;
    private int _progress;
    private string _result;

    public string FileName {
      get { return _fileName; }
      set {
        _fileName = value;
        OnPropertyChanged("FileName");
      }
    }

    public int Progress {
      get { return _progress; }
      set {
        _progress = value;
        OnPropertyChanged("Progress");
      }
    }

    public string Result {
      get { return _result; }
      set {
        _result = value;
        OnPropertyChanged("Result");
      }
    }

    private void OnPropertyChanged(string propertyName) {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
