using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Business {
  class DataGridManager {
    private List<DataGridInformation> _dataGrids;
    public IEnumerable<DataGridInformation> GetDataGrids { get { return _dataGrids; } }
    internal DataGridManager() {
      _dataGrids = new List<DataGridInformation>();
    }
    public void AddProgressToGroup(DataGridInformation dataGrid) {
      _dataGrids.Add(dataGrid);
    }
  }
}
