using SeeIP.Models;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace SeeIP;

public static class GetIP
{
    public static string IPv4(bool skipNetworkCheck = false) => GetIPAddressByVersion(IPVersion.IPv4, skipNetworkCheck);
    public static string IPv6(bool skipNetworkCheck = false) => GetIPAddressByVersion(IPVersion.IPv6, skipNetworkCheck);
    public static GeoInformation GeoInfo(bool skipNetworkCheck = false) => GetGeoInformation(skipNetworkCheck);

    private static string GetIPAddressByVersion(IPVersion version, bool skipNetworkCheck)
    {
        try
        {
            if (!skipNetworkCheck)
                PerformNetworkCheck();

            switch (version)
            {
                case IPVersion.IPv4:
                    return DownloadString(ApiStatics.IPv4_URL).Result;

                case IPVersion.IPv6:
                    return DownloadString(ApiStatics.IPv6_URL).Result;

                default:
                    throw new Exceptions.BadIPVersionException(version);
            }

        }
        catch (Exception ex)
        {
            throw new Exceptions.UnknownException(ex);
        }
    }

    private static GeoInformation GetGeoInformation(bool skipNetworkCheck)
    {
        try
        {
            if (!skipNetworkCheck)
                PerformNetworkCheck();

            var json = DownloadString(ApiStatics.Geo_URL).Result;

            var geoInfo = JsonSerializer.Deserialize<GeoInformation>(json);

            if (geoInfo == null)
                throw new Exceptions.NoGeoIPInfoException();

            return geoInfo;
        }
        catch (Exception ex)
        {
            throw new Exceptions.UnknownException(ex);
        }
    }

    private static void PerformNetworkCheck()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
            throw new Exceptions.NetworkUnavailableException();
    }

    private static async Task<string> DownloadString(string url)
    {
        try
        {
            var httpClient = new HttpClient();
            return await httpClient.GetStringAsync(url);
        }
        catch (Exception ex)
        {
            throw new Exceptions.BadWebRequestException(ex);
        }
    }
}
