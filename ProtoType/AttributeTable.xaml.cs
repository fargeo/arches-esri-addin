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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ProtoType
{
    /// <summary>
    /// Interaction logic for AttributeTableView.xaml
    /// </summary>
    public sealed class AttributeViewModel
    {
        private readonly string name;
        private readonly string value;
        public AttributeViewModel(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        public string Name => this.name;
        public string Value => this.value;
    }
    public partial class AttributeTableView : UserControl
    {
        public AttributeTableView()
        {
            InitializeComponent();
        }
    }
}
