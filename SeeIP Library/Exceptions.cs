namespace SeeIP;
internal class Exceptions
{
    internal class BadIPVersionException : Exception
    {
        internal BadIPVersionException(IPVersion iPVersion) : base(String.Format("The parsed ip version {0} is invalid", iPVersion)) { }
        internal BadIPVersionException(IPVersion iPVersion, Exception nestedException) : base(String.Format("The parsed ip version {0} is invalid", iPVersion), nestedException) { }
    }

    internal class NoGeoIPInfoException : Exception
    {
        internal NoGeoIPInfoException() : base("The API did not return any information regarding your physical location.") { }
        internal NoGeoIPInfoException(Exception nestedException) : base("The API did not return any information regarding your physical location.", nestedException) { }
    }

    internal class BadWebRequestException : Exception
    {
        internal BadWebRequestException() : base("An error occurred during connecting to the API.") { }
        internal BadWebRequestException(Exception nestedException) : base("An error occurred during connecting to the API.", nestedException) { }
    }

    internal class NetworkUnavailableException : Exception
    {
        internal NetworkUnavailableException() : base("The network connection is being reported unavailable by the os.") { }
        internal NetworkUnavailableException(Exception nestedException) : base("The network connection is being reported unavailable by the os.", nestedException) { }
    }

    internal class UnknownException : Exception
    {
        internal UnknownException() : base("An unknown error occurred during fetching the ip.") { }
        internal UnknownException(Exception nestedException) : base("An unknown error occurred during fetching the ip.", nestedException) { }
    }
}