using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using BLL.DTO;
using BLL.Module;
using BLL.Service;
using InternetManager;
using MaterialDesignThemes.Wpf;
using Ninject;
using UI.Infrastructure;
using UI.View;


namespace UI.ViewModel
{
    public class ListViewModel : BaseNotify
    {

        #region PrivateFields
        private string _searchString;
        private CoctailDTO _selecteCoctail;

     
      
        private CoctailService _coctailService;
        private FavoriteService _favoriteService;
        private ObservableCollection<CoctailDTO> _coctails;
        private ObservableCollection<CoctailDTO> _favoritecoctails;
        private CoctailManager coctailManager = new CoctailManager();
        private RelayCommand _sort;
        private RelayCommand _viewCoctail;
        private RelayCommand _add;
        private RelayCommand _viewlist;
        private RelayCommand _delete;
        #endregion



        #region Constructors

        public ListViewModel(CoctailService coctailService, FavoriteService favoriteService)
        {
            _coctailService = coctailService;
            _favoriteService = favoriteService;

            var col = new List<CoctailDTO>();
            _coctailService.GetAll().ToList().ForEach(z =>
            {
                if (z.Id == _favoriteService.GetAll().Where(x=> x.FavoriteId == z.Id).Select(x=> x.FavoriteId).FirstOrDefault())
                    col.Add(z);
            });
        
                var boof = coctailService.GetAll();
            if (boof.ToList().Count > 0)
            {
                Coctails = new ObservableCollection<CoctailDTO>(boof);
            }
            else
            {
                Coctails = coctailManager.GetAll();
                var A = Coctails.ToList().Count;
                foreach (var v in Coctails)
                {
                    _coctailService.Create(v);
                }
            }
            FavoriteCoctails = new ObservableCollection<CoctailDTO>(_coctailService.GetAll().Select(x =>x).Where(x=> x.Favorite == true).ToList());
            FavoriteCoctailsView = CollectionViewSource.GetDefaultView(FavoriteCoctails);
            CoctailsView = CollectionViewSource.GetDefaultView(Coctails);
            CoctailsView.Filter = Filter;
            FavoriteCoctailsView.Filter = Filter;



        }

        #endregion


        #region Properties

        public string NIngridients { get; set; }
        public string NInstructions { get; set; }
        public string NImage { get; set; }
        public string NName { get; set; }
        public string NGlassName { get; set; }
        public string NType { get; set; }



        public static ICollectionView CoctailsView { get; set; }

        public static ICollectionView FavoriteCoctailsView { get; set; }
        public  ObservableCollection<CoctailDTO> Coctails
        {
            get => _coctails;
            set
            {
                _coctails = value;
                Notify();
            }
        }
        public ObservableCollection<CoctailDTO> FavoriteCoctails
        {
            get => _favoritecoctails;
            set
            {
                _favoritecoctails = value;
                Notify();
            }
        }


        public CoctailDTO SelectedCoctail
        {
            get => _selecteCoctail;
            set
            {
                _selecteCoctail = value;
                Notify();
            }
        }
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                Notify(nameof(SearchString));
                CoctailsView.Refresh();
            }
        }

        #endregion
       

        #region Methods

        private void FindSelectedItem(object par)
        {
            var o = par as string;
            foreach (var c in Coctails)
            {
                if ((int)par == c.Id)
                {
                    SelectedCoctail = c;
                    Notify();
                    break;
                }
            }
        }
        private bool Filter(object obj)
        {
            var coctail = obj as CoctailDTO;
            var isContainsModel = true;
            if (!string.IsNullOrEmpty(_searchString) && !string.IsNullOrWhiteSpace(_searchString))
            {
                if (coctail?.Name != null && coctail.GlassName != null && coctail.Type !=null)
                    isContainsModel = coctail.Name.Contains(_searchString) ||
                                      coctail.GlassName.Contains(_searchString) || coctail.Type.Contains(_searchString);

            }

            return isContainsModel;
        }

        #endregion


        #region Comands

        public ICommand Add => _add ?? (_add = new RelayCommand(param =>
        {
            if (NName != null && NGlassName != null && NImage != null &&
                NType != null && NIngridients != null && NInstructions != null)
            {
                CoctailDTO coctail = new CoctailDTO()
                {
                    Favorite = false, 
                    Name = NName, 
                    GlassName = NGlassName, 
                    Type = NType, 
                    Ingridients = NIngridients.Split(new []{','},StringSplitOptions.RemoveEmptyEntries).ToList(), 
                    Instructions = NInstructions,
                    Image = NImage
                };

                Coctails.Add(coctail);
                _coctailService.CreateAsync(coctail);
                CoctailsView = CollectionViewSource.GetDefaultView(Coctails);
                CoctailsView.Refresh();
                MessageBox.Show("Successfully added");
                
                MainView mainWindow = new MainView();
                mainWindow.Show();
                ((Window)param).Close();
            }
            else
            {
                MessageBox.Show("Write full data!!!");
            }
        }));
        public ICommand SortCommand => _sort ?? (new RelayCommand(param =>
        {
            string sortParam = param.ToString();
            CoctailsView.SortDescriptions.Clear();
            CoctailsView.SortDescriptions.Add(new SortDescription(sortParam, ListSortDirection.Ascending));

        }));
        public ICommand ViewCoctail => _viewCoctail ?? (new RelayCommand(param =>
        {
            
            TransferCoctailDTO.Data = param as CoctailDTO;
            bool boof = TransferCoctailDTO.Data.Favorite;
          var coctailWindow = new CoctailView();
            coctailWindow.ShowDialog();
            if ((param as CoctailDTO).Favorite != boof)
            {
_coctailService.Update(TransferCoctailDTO.Data);
FavoriteCoctailsView.Refresh();
                CoctailsView.Refresh();

            }
            
        }));
        public ICommand ViewList => _viewlist ?? (new RelayCommand(param =>
        {
            MainView mainWindow = new MainView();
            mainWindow.Show();
            ((Window) param).Close();


        }));
        public ICommand Delete => _delete ?? (new RelayCommand(param =>
        {
            var coctail = param as CoctailDTO;
            Coctails.Remove(coctail);
            _coctailService.Delete(coctail);
                CoctailsView = CollectionViewSource.GetDefaultView(Coctails);
                CoctailsView.Refresh();
                MessageBox.Show("Successfully deleted");
      
            
        }));


        #endregion

    }
}