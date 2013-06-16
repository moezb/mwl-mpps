using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Edgeteks.MWLServer
{
    public partial class SeriesDataDialog : Form
    {
        public SeriesDataDialog()
        {
            InitializeComponent();
        }

        private void performedSeriesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.performedSeriesBindingSource.EndEdit();
            this.performedSeriesTableAdapter.Update(this.workListDBDataSet.PerformedSeries);

        }

        private void SeriesDataDialog_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'workListDBDataSet.ReferencedImages' table. You can move, or remove it, as needed.
            this.referencedImagesTableAdapter.Fill(this.workListDBDataSet.ReferencedImages);
            // TODO: This line of code loads data into the 'workListDBDataSet.PerformedSeries' table. You can move, or remove it, as needed.
            this.performedSeriesTableAdapter.Fill(this.workListDBDataSet.PerformedSeries);
            // TODO: This line of code loads data into the 'workListDBDataSet.ReferencedImages' table. You can move, or remove it, as needed.
            this.referencedImagesTableAdapter.Fill(this.workListDBDataSet.ReferencedImages);
            // TODO: This line of code loads data into the 'workListDBDataSet.PerformedSeries' table. You can move, or remove it, as needed.
            this.performedSeriesTableAdapter.Fill(this.workListDBDataSet.PerformedSeries);

        }

        private void performedSeriesBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.performedSeriesBindingSource.EndEdit();
            this.performedSeriesTableAdapter.Update(this.workListDBDataSet.PerformedSeries);

        }
    }
}