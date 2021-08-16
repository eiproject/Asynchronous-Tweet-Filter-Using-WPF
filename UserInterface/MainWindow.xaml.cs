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
using TweetFilter.Business;
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
    private DataGridManager _progressBars;
    private List<Tweet> _tweets;
    ITweetManager _twtManager;
    ITweetFilterManager _filterManager;

    public MainWindow() {
      _progressBars = new DataGridManager();

      InitializeComponent();
    }

    private void Button_ChooseCSV(object sender, RoutedEventArgs e) {
      OpenFileDialog();
    }

    private void Button_Start(object sender, RoutedEventArgs e) {
      InitializeManagers();
      ParseMinimalFollowers();
      DataGridInformation dataGrid = CreateDataGrid();

      Thread startClicked = new Thread(() => StartFilterByNumOfFollower(dataGrid));
      startClicked.Start();

      Thread progress = new Thread(() => UpdateProgress(dataGrid));
      progress.Start();
    }

    private void StartFilterByNumOfFollower(DataGridInformation progress) {
      _tweets = _btnManager.FilterTweetByFollower(_fullFilePath, _minimumFollower);
      if (_tweets.Count == 0) {
        progress.Result = "Error";
      }
      else {
        progress.Result = "Success";
      }
      progress.IsEnded = true;
      progress.Progress = 100;
    }

    private void UpdateProgress(DataGridInformation progress) {
      while (!progress.IsEnded) {
        progress.Progress = (int)_filterManager.ProgressPercentage;
      }
    }

    private void OpenFileDialog() {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      if (openFileDialog.ShowDialog() == true) {
        _fullFilePath = openFileDialog.FileName;
        _safeFileName = openFileDialog.SafeFileName;
        filePath.Text = _fullFilePath;
      }
    }

    private void InitializeManagers() {
      _twtManager = new TweetManager();
      _filterManager = new TweetFilterManager(_twtManager);
      _btnManager = new ButtonManager(_twtManager, _filterManager);
    }

    private void ParseMinimalFollowers() {
      _minimumFollower = int.TryParse(
        minimumFollower.Text, out _minimumFollower) ? _minimumFollower : 0;
    }

    private DataGridInformation CreateDataGrid(){
      DataGridInformation process = new DataGridInformation() {
        FileName = _safeFileName,
        Progress = 0,
        Result = "Started...",
        IsEnded = false
      };
      return null;
    }

    private void AppendDataGrids(DataGridInformation oneGrid) {
      _progressBars.AddProgressToGroup(oneGrid);
      dgFileInfo.Items.Add(oneGrid);
    }
  }
}
