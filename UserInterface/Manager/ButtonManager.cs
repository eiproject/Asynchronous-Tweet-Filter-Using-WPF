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

      Thread filter = new Thread(() => FilterTweetByFollower(_fullFilePath, _minimumFollower));
      filter.Start();

      Thread update = new Thread(() => UpdateProgress());
      update.Start();
    }

    private void FilterTweetByFollower(string filePath, int minFollower) {
      using (_twtManager)
      using (_filterManager) {
        _twtManager.LoadTweetFromCSV(filePath);
        _tweets = _filterManager.FilterByMinimumFollower(minFollower);
        _twtManager.WriteTweetToCSV(ResultPathGenerator(filePath), _tweets);
        if (_tweets.Count == 0) {
          _dataGrid.Logs = "Error";
        }
        else {
          _dataGrid.Logs = $"Success {_tweets.Count} tweets filtered";
        }
        _dataGrid.IsEnded = true;
        _dataGrid.Progress = 100;
      }
    }

    private string ResultPathGenerator(string path) {
      string[] splitted = path.Split('.');
      splitted[splitted.Length - 2] += $"_result_{ _minimumFollower }";
      string result = String.Join(".", splitted);
      return result;
    }

    private void UpdateProgress() {
      while (!_dataGrid.IsEnded) {
        _dataGrid.Progress = _twtManager.ProgressPercentage + _filterManager.ProgressPercentage;
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
