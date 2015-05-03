using System;
using System.Collections.Generic;
using System.Text;
using Fiddler;
using System.Xml;
using Newtonsoft.Json;

namespace FiddlerGridView
{
    public class GridViewRequestInspector : Inspector2, IRequestInspector2
    {
        #region private
        private GridViewControl _control;
        private Encoding _encoding;
        private HTTPRequestHeaders _headers;

        private enum ViewContentType
        {
            Json,
            Xml
        }

        private XmlDocument CreateXml(string value, ViewContentType contentType)
        {
            if (contentType == ViewContentType.Xml)
            {
                // Xml
                XmlDocument result = new XmlDocument();
                result.XmlResolver = null;
                result.LoadXml(value);
                return result;
            }
            else
            {
                // Json
                return JsonConvert.DeserializeXmlNode(value);
            }
        }

        private ViewContentType GetViewContentType()
        {
            if (_headers != null)
            {
                string contentType = _headers["Content-Type"];
                if (!string.IsNullOrEmpty(contentType))
                {
                    if (contentType.ToLower().Contains("json"))
                    {
                        return ViewContentType.Json;
                    }
                }
            }
            return ViewContentType.Xml;
        }
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
            if (sMIMEType.Contains("xml") || sMIMEType.Contains("json"))
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

        #region IRequestInspector2 Members
        public HTTPRequestHeaders headers
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
                            string bodyString = Utilities.GetStringFromArrayRemovingBOM(value, _encoding);

                            XmlDocument dom = CreateXml(bodyString, GetViewContentType());

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
