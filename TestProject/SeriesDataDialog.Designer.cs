using Edgeteks.MWLServer.WorkListDBDataSetTableAdapters;

namespace Edgeteks.MWLServer
{
    partial class SeriesDataDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriesDataDialog));
            this.workListDBDataSet = new WorkListDBDataSet();
            this.performedSeriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.performedSeriesTableAdapter = new PerformedSeriesTableAdapter();
            this.performedSeriesBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.performedSeriesBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.performedSeriesDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referencedImagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.referencedImagesTableAdapter = new ReferencedImagesTableAdapter();
            this.referencedImagesDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.workListDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performedSeriesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performedSeriesBindingNavigator)).BeginInit();
            this.performedSeriesBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.performedSeriesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.referencedImagesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.referencedImagesDataGridView)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // workListDBDataSet
            // 
            this.workListDBDataSet.DataSetName = "WorkListDBDataSet";
            this.workListDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // performedSeriesBindingSource
            // 
            this.performedSeriesBindingSource.DataMember = "PerformedSeries";
            this.performedSeriesBindingSource.DataSource = this.workListDBDataSet;
            // 
            // performedSeriesTableAdapter
            // 
            this.performedSeriesTableAdapter.ClearBeforeFill = true;
            // 
            // performedSeriesBindingNavigator
            // 
            this.performedSeriesBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.performedSeriesBindingNavigator.BindingSource = this.performedSeriesBindingSource;
            this.performedSeriesBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.performedSeriesBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.performedSeriesBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.performedSeriesBindingNavigatorSaveItem});
            this.performedSeriesBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.performedSeriesBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.performedSeriesBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.performedSeriesBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.performedSeriesBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.performedSeriesBindingNavigator.Name = "performedSeriesBindingNavigator";
            this.performedSeriesBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.performedSeriesBindingNavigator.Size = new System.Drawing.Size(763, 25);
            this.performedSeriesBindingNavigator.TabIndex = 0;
            this.performedSeriesBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // performedSeriesBindingNavigatorSaveItem
            // 
            this.performedSeriesBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.performedSeriesBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("performedSeriesBindingNavigatorSaveItem.Image")));
            this.performedSeriesBindingNavigatorSaveItem.Name = "performedSeriesBindingNavigatorSaveItem";
            this.performedSeriesBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.performedSeriesBindingNavigatorSaveItem.Text = "Save Data";
            this.performedSeriesBindingNavigatorSaveItem.Click += new System.EventHandler(this.performedSeriesBindingNavigatorSaveItem_Click_1);
            // 
            // performedSeriesDataGridView
            // 
            this.performedSeriesDataGridView.AutoGenerateColumns = false;
            this.performedSeriesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.performedSeriesDataGridView.DataSource = this.performedSeriesBindingSource;
            this.performedSeriesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.performedSeriesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.performedSeriesDataGridView.Name = "performedSeriesDataGridView";
            this.performedSeriesDataGridView.Size = new System.Drawing.Size(763, 268);
            this.performedSeriesDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ProcedureStepID";
            this.dataGridViewTextBoxColumn1.HeaderText = "ProcedureStepID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "StudyInstanceUID";
            this.dataGridViewTextBoxColumn2.HeaderText = "StudyInstanceUID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "SeriesInstanceUID";
            this.dataGridViewTextBoxColumn3.HeaderText = "SeriesInstanceUID";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PerformingPhysicianName";
            this.dataGridViewTextBoxColumn4.HeaderText = "PerformingPhysicianName";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "OperatorName";
            this.dataGridViewTextBoxColumn5.HeaderText = "OperatorName";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ProtocolName";
            this.dataGridViewTextBoxColumn6.HeaderText = "ProtocolName";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "SeriesDescription";
            this.dataGridViewTextBoxColumn7.HeaderText = "SeriesDescription";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "RetrieveAET";
            this.dataGridViewTextBoxColumn8.HeaderText = "RetrieveAET";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // referencedImagesBindingSource
            // 
            this.referencedImagesBindingSource.DataMember = "ReferencedImages";
            this.referencedImagesBindingSource.DataSource = this.workListDBDataSet;
            // 
            // referencedImagesTableAdapter
            // 
            this.referencedImagesTableAdapter.ClearBeforeFill = true;
            // 
            // referencedImagesDataGridView
            // 
            this.referencedImagesDataGridView.AutoGenerateColumns = false;
            this.referencedImagesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
            this.referencedImagesDataGridView.DataSource = this.referencedImagesBindingSource;
            this.referencedImagesDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.referencedImagesDataGridView.Location = new System.Drawing.Point(0, 0);
            this.referencedImagesDataGridView.Name = "referencedImagesDataGridView";
            this.referencedImagesDataGridView.Size = new System.Drawing.Size(763, 264);
            this.referencedImagesDataGridView.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "SeriesInstanceUID";
            this.dataGridViewTextBoxColumn9.HeaderText = "SeriesInstanceUID";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "ReferencedImageSopInstanceUID";
            this.dataGridViewTextBoxColumn10.HeaderText = "ReferencedImageSopInstanceUID";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "ReferencedImageSopClassUID";
            this.dataGridViewTextBoxColumn11.HeaderText = "ReferencedImageSopClassUID";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.performedSeriesDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.referencedImagesDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(763, 536);
            this.splitContainer1.SplitterDistance = 268;
            this.splitContainer1.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(763, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // SeriesDataDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.performedSeriesBindingNavigator);
            this.Name = "SeriesDataDialog";
            this.Text = "SeriesDataDialog";
            this.Load += new System.EventHandler(this.SeriesDataDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.workListDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performedSeriesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performedSeriesBindingNavigator)).EndInit();
            this.performedSeriesBindingNavigator.ResumeLayout(false);
            this.performedSeriesBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.performedSeriesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.referencedImagesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.referencedImagesDataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WorkListDBDataSet workListDBDataSet;
        private System.Windows.Forms.BindingSource performedSeriesBindingSource;
        private PerformedSeriesTableAdapter performedSeriesTableAdapter;
        private System.Windows.Forms.BindingNavigator performedSeriesBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton performedSeriesBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView performedSeriesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.BindingSource referencedImagesBindingSource;
        private ReferencedImagesTableAdapter referencedImagesTableAdapter;
        private System.Windows.Forms.DataGridView referencedImagesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;

    }
}