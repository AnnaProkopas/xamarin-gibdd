using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Provider;
using Android.Support.V4.App;
using Android.Graphics;
using Xamarin.Forms;
using System.IO;
using SQLite;

namespace Gibdd.Droid
{
    [Activity(Label = "Gibdd", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IPhotographerPlatform
    {
        private const int CameraRequest = 2;
        private byte[] imageData;
        public Action<byte[]> PhotoCallback { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(this));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            for (int i = 0; i < permissions.Length; i++)
            {
                var p = permissions[i];
                var g = i < grantResults.Length ? grantResults[i] : Permission.Denied;
                if (p == Android.Manifest.Permission.Camera  && g == Permission.Granted)
                {
                    TakePhotoInternal();
                }
                if (p == Android.Manifest.Permission.WriteExternalStorage && g == Permission.Granted)
                {
                    SaveImageInternal();
                }
            }
        }

        public void TakePhoto()
        {
            var permmission = Android.Manifest.Permission.Camera;
            if (Android.App.Application.Context.CheckSelfPermission(permmission) == Permission.Granted) {
                TakePhotoInternal();
            } else
            {
                ActivityCompat.RequestPermissions(this, new[] { permmission }, 0);
            }
        }

        private void TakePhotoInternal()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, CameraRequest);
        }

        public bool IsCameraAvailable()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            var availableActivities = Android.App.Application.Context.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == CameraRequest && resultCode == Result.Ok)
            {
                var bitmap = (Bitmap)data.Extras.Get("data");
                if (bitmap != null)
                {
                    byte[] bitmapData;
                    using (var stream = new MemoryStream()) {
                        bitmap.Compress(Bitmap.CompressFormat.Jpeg, 80, stream);
                        bitmapData = stream.ToArray();
                    }
                    PhotoCallback(bitmapData);
                }
            }
        }
        public void SaveImage(byte[] data) 
        {
            this.imageData = data;
            var permmission = Android.Manifest.Permission.WriteExternalStorage;
            if (Android.App.Application.Context.CheckSelfPermission(permmission) == Permission.Granted)
            {
                SaveImageInternal();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new[] { permmission }, 0);
            }
        }

        private void SaveImageInternal()
        {
            var picture = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            if (!picture.Exists())
            {
                picture.Mkdirs();
            }
            var file = new Java.IO.File(picture, $"Photo{Guid.NewGuid()}.jpg");
            File.WriteAllBytes(file.Path, imageData);

            var intent = new Intent(Intent.ActionMediaScannerScanFile);
            intent.SetData(Android.Net.Uri.FromFile(file));
            SendBroadcast(intent);
        }
        [Table("items")]
        private class Sample {
            [PrimaryKey, AutoIncrement, Column("id")]
            public int Id { get; set; }
            [MaxLength(255)]
            public string Text { get; set; }
        }

        int c = 0;
        public void SaveToDB()
        {
            var permmission = Android.Manifest.Permission.WriteExternalStorage;
            if (Android.App.Application.Context.CheckSelfPermission(permmission) == Permission.Granted)
            {
                var docs = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
                if (!docs.Exists())
                {
                    docs.Mkdirs();
                }
                var file = new Java.IO.File(docs, $"sampledb.db");
                var db = new SQLiteConnection(file.Path);
                db.CreateTable<Sample>();
                var sample = new Sample()
                {
                    Text = $"Text{++c}"
                };
                db.Insert(sample);
                db.Close();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new[] { permmission }, 0);
            }
        }

        public string ReadFromDB()
        {
            var docs = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            if (!docs.Exists())
            {
                docs.Mkdirs();
            }
            var file = new Java.IO.File(docs, $"sampledb.db");
            var db = new SQLiteConnection(file.Path);
            var result = "";
            foreach (var s in db.Table<Sample>()) {
                result += s.Text + '\n';
            }
            db.Close();
            return result;
        }
    }
}