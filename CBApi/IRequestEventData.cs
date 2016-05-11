using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBApi {
    public interface IRequestEventData {
        string BaseURL { get; }
        string Method { get; }
        string ResponseContent { get; }
        Dictionary<string, string> Parameters { get; }
    }
}
