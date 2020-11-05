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

namespace protosort2
{
    /// <summary>
    /// Interaction logic for DatasetView.xaml
    /// </summary>
    public partial class DatasetView : Window
    {
        private string text;

        public string Text
        {
            get => text; set
            {
                text = value;
                TextblockDataset.Text = Text;
            }
        }

        public DatasetView()
        {
            InitializeComponent();
            TextblockDataset.Text = Text;
        }
    }
}
