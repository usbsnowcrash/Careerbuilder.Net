using System;
using System.Collections.Generic;

namespace CBApi.Models {
    public class JobSearchMetaData {

        public bool ResultsAlteredByUsersSearchPreferences { get; set; }
        public List<SearchLocation> SearchLocations { get; set; }

    }
}