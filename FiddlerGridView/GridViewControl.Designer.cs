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
            this.spView = new System.Windows.Forms.SplitContainer();
            this.tvSelection = new System.Windows.Forms.TreeView();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowTree = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSplitVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowAttributes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowElements = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblGridViewStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbWorking = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.spView)).BeginInit();
            this.spView.Panel1.SuspendLayout();
            this.spView.Panel2.SuspendLayout();
            this.spView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
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
            this.spView.Size = new System.Drawing.Size(320, 298);
            this.spView.SplitterDistance = 97;
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
            this.tvSelection.Size = new System.Drawing.Size(320, 97);
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
            this.dgvView.Location = new System.Drawing.Point(0, 0);
            this.dgvView.Name = "dgvView";
            this.dgvView.ReadOnly = true;
            this.dgvView.RowHeadersWidth = 22;
            this.dgvView.Size = new System.Drawing.Size(320, 197);
            this.dgvView.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowTree,
            this.toolStripSeparator1,
            this.mnuSplitVertical,
            this.toolStripMenuItem1,
            this.mnuShowAttributes,
            this.mnuShowElements});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 104);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblGridViewStatus,
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
            this.lblGridViewStatus.Size = new System.Drawing.Size(305, 17);
            this.lblGridViewStatus.Spring = true;
            this.lblGridViewStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbWorking
            // 
            this.pbWorking.MarqueeAnimationSpeed = 30;
            this.pbWorking.Name = "pbWorking";
            this.pbWorking.Size = new System.Drawing.Size(100, 16);
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

    }
}
