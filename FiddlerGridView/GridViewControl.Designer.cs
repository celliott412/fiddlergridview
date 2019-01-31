namespace FiddlerGridView
{
    partial class GridViewControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridViewControl));
            this.spView = new System.Windows.Forms.SplitContainer();
            this.tvSelection = new System.Windows.Forms.TreeView();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.cbFilter = new System.Windows.Forms.CheckBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowTree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSplitVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowAttributes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowElements = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblGridViewStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFilterStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbWorking = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.spView)).BeginInit();
            this.spView.Panel1.SuspendLayout();
            this.spView.Panel2.SuspendLayout();
            this.spView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spView
            // 
            this.spView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spView.Location = new System.Drawing.Point(0, 0);
            this.spView.Name = "spView";
            this.spView.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spView.Panel1
            // 
            this.spView.Panel1.Controls.Add(this.tvSelection);
            // 
            // spView.Panel2
            // 
            this.spView.Panel2.Controls.Add(this.dgvView);
            this.spView.Panel2.Controls.Add(this.pnlFilter);
            this.spView.Size = new System.Drawing.Size(320, 298);
            this.spView.SplitterDistance = 96;
            this.spView.TabIndex = 0;
            this.spView.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spView_SplitterMoved);
            // 
            // tvSelection
            // 
            this.tvSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSelection.FullRowSelect = true;
            this.tvSelection.HideSelection = false;
            this.tvSelection.Location = new System.Drawing.Point(0, 0);
            this.tvSelection.Name = "tvSelection";
            this.tvSelection.Size = new System.Drawing.Size(320, 96);
            this.tvSelection.TabIndex = 0;
            this.tvSelection.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSelection_AfterSelect);
            this.tvSelection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvSelection_KeyDown);
            // 
            // dgvView
            // 
            this.dgvView.AllowUserToAddRows = false;
            this.dgvView.AllowUserToDeleteRows = false;
            this.dgvView.AllowUserToOrderColumns = true;
            this.dgvView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgvView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvView.Location = new System.Drawing.Point(0, 26);
            this.dgvView.Name = "dgvView";
            this.dgvView.ReadOnly = true;
            this.dgvView.RowHeadersWidth = 22;
            this.dgvView.Size = new System.Drawing.Size(320, 172);
            this.dgvView.TabIndex = 0;
            this.dgvView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvView_CellClick);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.cbFilter);
            this.pnlFilter.Controls.Add(this.txtFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(320, 26);
            this.pnlFilter.TabIndex = 1;
            this.pnlFilter.Visible = false;
            // 
            // cbFilter
            // 
            this.cbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFilter.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbFilter.Image = ((System.Drawing.Image)(resources.GetObject("cbFilter.Image")));
            this.cbFilter.Location = new System.Drawing.Point(295, 1);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(26, 23);
            this.cbFilter.TabIndex = 1;
            this.cbFilter.UseVisualStyleBackColor = true;
            this.cbFilter.CheckedChanged += new System.EventHandler(this.cbFilter_CheckedChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(0, 2);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(294, 21);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowTree,
            this.toolStripSeparator1,
            this.mnuSplitVertical,
            this.toolStripMenuItem1,
            this.mnuShowAttributes,
            this.mnuShowElements,
            this.toolStripSeparator2,
            this.mnuShowFilter});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 132);
            // 
            // mnuShowTree
            // 
            this.mnuShowTree.Checked = true;
            this.mnuShowTree.CheckOnClick = true;
            this.mnuShowTree.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowTree.Name = "mnuShowTree";
            this.mnuShowTree.Size = new System.Drawing.Size(158, 22);
            this.mnuShowTree.Text = "Show Tree";
            this.mnuShowTree.CheckedChanged += new System.EventHandler(this.mnuShowTree_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // mnuSplitVertical
            // 
            this.mnuSplitVertical.CheckOnClick = true;
            this.mnuSplitVertical.Name = "mnuSplitVertical";
            this.mnuSplitVertical.Size = new System.Drawing.Size(158, 22);
            this.mnuSplitVertical.Text = "Split Vertical";
            this.mnuSplitVertical.Click += new System.EventHandler(this.mnuSplitVertical_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // mnuShowAttributes
            // 
            this.mnuShowAttributes.CheckOnClick = true;
            this.mnuShowAttributes.Name = "mnuShowAttributes";
            this.mnuShowAttributes.Size = new System.Drawing.Size(158, 22);
            this.mnuShowAttributes.Text = "Show Attributes";
            this.mnuShowAttributes.CheckedChanged += new System.EventHandler(this.DisplayOptions_CheckedChanged);
            // 
            // mnuShowElements
            // 
            this.mnuShowElements.Checked = true;
            this.mnuShowElements.CheckOnClick = true;
            this.mnuShowElements.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowElements.Name = "mnuShowElements";
            this.mnuShowElements.Size = new System.Drawing.Size(158, 22);
            this.mnuShowElements.Text = "Show Elements";
            this.mnuShowElements.CheckedChanged += new System.EventHandler(this.DisplayOptions_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // mnuShowFilter
            // 
            this.mnuShowFilter.CheckOnClick = true;
            this.mnuShowFilter.Name = "mnuShowFilter";
            this.mnuShowFilter.Size = new System.Drawing.Size(158, 22);
            this.mnuShowFilter.Text = "Show &Filter";
            this.mnuShowFilter.CheckedChanged += new System.EventHandler(this.mnuShowFilter_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblGridViewStatus,
            this.lblFilterStatus,
            this.pbWorking});
            this.statusStrip1.Location = new System.Drawing.Point(0, 298);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(320, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblGridViewStatus
            // 
            this.lblGridViewStatus.Name = "lblGridViewStatus";
            this.lblGridViewStatus.Size = new System.Drawing.Size(152, 17);
            this.lblGridViewStatus.Spring = true;
            this.lblGridViewStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFilterStatus
            // 
            this.lblFilterStatus.Name = "lblFilterStatus";
            this.lblFilterStatus.Size = new System.Drawing.Size(152, 17);
            this.lblFilterStatus.Spring = true;
            // 
            // pbWorking
            // 
            this.pbWorking.MarqueeAnimationSpeed = 30;
            this.pbWorking.Name = "pbWorking";
            this.pbWorking.Size = new System.Drawing.Size(100, 17);
            this.pbWorking.Visible = false;
            // 
            // GridViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.spView);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewControl";
            this.Size = new System.Drawing.Size(320, 320);
            this.spView.Panel1.ResumeLayout(false);
            this.spView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spView)).EndInit();
            this.spView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer spView;
        private System.Windows.Forms.TreeView tvSelection;
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuSplitVertical;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblGridViewStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuShowAttributes;
        private System.Windows.Forms.ToolStripMenuItem mnuShowElements;
        private System.Windows.Forms.ToolStripProgressBar pbWorking;
        private System.Windows.Forms.ToolStripMenuItem mnuShowTree;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ToolStripStatusLabel lblFilterStatus;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuShowFilter;
        private System.Windows.Forms.CheckBox cbFilter;
    }
}
