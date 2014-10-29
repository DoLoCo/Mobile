using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Doloco.Droid.Renderers;
using Doloco.Pages;
using DolocoApiClient.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using Button = Android.Widget.Button;
using Color = Xamarin.Forms.Color;
using SearchView = Android.Support.V7.Widget.SearchView;

[assembly: ExportRenderer(typeof(MapContentPage), typeof(MapContentPageRenderer))]

namespace Doloco.Droid.Renderers
{
    public class MapContentPageRenderer : PageRenderer
    {
        Android.Views.View _view;
        private GoogleMap _map;
        private MapFragment _mapFragment;
        private LatLng _initLatLng;
        private IEnumerable<Campaign> _campaigns;
        private Activity _activity;
        private MapContentPage _page;
        private SearchView _searchView;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var page = e.NewElement as MapContentPage;
            _activity = this.Context as Activity;

            if (_activity != null)
            {
                _view = _activity.LayoutInflater.Inflate(Resource.Layout.MapLayout, this, false);
                AddView(_view);
                if (page != null)
                {
                    _initLatLng = new LatLng(page.InitLat, page.InitLng);
                    _campaigns = page.NearbyCampaigns;
                    _page = page;
                }
                _searchView = (SearchView) _activity.FindViewById(Resource.Id.mapSearch);
                InitMapFragment();
                SetupMapIfNeeded();
                SetupSearchView();
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
            if (_view == null) return;
            _view.Measure(msw, msh);
            _view.Layout(0, 0, r - l, b - t);
        }

        private void InitMapFragment()
        {
            _mapFragment = _activity.FragmentManager.FindFragmentById(Resource.Id.mapWithOverlay) as MapFragment;
        }

        private void SetupMapIfNeeded()
        {

            if (_map == null)
            {
                if (_mapFragment.Map != null)
                {
                    _map = _mapFragment.Map;
                }
            }

            if (_map != null)
            {
                ZoomToLocation(_initLatLng);

                if(_campaigns != null)
                    AddCampaignsToMap();

                SetMarkerInfoClick();
            }
        }

        private void AddCampaignsToMap()
        {
            foreach (var cp in _campaigns)
            {
                if (cp.Organization.Lat == null || cp.Organization.Lng == null) continue;
                var cPos = new LatLng((double)cp.Organization.Lat, (double)cp.Organization.Lng);
                Addmarker(cPos, cp.Title, String.Format("{0}: {1}", cp.Id, cp.Description));
            }
        }

        private void Addmarker(LatLng position, string title, string snippet)
        {
            var markerOptions = new MarkerOptions();
            markerOptions.SetPosition(position);
            markerOptions.SetTitle(title);
            markerOptions.InvokeIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));
            markerOptions.SetSnippet(snippet);

            _map.AddMarker(markerOptions);
        }

        private void ZoomToLocation(LatLng position)
        {
            var cameraPosition = new CameraPosition.Builder().Target(position).Zoom(14.0f).Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            _map.MoveCamera(cameraUpdate);
        }

        private void SetMarkerInfoClick()
        {
            _map.InfoWindowClick += MapOnInfoWindowClick;
        }

        private void MapOnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var myMarker = e.P0;
            Campaign selCampaign = null;
            foreach (var cp in _campaigns.Where(cp => cp.Organization.Lat != null && cp.Organization.Lng != null).Where(cp => cp.Title == myMarker.Title && String.Format("{0}: {1}", cp.Id, cp.Description) == myMarker.Snippet))
            {
                selCampaign = cp;
            }

            if (selCampaign != null)
            {
                var campaignPage = new CampaignPage(selCampaign.Id, selCampaign.OrganizationId);
                _page.Navigation.PushAsync(campaignPage);
            }
        }

        public void SetupSearchView()
        {
            if (_searchView != null)
            {
                _searchView.QueryTextSubmit += async (sender, e) =>
                {
                    Console.WriteLine(e.Query);
                };
            }
        }
    }
}