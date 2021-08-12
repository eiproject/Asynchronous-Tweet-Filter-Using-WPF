using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface {
  class FilterProgressGroup {
    private List<FilterProgress> _progressGroup; 
    public IEnumerable<FilterProgress> ProgressGroup { get { return _progressGroup; }}
    internal FilterProgressGroup() {
      _progressGroup = new List<FilterProgress>();
    }
    public void Add(FilterProgress progress) {
      _progressGroup.Add(progress);
    }
  }
}
