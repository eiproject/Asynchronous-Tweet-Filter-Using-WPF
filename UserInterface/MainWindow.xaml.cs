using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TweetFilter.Models;
using UserInterface.Business;

namespace UserInterface {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    private string _fullFilePath;
    private string _safeFileName;
    private int _minimumFollower;
    private ButtonManager _btnManager;
    private FilterProgressGroup _progress;
    private List<Tweet> _tweets;

    public MainWindow() {
      _btnManager = new ButtonManager();
      _progress = new FilterProgressGroup();

      InitializeComponent();
    }

    private void Button_ChooseCSV(object sender, RoutedEventArgs e) {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      if (openFileDialog.ShowDialog() == true) {
        _fullFilePath = openFileDialog.FileName;
        _safeFileName = openFileDialog.SafeFileName;
        filePath.Text = _fullFilePath;
      }
    }

    private void Button_Start(object sender, RoutedEventArgs e) {
      // dgFileInfo.Items.Add(_progress.ProgressGroup);
      Console.WriteLine(_progress.ProgressGroup.ToList().Count);

      _minimumFollower = int.TryParse(
        minimumFollower.Text, out _minimumFollower) ? _minimumFollower : 0;

      FilterProgress newProgress = new FilterProgress() {
        FileName = _safeFileName,
        Progress = 0,
        Result = "On Progress..."
      };
      _progress.Add(newProgress);
      dgFileInfo.Items.Add(newProgress);

      Thread startClicked = new Thread(() => FilterByFollower(newProgress));
      startClicked.Start();

      Thread progress = new Thread(() => UpdateProgress(newProgress));
      progress.Start();
    }

    private void FilterByFollower(FilterProgress progress) {
      _tweets = _btnManager.FilterTweetByFollower(_fullFilePath, _minimumFollower);
      if (_tweets.Count == 0) {
        progress.Result = "Error";
      }
      else {
        progress.Result = "Success";
      }
    }

    private void UpdateProgress(FilterProgress progress) {
      for (int i = 0; i < 90; i++) {
        progress.Progress += i;
        Thread.Sleep(100);
      }
    }
    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {

    }
  }
}
