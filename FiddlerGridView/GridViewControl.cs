using Fiddler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

[assembly: Fiddler.RequiredVersion("2.2.7.0")]

namespace FiddlerGridView
{
    public partial class GridViewControl : UserControl
    {
        #region private
        private TreeNode _currentNode;
        private BindingSource _bindingSource;

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode, ref uint iNodeCount)
        {
            if (inXmlNode.HasChildNodes)
            {
                int count = inXmlNode.ChildNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    XmlNode node = inXmlNode.ChildNodes[i];
                    string text = GetNodeText(node);
                    if ((node.Attributes != null) && (node.Attributes.Count > 0))
                    {
                        text += " [";
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            text += " " + attribute.Name + "=" + attribute.Value;
                        }
                        text += " ]";
                    }

                    XmlTreeNode treeNode = new XmlTreeNode(text, node);
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

        private string GetNodeText(XmlNode node)
        {
            if (node.Name == "Object")
            {
                return "{}";
            }
            else
            {
                if (node.Name == "Array")
                {
                    return "[]";
                }
            }
            return node.Name;
        }

        private string GetSafeName(DataColumnCollection existingCols, string name, string possiblePrefix)
        {
            if (existingCols.Contains(name))
            {
                if (existingCols.Contains(possiblePrefix + name))
                {
                    return possiblePrefix + name + $"_{existingCols.Count}";
                }
                else
                {
                    return possiblePrefix + name;
                }
            }
            else
            {
                return name;
            }
        }

        private void DisplayNodeInGrid(TreeNode treeNode, bool showElements, bool showAttributes)
        {
            pbWorking.Visible = true;
            pbWorking.Value = 0;
            Application.DoEvents();
            try
            {
                // Clear any background colors
                SetChildNodeSelection(_currentNode, -1, -1);

                _currentNode = treeNode;

                if (_bindingSource != null)
                {
                    _bindingSource.DataSource = null;
                }

                dgvView.DataSource = null;
                dgvView.AutoGenerateColumns = false;
                lblGridViewStatus.Text = "Row count: 0";

                XmlTreeNode node = treeNode as XmlTreeNode;
                if ((node != null) && (node.XmlNode != null))
                {
                    DataTable dt = new DataTable();
                    Dictionary<DataColumn, string> columnMappings = new Dictionary<DataColumn, string>();

                    if (node.XmlNode.HasChildNodes)
                    {
                        bool objectOrArray = false;

                        // Determine if this an Object or not
                        if (showElements)
                        {
                            int count = 0;
                            List<string> sigs = new List<string>();
                            foreach (XmlNode childNode in node.XmlNode.ChildNodes)
                            {
                                if (childNode.ChildNodes.Count > 1)
                                {
                                    sigs.Add(string.Join("|", childNode.ChildNodes.Cast<XmlNode>().Select(p => p.Name)));
                                }
                                else
                                {
                                    if (childNode.ChildNodes.Count == 1)
                                    {
                                        if (childNode.ChildNodes[0].Name == "#text")
                                        {
                                            sigs.Add(childNode.Name);
                                        }
                                        else
                                        {
                                            sigs.Add(childNode.ChildNodes[0].Name);
                                        }
                                    }
                                    else
                                    {
                                        sigs.Add(childNode.Name);
                                    }
                                }

                                if (count++ >= 3)
                                {
                                    break;
                                }
                            }

                            if ((count == 1) || ((count > 1) && !sigs.All(p => p == sigs.First())))
                            {
                                objectOrArray = true;
                                columnMappings.Add(dt.Columns.Add(GetSafeName(dt.Columns, "Name", "")), "Name");
                                columnMappings.Add(dt.Columns.Add(GetSafeName(dt.Columns, "Value", "")), "Value");
                            }
                        }

                        // Columns
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
                                        {
                                            names.Add(name, name);
                                        }
                                    }
                                }
                            }

                            foreach (string name in names.Keys)
                            {
                                columnMappings.Add(dt.Columns.Add(GetSafeName(dt.Columns, name, "attr.")), name);
                            }
                        }

                        if (showElements && !objectOrArray)
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
                            {
                                columnMappings.Add(dt.Columns.Add(GetSafeName(dt.Columns, name, "")), name);
                            }
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
                                    if (objectOrArray)
                                    {
                                        if (childNode.HasChildNodes)
                                        {
                                            dr["Name"] = childNode.Name;
                                            dr["Value"] = GetValueForNode(childNode);
                                        }
                                        else
                                        {
                                            if ((childNode.Name == "#text") && (childNode.ParentNode != null))
                                            {
                                                dr["Name"] = childNode.ParentNode.Name;
                                                dr["Value"] = childNode.Value;
                                            }
                                            else
                                            {
                                                dr["Name"] = childNode.Name;
                                                dr["Value"] = childNode.Value;
                                            }
                                        }
                                    }

                                    for (int i = 0; i < dt.Columns.Count; i++)
                                    {
                                        string value = null;
                                        if (showElements && !objectOrArray)
                                        {
                                            value = GetChildValueByName(childNode.ChildNodes, columnMappings[dt.Columns[i]]);
                                        }
                                        if ((value == null) && showAttributes)
                                        {
                                            value = GetAttributeValueByName(childNode.Attributes, columnMappings[dt.Columns[i]]);
                                        }
                                        if (value != null)
                                        {
                                            dr[i] = value;
                                        }
                                    }
                                }
                                catch
                                {
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                    }

