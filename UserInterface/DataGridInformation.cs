using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface {
  public class DataGridInformation : INotifyPropertyChanged {
    private string _fileName;
    private int _progress;
    private string _logs;
    private bool _isEnded;

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

    public string Logs {
      get { return _logs; }
      set {
        _logs = value;
        OnPropertyChanged("Logs");
      }
    }

    public bool IsEnded {
      get { return _isEnded; }
      set {
        _isEnded = value;
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
