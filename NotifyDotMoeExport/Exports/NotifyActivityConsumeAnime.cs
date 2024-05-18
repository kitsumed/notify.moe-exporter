using Newtonsoft.Json;
using System;

namespace NotifyDotMoeExport.Exports
{
    /// <summary>
    /// Object for the notify NotifyActivityConsumeAnime json database
    /// </summary>
    public class NotifyActivityConsumeAnime
    {
        [JsonProperty("animeId")]
        public string animeID;

        /* Due to some activity having insane numbers (Im talking to you, 121212121212 episode watched)
         * all int need to be 64bit long insead of 32bit
         */
        [JsonProperty("fromEpisode")]
        public long fromEpisode;

        [JsonProperty("toEpisode")]
        public long toEpisode;

        [JsonProperty("created")]
        public DateTime created;

        [JsonProperty("createdBy")]
        public string userID;
    }
}