                    if (_bindingSource == null)
                    {
                        _bindingSource = new BindingSource();
                    }

                    _bindingSource.Filter = "";
                    _bindingSource.DataSource = dt;
                    SetFilter();

                    dgvView.DataSource = _bindingSource;
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

        private string GetValueForNode(XmlNode node)
        {
            if (node.FirstChild != null)
            {
                if (node.FirstChild.HasChildNodes)
                    return "(object) : " + node.ChildNodes.Count.ToString();
                else
                    return node.FirstChild.Value;
            }
            else
                return "(null)";
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

        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tvSelection.SelectedNode != null)
            {
                if ((tvSelection.SelectedNode.Nodes.Count > 0) && (e.RowIndex < tvSelection.SelectedNode.Nodes.Count))
                {
                    SetChildNodeSelection(tvSelection.SelectedNode, e.RowIndex, e.ColumnIndex);
                }
            }
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

        private void mnuShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            pnlFilter.Visible = mnuShowFilter.Checked;
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

        private void SetChildNodeSelection(TreeNode parent, int rowIndex, int colIndex)
        {
            // Set selection
            if ((parent != null) && (parent.Nodes != null))
            {
                foreach (TreeNode node in parent.Nodes)
                {
                    if (node.Index == rowIndex)
                    {
                        node.BackColor = SystemColors.ActiveCaption;
                        node.EnsureVisible();
                        if (colIndex >= 0)
                        {
                            SetChildNodeSelection(node, colIndex, -1);
                        }
                    }
                    else
                    {
                        node.BackColor = tvSelection.BackColor;
                        SetChildNodeSelection(node, -1, -1);
                    }
                }
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cbFilter.Checked = !cbFilter.Checked;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void SetFilter()
        {
            if (_bindingSource != null)
            {
                try
                {
                    if (cbFilter.Checked)
                    {
                        _bindingSource.Filter = txtFilter.Text;
                    }
                    else
                    {
                        _bindingSource.Filter = "";
                    }
                    lblFilterStatus.Text = "";
                }
                catch (Exception ex)
                {
                    cbFilter.Checked = false;
                    lblFilterStatus.Text = ex.Message;
                }
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
            if (dom == null)
                return;

            pbWorking.Visible = true;
            pbWorking.Maximum = 100;
            pbWorking.Value = 100;
            Application.DoEvents();

            XmlTreeNode inTreeNode = new XmlTreeNode("", dom.DocumentElement);
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
            catch
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

        private void cbFilter_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
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
