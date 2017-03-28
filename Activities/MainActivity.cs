using System.IO;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using JimBobBennett.MvvmLight.AppCompat;
using LoginScreen;
using WhatOnEvents.Droid;
using WhatsOnEvents.Core;
using WhatsOnEvents.Core.ViewModel;
using WhatsOnEvents.Droid.Fragments;
using Environment = System.Environment;
using Fragment = Android.Support.V4.App.Fragment;

namespace WhatsOnEvents.Droid.Activities
{
    [Activity(Label = "Whats On Events", Icon = "@drawable/Icon")]
    public class MainActivity : BaseActivity
    {
        private DrawerLayout _drawerLayout;
        private NavigationView _navigationView;

        private int _oldPosition = -1;

        public MainActivity()
        {
            var navigationService = new AppCompatNavigationService();
            navigationService.Configure(ViewModelLocator.NewCounterPageKey, typeof(NewCounterActivity));
            navigationService.Configure(ViewModelLocator.EditCounterPageKey, typeof(EditCounterActivity));
            ViewModelLocator.RegisterNavigationService(navigationService);
            ViewModelLocator.RegisterDialogService(new AppCompatDialogService());
        }

        protected override int LayoutResource => Resource.Layout.main;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            LoginScreenControl<CredentialsProvider>.Activate(this);
            //Login Screen function, If theres errors delete this and the credentials provider class
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(path, "counters.db3");
            DatabaseHelper.CreateDatabase(dbPath);

            await ViewModelLocator.Counters.LoadCountersAsync();

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //setup navigation view
            _navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //handle navigation
            _navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                // Switch statement for nav buttons
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_counters:
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_about:
                        ListItemClicked(1);
                        break;
                    case Resource.Id.nav_Add:
                        ListItemClicked(2);
                        break;
                    case Resource.Id.nav_Sort:
                        ListItemClicked(3);
                        break;
                }

                _drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
                ListItemClicked(0);
        }

        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == _oldPosition)
                return;

            _oldPosition = position;

            // Switch statement to pick a fragment from the nav bar
            Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = CountersFragment.NewInstance();
                    break;
                case 1:
                    fragment = AboutFragment.NewInstance();
                    break;
                case 2:
                    fragment = AddEventFragment.NewInstance();
                    break;
                case 3:
                    fragment = SortEventFragment.NewInstance();
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _drawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}