using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(XamarinFormsNavService))]
namespace TripLog.Services
{
    public class XamarinFormsNavService : INavService
    {
        //Property needed to initialize the navigation service
        public INavigation XamarinFormsNav { get; set; }
        readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public bool CanGoBack => 
            XamarinFormsNav.NavigationStack != null &&
            XamarinFormsNav.NavigationStack.Count > 0;

        public event PropertyChangedEventHandler CanGoBackChanged;

        /// <summary>
        /// Navigation services implemented as a page-to-ViewModel mapping
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="view"></param>
        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }

        public void ClearBackStack()
        {
            if (XamarinFormsNav.NavigationStack.Count < 2) return;
            for(var i = 0; i < XamarinFormsNav.NavigationStack.Count - 1; i++)
            {
                XamarinFormsNav.RemovePage(XamarinFormsNav.NavigationStack[i]);
            }
        }

        public async Task GoBack()
        {
            if (CanGoBack)
            {
                await XamarinFormsNav.PopAsync(true);
                OnCanGoBackChanged();
            }
            
        }

        public async Task NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            await NavigateToView(typeof(TViewModel));
            if(XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel)
            {
                ((BaseViewModel)XamarinFormsNav.NavigationStack.Last().BindingContext).Init();
            }
        }

        public async Task NavigateTo<TViewModel, TParameter>(TParameter param) where TViewModel : BaseViewModel
        {
            await NavigateToView(typeof(TViewModel));
            if (XamarinFormsNav.NavigationStack.Last().BindingContext is BaseViewModel<TParameter>)
            {
                ((BaseViewModel<TParameter>)XamarinFormsNav.NavigationStack.Last().BindingContext).Init(param);
            }
        }

        public void NavigateToUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Invalid URI");
            }
            Launcher.TryOpenAsync(uri);
        }

        public void RemoveLastView()
        {
            if(XamarinFormsNav.NavigationStack.Count < 2)
            {
                return;
            }
            var lastView = XamarinFormsNav.NavigationStack[XamarinFormsNav.NavigationStack.Count - 2];
            XamarinFormsNav.RemovePage(lastView);
        }

        private async Task NavigateToView(Type viewModelType)
        {
            if(!_map.TryGetValue(viewModelType, out Type viewType))
            {
                throw new ArgumentException("No view found in view mapping for " + viewModelType.Name + ".");
            }
            //Use reflection to get Views constructor and create the view (searching for a constructor without an enter param)
            //var dec = viewType.GetTypeInfo().DeclaredConstructors;
            //var g = dec.FirstOrDefault(z =>!z.GetParameters().Any());
            var constructor = viewType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(x => !x.GetParameters().Any());
            var view = constructor.Invoke(null) as Page;
            await XamarinFormsNav.PushAsync(view, true);
        }

        void OnCanGoBackChanged() => CanGoBackChanged?.Invoke(this, new PropertyChangedEventArgs("CanGoBack"));
    }
}
