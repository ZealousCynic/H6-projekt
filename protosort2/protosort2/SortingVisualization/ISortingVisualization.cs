using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace protosort2.SortingVisualization
{
    interface ISortingVisualization
    {
        Dataset Dataset { get; set; }

        void StartVisualization();



    }
}
