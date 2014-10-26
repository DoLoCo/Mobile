using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Renderscripts;
using Android.Views;
using Doloco.Droid.Renderers;
using Doloco.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Android.Widget.Button;
using Color = Xamarin.Forms.Color;

[assembly:ExportRenderer(typeof(MapContentPage), typeof(MapContentPageRenderer))]

namespace Doloco.Droid.Renderers
{
    public class MapContentPageRenderer: PageRenderer
    {
        Android.Views.View _view;
        private GoogleMap _map;
        private MapFragment _mapFragment;
        private LatLng _initLatLng;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            var page = e.NewElement as MapContentPage;
            var activity = this.Context as Activity;

            if (activity != null)
            {
                _view = activity.LayoutInflater.Inflate(Resource.Layout.MapLayout, this, false);
                AddView(_view);
                if (page != null) _initLatLng = new LatLng(page.InitLat, page.InitLng);
                InitMapFragment(activity);
            }
            SetupMapIfNeeded();
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

        private void InitMapFragment(Activity activity)
        {
            var cameraPosition = new CameraPosition.Builder().Target(_initLatLng).Zoom(14.0f).Build();
            _mapFragment = activity.FragmentManager.FindFragmentByTag("mapWithOverlay") as MapFragment;
            if (_mapFragment != null) return;
            var mapOptions = new GoogleMapOptions()
                .InvokeMapType(GoogleMap.MapTypeNormal)
                .InvokeRotateGesturesEnabled(false)
                .InvokeZoomControlsEnabled(false)
                .InvokeCamera(cameraPosition)
                .InvokeCompassEnabled(true);

            var fragTx = activity.FragmentManager.BeginTransaction();
            _mapFragment = MapFragment.NewInstance(mapOptions);

            fragTx.Add(Resource.Id.mapWithOverlay, _mapFragment, "mapWithOverlay");
            fragTx.Commit();
        }

        private void SetupMapIfNeeded()
        {
            if (_map != null) return;
            _map = _mapFragment.Map;
            if (_map == null) return;
            ZoomToPosition();
            SetupZoomInButton();
            SetupZoomOutButton();

            var mapOverlay = new GroundOverlayOptions()
                .Position(_initLatLng, 150, 200)
                .InvokeImage(BitmapDescriptorFactory.FromResource(Resource.Drawable.Icon));
            _map.AddGroundOverlay(mapOverlay);
        }

        private void ZoomToPosition()
        {
            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(_initLatLng, 15);
            
            _map.MoveCamera(cameraUpdate);
        }

        private void SetupZoomInButton()
        {
            var zoomInButton = FindViewById<Button>(Resource.Id.zoomInButton);
            zoomInButton.Click += (sender, e) => _map.AnimateCamera(CameraUpdateFactory.ZoomIn());
        }

        private void SetupZoomOutButton()
        {
            var zoomOutButton = FindViewById<Button>(Resource.Id.zoomOutButton);
            zoomOutButton.Click += (sender, e) => _map.AnimateCamera(CameraUpdateFactory.ZoomOut());
        }
    }
}