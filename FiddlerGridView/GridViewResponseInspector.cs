using System;
using System.Collections.Generic;
using System.Text;
using Fiddler;
using System.Xml;

namespace FiddlerGridView
{
    public class GridViewResponseInspector : Inspector2, IResponseInspector2
    {
        #region private
        private GridViewControl _control;
        private Encoding _encoding;
        private HTTPResponseHeaders _headers;
        #endregion

        #region public
        public override void AddToTab(System.Windows.Forms.TabPage o)
        {
            _control = new GridViewControl();
            o.Text = "GridView";
            o.Controls.Add(_control);
            o.Controls[0].Dock = System.Windows.Forms.DockStyle.Fill;
        }

        public override int GetOrder()
        {
            return 100;
        }

        public override int ScoreForContentType(string sMIMEType)
        {
            if (sMIMEType.Contains("xml"))
            {
                return 55;
            }
            return -1;
        }

        public override void SetFontSize(float flSizeInPoints)
        {
            if (_control != null)
                _control.SetFontSize(flSizeInPoints);
        }
        #endregion

        #region IResponseInspector2 Members
        public HTTPResponseHeaders headers
        {
            get { return null; }
            set { _headers = value; }
        }
        #endregion

        #region IBaseInspector2 Members
        public void Clear()
        {
            _encoding = null;
            _headers = null;
            _control.Clear();
        }

        public bool bDirty
        {
            get { return false; }
        }

        public bool bReadOnly
        {
            get { return true; }
            set { }
        }

        public byte[] body
        {
            get { return null; }
            set
            {
                if (value == null)
                {
                    Clear();
                }
                else
                {
                    try
                    {
                        if ((value == null) || (value.Length < 1))
                        {
                            Clear();
                        }
                        else
                        {
                            _encoding = Utilities.getEntityBodyEncoding(_headers, value);
                            string stringFromArrayRemovingBOM = Utilities.GetStringFromArrayRemovingBOM(value, _encoding);
                            XmlDocument dom = new XmlDocument();
                            dom.XmlResolver = null;
                            dom.LoadXml(stringFromArrayRemovingBOM);
                            _control.Display(dom);
                        }
                    }
                    catch (Exception)
                    {
                        Clear();
                    }
                }
            }
        }
        #endregion
    }
}
