using System;
using Doloco.Pages;
using Xamarin.Forms;
using Doloco.Models;
using Doloco.ViewModel;

namespace Doloco
{
	public class CreateCircleBankAccountPage:ContentPage
	{
        public CreateCircleBankAccountPage(int circleId)
		{
			BindingContext = new AddCircleAccountViewModel(Navigation, circleId);
            var addBankAccountUrl = String.Format("{0}/bank_account/new?token={1}", App.AppEnv[App.CurrentAppEnv], App.Token);
            var addBankAccountView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = addBankAccountUrl,
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var closeImage = new Image
            {
                Source = ImageSource.FromUri(new Uri("https://s3.amazonaws.com/doloco_images/assets/cross66_64.png")),
                Aspect = Aspect.AspectFit,
                AnchorX = 0,
                HorizontalOptions = LayoutOptions.End
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += async (s, e) =>
            {
                await Navigation.PushAsync(new CirclePage(circleId));
            };
            closeImage.GestureRecognizers.Add(tapGestureRecognizer);

            this.BackgroundColor = Color.White;

            Content = new StackLayout
            {
                Children =
		        {
		            closeImage,
		            addBankAccountView
		        }
            };
		}
	}
}