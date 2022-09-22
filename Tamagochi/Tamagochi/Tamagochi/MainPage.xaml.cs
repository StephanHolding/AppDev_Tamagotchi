using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Tamagochi.Functionality;

namespace Tamagochi
{
	public partial class MainPage : ContentPage
	{
		private Creature creatureInstance;

		public MainPage()
		{
			creatureInstance = new Creature();
			BindingContext = this;

			InitializeComponent();
		}

		private void Submit(object sender, EventArgs e)
		{
			Navigation.PushAsync(new HomeScreen());
		}
	}
}
