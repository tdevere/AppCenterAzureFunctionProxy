using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;


namespace AppCenterAzureProxy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        System.Timers.Timer AppCenterEventTest;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppCenterEventTest = new System.Timers.Timer(5000);
            AppCenterEventTest.Elapsed += AppCenterEventTest_Elapsed;
            AppCenter.SetLogUrl("https://azurefunctionproxyforappcenter.azurewebsites.net");
            AppCenter.LogLevel = LogLevel.Verbose;
            AppCenter.Start(System.Environment.GetEnvironmentVariable("AppCenterAzureProxy", EnvironmentVariableTarget.Machine),
                   typeof(Analytics));

            AppCenterEventTest.Start();

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        private void AppCenterEventTest_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Analytics.TrackEvent("AppCenterEventTest_Elapsed");
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
