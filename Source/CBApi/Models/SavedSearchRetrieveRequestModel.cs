﻿using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi.Models
{

    public class SavedSearchRetrieveRequestModel
    {
        public string DeveloperKey { get; set; }
        public string ExternalUserID { get; set; }
        public string ExternalID { get; set; }
    }
}
