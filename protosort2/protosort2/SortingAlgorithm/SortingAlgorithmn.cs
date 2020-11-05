using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace protosort2.SortingAlgorithm
{
    abstract class SortingAlgorithmn : ISortingAlgorithm
    {
        protected SortingAlgorithmn()
        {
            Dataset = new Dataset();
        }

        public Dataset Dataset { get; set; }
        public TimeSpan TimeConsumed { get; set; }
        public abstract string Name { get; }

        public void SortDataset()
        {
            DateTime begin, end;
            begin = DateTime.Now;
            Algorithm();
            end = DateTime.Now;
            TimeConsumed = new TimeSpan(end.Ticks - begin.Ticks);
        }

        public void SortDataset(Dataset dataset)
        {
            Dataset.DeepCopy(dataset);
            SortDataset();
        }

        protected abstract void Algorithm();
    }
}
