using protosort2.SortingAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace protosort2
{
    class SortingAlgorithmHandler
    {
        readonly ISortingAlgorithm[] sortingAlgorithms;
        internal ISortingAlgorithm[] SortingAlgorithms { get => sortingAlgorithms;}

        public SortingAlgorithmHandler()
        {
            sortingAlgorithms = new ISortingAlgorithm[]
            {
                new BubbleSort()
            };
        }

        
    }

}
