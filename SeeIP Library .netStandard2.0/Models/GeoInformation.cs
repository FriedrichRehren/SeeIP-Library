namespace SeeIP.Models
{
    public class GeoInformation
    {
        public string ip { get; set; }
        public string continent_code { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string country_code3 { get; set; }
        public string region { get; set; }
        public string region_code { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string timezone { get; set; }
        public int offset { get; set; }
        public int asn { get; set; }
        public string organization { get; set; }
    }
}