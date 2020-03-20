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
    /// Interaction logic for MainDockpaneView.xaml
    /// </summary>
    public partial class MainDockpaneView : UserControl
    {
        public MainDockpaneView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("You are connecting "+InstanceURL.Text+" as "+Nickname.Text+" using credentials "+Username.Text+"/"+Password.Password);
        }
    }
}
