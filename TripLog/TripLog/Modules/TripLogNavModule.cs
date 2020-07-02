using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;

namespace TripLog.Modules
{
    class TripLogNavModule : NinjectModule
    {
        /// <summary>
        /// Handles view mappings, creating the service and register that service 
        /// into the IoC container
        /// </summary>
        public override void Load()
        {
            var navService = new XamarinFormsNavService();
            //Register viewmodel-page(view)
            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
            navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));

            Bind<INavService>().ToMethod(x => navService).InSingletonScope();
        }
    }
}
