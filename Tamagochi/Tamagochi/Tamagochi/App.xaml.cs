using System;
using System.Linq;
using Tamagotchi.Functionality;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Poetsen.ttf", Alias = "MainFont")]

namespace Tamagotchi
{
	public partial class App : Application
	{

		public delegate void AppEvent();
		public static event AppEvent OnAppStart;
		public static event AppEvent OnAppSleep;
		public static event AppEvent OnAppResume;

		private readonly Color backgroundColor = Color.FromHex("202124");

		public App()
		{
			_ = new TimeManager();
			_ = new Creature();

			InitializeComponent();

			MainPage = new NavigationPage(new HomeScreen())
			{
				BarBackgroundColor = backgroundColor,
				BarTextColor = Color.White,
				BackgroundColor = backgroundColor
			};
		}

		public static bool IsPageActive(Page page)
		{
			return Current.MainPage.Navigation.NavigationStack.Last() == page;
		}

		protected override void OnStart()
		{
			OnAppStart?.Invoke();
		}

		protected override void OnSleep()
		{
			OnAppSleep?.Invoke();
		}

		protected override void OnResume()
		{
			OnAppResume?.Invoke();
		}
	}
}
