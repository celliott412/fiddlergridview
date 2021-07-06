using Fiddler;
using Fiddler.WebFormats;
using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;

namespace FiddlerGridView
{
    public class GridViewInspector : Inspector2
    {
        #region private
        private GridViewControl _control;
        private Encoding _encoding;

        private enum ViewContentType
        {
            Json,
            Xml
        }

        private void CreateNodes(XmlDocument doc, XmlElement inTreeNode, object o, bool bParentIsArray, ref uint iNodeCount)
        {
            if (o is ArrayList)
            {
                ArrayList arrayList = o as ArrayList;
                if (arrayList.Count <= 0)
                {
                    return;
                }
                if (bParentIsArray)
                {
                    XmlElement node = doc.CreateElement("Array");
                    inTreeNode.AppendChild(node);
                    inTreeNode = node;
                    ++iNodeCount;
                }
                foreach (object o1 in arrayList)
                {
                    this.CreateNodes(doc, inTreeNode, o1, true, ref iNodeCount);
                }
            }
            else if (o is Hashtable)
            {
                Hashtable hashtable = o as Hashtable;
                if (hashtable.Count <= 0)
                {
                    return;
                }
                if (bParentIsArray && hashtable.Count > 1)
                {
                    XmlElement node = doc.CreateElement("Object");
                    inTreeNode.AppendChild(node);
                    inTreeNode = node;
                    ++iNodeCount;
                }
                foreach (DictionaryEntry dictionaryEntry in new SortedList((IDictionary)hashtable))
                {
                    string sText = dictionaryEntry.Key.ToString();
                    XmlElement node = doc.CreateElement(XmlConvert.EncodeNmToken(sText));
                    inTreeNode.AppendChild(node);
                    ++iNodeCount;
                    this.CreateNodes(doc, node, dictionaryEntry.Value, false, ref iNodeCount);
                }
            }
            else
            {
                string sText = o != null ? (!(o is DateTime) ? (!(o is double) ? o.ToString() : ((double)o).ToString("R", (IFormatProvider)CultureInfo.InvariantCulture)) : ((DateTime)o).ToString("r")) : "(null)";
                if (bParentIsArray)
                {
                    XmlElement node = doc.CreateElement(XmlConvert.EncodeLocalName(sText));
                    inTreeNode.AppendChild(node);
                    ++iNodeCount;
                }
                else
                {
                    inTreeNode.InnerText = sText;
                }
            }
        }

        private XmlDocument CreateXml(string value, ViewContentType contentType)
        {
            XmlDocument result = new XmlDocument();
            if (contentType == ViewContentType.Xml)
            {
                // Xml
                result.XmlResolver = null;
                result.LoadXml(value);
            }
            else
            {
                // Json
                JSON.JSONParseErrors oErrors;
                object o = JSON.JsonDecode(value, out oErrors);
                if (o != null)
                {
                    XmlElement root = result.CreateElement("JSON");
                    result.AppendChild(root);
                    uint nodeCount = 0;
                    CreateNodes(result, root, o, false, ref nodeCount);
                }
            }
            return result;
        }

        private ViewContentType GetViewContentType()
        {
            if ((Headers != null) && Headers.Exists("Content-Type"))
            {
                try
                {
                    string contentType = Headers["Content-Type"].ToLower();
                    if (contentType.Contains("json"))
                    {
                        return ViewContentType.Json;
                    }
                }
                catch
                {
                }
            }
            return ViewContentType.Xml;
        }
        #endregion

        #region protected
        protected HTTPHeaders Headers { get; set; }
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

        #region IBaseInspector2 Members
        public void Clear()
        {
            _encoding = null;
            Headers = null;
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
                            _encoding = Utilities.getEntityBodyEncoding(Headers, value);
                            string bodyString = Utilities.GetStringFromArrayRemovingBOM(value, _encoding);
                            _control.Display(CreateXml(bodyString, GetViewContentType()));
                        }
                    }
                    catch (Exception ex)
                    {
                        _control.DisplayStatus(ex.Message);
                        Clear();
                    }
                }
            }
        }
        #endregion
    }
}
