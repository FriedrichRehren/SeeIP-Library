using SeeIP.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeeIP
{
    public static class GetIP
    {
        /// <summary>
        /// Get the current IPv4 address.
        /// </summary>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>IP in string format</returns>
        public static string IPv4(bool skipNetworkCheck = false) =>
            GetIPAddressByVersion(IPVersion.IPv4, skipNetworkCheck);

        /// <summary>
        /// Asynchronously get the current IPv4 address.
        /// </summary>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>IP in string format</returns>
        public static async Task<string> IPv4Async(bool skipNetworkCheck = false) =>
            await GetIPAddressByVersionAsync(IPVersion.IPv4, skipNetworkCheck);

        /// <summary>
        /// Get the current IPv6 address.
        /// </summary>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>IP in string format</returns>
        public static string IPv6(bool skipNetworkCheck = false) =>
            GetIPAddressByVersion(IPVersion.IPv6, skipNetworkCheck);

        /// <summary>
        /// Asynchronously get the current IPv6 address.
        /// </summary>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>IP in string format</returns>
        public static async Task<string> IPv6Async(bool skipNetworkCheck = false) =>
            await GetIPAddressByVersionAsync(IPVersion.IPv6, skipNetworkCheck);

        /// <summary>
        /// Get the geolocation information associated with your ip.
        /// </summary>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>Custom GeoInformation object</returns>
        public static GeoInformation GeoInfo(bool skipNetworkCheck = false) =>
            GetGeoInformation(skipNetworkCheck);

        /// <summary>
        /// Asynchronously get the geolocation information associated with your ip.
        /// </summary>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>Custom GeoInformation object</returns>
        public static async Task<GeoInformation> GeoInfoAsync(bool skipNetworkCheck = false) =>
            await GetGeoInformationAsync(skipNetworkCheck);

        /// <summary>
        /// Convert to <see cref="IPAddress"/>.
        /// </summary>
        /// <param name="ip">The ip as a string</param>
        /// <returns>IP in <see cref="IPAddress"/> format</returns>
        /// <exception cref="Exceptions.ParsingException"></exception>
        public static IPAddress AsIPAddress(this string ip)
        {
            try
            {
                return IPAddress.Parse(ip);
            }
            catch (Exception ex)
            {
                throw new Exceptions.ParsingException(ex);
            }
        }

        /// <summary>
        /// Get the ip address.
        /// </summary>
        /// <param name="version">IP Version <see cref="IPVersion"/></param>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>IP in string format</returns>
        /// <exception cref="Exceptions.UnknownException"></exception>
        private static string GetIPAddressByVersion(IPVersion version, bool skipNetworkCheck)
        {
            try
            {
                if (!skipNetworkCheck)
                    PerformNetworkCheck();

                switch (version)
                {
                    case IPVersion.IPv4:
                        return DownloadString(ApiStatics.IPv4_URL);

                    case IPVersion.IPv6:
                        return DownloadString(ApiStatics.IPv6_URL);

                    default:
                        throw new Exceptions.BadIPVersionException(version);
                }
            }
            catch (Exception ex)
            {
                throw new Exceptions.UnknownException(ex);
            }
        }

        /// <summary>
        /// Asynchronously get the ip address.
        /// </summary>
        /// <param name="version">IP Version <see cref="IPVersion"/></param>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>IP in string format</returns>
        /// <exception cref="Exceptions.UnknownException"></exception>
        private static async Task<string> GetIPAddressByVersionAsync(IPVersion version, bool skipNetworkCheck)
        {
            try
            {
                if (!skipNetworkCheck)
                    await PerformNetworkCheckAsync();

                switch (version)
                {
                    case IPVersion.IPv4:
                        return await DownloadStringAsync(ApiStatics.IPv4_URL);

                    case IPVersion.IPv6:
                        return await DownloadStringAsync(ApiStatics.IPv6_URL);

                    default:
                        throw new Exceptions.BadIPVersionException(version);
                }
            }
            catch (Exception ex)
            {
                throw new Exceptions.UnknownException(ex);
            }
        }

        /// <summary>
        /// Get geolocation information.
        /// </summary>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>Custom GeoInformation object</returns>
        /// <exception cref="Exceptions.UnknownException"></exception>
        private static GeoInformation GetGeoInformation(bool skipNetworkCheck)
        {
            try
            {
                if (!skipNetworkCheck)
                    PerformNetworkCheck();

                var json = DownloadString(ApiStatics.Geo_URL);

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

        /// <summary>
        /// Asynchronously get geolocation information.
        /// </summary>
        /// <param name="skipNetworkCheck">Skip checking for network availability.</param>
        /// <returns>Custom GeoInformation object</returns>
        /// <exception cref="Exceptions.UnknownException"></exception>
        private static async Task<GeoInformation> GetGeoInformationAsync(bool skipNetworkCheck)
        {
            try
            {
                if (!skipNetworkCheck)
                    await PerformNetworkCheckAsync();

                var json = await DownloadStringAsync(ApiStatics.Geo_URL);

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

        /// <summary>
        /// Check if network connection is available.
        /// </summary>
        /// <exception cref="Exceptions.NetworkUnavailableException"></exception>
        private static void PerformNetworkCheck()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new Exceptions.NetworkUnavailableException();
        }

        /// <summary>
        /// Asynchronously check if network connection is available.
        /// </summary>
        /// <exception cref="Exceptions.NetworkUnavailableException"></exception>
        private static async Task PerformNetworkCheckAsync()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                throw new Exceptions.NetworkUnavailableException();
        }

        /// <summary>
        /// Download string from web url.
        /// </summary>
        /// <param name="url">The url to downlaod from</param>
        /// <returns>The downloaded string</returns>
        /// <exception cref="Exceptions.BadWebRequestException"></exception>
        private static string DownloadString(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                return httpClient.GetStringAsync(url).Result;
            }
            catch (Exception ex)
            {
                throw new Exceptions.BadWebRequestException(ex);
            }
        }

        /// <summary>
        /// Asynchronously download string from web url.
        /// </summary>
        /// <param name="url">The url to downlaod from</param>
        /// <returns>The downloaded string</returns>
        /// <exception cref="Exceptions.BadWebRequestException"></exception>
        private static async Task<string> DownloadStringAsync(string url)
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
}