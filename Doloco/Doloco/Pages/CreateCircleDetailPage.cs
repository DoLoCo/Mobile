using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class CreateCircleDetailPage:ContentPage
    {
        public CreateCircleDetailPage()
        {
            var viewModel = new CreateCircleViewModel(Navigation);
            BindingContext = viewModel;

            var layout = new StackLayout();

            var organizationName = new Entry { Placeholder = "Name" };
            organizationName.SetBinding(Entry.TextProperty, CreateCircleViewModel.OrganizationNamePropertyName);
            layout.Children.Add(organizationName);

            var description = new Entry { Placeholder = "Description" };
            description.SetBinding(Entry.TextProperty, CreateCircleViewModel.DescriptionPropertyName);
            layout.Children.Add(description);

            var phoneNumber = new Entry { Placeholder = "Phone Number" };
            phoneNumber.SetBinding(Entry.TextProperty, CreateCircleViewModel.PhoneNumberPropertyName);
            layout.Children.Add(phoneNumber);

            var address = new Entry { Placeholder = "Street Address" };
            address.SetBinding(Entry.TextProperty, CreateCircleViewModel.AddressPropertyName);
            layout.Children.Add(address);

            var city = new Entry { Placeholder = "City" };
            city.SetBinding(Entry.TextProperty, CreateCircleViewModel.CityPropertyName);
            layout.Children.Add(city);

            var state = new Entry { Placeholder = "State" };
            state.SetBinding(Entry.TextProperty, CreateCircleViewModel.StatePropertyName);
            layout.Children.Add(state);

            var postalCode = new Entry { Placeholder = "Zip Code" };
            postalCode.SetBinding(Entry.TextProperty, CreateCircleViewModel.PostalCodePropertyName);
            layout.Children.Add(postalCode);

            var button = new Button { Text = "Next", BackgroundColor = Helpers.Color.Blue.ToFormsColor() };
            button.SetBinding(Button.CommandProperty, CreateCircleViewModel.AddCommandPropertyName);
            layout.Children.Add(button);

            Content = new ScrollView
            {
                Content = layout
            };
        }
    }
}
