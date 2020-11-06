using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace protosort2.SortingVisualization
{
    /// <summary>
    /// Interaction logic for BubblesortVisualization.xaml
    /// </summary>
    public partial class BubblesortVisualization : Window
    {
        private BubbleSortData before, after;
        private Dataset dataset;

        private string[] variableNames;

        internal Dataset Dataset
        {
            get => dataset; set
            {
                dataset = value;
                before = new BubbleSortData(Dataset);
                after = new BubbleSortData(Dataset);
                AddDataAndVariables();
            }
        }


        public BubblesortVisualization()
        {
            InitializeComponent();
            variableNames = new string[]
            {
                "Pointer",
                "Temp",
                "IndexOfLastSortedElement",
                "Swapped"
            };
        }

        private void AddDataAndVariables()
        {
            StackPanel sp;
            StackBeforeData.Children.Clear();
            StackAfterData.Children.Clear();
            for (int i = 0; i < dataset.Count; i++) // data fields
            {
                StackBeforeData.Children.Add(before.Data[i]);
                StackAfterData.Children.Add(after.Data[i]);
            }

            StackBeforeVars.Children.Clear();
            StackAfterVars.Children.Clear();
            for (int i = 0; i < variableNames.Length; i++) // variable fields
            {
                sp = new StackPanel();
                sp.HorizontalAlignment = HorizontalAlignment.Center;
                sp.Margin = new Thickness(10, 10, 10, 10);
                sp.Children.Add(before.Variables[variableNames[i]].Label);
                sp.Children.Add(before.Variables[variableNames[i]].Textbox);
                StackBeforeVars.Children.Add(sp);
                sp = new StackPanel();
                sp.HorizontalAlignment = HorizontalAlignment.Center;
                sp.Margin = new Thickness(10, 10, 10, 10);
                sp.Children.Add(after.Variables[variableNames[i]].Label);
                sp.Children.Add(after.Variables[variableNames[i]].Textbox);
                StackAfterVars.Children.Add(sp);
            }
        }

        private void IndicateDifferences()
        {
            for (int i = 0; i < dataset.Count; i++) // data fields
            {
                if (before.Data[i].Text == after.Data[i].Text)
                {
                    before.Data[i].Background = Brushes.White;
                    after.Data[i].Background = Brushes.White;
                }
                else
                {
                    before.Data[i].Background = Brushes.LightGreen;
                    after.Data[i].Background = Brushes.LightGreen;
                }
            }

            for (int i = 0; i < variableNames.Length; i++) // variable fields
            {
                if (before.Variables[variableNames[i]].Textbox.Text == after.Variables[variableNames[i]].Textbox.Text)
                {
                    before.Variables[variableNames[i]].Textbox.Background = Brushes.White;
                    after.Variables[variableNames[i]].Textbox.Background = Brushes.White;
                }
                else
                {
                    before.Variables[variableNames[i]].Textbox.Background = Brushes.LightGreen;
                    after.Variables[variableNames[i]].Textbox.Background = Brushes.LightGreen;
                }
            }

        }

        private void NextStep(object sender, RoutedEventArgs e)
        {
            int right, rightValue, leftValue, indexOfLastSortedElement;
            // prepare next step
            CopyAfterToBefore();
            right = int.Parse(after.Variables["Pointer"].Textbox.Text);
            indexOfLastSortedElement = int.Parse(after.Variables["IndexOfLastSortedElement"].Textbox.Text);
            leftValue = int.Parse(after.Data[right - 1].Text);
            rightValue = int.Parse(after.Data[right].Text);
            // check

            if(leftValue > rightValue)
            {
                after.Variables["Temp"].Textbox.Text = after.Data[right - 1].Text;
                after.Data[right - 1].Text = after.Data[right].Text;
                after.Data[right].Text = after.Variables["Temp"].Textbox.Text;
                after.Variables["Swapped"].Textbox.Text = "true";
            }


            // end of step

            if (right == indexOfLastSortedElement)
            {
                if (indexOfLastSortedElement == 1) Finish();
                else if (after.Variables["Swapped"].Textbox.Text == "false") Finish();
                else
                {
                    after.Variables["Pointer"].Textbox.Text = "1";
                    after.Variables["IndexOfLastSortedElement"].Textbox.Text = (indexOfLastSortedElement - 1).ToString();
                    after.Variables["Swapped"].Textbox.Text = "false";
                }
            }
            else
            {
                after.Variables["Pointer"].Textbox.Text = (right + 1).ToString();
            }

            IndicateDifferences();
        }

        private void Finish()
        {
            this.ButtonNextStep.IsEnabled = false;
        }

        internal void CopyAfterToBefore()
        {
            for (int i = 0; i < dataset.Count; i++) // data fields
            {
                before.Data[i].Text = after.Data[i].Text;
            }

            for (int i = 0; i < variableNames.Length; i++)
            {
                before.Variables[variableNames[i]].Textbox.Text =
                    after.Variables[variableNames[i]].Textbox.Text;
            }

        }

        private void EndVisualization(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private class BubbleSortData
        {
            private int datawidth;
            public TextBox[] Data { get; set; }
            public Dictionary<string, Variable> Variables { get; set; }

            public BubbleSortData(Dataset dataset)
            {
                Data = new TextBox[dataset.Count];
                datawidth = 10 + ((int)Math.Ceiling(Math.Log10(dataset.MaxNumber)) * 6);
                for (int i = 0; i < dataset.Count; i++)
                {
                    Data[i] = new TextBox();
                    Data[i].Text = dataset.Data[i].ToString();
                    Data[i].Width = datawidth;

                }
                Variables = new Dictionary<string, Variable>();
                Variables.Add("Pointer", new Variable("Pointer", "1"));
                Variables.Add("Temp", new Variable("Temp", ""));
                Variables.Add("IndexOfLastSortedElement", new Variable("Index Of Last Sorted Element", (dataset.Count - 1).ToString()));
                Variables.Add("Swapped", new Variable("Swapped", "false"));
            }


        }
    }
}
