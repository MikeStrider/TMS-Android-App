using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using TMSNextLevel;
using static Android.App.ActivityManager;
using System.Threading;
using System.Threading.Tasks;

namespace TMSNetLevel_AndroidApp
{
    [Activity(Theme = "@android:style/Theme.Material.NoActionBar", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("PREF_NAME", FileCreationMode.Private);
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            EditText userid = FindViewById<EditText>(Resource.Id.EditText_userID);
            EditText password = FindViewById<EditText>(Resource.Id.EditText_Password);
            EditText location = FindViewById<EditText>(Resource.Id.EditText_Location);
            EditText date = FindViewById<EditText>(Resource.Id.EditText_Date);
            EditText time = FindViewById<EditText>(Resource.Id.EditText_Time);
            Button button = FindViewById<Button>(Resource.Id.Button_setavail);
            Button button2 = FindViewById<Button>(Resource.Id.Button_setnotavail);
            Button button3 = FindViewById<Button>(Resource.Id.Button_getstatus);
            Button btnGoToTracking = FindViewById<Button>(Resource.Id.btnGoToTracking);
            TMSNextLevel.webrefMStrongCreates.WebService client = new TMSNextLevel.webrefMStrongCreates.WebService();

            userid.Text = prefs.GetString("strUserID", null);
            password.Text = prefs.GetString("strPassword", null);

            btnGoToTracking.Click += delegate
            {
                StartActivity(new Intent(this.ApplicationContext, typeof(Secondary)));
                Finish();
            };

            button.Click += delegate    // set driver avail
            {
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("strUserID", userid.Text);
                editor.PutString("strPassword", password.Text);
                editor.Apply();

                ProgressDialog progressDialog = ProgressDialog.Show(this, "Loading...", "checking account info...", true);
                var dialogRunning = 1;

                Task.Run(() =>
                {
                    Thread.Sleep(20000);
                    if (dialogRunning == 1)
                    {
                        RunOnUiThread(() =>
                        {
                            AlertDialog.Builder ad = new AlertDialog.Builder(this);
                            ad.SetTitle("Failed");
                            ad.SetMessage("Operation Timed Out. Service Unreachable.");
                            ad.Show();
                            progressDialog.Dismiss();
                        });
                    }
                });
                Task.Run(() =>
                {
                    var result = client.SetDriverAvail(userid.Text, password.Text, location.Text, date.Text + time.Text);
                    RunOnUiThread(() => 
                    {
                        if (result == 1)
                        {
                            AlertDialog.Builder ad = new AlertDialog.Builder(this);
                            ad.SetTitle("Success");
                            ad.SetMessage("You are now set to Available.");
                            ad.Show();
                            dialogRunning = 0;
                        }
                        else
                        {
                            AlertDialog.Builder ad = new AlertDialog.Builder(this);
                            ad.SetTitle("Failed");
                            ad.SetMessage("Please check your login credentials. Location, Date, and Time are required.");
                            ad.Show();
                            dialogRunning = 0;
                        }
                        progressDialog.Dismiss();
                    });
                });

            };

            button2.Click += delegate
            {
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("strUserID", userid.Text);
                editor.PutString("strPassword", password.Text);
                editor.Apply();

                ProgressDialog progressDialog = ProgressDialog.Show(this, "Loading...", "checking account info...", true);
                var dialogRunning = 1;

                Task.Run(() =>
                {
                    Thread.Sleep(20000);
                    if (dialogRunning == 1)
                    {
                        RunOnUiThread(() =>
                        {
                            AlertDialog.Builder ad = new AlertDialog.Builder(this);
                            ad.SetTitle("Failed");
                            ad.SetMessage("Operation Timed Out. Service Unreachable.");
                            ad.Show();
                            progressDialog.Dismiss();
                        });
                    }
                });
                Task.Run(() =>
                {
                    var result = client.SetDriverNotAvail(userid.Text, password.Text);
                    RunOnUiThread(() =>
                    {
                        if (result == 1)
                        {
                            AlertDialog.Builder ad = new AlertDialog.Builder(this);
                            ad.SetTitle("Success");
                            ad.SetMessage("You are now set to NOT available.");
                            ad.Show();
                            dialogRunning = 0;
                        }
                        else
                        {
                            AlertDialog.Builder ad = new AlertDialog.Builder(this);
                            ad.SetTitle("Failed");
                            ad.SetMessage("Please check your login credentials.");
                            ad.Show();
                            dialogRunning = 0;
                        }
                        progressDialog.Dismiss();
                    });
                });
            };

            button3.Click += delegate    // GET STATUS
            {
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("strUserID", userid.Text);
                editor.PutString("strPassword", password.Text);
                editor.Apply();

                ProgressDialog progressDialog = ProgressDialog.Show(this, "Loading...", "checking account info...", true);
                var dialogRunning = 1;

                Task.Run(() =>     // RUN TIMEOUT CHECKER
                {
                    Thread.Sleep(20000);
                    if (dialogRunning == 1)
                    {
                        RunOnUiThread(() =>
                        {
                            AlertDialog.Builder ad = new AlertDialog.Builder(this);
                            ad.SetTitle("Failed");
                            ad.SetMessage("Operation Timed Out. Service Unreachable.");
                            ad.Show();
                            progressDialog.Dismiss();
                        });
                    }
                });

                Task.Run(() =>   // RUN WebService call in Thread, once returned display Dialog
                {
                    var result = client.GetStatus(userid.Text, password.Text);
                    RunOnUiThread(() =>
                    {
                        AlertDialog.Builder ad = new AlertDialog.Builder(this);
                        ad.SetTitle("TMS Next Level");
                        ad.SetMessage(result);
                        ad.Show();
                        progressDialog.Dismiss();
                        dialogRunning = 0;
                    });
                });
            };

            date.Click += (sender, e) =>
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                dialog.Show();
            };

            void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
            {
                string varTime = e.Date.ToString();
                varTime = varTime.Remove(varTime.Length - 11);
                date.Text = varTime;
            }

            time.Click += (sender, e) =>
            {
                DateTime today = DateTime.Today;
                TimePickerDialog dialog = new TimePickerDialog(this, OnDateSet2, today.Hour, today.Minute, false);
                dialog.Show();
            };

            void OnDateSet2(object sender, TimePickerDialog.TimeSetEventArgs e)
            {
                string hours = e.HourOfDay.ToString();
                if (e.HourOfDay < 10)
                {
                    hours = "0" + e.HourOfDay;
                }
                if (e.Minute < 10)
                {
                    time.Text = hours + ":0" + e.Minute;
                }
                else
                {
                    time.Text = hours + ":" + e.Minute;
                }
            } 
        }
    }
}

