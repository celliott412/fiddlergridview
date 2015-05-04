using System;
using System.Collections.Generic;
using System.Text;
using Fiddler;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Dynamic;
using Fiddler.WebFormats;
using System.Collections;
using System.Globalization;

namespace FiddlerGridView
{
    public class GridViewResponseInspector : GridViewInspector, IResponseInspector2
    {
        #region IResponseInspector2 Members
        public HTTPResponseHeaders headers
        {
            get { return null; }
            set { Headers = value; }
        }
        #endregion
    }
}
