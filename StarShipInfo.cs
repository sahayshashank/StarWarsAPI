using Newtonsoft.Json;

namespace WebAPI
{
    public class StarShipInfo
    {
        public List<string> pilots;
        public string passengers { get; set; }
        public StarShipInfo(List<string> pilots, string passengers)
        {
            this.pilots = pilots;
            this.passengers = passengers;
        }
    }
}