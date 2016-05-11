using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBApi.Framework.Events;

namespace CBApi.Framework.Requests {
    public abstract class BaseRequest {
        protected BeforeRequestEvent _BeforeRequestEvent = delegate { };
        protected AfterRequestEvent _AfterRequestEvent = delegate { };

        internal event BeforeRequestEvent OnBeforeRequest {
            add { _BeforeRequestEvent += value; }
            remove { _BeforeRequestEvent += value; }
        }

        internal event AfterRequestEvent OnAfterRequest {
            add { _AfterRequestEvent += value; }
            remove { _AfterRequestEvent += value; }
        }
    }
}
