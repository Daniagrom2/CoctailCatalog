using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI.View
{
    /// <summary>
    /// Interaction logic for CoctailView.xaml
    /// </summary>
    public partial class CoctailView : Window
    {
        public CoctailView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var a = ((ToggleButton) sender);
            if(a.IsChecked == false)
            a.IsChecked = true;
            else
                a.IsChecked = false;
        }
    }
}
