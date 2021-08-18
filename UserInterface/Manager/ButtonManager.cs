using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using TweetFilter.Business;
using TweetFilter.Models;
using UserInterface;

namespace UserInterface.Business {
  class ButtonManager : IButtonManager {
    private List<Tweet> _tweets;
    private ITweetManager _twtManager;
    private ITweetFilterManager _filterManager;
    private DataGridManager _dataGridManager;
    private DataGridInformation _dataGrid;
    private DataGrid _dgFileInfo;
    private TextBox _minimumFollowerTextBox;
    private string _fullFilePath;
    private string _safeFileName;
    private int _minimumFollower;
    internal ButtonManager(DataGridManager dataGridManager) {
      _dataGridManager = dataGridManager;
    }

    public void StartingProcess(TextBox textBox, DataGrid dataGrid) {
      _minimumFollowerTextBox = textBox;
      _dgFileInfo = dataGrid;

      _twtManager = new TweetManager();
      _filterManager = new TweetFilterManager(_twtManager);

      ParseMinimalFollowers();
      _dataGrid = CreateDataGrid();
      AppendDataGrids(_dataGrid);

      Thread startClicked = new Thread(() => StartFilterByNumOfFollower(_twtManager, _filterManager,  _dataGrid));
      startClicked.Start();

      Thread progress = new Thread(() => UpdateProgress(_twtManager, _filterManager, _dataGrid));
      progress.Start();
    }

    private void StartFilterByNumOfFollower(ITweetManager twtManager, ITweetFilterManager filterManager,  DataGridInformation progress) {
      using (twtManager)
      using (filterManager) {
        _tweets = FilterTweetByFollower(_fullFilePath, _minimumFollower);
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

    private List<Tweet> FilterTweetByFollower(string filePath, int minFollower) {
      _twtManager.LoadTweetFromCSV(filePath);
      List<Tweet> result = _filterManager.FilterByMinimumFollower(minFollower);
      Console.WriteLine($"Result:  { result.Count} tweets.");
      return result;
    }

    private void UpdateProgress(ITweetManager twtManager, ITweetFilterManager filterManager, DataGridInformation progress) {
      while (!progress.IsEnded) {
        progress.Progress = twtManager.ProgressPercentage + filterManager.ProgressPercentage;
      }
    }

    public string OpenFileDialog() {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      if (openFileDialog.ShowDialog() == true) {
        _fullFilePath = openFileDialog.FileName;
        _safeFileName = openFileDialog.SafeFileName;
      }
      return _fullFilePath;
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

    private void ParseMinimalFollowers() {
      _minimumFollower = int.TryParse(
        _minimumFollowerTextBox.Text, out _minimumFollower) ? _minimumFollower : 0;
    }

    private void AppendDataGrids(DataGridInformation oneGrid) {
      _dataGridManager.AddProgressToGroup(oneGrid);
      _dgFileInfo.Items.Add(oneGrid);
    }
  }
}
