using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Tamagochi.Functionality;
using Tamagotchi.Functionality;

namespace Tamagochi
{
	public partial class MainPage : ContentPage
	{

		public MainPage()
		{
			BindingContext = this;

			InitializeComponent();
		}

		private void Submit(object sender, EventArgs e)
		{
			Settings.playerName = NameInput.Text;
			Navigation.PushAsync(new HomeScreen());
		}
	}
}
