using System.Collections.Generic;
using Newtonsoft.Json;

namespace BowlingApi
{
    public class ListOfFrames
    {
        // Corrects deserialization issue with json
        [JsonProperty("frames")]
        public List<Frame> Frames { get; set; }
    }
}