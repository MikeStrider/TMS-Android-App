using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Locations;
using TMSNetLevel_AndroidApp;
using Plugin.Connectivity;

namespace TMSNextLevel
{
    [Activity(Theme = "@android:style/Theme.Material.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Icon = "@drawable/icon")]
    public class Secondary : Activity, ILocationListener
    {
        TextView addressText;
        TextView locationText;
        LocationManager locationManager;
        string locationProvider;
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);
        Handler myHandler = new Handler();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Secondary);

            addressText = FindViewById<TextView>(Resource.Id.addressText);
            locationText = FindViewById<TextView>(Resource.Id.gpsLocationText);
            EditText txtTruckName = FindViewById<EditText>(Resource.Id.txtTruckName);
            EditText txtCompID = FindViewById<EditText>(Resource.Id.txtCompID);
            Button btnGoToAvailability = FindViewById<Button>(Resource.Id.btnGoToAvailability);
            Button btnStart = FindViewById<Button>(Resource.Id.btnStart);
            Button btnStop = FindViewById<Button>(Resource.Id.btnStop);
            RadioButton radio20secs = FindViewById<RadioButton>(Resource.Id.radio20secs);
            RadioButton radio5mins = FindViewById<RadioButton>(Resource.Id.radio5mins);
            RadioButton radio15mins = FindViewById<RadioButton>(Resource.Id.radio15mins);
            RadioButton radio30mins = FindViewById<RadioButton>(Resource.Id.radio30mins);
            RadioButton radio1hour = FindViewById<RadioButton>(Resource.Id.radio1hour);
            RadioButton radio2hours = FindViewById<RadioButton>(Resource.Id.radio2hours);

            // ON LOAD
            if (!CrossConnectivity.Current.IsConnected)
            {
                myHandler.Post(() => {
                    Toast.MakeText(this, "No internet detected.  Resource failed to load.", ToastLength.Long).Show();
                });
                StartActivity(new Intent(this.ApplicationContext, typeof(MainActivity)));
                Finish();
            }
            else
            {
            btnStop.Enabled = false;
            txtTruckName.Text = prefs.GetString("strTruckName", null);
            txtCompID.Text = prefs.GetString("strCompID", null);
            var whichRadio = prefs.GetString("interval", null);
            addressText.Text = prefs.GetString("strAddress", null);
            if (isMyServiceRunning(typeof(ClsService)))
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                radio20secs.Enabled = false;
                radio5mins.Enabled = false;
                radio15mins.Enabled = false;
                radio30mins.Enabled = false;
                radio1hour.Enabled = false;
                radio2hours.Enabled = false;
            } else {
                radio20secs.Enabled = true;
                radio5mins.Enabled = true;
                radio15mins.Enabled = true;
                radio30mins.Enabled = true;
                radio1hour.Enabled = true;
                radio2hours.Enabled = true;
            }

            switch(whichRadio)
            {
                case "20000":
                    radio20secs.Enabled = true;
                    radio20secs.Checked = true;
                    break;
                case "300000":
                    radio5mins.Enabled = true;
                    radio5mins.Checked = true;
                    break;
                case "900000":
                    radio15mins.Enabled = true;
                    radio15mins.Checked = true;
                    break;
                case "1800000":
                    radio30mins.Enabled = true;
                    radio30mins.Checked = true;
                    break;
                case "3600000":
                    radio1hour.Enabled = true;
                    radio1hour.Checked = true;
                    break;
                case "7200000":
                    radio2hours.Enabled = true;
                    radio2hours.Checked = true;
                    break;
            }

            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria locationServiceCriteria = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProvider = locationManager.GetProviders(locationServiceCriteria, true);
            if (acceptableLocationProvider.Any())
                locationProvider = acceptableLocationProvider.First();
            else
                locationProvider = String.Empty;

            btnGoToAvailability.Click += delegate
            {
                StartActivity(new Intent(this.ApplicationContext, typeof(MainActivity)));
                Finish();
            };

            btnStart.Click += delegate
            {
                int myInt;
                bool isNumerical = int.TryParse(txtCompID.Text, out myInt);
                if (isNumerical == true)
                {
                    if(radio20secs.Checked == true || radio5mins.Checked == true || radio15mins.Checked == true || radio30mins.Checked == true || radio1hour.Checked == true || radio2hours.Checked == true)
                    {
                        ISharedPreferencesEditor editor = prefs.Edit();
                        editor.PutString("strTruckName", txtTruckName.Text);
                        editor.PutString("strCompID", txtCompID.Text);
                        editor.PutString("strAddress", addressText.Text);
                        editor.Apply();
                        whichRadio = prefs.GetString("interval", null);
                        btnStop.Enabled = true;
                        btnStart.Enabled = false;
                        radio20secs.Enabled = false;
                        radio5mins.Enabled = false;
                        radio15mins.Enabled = false;
                        radio30mins.Enabled = false;
                        radio1hour.Enabled = false;
                        radio2hours.Enabled = false;
                        switch (whichRadio)
                        {
                            case "20000":
                                radio20secs.Enabled = true;
                                radio20secs.Checked = true;
                                break;
                            case "300000":
                                radio5mins.Enabled = true;
                                radio5mins.Checked = true;
                                break;
                            case "900000":
                                radio15mins.Enabled = true;
                                radio15mins.Checked = true;
                                break;
                            case "1800000":
                                radio30mins.Enabled = true;
                                radio30mins.Checked = true;
                                break;
                            case "3600000":
                                radio1hour.Enabled = true;
                                radio1hour.Checked = true;
                                break;
                            case "7200000":
                                radio2hours.Enabled = true;
                                radio2hours.Checked = true;
                                break;
                        }
                        StartService(new Intent(this, typeof(ClsService)));
                    }
                    else
                    {
                        AlertDialog.Builder ad = new AlertDialog.Builder(this);
                        ad.SetTitle("TMS Next Level");
                        ad.SetMessage("Please select an interval");
                        ad.Show();
                    }
                }
                else
                {
                    AlertDialog.Builder ad = new AlertDialog.Builder(this);
                    ad.SetTitle("TMS Next Level");
                    ad.SetMessage("Please enter a valid Company ID");
                    ad.Show();
                }
            };

            btnStop.Click += delegate
            {
                StopService(new Intent(this, typeof(ClsService)));
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                radio20secs.Enabled = true;
                radio5mins.Enabled = true;
                radio15mins.Enabled = true;
                radio30mins.Enabled = true;
                radio1hour.Enabled = true;
                radio2hours.Enabled = true;
            };

            radio20secs.Click += delegate
            {
                radio20secs.Checked = true;
                radio5mins.Checked = false;
                radio15mins.Checked = false;
                radio30mins.Checked = false;
                radio1hour.Checked = false;
                radio2hours.Checked = false;
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("interval", "20000");
                editor.Apply();
            };

            radio5mins.Click += delegate
            {
                radio20secs.Checked = false;
                radio5mins.Checked = true;
                radio15mins.Checked = false;
                radio30mins.Checked = false;
                radio1hour.Checked = false;
                radio2hours.Checked = false;
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("interval", "300000");
                editor.Apply();
            };

            radio15mins.Click += delegate
            {
                radio20secs.Checked = false;
                radio5mins.Checked = false;
                radio15mins.Checked = true;
                radio30mins.Checked = false;
                radio1hour.Checked = false;
                radio2hours.Checked = false;
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("interval", "900000");
                editor.Apply();
            };

            radio30mins.Click += delegate
            {
                radio20secs.Checked = false;
                radio5mins.Checked = false;
                radio15mins.Checked = false;
                radio30mins.Checked = true;
                radio1hour.Checked = false;
                radio2hours.Checked = false;
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("interval", "1800000");
                editor.Apply();
            };

            radio1hour.Click += delegate
            {
                radio20secs.Checked = false;
                radio5mins.Checked = false;
                radio15mins.Checked = false;
                radio30mins.Checked = false;
                radio1hour.Checked = true;
                radio2hours.Checked = false;
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("interval", "3600000");
                editor.Apply();
            };

            radio2hours.Click += delegate
            {
                radio20secs.Checked = false;
                radio5mins.Checked = false;
                radio15mins.Checked = false;
                radio30mins.Checked = false;
                radio1hour.Checked = false;
                radio2hours.Checked = true;
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("interval", "7200000");
                editor.Apply();
            };
            }
        }

        private bool isMyServiceRunning(System.Type cls)
        {
            ActivityManager manager = (ActivityManager)GetSystemService(Context.ActivityService);
            foreach (var service in manager.GetRunningServices(int.MaxValue))
            {
                if (service.Service.ClassName.Equals(Java.Lang.Class.FromType(cls).CanonicalName))
                {
                    return true;
                }
            }
            return false;
        }

        public void OnLocationChanged(Location location)
        {
                locationText.Text = String.Format("{0} {1}", location.Latitude, location.Longitude);
                Geocoder geocoder = new Geocoder(this);
                IList<Address> addressList = geocoder.GetFromLocation(location.Latitude, location.Longitude, 5);
                Address address = addressList.FirstOrDefault();
            if (address != null)
            {
                if (address.FeatureName == address.Thoroughfare)
                {
                    var deviceAddress = address.FeatureName + ", " + address.Locality + ", " + address.AdminArea + ", " + address.CountryCode + ", " + address.PostalCode;
                    addressText.Text = deviceAddress;
                }
                else
                {
                    var deviceAddress = address.FeatureName + " " + address.Thoroughfare + ", " + address.Locality + ", " + address.AdminArea + ", " + address.CountryCode + ", " + address.PostalCode;
                    addressText.Text = deviceAddress;
                }
            }
            else
            {
                addressText.Text = "Lat and Long found, but Address not found.";
            } 
        }

        protected override void OnResume()
        {
            base.OnResume();
            locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        public void OnProviderDisabled(string provider)
        {

        }

        public void OnProviderEnabled(string provider)
        {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }
    }
}