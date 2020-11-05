using Microsoft.Win32;
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
        SortingAlgorithmConstroller SortingAlgorithmConstroller;
        CheckBox[] checkboxSortingAlgorithms;

        public MainWindow()
        {
            InitializeComponent();
            dataset = new Dataset();
            SortingAlgorithmConstroller = new SortingAlgorithmConstroller();

            checkboxSortingAlgorithms = new CheckBox[SortingAlgorithmConstroller.SortingAlgorithms.Length];
            for(int i = 0; i < SortingAlgorithmConstroller.SortingAlgorithms.Length; i++)
            {
                checkboxSortingAlgorithms[i] = new CheckBox();
                checkboxSortingAlgorithms[i].Content = SortingAlgorithmConstroller.SortingAlgorithms[i].Name;
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

        private void ButtonGenerateDataset(object sender, RoutedEventArgs e)
        {
            DataSetGenerateDialogue dlg = new DataSetGenerateDialogue();

            // Configure the dialog box
            dlg.Owner = this;

            // Open the dialog box modally
            dlg.ShowDialog();
            // Process data entered by user if dialog box is accepted
            if (dlg.DialogResult == true)
            {
                dataset.GenerateDataset(int.Parse(dlg.InputCount.Text), int.Parse(dlg.InputMaxNumber.Text));
            }
        }

        private void ButtonOpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Comma seperated files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string fileText = File.ReadAllText(fileName);
                dataset.Import(fileText, fileName);
            }
        }

        private void ButtonSaveFileDialog(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Comma-seperated file (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, dataset.ToCSVString());
        }

        private void ButtonInspectDataset(object sender, RoutedEventArgs e)
        {
            DatasetView dlg = new DatasetView();

            // Configure the dialog box
            dlg.Owner = this;
            dlg.Text = dataset.ToNewlineString();
            // Open the dialog box modally
            dlg.Show();
        }

        private void ButtonCreateStatistics(object sender, RoutedEventArgs e)
        {
            string statistics = "Resultat af sorterings-algoritmer:";
            for(int i = 0; i < checkboxSortingAlgorithms.Length; i++)
            {
                if(checkboxSortingAlgorithms[i].IsChecked == true)
                {
                    SortingAlgorithmConstroller.SortingAlgorithms[i].SortDataset(dataset);
                    statistics +=
                        $"{Environment.NewLine}" + 
                        $"Algoritme: {SortingAlgorithmConstroller.SortingAlgorithms[i].Name}" +
                        $" med tiden: {SortingAlgorithmConstroller.SortingAlgorithms[i].TimeConsumed.TotalSeconds}";
                }
            }
            MessageBox.Show(statistics);
        }
    }
}
