using Newtonsoft.Json;
using System;

namespace NotifyDotMoeExport.Exports
{
    /// <summary>
    /// Object for a notify anime list export in json
    /// </summary>
    public class NotifyTemplate
    {
        [JsonProperty("userId")]
        public string userID;

        [JsonProperty("items")]
        public Items[] items;
    }

    /// <summary>
    /// Object for one anime entry inside a notify anime list export in json
    /// </summary>
    public class Items
    {
        [JsonProperty("animeId")]
        public string animeID;

        [JsonProperty("status")]
        public AnimeEnums.NotifyStatus status;

        [JsonProperty("episodes")]
        public int watchedEpisodes;

        [JsonProperty("rating")]
        public Ratings rating;

        [JsonProperty("notes")]
        public string notes;

        [JsonProperty("rewatchCount")]
        public int rewatchCount;

        [JsonProperty("private")]
        public bool isPrivate;

        public string title;

        public Mappings[] mappings;

        // Create DateTime as a nullable type
        public DateTime? watchedDate;

        public DateTime? endedDate;

        // True if the item is new or has been overwritten. True by default.
        public bool isNewEntry = true;
    }

    public class Ratings 
    {
        [JsonProperty("overall")]
        public double overall;

        [JsonProperty("story")]
        public double story;

        [JsonProperty("visuals")]
        public double visuals;

        [JsonProperty("soundtrack")]
        public double soundtrack;
    }

    public class Mappings
    {
        [JsonProperty("service")]
        public string service;

        [JsonProperty("serviceId")]
        public string serviceId;
    }
}
