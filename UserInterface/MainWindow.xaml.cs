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
    private DataGridInformation _dataGrid;
    private DataGridManager _progressBars;
    private List<Tweet> _tweets;

    public MainWindow() {
      _progressBars = new DataGridManager();

      InitializeComponent();
    }

    private void Button_ChooseCSV(object sender, RoutedEventArgs e) {
      OpenFileDialog();
    }

    private void Button_Start(object sender, RoutedEventArgs e) {
      StartingProcess();
    }

    private void StartingProcess() {
      // one instance each click
      ITweetManager twtManager = new TweetManager();
      ITweetFilterManager filterManager = new TweetFilterManager(twtManager);
      IButtonManager btnManager = new ButtonManager(twtManager, filterManager);

      ParseMinimalFollowers();
      _dataGrid = CreateDataGrid();
      AppendDataGrids(_dataGrid);

      Thread startClicked = new Thread(() => StartFilterByNumOfFollower(twtManager, filterManager, btnManager, _dataGrid));
      startClicked.Start();

      Thread progress = new Thread(() => UpdateProgress(twtManager, filterManager, _dataGrid));
      progress.Start();
    }

    private void StartFilterByNumOfFollower(ITweetManager twtManager, ITweetFilterManager filterManager, IButtonManager btnManager, DataGridInformation progress) {
      using (twtManager)
      using (filterManager) {
        _tweets = btnManager.FilterTweetByFollower(_fullFilePath, _minimumFollower);
        if (_tweets.Count == 0) {
          progress.Logs = "Error";
        }
        else {
          progress.Logs = $"Success {_tweets.Count} tweets filtered";
        }
        progress.IsEnded = true;
        progress.Progress = 100;
      }
    }

    private void UpdateProgress(ITweetManager twtManager, ITweetFilterManager filterManager, DataGridInformation progress) {
      while (!progress.IsEnded) {
        progress.Progress = twtManager.ProgressPercentage + filterManager.ProgressPercentage;
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


    private void ParseMinimalFollowers() {
      _minimumFollower = int.TryParse(
        minimumFollower.Text, out _minimumFollower) ? _minimumFollower : 0;
    }

    private DataGridInformation CreateDataGrid() {
      DataGridInformation dataGrid = new DataGridInformation() {
        FileName = _safeFileName,
        Progress = 0,
        Logs = "Started...",
        IsEnded = false
      };
      return dataGrid;
    }

    private void AppendDataGrids(DataGridInformation oneGrid) {
      _progressBars.AddProgressToGroup(oneGrid);
      dgFileInfo.Items.Add(oneGrid);
    }

    private void dgFileInfo_SelectionChanged(object sender, SelectionChangedEventArgs e) {

    }
  }
}
