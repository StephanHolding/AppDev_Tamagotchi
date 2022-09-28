using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Tamagotchi.Functionality;

namespace Tamagotchi
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
			CommonFunctionality.playerName = NameInput.Text;
			Navigation.PushAsync(new HomeScreen());
		}
	}
}
