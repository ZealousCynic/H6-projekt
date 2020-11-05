using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace protosort2.SortingAlgorithm
{
    class BubbleSort : SortingAlgorithmn
    {
        public override string Name => "Bubblesort C#";

        protected override void Algorithm()
        {
            bool swapped = false;
            int indexOfLastUnsortedElement = Dataset.Count;
            do
            {
                swapped = false;
                int left, right, temp;
                indexOfLastUnsortedElement--;
                for (right = 1; right < indexOfLastUnsortedElement; right++)
                {
                    left = right - 1;
                    if (Dataset.Data[left] > Dataset.Data[right])
                    {
                        temp = Dataset.Data[left];
                        Dataset.Data[left] = Dataset.Data[right];
                        Dataset.Data[right] = temp;
                        swapped = true;
                    }
                }
            }
            while (swapped);
        }
    }
}
