using Android.App;
using System;
using Android.Content;
using Android.OS;
using Android.Widget;
using TMSNextLevel;
using Android.Locations;
using Android.Runtime;
using System.Collections.Generic;
using System.Linq;
using Java.Lang;

[Service]
public class ClsService : Service, ILocationListener
{

    bool isRunning = true;
    string addressToSend = "undefined";
    private LocationManager locationManager;
    private string locationProvider;
    TMSNextLevel.webrefMStrongCreates.WebService client = new TMSNextLevel.webrefMStrongCreates.WebService();
    Handler myHandler = new Handler();
    ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);

    public ClsService()
    {
    }

    public override IBinder OnBind(Intent intent)
    {
        throw new NotImplementedException();
    }

    public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
    {
        // START - Create the Notification
        Intent intent2 = new Intent(this, typeof(Secondary));
        const int pendingIntentId = 0;
        PendingIntent pendingIntent = PendingIntent.GetActivity(this, pendingIntentId, intent2, PendingIntentFlags.UpdateCurrent);
        Notification.Builder builder = new Notification.Builder(this)
                    .SetContentIntent(pendingIntent)
                    .SetContentTitle("TMS Next Level is Tracking")
                    .SetContentText("Touch to Launch App")
                    .SetOngoing(true)
                    .SetSmallIcon(Resource.Drawable.icon);
        Notification notification = builder.Build();
        NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
        const int notificationId = 0;
        notificationManager.Notify(notificationId, notification);

        // START - Create the Location Manager
        locationManager = (LocationManager)GetSystemService(LocationService);
        Criteria locationServiceCriteria = new Criteria {
            Accuracy = Accuracy.Fine
        };
        IList<string> acceptableLocationProvider = locationManager.GetProviders(locationServiceCriteria, true);
        if (acceptableLocationProvider.Any()) { 
            locationProvider = acceptableLocationProvider.First();
        } else { 
            locationProvider = System.String.Empty;
        }
        locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);

        addressToSend = prefs.GetString("strAddress", null);

        DoWork();
        myHandler.Post(() => {
            Toast.MakeText(this, "Tracking has started", ToastLength.Long).Show();
        });
        
        return StartCommandResult.Sticky;
    }

    public void DoWork()
    {
        var t = new System.Threading.Thread(() =>
        {
            System.Threading.Thread.Sleep(10000);
            while (isRunning) {
                var truckName = prefs.GetString("strTruckName", null);
                var compID = prefs.GetString("strCompID", null);
                var interval = prefs.GetString("interval", null);
                client.updateVehicle(Int32.Parse(compID), truckName, addressToSend, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                myHandler.Post(() => {
                    Toast.MakeText(this, "New Position Sent - " + addressToSend, ToastLength.Long).Show();
                });
                System.Threading.Thread.Sleep(Int32.Parse(interval));
            }
        });
        t.Start();
    }

    public override void OnDestroy()
    {
        isRunning = false;
        locationManager.RemoveUpdates(this);
        NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
        notificationManager.Cancel(0);
        myHandler.Post(() => {
            Toast.MakeText(this, "Tracking is Now Stopped", ToastLength.Long).Show();
        });
    }

    public void OnLocationChanged(Location location)
    {
        try {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList = geocoder.GetFromLocation(location.Latitude, location.Longitude, 5);
            Address address = addressList.FirstOrDefault();
            if (address != null) {

                if (address.FeatureName == address.Thoroughfare)
                {
                    var deviceAddress = address.FeatureName + ", " + address.Locality + ", " + address.AdminArea + ", " + address.CountryCode + ", " + address.PostalCode;
                    addressToSend = deviceAddress;
                }
                else
                {
                    var deviceAddress = address.FeatureName + " " + address.Thoroughfare + ", " + address.Locality + ", " + address.AdminArea + ", " + address.CountryCode + ", " + address.PostalCode;
                    addressToSend = deviceAddress;
                }
            }
            else
            {
                addressToSend = "Address not found.";
            }
        }
        catch {
            addressToSend = "unknown error";
        }
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