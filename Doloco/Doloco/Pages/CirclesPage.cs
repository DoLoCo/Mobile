using System;
using Xamarin.Forms;
using Doloco.ViewModel;

namespace Doloco.Pages
{
	public class CirclesPage:ContentPage
	{
		public CirclesPage ()
		{
			var viewModel = new CirclesViewModel(Navigation);
			BindingContext = viewModel;
			var list = new ListView();

			//Bind the items in our list to the observable collection of models
			list.ItemsSource = viewModel.Model;

			var stack = new StackLayout();
			stack.Children.Add(list);
			Content = stack;
		}
	}
}

