# SeeIP Library
Please note, that I am in no way associated with the [SeeIP Project](https://seeip.org/) or it's founding company [UNVIO, LLC.](https://unvio.com/).

## Installation
To install the library,

either manually download the DLL from the releases tab [here](https://github.com/FriedrichRehren/SeeIP-Library/releases/latest) and reference it in your project

or downlaod it using NuGet like this:

    PM> Install-Package SeeIP


## Usage
After including the library, using it is as simple as this:

    string currentIPv4 = SeeIP.GetIP.IPv4();
    IPAddress currentIPv4 = SeeIP.GetIP.IPv4().AsIPAddress();

    string currentIPv6 = SeeIP.GetIP.IPv6();
    IPAddress currentIPv6 = SeeIP.GetIP.IPv6().AsIPAddress();
    
    SeeIP.Models.GeoInformation currentIPLocation = SeeIP.GetIP.GeoInfo();


for asynchronous operation use:

    string currentIPv4 = SeeIP.GetIP.IPv4Async();
    IPAddress currentIPv4 = SeeIP.GetIP.IPv4Async().AsIPAddress();

    string currentIPv6 = SeeIP.GetIP.IPv6Async();
    IPAddress currentIPv6 = SeeIP.GetIP.IPv6Async().AsIPAddress();
    
    SeeIP.Models.GeoInformation currentIPLocation = SeeIP.GetIP.GeoInfoAsync();