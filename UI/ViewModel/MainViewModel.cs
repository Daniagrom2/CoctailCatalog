using System.Windows.Controls;
using System.Windows.Input;
using BLL.Module;
using Ninject;
using UI.Infrastructure;
using UI.View;
using ListView = UI.View.ListView;

namespace UI.ViewModel
{
 
        public class MainViewModel : BaseNotify
        {

            private RelayCommand _add;
            public static UserControl currentView;
            public static RelayCommand _listview;
            public static RelayCommand _favoriteview;
        public  UserControl CurrentView
            {
                get => currentView;
                set
                {
                    currentView = value;
                    Notify();
                    
                }
            }
            public MainViewModel()
            {
            var kernel = new StandardKernel(new CoctailsModule());
            ListView listView = kernel.Get<ListView>();
            CurrentView = listView;

            }

        public  ICommand OpenListView => _listview ?? (new RelayCommand(param =>
        {

            var kernel = new StandardKernel(new CoctailsModule());
            ListView listView = kernel.Get<ListView>();
            CurrentView = listView;


        }));
         public ICommand OpenFavoriteView => _favoriteview ?? (new RelayCommand(param =>
        {

            var kernel = new StandardKernel(new CoctailsModule());
            Favorites favoritesView = kernel.Get<Favorites>();
            CurrentView = favoritesView;


        }));
        public ICommand Add => _add ?? (new RelayCommand(param =>
        {

            var kernel = new StandardKernel(new CoctailsModule());
            AddView addView = kernel.Get<AddView>();
           CurrentView = addView;
     

        }));
    }
}