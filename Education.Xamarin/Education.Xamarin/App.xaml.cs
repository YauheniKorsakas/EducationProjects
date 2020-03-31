using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Education.Xamarin.Services;
using Education.Xamarin.Views;

namespace Education.Xamarin {
    public partial class App : Application {

        public App() {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
