using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace CBApi.Framework.Requests {
    internal abstract class QsParam {
        public string ParamName;
        public string value;
        public QsParam(string paramName, string value) {
            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentNullException(this.GetType().ToString(), this.GetType().ToString() + " value is required");
            }
            this.ParamName = paramName;
            this.value = value;
        }

        public void addIDParam(IRestRequest request) {
            request.AddParameter(ParamName, value);
        }
    }

    internal class VisitorID : QsParam {
        public VisitorID(string id) : base("VisitorID", id) { }
    }

    internal class ExternalID : QsParam {
        public ExternalID(string id) : base("ExternalID", id) { }
    }

    internal class GenericParam : QsParam {
        public GenericParam(string name, string value) : base(name, value) { }
    }
}
