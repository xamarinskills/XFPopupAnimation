using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFPopupAnimation
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoginPopupPage());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new RegPage());
        }
    }
}
