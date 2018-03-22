namespace W1010_ABP_NetCode2.SignalR
{
    public static class SignalRFeature
    {
        public static bool IsAvailable
        {
            get
            {
#if FEATURE_SIGNALR
                return true;
#elif FEATURE_SIGNALR_ASPNETCORE
                return true;
#else
                return false;
#endif
            }
        }

        public static bool IsAspNetCore
        {
            get
            {
#if FEATURE_SIGNALR_ASPNETCORE
                return true;
#else
                return false;
#endif
            }
        }
    }
}
