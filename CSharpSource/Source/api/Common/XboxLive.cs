﻿// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services
#else
namespace Microsoft.Xbox.Services
#endif
{
    using global::System;
    using global::System.IO;
    using global::System.ComponentModel;
    using global::System.Runtime.InteropServices;
    using Microsoft.Xbox.Services.Presence;
    using Microsoft.Xbox.Services.Social.Manager;
    using Microsoft.Xbox.Services.Statistics.Manager;

    public partial class XboxLive : IDisposable
    {
        private bool disposed;
        private static XboxLive instance;
        private static IntPtr xsapiNativeDll = IntPtr.Zero;
        private XboxLiveSettings settings;
        private IStatsManager statsManager;
        private ISocialManager socialManager;
        private IPresenceWriter presenceWriter;

        private static readonly object instanceLock = new object();
        private readonly XboxLiveAppConfiguration appConfig;

#if XDK_API
        internal const string FlatCDllName = "Microsoft.Xbox.Services.140.XDK.C.dll";

        [DllImport(FlatCDllName)]
        public static extern void XBLGlobalInitialize();

        [DllImport(FlatCDllName)]
        public static extern void XBLGlobalCleanup();
#else
        private delegate void XBLGlobalInitialize();
        private delegate void XBLGlobalCleanup();
#endif

        private XboxLive()
        {
            this.settings = new XboxLiveSettings();

            try
            {
                this.appConfig = XboxLiveAppConfiguration.Instance;
            }
            catch (FileLoadException)
            {
                this.appConfig = null;
            }

#if NETFX_CORE
            string fileName = @"\Microsoft.Xbox.Services.140.UWP.C.dll";
            try
            {
                string path = Directory.GetCurrentDirectory() + fileName;
                xsapiNativeDll = LoadNativeDll(path);

                this.Invoke<XBLGlobalInitialize>();
            }
            catch (Exception)
            {
                throw new XboxException("Failed to load " + fileName);
            }
#elif XDK_API
            XBLGlobalInitialize();
#endif
        }

        ~XboxLive()
        {
#if XDK_API
            XBLGlobalCleanup();
#else
            this.Invoke<XBLGlobalCleanup>();
#endif
        }

        public static XboxLive Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new XboxLive();
                        }
                    }
                }
                return instance;
            }

            private set
            {
                instance = null;
            }
        }

        public IPresenceWriter PresenceWriter {
            get 
			{
                if (Instance.presenceWriter == null) {
                    Instance.presenceWriter = Presence.PresenceWriter.Instance;
                }
                return Instance.presenceWriter;
            }
        }

        public ISocialManager SocialManager
        {
            get
            {
                if (Instance.socialManager == null)
                {
                    Instance.socialManager = Social.Manager.SocialManager.Instance;
                }
                return Instance.socialManager;
            }
        }

        public IStatsManager StatsManager
        {
            get
            {
                if (Instance.statsManager == null)
                {
                    Instance.statsManager = Statistics.Manager.StatsManager.Instance;
                }
                return Instance.statsManager;
            }
        }

        public XboxLiveSettings Settings
        {
            get { return Instance.settings; }
            set { Instance.settings = value; }
        }

        public XboxLiveAppConfiguration AppConfig
        {
            get { return Instance.appConfig; }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Instance = null;
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static Int64 DefaultTaskGroupId
        {
            get
            {
                return 0;
            }
        }

#if !XDK_API
        internal static class NativeMethods
        {
            [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
            internal static extern IntPtr LoadLibrary(string lpFileName);

            [DllImport("kernel32", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport("kernel32")]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
        }

        public static IntPtr LoadNativeDll(string fileName)
        {
            IntPtr nativeDll = NativeMethods.LoadLibrary(fileName);
            if (nativeDll == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            return nativeDll;
        }
        
        public T Invoke<T, T2>(params object[] args)
        {
            IntPtr procAddress = NativeMethods.GetProcAddress(xsapiNativeDll, typeof(T2).Name);
            if (procAddress == IntPtr.Zero)
            {
                return default(T);
            }
#if WINDOWS_UWP
            var function = Marshal.GetDelegateForFunctionPointer<T2>(procAddress) as Delegate;
#else
            var function = Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T2));
#endif
            return (T)function.DynamicInvoke(args);
        }

        public void Invoke<T>(params object[] args)
        {
            IntPtr procAddress = NativeMethods.GetProcAddress(xsapiNativeDll, typeof(T).Name);
            if (procAddress == IntPtr.Zero)
            {
                return;
            }
#if WINDOWS_UWP
            var function = Marshal.GetDelegateForFunctionPointer<T>(procAddress) as Delegate;
#else
            var function = Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T));            
#endif
            function.DynamicInvoke(args);
        }
#endif //!XDK_API
    }
}
