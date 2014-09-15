﻿using System;
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
            OptionItems.Clear();
            OptionItems.Add(new HomeOptionItem());
            OptionItems.Add(new CirclesOptionItem());
			OptionItems.Add (new SettingsOptionItem ());

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
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
            cell.SetValue(VisualElement.BackgroundColorProperty, Color.Transparent);

            Menu.ItemTemplate = cell;

            layout.Children.Add(Menu);

            var logoutButton = new Button { Text = "Logout" };
            logoutButton.Clicked += (sender, e) => App.Logout();
            layout.Children.Add(logoutButton);

            Content = layout;
        }
    }
}
