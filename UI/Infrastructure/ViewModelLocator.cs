using BLL.Module;
using Ninject;
using UI.ViewModel;

namespace UI.Infrastructure
{
    public class ViewModelLocator
    {
        private IKernel container;
        public ViewModelLocator()
        {
            container = new StandardKernel(new CoctailsModule());

        }

        public MainViewModel MainViewModel => container.Get<MainViewModel>();
        public ListViewModel ListViewModel => container.Get<ListViewModel>();
        public  CoctailViewModel CoctailViewModel => container.Get<CoctailViewModel>();


    }
}