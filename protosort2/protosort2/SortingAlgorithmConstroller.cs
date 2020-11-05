using protosort2.SortingAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace protosort2
{
    class SortingAlgorithmConstroller
    {
        readonly ISortingAlgorithm[] sortingAlgorithms;
        internal ISortingAlgorithm[] SortingAlgorithms { get => sortingAlgorithms;}

        public SortingAlgorithmConstroller()
        {
            sortingAlgorithms = new ISortingAlgorithm[]
            {
                new BubbleSort()
            };
        }

        
    }

}
