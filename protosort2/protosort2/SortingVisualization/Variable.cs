using System.Reflection.Emit;
using System.Windows.Controls;

namespace protosort2.SortingVisualization
{
    public class Variable
    {
        public System.Windows.Controls.Label Label { get; set; }
        public TextBox Textbox { get; set; }

        public Variable(string name, string value)
        {
            Label = new System.Windows.Controls.Label();
            Label.Content = name;
            Textbox = new TextBox();
            Textbox.Text = value;
        }

        
    }
}