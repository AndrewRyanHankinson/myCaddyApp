using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using System.Collections.Generic;
using AndroidX.Fragment;
using System;
using myCaddyApp.Fragments;
using Google.Android.Material.Navigation;
using AndroidX.ViewPager.Widget;

namespace myCaddyApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        //Setup for tabbed page, initilaises fragments in list to be used for navigation
        private BottomNavigationView mBottomNavigationView;
        ViewPager viewPager;


        List<AndroidX.Fragment.App.Fragment> mFragments;
        private int lastIndex;

        AndroidX.AppCompat.Widget.Toolbar toolbar;
        AndroidX.DrawerLayout.Widget.DrawerLayout drawerLayout;
        Google.Android.Material.Navigation.NavigationView navigationView;
        



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InitBottomNavigation();
            InitFragments();

            toolbar = (AndroidX.AppCompat.Widget.Toolbar)FindViewById(Resource.Id.toolbar); 
            drawerLayout = (AndroidX.DrawerLayout.Widget.DrawerLayout)FindViewById(Resource.Id.drawerLayout);
            navigationView = (Google.Android.Material.Navigation.NavigationView)FindViewById(Resource.Id.navview);
            navigationView.NavigationItemSelected += NavigationView_Navigation_Item_Selected;

            //Set Up Toolbar
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "";
            AndroidX.AppCompat.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetHomeAsUpIndicator(Resource.Drawable.menuaction);
            actionBar.SetDisplayHomeAsUpEnabled(true);

            
        }

        private void NavigationView_Navigation_Item_Selected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            if (e.MenuItem.ItemId == Resource.Id.weather_menu_option)
            {
                SetFragmentPosition(3);
            }
            else if (e.MenuItem.ItemId == Resource.Id.directions_menu_option_)
            {
                SetFragmentPosition(4);
            }
            else if (e.MenuItem.ItemId == Resource.Id.yardages_menu_option)
            {
                SetFragmentPosition(5);
            }
                

            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer((int)GravityFlags.Left);
                    return true;

                    default:
                    return base.OnOptionsItemSelected(item);
            }
        }



        private void InitFragments()
        {
            mFragments = new List<AndroidX.Fragment.App.Fragment>
            {
                new LoadFragment(),
                new RangeFragment(),
                new ScoreFragment(),
                new WeatherFragment(),
                new DirectionsFragment(),
                new YardagesFragment()
            };
            //Initialisation display: LoadFragment, load course page
            SetFragmentPosition(0);
        }

        private void SetFragmentPosition(int position)
        {
            
            AndroidX.Fragment.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
            AndroidX.Fragment.App.Fragment currentFragment = mFragments[position];
            AndroidX.Fragment.App.Fragment lastFragment = mFragments[lastIndex];
            lastIndex = position;
            ft.Hide(lastFragment);
            if (!currentFragment.IsAdded)
            {
                SupportFragmentManager.BeginTransaction().Remove(currentFragment).Commit();
                ft.Add(Resource.Id.ll_frameLayout, currentFragment);
            }
            ft.Show(currentFragment);
            ft.CommitAllowingStateLoss();
            
        }

        private void InitBottomNavigation()
        {
            mBottomNavigationView = (BottomNavigationView)FindViewById(Resource.Id.navigation);

            mBottomNavigationView.SetOnNavigationItemSelectedListener(this);
        }

        

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    SetFragmentPosition(0);
                    return true;
                case Resource.Id.navigation_dashboard:
                    SetFragmentPosition(1);
                    return true;
                case Resource.Id.navigation_notifications:
                    SetFragmentPosition(2);
                    return true;
            }
            return false;
        }
    }
}

