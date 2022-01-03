namespace SeeIP;

internal static class ApiStatics
{
    internal static string IPv4_URL => "https://ip4.seeip.org/";
    internal static string IPv6_URL => "https://ip6.seeip.org/";
    internal static string Geo_URL => "https://ip.seeip.org/geoip";
}

internal enum IPVersion
{
    IPv4,
    IPv6
}