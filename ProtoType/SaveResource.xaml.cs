using ArcGIS.Desktop.Framework.Threading.Tasks;
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
    /// Interaction logic for Dockpane1View.xaml
    /// </summary>
    public partial class SaveResourceView : UserControl
    {
        public SaveResourceView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task t = QueuedTask.Run(() =>
            {
                try
                {
                    var selectedFeatures = ArcGIS.Desktop.Mapping.MapView.Active.Map.GetSelection();
                    var firstSelectionSet = selectedFeatures.First();
                    var inspector = new ArcGIS.Desktop.Editing.Attributes.Inspector();
                    inspector.Load(firstSelectionSet.Key, firstSelectionSet.Value);
                    var currentResourceID = inspector["resourceinstanceid"];
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("The ResourceID of the selected feature is " + currentResourceID);
                }
                catch {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Make sure you selected only one feature!");
                }
            });
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("You Clicked the Cancel Button in the Save Resources Dockpane");
        }
    }
}
