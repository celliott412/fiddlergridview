using System;
using System.Collections.Generic;
using System.Text;
using Fiddler;
using System.Xml;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace FiddlerGridView
{
    public class GridViewRequestInspector : GridViewInspector, IRequestInspector2
    {
        #region IRequestInspector2 Members
        public HTTPRequestHeaders headers
        {
            get { return null; }
            set { Headers = value; }
        }
        #endregion
    }
}
