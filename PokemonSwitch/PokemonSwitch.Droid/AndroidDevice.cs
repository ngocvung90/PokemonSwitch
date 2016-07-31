// ***********************************************************************
// Assembly         : XLabs.Platform.Droid
// Author           : XLabs Team
// Created          : 12-27-2015
//
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="AndroidDevice.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//
//       XLabs is a open source project that aims to provide a powerfull and cross
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
//

using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using Android.Views;
using Java.IO;
using Java.Util;
using Java.Util.Concurrent;
using FileMode = System.IO.FileMode;
using PokemonSwitch.Interface;

namespace PokemonSwitch.Droid
{
    /// <summary>
    /// Android device implements <see cref="IDevice"/>.
    /// </summary>
    public class AndroidDevice : IDevice
    {
        private static readonly long DeviceTotalMemory = GetTotalMemory();
        private static IDevice currentDevice;

        public AndroidDevice()
        {
            this.Display = new Display();

            this.Manufacturer = Build.Manufacturer;
            this.Name = Build.Model;
            this.HardwareVersion = Build.Hardware;
            this.FirmwareVersion = Build.VERSION.Release;
        }

        public static IDevice CurrentDevice
        {
            get { return currentDevice ?? (currentDevice = new AndroidDevice()); }
            set { currentDevice = value; }
        }

        public virtual string Id
        {
            get
            {
                return Build.Serial;
            }
        }

        private static long GetTotalMemory()
        {

            using (var reader = new RandomAccessFile("/proc/meminfo", "r"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Log.Debug("Memory", line);
                }
            }

            using (var reader = new RandomAccessFile("/proc/meminfo", "r"))
            {
                var line = reader.ReadLine(); // first line --> MemTotal: xxxxxx kB
                var split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return Convert.ToInt64(split[1]) * 1024;
            }
        }
        public IDisplay Display { get; private set; }

        public string Name { get; private set; }

        public string FirmwareVersion { get; private set; }

        public string HardwareVersion { get; private set; }

        public string Manufacturer { get; private set; }

        public string LanguageCode
        {
            get { return Locale.Default.Language; }
        }

        public double TimeZoneOffset
        {
            get
            {
                using (var calendar = new GregorianCalendar())
                {
                    return TimeUnit.Hours.Convert(calendar.TimeZone.RawOffset, TimeUnit.Milliseconds) / 3600;
                }
            }
        }

        public string TimeZone
        {
            get { return Java.Util.TimeZone.Default.ID; }
        }

        public Task<bool> LaunchUriAsync(Uri uri)
        {
            var launchTaskSource = new TaskCompletionSource<bool>();
            try
            {
                StartActivity(new Intent("android.intent.action.VIEW", Android.Net.Uri.Parse(uri.ToString())));
                launchTaskSource.SetResult(true);
            }
            catch (Exception ex)
            {
                Log.Error("Device.LaunchUriAsync", ex.Message);
                launchTaskSource.SetException(ex);
            }

            return launchTaskSource.Task;
        }

        public void StartActivity(Intent intent)
        {
            var context = this;
            if (context != null)
            {
                context.StartActivity(intent);
            }
            else
            {
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            }
        }

        public long TotalMemory
        {
            get
            {
                return DeviceTotalMemory;
            }
        }
    }
}