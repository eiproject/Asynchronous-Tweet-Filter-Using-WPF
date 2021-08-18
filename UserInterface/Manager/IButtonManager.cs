using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TweetFilter.Models;

namespace UserInterface.Business {
  interface IButtonManager {
    string OpenFileDialog();
    void StartingProcess(TextBox textBox, DataGrid dataGrid);
  }
}
