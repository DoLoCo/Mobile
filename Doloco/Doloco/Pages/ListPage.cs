using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class ListPageBase<T> : ContentPage where T : class, new()
    {
        public ListPageBase(BaseViewModel<T> viewModel)
        {
            BindingContext = viewModel;
        }
    }
    public class ListPage<T> : ListPageBase<T> where T : class, new()
    {
        public DataTemplate Cell { get; private set; }

        private BaseViewModel<T> ViewModel
        {
            get { return BindingContext as BaseViewModel<T>; }
        }

        public ListPage(BaseViewModel<T> viewModel)
            : base(viewModel)
        {
            //Set value will directly set the value to "List"
            this.SetValue(Page.TitleProperty, "List");
            //This will bind to our BindingContext.Icon
            this.SetBinding(Page.IconProperty, "Icon");

            var list = new ListView();

            //Bind the items in our list to the observable collection of models
            list.ItemsSource = ViewModel.Models;

            list.ItemTemplate = Cell;

            var stack = new StackLayout();
            stack.Children.Add(list);
            Content = stack;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //lazy load data when appearing, simply call the command if no data
            if (ViewModel.Models.Count == 0)
                ViewModel.LoadModelsCommand.Execute(null);
        }
    }
}
