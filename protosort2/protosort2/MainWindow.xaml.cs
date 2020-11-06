using Microsoft.Win32;
using protosort2.SortingVisualization;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace protosort2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dataset dataset;
        SortingAlgorithmHandler sortingAlgorithmHandler;
        CheckBox[] checkboxSortingAlgorithms;

        public MainWindow()
        {
            InitializeComponent();
            dataset = new Dataset();
            sortingAlgorithmHandler = new SortingAlgorithmHandler();

            checkboxSortingAlgorithms = new CheckBox[sortingAlgorithmHandler.SortingAlgorithms.Length];
            for(int i = 0; i < sortingAlgorithmHandler.SortingAlgorithms.Length; i++)
            {
                checkboxSortingAlgorithms[i] = new CheckBox();
                checkboxSortingAlgorithms[i].Content = sortingAlgorithmHandler.SortingAlgorithms[i].Name;
                SortingAlgorithmCheckList.Children.Add(checkboxSortingAlgorithms[i]);
            }

            Binding datasetBinding = new Binding();
            datasetBinding.Source = dataset;
            datasetBinding.Path = new PropertyPath("Count");
            datasetBinding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(DatasetCountText, TextBox.TextProperty, datasetBinding);

            datasetBinding = new Binding();
            datasetBinding.Source = dataset;
            datasetBinding.Path = new PropertyPath("MaxNumber");
            datasetBinding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(DatasetMaxNumberText, TextBox.TextProperty, datasetBinding);

            datasetBinding = new Binding();
            datasetBinding.Source = dataset;
            datasetBinding.Path = new PropertyPath("Filenote");
            datasetBinding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(DatasetDescriptionText, TextBox.TextProperty, datasetBinding);


        }

        private void GenerateDataset(object sender, RoutedEventArgs e)
        {
            DataSetGenerateDialogue dlg = new DataSetGenerateDialogue();

            dlg.Owner = this;

            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                dataset.GenerateDataset(int.Parse(dlg.InputCount.Text), int.Parse(dlg.InputMaxNumber.Text));
            }
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Comma seperated files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string fileText = File.ReadAllText(fileName);
                dataset.ImportCSVFile(fileText, fileName);
            }
        }

        private void SaveFileDialog(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Comma-seperated file (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, dataset.ToCSVString());
        }

        private void InspectDataset(object sender, RoutedEventArgs e)
        {
            DatasetView dlg = new DatasetView();

            dlg.Owner = this;
            dlg.Text = dataset.ToNewlineString();
            dlg.Show();
        }

        private void CreateStatistics(object sender, RoutedEventArgs e)
        {
            if(dataset.Count > 10000)
            {
                if (ShowContinueSortingDialogue() == false) return;
            }
            this.ForceCursor = true;
            this.Cursor = Cursors.Wait;
            string statistics = "Resultat af sorterings-algoritmer:";
            for(int i = 0; i < checkboxSortingAlgorithms.Length; i++)
            {
                if(checkboxSortingAlgorithms[i].IsChecked == true)
                {
                    checkboxSortingAlgorithms[i].FontWeight = FontWeights.Bold; // not working???
                    sortingAlgorithmHandler.SortingAlgorithms[i].SortDataset(dataset);
                    statistics +=
                        $"{Environment.NewLine}" + 
                        $"Algoritme: {sortingAlgorithmHandler.SortingAlgorithms[i].Name}" +
                        $" med tiden: {sortingAlgorithmHandler.SortingAlgorithms[i].TimeConsumed.TotalSeconds}";
                    checkboxSortingAlgorithms[i].FontWeight = FontWeights.Normal;
                }
            }
            this.Cursor = Cursors.Arrow;
            MessageBox.Show(statistics);
        }

        private bool ShowContinueSortingDialogue()
        {
            string messageBoxText =
                $"Datasættet indeholder et højt antal elementer, og det kan tage lang tid at sortere.{Environment.NewLine}Ønsker du at fortsætte?";
            string caption = "Tidskrævende sortering";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes) return true;
            return false;
        }

        private void DatasetCountText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(int.Parse(DatasetCountText.Text) > 0)
            {
                this.ButtonCreateStatistics.IsEnabled = true;
                this.ButtonCreateStatistics.FontStyle = FontStyles.Normal;
                foreach(Button button in this.SortingVisualizationButtonList.Children)
                {
                    button.IsEnabled = true;
                    button.FontStyle = FontStyles.Normal;
                }
            }
            else
            {
                this.ButtonCreateStatistics.IsEnabled = false;
                this.ButtonCreateStatistics.FontStyle = FontStyles.Italic;
                foreach (Button button in this.SortingVisualizationButtonList.Children)
                {
                    button.IsEnabled = false;
                    button.FontStyle = FontStyles.Italic;
                }
            }
        }

        private void ButtonVisualizeBubblesort(object sender, RoutedEventArgs e)
        {
            if (dataset.Count > 20)
            {
                if (ShowContinueVisualizingDialogue() == false) return;
            }

            BubblesortVisualization bsv = new BubblesortVisualization();
            bsv.Dataset = this.dataset;
            bsv.Show();
        }

        private bool ShowContinueVisualizingDialogue()
        {
            string messageBoxText =
                $"Datasættet indeholder et højt antal elementer, der kan ødelægge visualiseringen.{Environment.NewLine}Ønsker du at fortsætte?";
            string caption = "Stor visualisering";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            if (result == MessageBoxResult.Yes) return true;
            return false;
        }
    }
}
