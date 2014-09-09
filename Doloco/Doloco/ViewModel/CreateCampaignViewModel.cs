using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doloco.ViewModel
{
    public class CreateCampaignViewModel:BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly int _orgId;
        public CreateCampaignViewModel(INavigation navigation, int orgId)
        {
            _navigation = navigation;
            _orgId = orgId;
        }

        public const string CampaignNamePropertyName = "CampaignName";
        private string _campaignName = string.Empty;
        public string CampaignName
        {
            get { return _campaignName; }
            set { SetProperty(ref _campaignName, value, CampaignNamePropertyName); }
        }

        public const string CampaignDescriptionPropertyName = "CampaignDescription";
        private string _campaignDescription = string.Empty;
        public string CampaignDescription
        {
            get { return _campaignDescription; }
            set { SetProperty(ref _campaignDescription, value, CampaignDescriptionPropertyName); }
        }

        public const string CampaignTargetPropertyName = "CampaignTarget";
        private string _campaignTarget = string.Empty;
        public string CampaignTarget
        {
            get { return _campaignTarget; }
            set { SetProperty(ref _campaignTarget, value, CampaignTargetPropertyName); }
        }

        public const string CampaignTargetDatePropertyName = "CampaignTargetDate";
        private DateTime _campaignTargetDate = DateTime.Now;
        public DateTime CampaignTargetDate
        {
            get { return _campaignTargetDate; }
            set {SetProperty(ref _campaignTargetDate, value, CampaignTargetDatePropertyName);}
        }

        private Command _addCommand;
        public const string AddCommandPropertyName = "AddCommand";
        public Command AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new Command(async () => await ExecuteAddCommand()));
            }
        }

        protected async Task ExecuteAddCommand()
        {
            try
            {
                await App.ApiClient.CreateOrganizationCampaignAsync(_orgId, _campaignName, _campaignDescription);

                await _navigation.PopAsync();
            }
            catch (Exception ex)
            {
                var page = new ContentPage();
                var result = page.DisplayAlert("Error", ex.Message, "OK", "Cancel");
            }
        }
    }
}
