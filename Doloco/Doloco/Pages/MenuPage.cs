using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.Helpers;
using Doloco.Models;
using Doloco.Views;
using Xamarin.Forms;
using Binding = System.ServiceModel.Channels.Binding;
using Color = Xamarin.Forms.Color;

namespace Doloco.Pages
{
    public class MenuPage : ContentPage
    {
        static readonly List<OptionItem> OptionItems = new List<OptionItem>();

        public ListView Menu { get; set; }

        public MenuPage()
        {
            OptionItems.Clear();
            OptionItems.Add(new HomeOptionItem());
            OptionItems.Add(new CirclesOptionItem());
			OptionItems.Add (new SettingsOptionItem ());
            OptionItems.Add(new PrivacyOptionItem());
            OptionItems.Add(new TermsOptionItem());
            OptionItems.Add(new FeedbackOptionItem());
            OptionItems.Add(new AboutOptionItem());

            BackgroundColor = Color.FromHex("ffffff");

            var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

            const string defaultImgUrl = "http://www.gravatar.com/avatar/00000000000000000000000000000000?d=retro";
            var profileImgUrl = App.CurrentUser.Avatar == "" ? defaultImgUrl : App.CurrentUser.Avatar;
            var photo = new Image
            {
                WidthRequest = 180,
                HeightRequest = 180,
                Source = profileImgUrl
            };
            var mask = new Image
            {
                Source = "roundmask.png",
                WidthRequest = 200,
                HeightRequest = 200,
            };
            var photoGrid = new Grid
            {
                ColumnDefinitions = { new ColumnDefinition() },
                RowDefinitions = { new RowDefinition() },
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(0, 20, 0, 10)
            };
            photoGrid.Children.Add(photo);
            photoGrid.Children.Add(mask);
            layout.Children.Add(photoGrid);

            var fullName = String.Format("{0} {1}", App.CurrentUser.FirstName, App.CurrentUser.LastName);
            var label = new Label()
            {
                Text = fullName,
                HorizontalOptions = LayoutOptions.Center,
                Font = Font.SystemFontOfSize(NamedSize.Large)
            };

            layout.Children.Add(label);


            Menu = new ListView
            {
                ItemsSource = OptionItems,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent
            };

            var cell = new DataTemplate(typeof(MenuTextCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
            cell.SetValue(BackgroundColorProperty, Color.Transparent);
            cell.SetValue(TextCell.TextColorProperty, Color.FromHex("262626"));

            Menu.ItemTemplate = cell;

            layout.Children.Add(Menu);

            var logoutButton = new Button
            {
                Text = "LOGOUT",
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent,
                TextColor = Color.FromHex("262626"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Image = new FileImageSource { File = "logout.png"}
            };
            logoutButton.Clicked += (sender, e) => App.Logout();
            layout.Children.Add(logoutButton);

            Content = layout;
        }
    }
}
