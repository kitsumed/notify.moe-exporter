using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace NotifyDotMoeExport.Exports
{

    [Serializable, XmlRoot("myanimelist")]
    public class MALTemplate
    {
        [XmlElement("anime")]
        public List<MALAnimeItem> animes;
    }
    
    public class MALAnimeItem
    {
        [XmlElement("series_animedb_id")]
        public int ID = 0;

        // If this CData is a empty string, import will fail
        [XmlElement("series_title")]
        public XmlCDataSection title = new XmlDocument().CreateCDataSection("undefined");

        [XmlElement("my_watched_episodes")]
        public int watchedEpisodes = 0;

        [XmlElement("my_start_date")]
        public string startDate = "0000-00-00";

        [XmlElement("my_finish_date")]
        public string endDate = "0000-00-00";

        [XmlElement("my_score")]
        public double rating = 0;

        [XmlElement("my_status")]
        public string status;

        // Notes need to be a CData insead of a simple string due to the xml file causing failed import when there are "illegal" characters in the string
        [XmlElement("my_comments")]
        public XmlCDataSection notes = new XmlDocument().CreateCDataSection("");

        [XmlElement("my_times_watched")]
        public int timeRewatched = 0;
    }
}
