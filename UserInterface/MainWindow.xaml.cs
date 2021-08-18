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
    private DataGridManager _progressBars;
    private IButtonManager _btnManager;

    public MainWindow() {
      _progressBars = new DataGridManager();
      _btnManager = new ButtonManager(_progressBars);

      InitializeComponent();
    }

    private void Button_ChooseCSV(object sender, RoutedEventArgs e) {
      filePath.Text = _btnManager.OpenFileDialog();
    }

    private void Button_Start(object sender, RoutedEventArgs e) {
      _btnManager.StartingProcess(minimumFollower, dgFileInfo);
    }

    private void dgFileInfo_SelectionChanged(object sender, SelectionChangedEventArgs e) {

    }
  }
}
