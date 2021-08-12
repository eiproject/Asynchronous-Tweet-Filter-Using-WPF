using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using UserInterface.Business;

namespace UserInterface {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    private string _fullFilePath;
    private int _minimumFollower;
    private ButtonManager _btnManager;

    public MainWindow() {
      _btnManager = new ButtonManager();
      InitializeComponent();
    }

    private void Button_ChooseCSV(object sender, RoutedEventArgs e) {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
      if (openFileDialog.ShowDialog() == true) {
        filePath.Text = openFileDialog.SafeFileName;
        _fullFilePath = openFileDialog.FileName;
      }
    }

    private void Button_Start(object sender, RoutedEventArgs e) {
      _minimumFollower = int.TryParse(
        minimumFollower.Text, out _minimumFollower) ? _minimumFollower : 0;
      _btnManager.FilterTweetByFollower(_fullFilePath, _minimumFollower);
    }
  }
}
