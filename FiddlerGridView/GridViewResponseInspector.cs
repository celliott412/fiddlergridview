using Fiddler;

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
