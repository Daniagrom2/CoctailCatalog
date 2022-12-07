using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BLL.Module;
using Ninject;
using UI.ViewModel;

namespace UI.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        public static void  OpenListView()
        {
            MainViewModel  m = new MainViewModel();
            var kernel = new StandardKernel(new CoctailsModule());
            ListView listView = kernel.Get<ListView>();
            m.CurrentView = listView;
        }
       
    }
}
