using Fiddler;

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
