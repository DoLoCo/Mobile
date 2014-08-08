using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.Models;
using Doloco.Views;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class MenuPage : ContentPage
    {
        static readonly List<OptionItem> OptionItems = new List<OptionItem>();

        public ListView Menu { get; set; }

        public MenuPage()
        {
            OptionItems.Add(new LoginOptionItem());

            BackgroundColor = Color.FromHex("333333");

            var layout = new StackLayout { Spacing = 0, VerticalOptions = LayoutOptions.FillAndExpand };

            var label = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "MENU",
                }
            };

            Device.OnPlatform(
                iOS: () => ((Label)label.Content).Font = Font.SystemFontOfSize(NamedSize.Micro),
                Android: () => ((Label)label.Content).Font = Font.SystemFontOfSize(NamedSize.Medium)
            );

            layout.Children.Add(label);

            Menu = new ListView
            {
                ItemsSource = OptionItems,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Transparent
            };

            var cell = new DataTemplate(typeof(DarkTextCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetBinding(TextCell.DetailProperty, "Count");
            cell.SetValue(VisualElement.BackgroundColorProperty, Color.Transparent);

            Menu.ItemTemplate = cell;

            layout.Children.Add(Menu);

            Content = layout;
        }
    }
}
