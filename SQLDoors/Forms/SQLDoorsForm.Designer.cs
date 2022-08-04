namespace SQLDoors.Forms
{
    partial class SQLDoorsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLDoorsForm));
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_tableCreate = new System.Windows.Forms.Button();
            this.btn_import = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(25, 108);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(344, 44);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Export Door Data";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_tableCreate
            // 
            this.btn_tableCreate.Location = new System.Drawing.Point(25, 33);
            this.btn_tableCreate.Name = "btn_tableCreate";
            this.btn_tableCreate.Size = new System.Drawing.Size(344, 44);
            this.btn_tableCreate.TabIndex = 2;
            this.btn_tableCreate.Text = "Create SQL Table";
            this.btn_tableCreate.UseVisualStyleBackColor = true;
            this.btn_tableCreate.Click += new System.EventHandler(this.btn_tableCreate_Click);
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(29, 184);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(340, 44);
            this.btn_import.TabIndex = 3;
            this.btn_import.Text = "Import Door Data";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // SQLDoorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 248);
            this.Controls.Add(this.btn_import);
            this.Controls.Add(this.btn_tableCreate);
            this.Controls.Add(this.btn_save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SQLDoorsForm";
            this.Text = "SQLDoorsForm";
            this.Load += new System.EventHandler(this.SQLDoorsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_tableCreate;
        private System.Windows.Forms.Button btn_import;
    }
}