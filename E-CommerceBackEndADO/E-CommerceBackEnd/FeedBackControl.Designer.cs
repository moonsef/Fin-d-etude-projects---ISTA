
namespace E_CommerceBackEnd
{
    partial class FeedBackControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.feedBackGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.feedBackGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.feedBackGridView);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 8.249999F);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(977, 654);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Utilisateur feedback";
            // 
            // feedBackGridView
            // 
            this.feedBackGridView.AllowUserToAddRows = false;
            this.feedBackGridView.AllowUserToDeleteRows = false;
            this.feedBackGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.feedBackGridView.Location = new System.Drawing.Point(6, 19);
            this.feedBackGridView.Name = "feedBackGridView";
            this.feedBackGridView.ReadOnly = true;
            this.feedBackGridView.Size = new System.Drawing.Size(965, 629);
            this.feedBackGridView.TabIndex = 0;
            // 
            // FeedBackControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FeedBackControl";
            this.Size = new System.Drawing.Size(983, 660);
            this.Load += new System.EventHandler(this.FeedBackControl_Load);
            this.VisibleChanged += new System.EventHandler(this.FeedBackControl_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.feedBackGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView feedBackGridView;
    }
}
