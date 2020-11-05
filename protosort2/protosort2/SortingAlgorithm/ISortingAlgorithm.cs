using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace protosort2
{
    interface ISortingAlgorithm
    {

        Dataset Dataset { get; set; }
        TimeSpan TimeConsumed { get; }
        string Name { get; }

        void SortDataset();

        void SortDataset(Dataset dataset);
    }
}
