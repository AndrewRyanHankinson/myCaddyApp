using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Location;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myCaddyApp.Fragments
{
    public class DirectionsFragment : AndroidX.Fragment.App.Fragment, IOnMapReadyCallback
    {

        GoogleMap map;
        FusedLocationProviderClient locationProviderClient;
        Android.Locations.Location myLastLocation;
        private LatLng myPosition;
        readonly string[] permissionsGroup = { Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation };


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            //RequestPermissions(permissionsGroup, 0);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View inf = inflater.Inflate(Resource.Layout.fragment_Directions, container, false);

            SupportMapFragment mapFragment = ChildFragmentManager.FindFragmentById(Resource.Id.map).JavaCast<SupportMapFragment>(); ;
            mapFragment.GetMapAsync(this);

            return inf;
        }




        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        //{
        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    if (grantResults.Length < 1)
        //    {
        //        return;
        //    }
        //    if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
        //    {
        //        DisplayLocation();
        //    }
        //}

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            var mapStyle = MapStyleOptions.LoadRawResourceStyle(Activity, Resource.Raw.mapstyle);
            googleMap.SetMapStyle(mapStyle);

            map.UiSettings.ZoomControlsEnabled = true;

            //DisplayLocation();

            //if (CheckPermission())
            //{
            //    DisplayLocation();

            //}

        }

        //bool CheckPermission()
        //{
        //    bool permissionGranted;

        //    if (ActivityCompat.CheckSelfPermission(Activity, Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted
        //        && ActivityCompat.CheckSelfPermission(Activity, Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted)
        //    {
        //        permissionGranted = false;
        //    }
        //    else
        //    {
        //        permissionGranted = true;
        //    }

        //    return permissionGranted;
        //}

        async void DisplayLocation()
        {
            if(locationProviderClient == null)
            {
                locationProviderClient = LocationServices.GetFusedLocationProviderClient(Activity);

            }
            myPosition = new LatLng(myLastLocation.Latitude, myLastLocation.Longitude);
            map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(myPosition, 15));

            //myLastLocation = await locationProviderClient.GetLastLocationAsync();
            //if (myLastLocation == null)
            //{
            //    myPosition = new LatLng(myLastLocation.Latitude, myLastLocation.Longitude);
            //    map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(myPosition, 15));
            //}
        }
    }
}