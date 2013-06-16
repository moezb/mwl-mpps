using Edgeteks.MWLServer.WorkListDBDataSetTableAdapters;

namespace Edgeteks.MWLServer
{
    partial class worklistdbForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.accessionNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.surnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.forenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateOfBirthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referringPhysicianDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.performingPhysicianDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modalityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examScheduledDateAndTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.performedProcedureDateAndTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examRoomDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.studyInstanceUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.procedureIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.procedureStepIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hospitalNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scheduledAETDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examStatusIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examsScheduledBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.workListDBDataSet = new WorkListDBDataSet();
            this.examsScheduledTableAdapter = new ExamsScheduledTableAdapter();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.examsScheduledBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.workListDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.accessionNumberDataGridViewTextBoxColumn,
            this.patientIDDataGridViewTextBoxColumn,
            this.surnameDataGridViewTextBoxColumn,
            this.forenameDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.sexDataGridViewTextBoxColumn,
            this.dateOfBirthDataGridViewTextBoxColumn,
            this.referringPhysicianDataGridViewTextBoxColumn,
            this.performingPhysicianDataGridViewTextBoxColumn,
            this.modalityDataGridViewTextBoxColumn,
            this.examScheduledDateAndTimeDataGridViewTextBoxColumn,
            this.performedProcedureDateAndTimeDataGridViewTextBoxColumn,
            this.examRoomDataGridViewTextBoxColumn,
            this.examDescriptionDataGridViewTextBoxColumn,
            this.studyInstanceUIDDataGridViewTextBoxColumn,
            this.procedureIDDataGridViewTextBoxColumn,
            this.procedureStepIDDataGridViewTextBoxColumn,
            this.hospitalNameDataGridViewTextBoxColumn,
            this.scheduledAETDataGridViewTextBoxColumn,
            this.examStatusIdDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.examsScheduledBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(482, 347);
            this.dataGridView1.TabIndex = 0;
            // 
            // accessionNumberDataGridViewTextBoxColumn
            // 
            this.accessionNumberDataGridViewTextBoxColumn.DataPropertyName = "AccessionNumber";
            this.accessionNumberDataGridViewTextBoxColumn.HeaderText = "AccessionNumber";
            this.accessionNumberDataGridViewTextBoxColumn.Name = "accessionNumberDataGridViewTextBoxColumn";
            this.accessionNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // patientIDDataGridViewTextBoxColumn
            // 
            this.patientIDDataGridViewTextBoxColumn.DataPropertyName = "PatientID";
            this.patientIDDataGridViewTextBoxColumn.HeaderText = "PatientID";
            this.patientIDDataGridViewTextBoxColumn.Name = "patientIDDataGridViewTextBoxColumn";
            this.patientIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // surnameDataGridViewTextBoxColumn
            // 
            this.surnameDataGridViewTextBoxColumn.DataPropertyName = "Surname";
            this.surnameDataGridViewTextBoxColumn.HeaderText = "Surname";
            this.surnameDataGridViewTextBoxColumn.Name = "surnameDataGridViewTextBoxColumn";
            this.surnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // forenameDataGridViewTextBoxColumn
            // 
            this.forenameDataGridViewTextBoxColumn.DataPropertyName = "Forename";
            this.forenameDataGridViewTextBoxColumn.HeaderText = "Forename";
            this.forenameDataGridViewTextBoxColumn.Name = "forenameDataGridViewTextBoxColumn";
            this.forenameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "FullName";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sexDataGridViewTextBoxColumn
            // 
            this.sexDataGridViewTextBoxColumn.DataPropertyName = "Sex";
            this.sexDataGridViewTextBoxColumn.HeaderText = "Sex";
            this.sexDataGridViewTextBoxColumn.Name = "sexDataGridViewTextBoxColumn";
            this.sexDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateOfBirthDataGridViewTextBoxColumn
            // 
            this.dateOfBirthDataGridViewTextBoxColumn.DataPropertyName = "DateOfBirth";
            this.dateOfBirthDataGridViewTextBoxColumn.HeaderText = "DateOfBirth";
            this.dateOfBirthDataGridViewTextBoxColumn.Name = "dateOfBirthDataGridViewTextBoxColumn";
            this.dateOfBirthDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // referringPhysicianDataGridViewTextBoxColumn
            // 
            this.referringPhysicianDataGridViewTextBoxColumn.DataPropertyName = "ReferringPhysician";
            this.referringPhysicianDataGridViewTextBoxColumn.HeaderText = "ReferringPhysician";
            this.referringPhysicianDataGridViewTextBoxColumn.Name = "referringPhysicianDataGridViewTextBoxColumn";
            this.referringPhysicianDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // performingPhysicianDataGridViewTextBoxColumn
            // 
            this.performingPhysicianDataGridViewTextBoxColumn.DataPropertyName = "PerformingPhysician";
            this.performingPhysicianDataGridViewTextBoxColumn.HeaderText = "PerformingPhysician";
            this.performingPhysicianDataGridViewTextBoxColumn.Name = "performingPhysicianDataGridViewTextBoxColumn";
            this.performingPhysicianDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // modalityDataGridViewTextBoxColumn
            // 
            this.modalityDataGridViewTextBoxColumn.DataPropertyName = "Modality";
            this.modalityDataGridViewTextBoxColumn.HeaderText = "Modality";
            this.modalityDataGridViewTextBoxColumn.Name = "modalityDataGridViewTextBoxColumn";
            this.modalityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // examScheduledDateAndTimeDataGridViewTextBoxColumn
            // 
            this.examScheduledDateAndTimeDataGridViewTextBoxColumn.DataPropertyName = "ExamScheduledDateAndTime";
            this.examScheduledDateAndTimeDataGridViewTextBoxColumn.HeaderText = "ExamScheduledDateAndTime";
            this.examScheduledDateAndTimeDataGridViewTextBoxColumn.Name = "examScheduledDateAndTimeDataGridViewTextBoxColumn";
            this.examScheduledDateAndTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // performedProcedureDateAndTimeDataGridViewTextBoxColumn
            // 
            this.performedProcedureDateAndTimeDataGridViewTextBoxColumn.DataPropertyName = "PerformedProcedureDateAndTime";
            this.performedProcedureDateAndTimeDataGridViewTextBoxColumn.HeaderText = "PerformedProcedureDateAndTime";
            this.performedProcedureDateAndTimeDataGridViewTextBoxColumn.Name = "performedProcedureDateAndTimeDataGridViewTextBoxColumn";
            this.performedProcedureDateAndTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // examRoomDataGridViewTextBoxColumn
            // 
            this.examRoomDataGridViewTextBoxColumn.DataPropertyName = "ExamRoom";
            this.examRoomDataGridViewTextBoxColumn.HeaderText = "ExamRoom";
            this.examRoomDataGridViewTextBoxColumn.Name = "examRoomDataGridViewTextBoxColumn";
            this.examRoomDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // examDescriptionDataGridViewTextBoxColumn
            // 
            this.examDescriptionDataGridViewTextBoxColumn.DataPropertyName = "ExamDescription";
            this.examDescriptionDataGridViewTextBoxColumn.HeaderText = "ExamDescription";
            this.examDescriptionDataGridViewTextBoxColumn.Name = "examDescriptionDataGridViewTextBoxColumn";
            this.examDescriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // studyInstanceUIDDataGridViewTextBoxColumn
            // 
            this.studyInstanceUIDDataGridViewTextBoxColumn.DataPropertyName = "StudyInstanceUID";
            this.studyInstanceUIDDataGridViewTextBoxColumn.HeaderText = "StudyInstanceUID";
            this.studyInstanceUIDDataGridViewTextBoxColumn.Name = "studyInstanceUIDDataGridViewTextBoxColumn";
            this.studyInstanceUIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // procedureIDDataGridViewTextBoxColumn
            // 
            this.procedureIDDataGridViewTextBoxColumn.DataPropertyName = "ProcedureID";
            this.procedureIDDataGridViewTextBoxColumn.HeaderText = "ProcedureID";
            this.procedureIDDataGridViewTextBoxColumn.Name = "procedureIDDataGridViewTextBoxColumn";
            this.procedureIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // procedureStepIDDataGridViewTextBoxColumn
            // 
            this.procedureStepIDDataGridViewTextBoxColumn.DataPropertyName = "ProcedureStepID";
            this.procedureStepIDDataGridViewTextBoxColumn.HeaderText = "ProcedureStepID";
            this.procedureStepIDDataGridViewTextBoxColumn.Name = "procedureStepIDDataGridViewTextBoxColumn";
            this.procedureStepIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hospitalNameDataGridViewTextBoxColumn
            // 
            this.hospitalNameDataGridViewTextBoxColumn.DataPropertyName = "HospitalName";
            this.hospitalNameDataGridViewTextBoxColumn.HeaderText = "HospitalName";
            this.hospitalNameDataGridViewTextBoxColumn.Name = "hospitalNameDataGridViewTextBoxColumn";
            this.hospitalNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // scheduledAETDataGridViewTextBoxColumn
            // 
            this.scheduledAETDataGridViewTextBoxColumn.DataPropertyName = "ScheduledAET";
            this.scheduledAETDataGridViewTextBoxColumn.HeaderText = "ScheduledAET";
            this.scheduledAETDataGridViewTextBoxColumn.Name = "scheduledAETDataGridViewTextBoxColumn";
            this.scheduledAETDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // examStatusIdDataGridViewTextBoxColumn
            // 
            this.examStatusIdDataGridViewTextBoxColumn.DataPropertyName = "ExamStatusId";
            this.examStatusIdDataGridViewTextBoxColumn.HeaderText = "ExamStatusId";
            this.examStatusIdDataGridViewTextBoxColumn.Name = "examStatusIdDataGridViewTextBoxColumn";
            this.examStatusIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // examsScheduledBindingSource
            // 
            this.examsScheduledBindingSource.DataMember = "ExamsScheduled";
            this.examsScheduledBindingSource.DataSource = this.workListDBDataSet;
            // 
            // workListDBDataSet
            // 
            this.workListDBDataSet.DataSetName = "WorkListDBDataSet";
            this.workListDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // examsScheduledTableAdapter
            // 
            this.examsScheduledTableAdapter.ClearBeforeFill = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 325);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(482, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // worklistdbForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 347);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "worklistdbForm";
            this.Text = "worklistdbForm";
            this.Load += new System.EventHandler(this.worklistdbForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.examsScheduledBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.workListDBDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridView dataGridView1;
        private WorkListDBDataSet workListDBDataSet;
        private System.Windows.Forms.BindingSource examsScheduledBindingSource;
        private ExamsScheduledTableAdapter examsScheduledTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessionNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn patientIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn surnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn forenameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateOfBirthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn referringPhysicianDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn performingPhysicianDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modalityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examScheduledDateAndTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn performedProcedureDateAndTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examRoomDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn studyInstanceUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn procedureIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn procedureStepIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hospitalNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scheduledAETDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examStatusIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}