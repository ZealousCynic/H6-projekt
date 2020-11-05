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
    /// Interaction logic for DataSetGenerateDialogue.xaml
    /// </summary>
    public partial class DataSetGenerateDialogue : Window
    {

        public DataSetGenerateDialogue()
        {
            InitializeComponent();
        }

        private void ButtonOK(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButtonCancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
