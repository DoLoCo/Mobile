using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using DolocoApiClient.Models;
using Xamarin.Forms;
using Doloco.ViewModel;

namespace Doloco.Pages
{
	public class CirclesPage:ContentPage
	{
		public CirclesPage ()
		{

		}

        protected override void OnAppearing()
        {
            LoadPage();
        }

        public async Task LoadPage()
        {
            var viewModel = new CirclesViewModel(Navigation);
            BindingContext = viewModel;

            var stack = new StackLayout();

            try
            {
                 viewModel.Model = await App.ApiClient.GetMyOrganizationsAsync();
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }

            var createButton = new Button
            {
                Text = "Create Circle"
            };

            createButton.Clicked += async (sender, e) =>
            {
                var createCirclePage = new CreateCircleDetailPage();
                await Navigation.PushAsync(createCirclePage);
            };

            stack.Children.Add(createButton);

            var cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "Name");
            cell.SetBinding(TextCell.DetailProperty, "Description");

            var list = new ListView {ItemsSource = viewModel.Model, ItemTemplate = cell};
            list.ItemSelected += async (sender, e) =>
            {
                var selectedCircle = (Organization)e.SelectedItem;
                var circlePage = new CirclePage(selectedCircle.Id);

                await Navigation.PushAsync(circlePage);
            };
            stack.Children.Add(list);

            Content = stack;
        }
	}
}

