using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Xamarin.Forms;

namespace Doloco.Pages
{
    public class CreateCampaignPage:ContentPage
    {
        private readonly Label _campaignTargetLabel;
        private const double _minCampaignTarget = 100;

        public CreateCampaignPage(int orgId)
        {
            var viewModel = new CreateCampaignViewModel(Navigation, orgId);
            BindingContext = viewModel;

            var layout = new StackLayout();

            var campaignName = new Entry{ Placeholder = "Name" };
            campaignName.SetBinding(Entry.TextProperty, CreateCampaignViewModel.CampaignNamePropertyName);
            layout.Children.Add(campaignName);

            var campaignDescription = new Entry {Placeholder = "Description"};
            campaignDescription.SetBinding(Entry.TextProperty, CreateCampaignViewModel.CampaignDescriptionPropertyName);
            layout.Children.Add(campaignDescription);

            _campaignTargetLabel = new Label
            {
                Text = String.Format("Target Amount {0}", _minCampaignTarget.ToString("C", CultureInfo.CurrentCulture))
            };
            layout.Children.Add(_campaignTargetLabel);

            var campaignTarget = new Stepper
            {
                Maximum = 5000,
                Minimum = _minCampaignTarget,
                Increment = 0.5
            };
            campaignTarget.SetBinding(Stepper.ValueProperty, CreateCampaignViewModel.CampaignTargetPropertyName);
            campaignTarget.Value = _minCampaignTarget;
            campaignTarget.ValueChanged += async (sender, e) =>
            {
                _campaignTargetLabel.Text = String.Format("Target Amount {0}", e.NewValue.ToString("C", CultureInfo.CurrentCulture));
            };
            layout.Children.Add(campaignTarget);

            var targetDateLabel = new Label
            {
                Text = "Campaign Target Date"
            };
            layout.Children.Add(targetDateLabel);

            var campaignTargetDate = new DatePicker
            {
                Format = "D"
            };
            campaignTargetDate.SetBinding(DatePicker.DateProperty, CreateCampaignViewModel.CampaignTargetDatePropertyName);
            layout.Children.Add(campaignTargetDate);

            var button = new Button { Text = "Create Campaign" };
            button.SetBinding(Button.CommandProperty, CreateCircleViewModel.AddCommandPropertyName);
            layout.Children.Add(button);

            Content = new ScrollView
            {
                Content = layout
            };
        }
    }
}
