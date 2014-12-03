using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FiddlerGridView.Properties;
using Fiddler;
using System.Xml;

[assembly: Fiddler.RequiredVersion("2.2.7.0")]

namespace FiddlerGridView
{
    public partial class GridViewControl : UserControl
    {
        #region private
        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode, ref uint iNodeCount)
        {
            if (inXmlNode.HasChildNodes)
            {
                int count = inXmlNode.ChildNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    XmlNode node = inXmlNode.ChildNodes[i];
                    string name = node.Name;
                    if ((node.Attributes != null) && (node.Attributes.Count > 0))
                    {
                        name += " [";
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            name += " " + attribute.Name + "=" + attribute.Value;
                        }
                        name += " ]";
                    }

                    XmlTreeNode treeNode = new XmlTreeNode(name, node);
                    inTreeNode.Nodes.Add(treeNode);

                    TreeNode node2 = inTreeNode.Nodes[i];
                    iNodeCount++;
                    AddNode(node, node2, ref iNodeCount);
                }
            }
            else
            {
                inTreeNode.Text = inXmlNode.OuterXml.Trim();
            }
        }

        private void DisplayNodeInGrid(TreeNode treeNode, bool showElements, bool showAttributes)
        {
            pbWorking.Visible = true;
            pbWorking.Value = 0;
            Application.DoEvents();
            try
            {
                dgvView.DataSource = null;
                dgvView.AutoGenerateColumns = false;
                lblGridViewStatus.Text = "Row count: 0";

                XmlTreeNode node = treeNode as XmlTreeNode;
                if ((node != null) && (node.XmlNode != null))
                {
                    DataTable dt = new DataTable();

                    if (node.XmlNode.HasChildNodes)
                    {
                        // Columns
                        bool primitiveArray = false;
                        if (showElements)
                        {
                            if (node.XmlNode.FirstChild.ChildNodes.Count == 1)
                            {
                                primitiveArray = true;
                                dt.Columns.Add("Name");
                                dt.Columns.Add("Value");
                            }
                        }

                        if (showAttributes)
                        {
                            // Attributes for column names
                            var names = new Dictionary<string, string>();
                            foreach (XmlNode childNode in node.XmlNode.ChildNodes)
                            {
                                if (childNode.Attributes != null)
                                {
                                    foreach (XmlAttribute attribute in childNode.Attributes)
                                    {
                                        var name = attribute.Name;
                                        if (!names.ContainsKey(name))
                                            names.Add(name, name);
                                    }
                                }
                            }

                            foreach (string name in names.Keys)
                                dt.Columns.Add(name);
                        }

                        if (showElements && !primitiveArray)
                        {
                            // Elements for column names
                            var names = new Dictionary<string, string>();
                            foreach (XmlNode childNode in node.XmlNode.ChildNodes)
                            {
                                if (childNode.ChildNodes != null)
                                {
                                    foreach (XmlNode elementNode in childNode.ChildNodes)
                                    {
                                        var name = elementNode.Name;
                                        if (!names.ContainsKey(name))
                                            names.Add(name, name);
                                    }
                                }
                            }

                            foreach (string name in names.Keys)
                                dt.Columns.Add(name);
                        }

                        // Rows
                        if (dt.Columns.Count > 0)
                        {
                            pbWorking.Maximum = node.XmlNode.ChildNodes.Count;
                            foreach (XmlNode childNode in node.XmlNode.ChildNodes)
                            {
                                pbWorking.Increment(1);
                                DataRow dr = dt.NewRow();
                                try
                                {
                                    if (primitiveArray)
                                    {
                                        dr["Name"] = childNode.Name;
                                        dr["Value"] = GetValueForNode(childNode);
                                    }
                                    //else
                                    //{
                                    for (int i = 0; i < dt.Columns.Count; i++)
                                    {
                                        string value = null;
                                        if (showElements && !primitiveArray)
                                        {
                                            value = GetChildValueByName(childNode.ChildNodes, dt.Columns[i].ColumnName);
                                        }
                                        if ((value == null) && showAttributes)
                                        {
                                            value = GetAttributeValueByName(childNode.Attributes, dt.Columns[i].ColumnName);
                                        }
                                        if (value != null)
                                        {
                                            dr[i] = value;
                                        }
                                    }
                                    //}
                                }
                                catch
                                {
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }

                    dgvView.DataSource = dt;
                    dgvView.AutoGenerateColumns = true;
                    dgvView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    lblGridViewStatus.Text = string.Format("Row count: {0}", dt.Rows.Count);
                }
            }
            finally
            {
                pbWorking.Visible = false;
            }
        }

        private string GetAttributeValueByName(XmlAttributeCollection attributes, string name)
        {
            foreach (XmlAttribute attribute in attributes)
            {
                if (string.Equals(name, attribute.Name))
                {
                    return attribute.Value;
                }
            }
            return null;
        }

        private string GetChildValueByName(XmlNodeList childNodes, string name)
        {
            foreach (XmlNode node in childNodes)
            {
                if (string.Equals(name, node.Name))
                {
                    return GetValueForNode(node);
                }
            }

            if (childNodes.Count == 1)
            {
                return GetValueForNode(childNodes[0]);
            }

            return null;
        }

        private static string GetValueForNode(XmlNode node)
        {
            if (node.FirstChild != null)
            {
                if (node.FirstChild.HasChildNodes)
                    return "<object> : " + node.ChildNodes.Count.ToString();
                else
                    return node.FirstChild.Value;
            }
            else
                return "<null>";
        }

        private void spView_SplitterMoved(object sender, SplitterEventArgs e)
        {
            FiddlerApplication.Prefs.SetInt32Pref("fiddler.inspectors.gridview.splitterdistance", spView.SplitterDistance);
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            BeginUpdate();
            tvSelection.CollapseAll();
            if (tvSelection.Nodes.Count == 1)
            {
                tvSelection.Nodes[0].Expand();
            }
            EndUpdate();
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            BeginUpdate();
            tvSelection.ExpandAll();
            if (tvSelection.SelectedNode != null)
            {
                tvSelection.SelectedNode.EnsureVisible();
            }
            else if (tvSelection.Nodes.Count > 0)
            {
                tvSelection.Nodes[0].EnsureVisible();
            }
            EndUpdate();
        }

        private void DisplayOptions_CheckedChanged(object sender, EventArgs e)
        {
            if (tvSelection.SelectedNode != null)
            {
                DisplayNodeInGrid(tvSelection.SelectedNode, mnuShowElements.Checked, mnuShowAttributes.Checked);
            }
            FiddlerApplication.Prefs.SetBoolPref("fiddler.inspectors.gridview.showattributes", mnuShowAttributes.Checked);
            FiddlerApplication.Prefs.SetBoolPref("fiddler.inspectors.gridview.showelements", mnuShowElements.Checked);
        }

        private void mnuShowTree_CheckedChanged(object sender, EventArgs e)
        {
            spView.Panel1Collapsed = !mnuShowTree.Checked;
            FiddlerApplication.Prefs.SetBoolPref("fiddler.inspectors.gridview.showtree", mnuShowTree.Checked);
        }

        private void mnuSplitVertical_Click(object sender, EventArgs e)
        {
            spView.Orientation = mnuSplitVertical.Checked ? Orientation.Vertical : Orientation.Horizontal;
            FiddlerApplication.Prefs.SetStringPref("fiddler.inspectors.gridview.orientation", spView.Orientation.ToString());
        }

        private void tvSelection_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DisplayNodeInGrid(e.Node, mnuShowElements.Checked, mnuShowAttributes.Checked);
        }

        private void tvSelection_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && (e.KeyCode == Keys.C)) && (tvSelection.SelectedNode != null))
            {
                Utilities.CopyToClipboard(tvSelection.SelectedNode.Text);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        #endregion

        #region public
        public GridViewControl()
        {
            InitializeComponent();
            tvSelection.BackColor = CONFIG.colorDisabledEdit;

            if (CONFIG.flFontSize != tvSelection.Font.Size)
                tvSelection.Font = new Font(tvSelection.Font.FontFamily, CONFIG.flFontSize);

            spView.Orientation = (Orientation)Enum.Parse(typeof(Orientation), FiddlerApplication.Prefs.GetStringPref("fiddler.inspectors.gridview.orientation", "Vertical"));
            mnuShowTree.Checked = FiddlerApplication.Prefs.GetBoolPref("fiddler.inspectors.gridview.showtree", true);
            spView.Panel1Collapsed = !mnuShowTree.Checked;
            spView.SplitterDistance = FiddlerApplication.Prefs.GetInt32Pref("fiddler.inspectors.gridview.splitterdistance", 100);
            mnuShowAttributes.Checked = FiddlerApplication.Prefs.GetBoolPref("fiddler.inspectors.gridview.showattributes", false);
            mnuShowElements.Checked = FiddlerApplication.Prefs.GetBoolPref("fiddler.inspectors.gridview.showelements", true);
        }

        public void BeginUpdate()
        {
            tvSelection.BeginUpdate();
            tvSelection.SuspendLayout();
        }

        public void Clear()
        {
            BeginUpdate();
            tvSelection.Nodes.Clear();
            EndUpdate();
            dgvView.DataSource = null;
        }

        public void Display(XmlDocument dom)
        {
            pbWorking.Visible = true;
            pbWorking.Maximum = 100;
            pbWorking.Value = 100;
            Application.DoEvents();

            XmlTreeNode inTreeNode = new XmlTreeNode("", null);
            uint iNodeCount = 0;
            try
            {
                Clear();
                string name = dom.DocumentElement.Name;
                if ((dom.DocumentElement.Attributes != null) && (dom.DocumentElement.Attributes.Count > 0))
                {
                    name += " [";
                    foreach (XmlAttribute attribute in dom.DocumentElement.Attributes)
                    {
                        name += " " + attribute.Name + "=" + attribute.Value;
                    }
                    name += " ]";
                }
                inTreeNode.Text = name;
                AddNode(dom.DocumentElement, inTreeNode, ref iNodeCount);
            }
            catch (Exception)
            {
            }

            try
            {
                BeginUpdate();
                tvSelection.Nodes.Add(inTreeNode);
                if (iNodeCount < 2000)
                {
                    tvSelection.Nodes[0].ExpandAll();
                }
                else
                {
                    tvSelection.Nodes[0].Expand();
                }
                tvSelection.Nodes[0].EnsureVisible();
                EndUpdate();
            }
            finally
            {
                EndUpdate();
                pbWorking.Visible = false;
            }
        }

        public void EndUpdate()
        {
            tvSelection.EndUpdate();
            tvSelection.ResumeLayout();
        }

        public void SetFontSize(float flSizeInPoints)
        {
            Font = new Font(Font.FontFamily, flSizeInPoints);
        }
        #endregion
    }

    public class XmlTreeNode : TreeNode
    {
        #region private
        private XmlNode _node;
        #endregion

        #region public
        public XmlTreeNode(string text, XmlNode node)
            : base(text)
        {
            _node = node;
        }

        public XmlNode XmlNode
        {
            get { return _node; }
        }
        #endregion
    }
}
