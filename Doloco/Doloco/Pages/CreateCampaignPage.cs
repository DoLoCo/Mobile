using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doloco.ViewModel;
using Doloco.Views;
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

            var campaignName = new Entry{ Placeholder = "Campaign Name" };
            campaignName.SetBinding(Entry.TextProperty, CreateCampaignViewModel.CampaignNamePropertyName);

            var campaignDescription = new Entry{Placeholder = "Description"};
            campaignDescription.SetBinding(Entry.TextProperty, CreateCampaignViewModel.CampaignDescriptionPropertyName);

            var targetDateLabel = new Label
            {
                Text = "Target Date",
                HorizontalOptions = LayoutOptions.Center
            };

            var campaignTargetDate = new DatePicker
            {
                Format = "D"
            };
            campaignTargetDate.SetBinding(DatePicker.DateProperty, CreateCampaignViewModel.CampaignTargetDatePropertyName);

/*            var campaignImage = new Label();
            var imgButton = new Button
            {
                Text = "Upload Image"
            };
            imgButton.Clicked += async (sender, e) =>
            {
                var imgPath = App.MediaPicker.GetImage();
                campaignImage.Text = imgPath;
            };*/

            var targetLabel = new Label { Text = "Target Amount", HorizontalOptions = LayoutOptions.Center};
            _campaignTargetLabel = new Label
            {
                Text = String.Format("{0}", _minCampaignTarget.ToString("C", CultureInfo.CurrentCulture)),
                TextColor = Color.FromHex("f79848"),
                Font = Font.SystemFontOfSize(50, FontAttributes.Bold),
                HorizontalOptions = LayoutOptions.Center
            };

            var campaignTarget = new Slider
            {
                Maximum = 5000,
                Minimum = _minCampaignTarget
            };
            campaignTarget.ValueChanged += async (sender, e) =>
            {
                _campaignTargetLabel.Text = String.Format("{0}", e.NewValue.ToString("C", CultureInfo.CurrentCulture));
                viewModel.CampaignTargetAmmount = e.NewValue;
            };

            var button = new DefaultButton
            {
                Text = "CREATE CAMPAIGN",
                VerticalOptions = LayoutOptions.End
            };
            button.SetBinding(Button.CommandProperty, CreateCampaignViewModel.AddCommandPropertyName);

            var layout = new StackLayout
            {
                Children =
                {
                    campaignName,
                    campaignDescription,
                    targetDateLabel,
                    campaignTargetDate,
/*                    campaignImage,
                    imgButton,*/
                    targetLabel,
                    _campaignTargetLabel,
                    campaignTarget,
                    button
                }
            };

            Content = new ScrollView
            {
                Content = layout
            };
        }
    }
}
