namespace E_CommerceBackEnd
{
    partial class PromoControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromoControl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtReductionNom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReductionMontant = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.reductionDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimeFin = new System.Windows.Forms.DateTimePicker();
            this.dateTimeDebut = new System.Windows.Forms.DateTimePicker();
            this.button8 = new System.Windows.Forms.Button();
            this.reduProduitDataGridView = new System.Windows.Forms.DataGridView();
            this.txtReduProNom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.txtProduitNom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.produitDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reductionDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reduProduitDataGridView)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.produitDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtReductionNom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtReductionMontant);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.reductionDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(953, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reduction";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(660, 132);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 37);
            this.button3.TabIndex = 10;
            this.button3.Text = "  Modifier";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.ModifierRedution);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(802, 132);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(142, 37);
            this.button4.TabIndex = 11;
            this.button4.Text = "  Rechercher";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.ChercherReduction);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(802, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 37);
            this.button2.TabIndex = 12;
            this.button2.Text = "  Supprimer";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.SupprimerReduction);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(660, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 37);
            this.button1.TabIndex = 13;
            this.button1.Text = "  Ajouter";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.AjouterReduction);
            // 
            // txtReductionNom
            // 
            this.txtReductionNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReductionNom.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReductionNom.Location = new System.Drawing.Point(551, 38);
            this.txtReductionNom.Multiline = true;
            this.txtReductionNom.Name = "txtReductionNom";
            this.txtReductionNom.Size = new System.Drawing.Size(180, 24);
            this.txtReductionNom.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label3.Location = new System.Drawing.Point(547, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nom";
            // 
            // txtReductionMontant
            // 
            this.txtReductionMontant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReductionMontant.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReductionMontant.Location = new System.Drawing.Point(757, 38);
            this.txtReductionMontant.Multiline = true;
            this.txtReductionMontant.Name = "txtReductionMontant";
            this.txtReductionMontant.Size = new System.Drawing.Size(181, 24);
            this.txtReductionMontant.TabIndex = 9;
            this.txtReductionMontant.TextChanged += new System.EventHandler(this.ReductionMontantChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.Location = new System.Drawing.Point(754, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Reduction montant (%)";
            // 
            // reductionDataGridView
            // 
            this.reductionDataGridView.AllowUserToAddRows = false;
            this.reductionDataGridView.AllowUserToDeleteRows = false;
            this.reductionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reductionDataGridView.Location = new System.Drawing.Point(6, 19);
            this.reductionDataGridView.Name = "reductionDataGridView";
            this.reductionDataGridView.ReadOnly = true;
            this.reductionDataGridView.Size = new System.Drawing.Size(481, 150);
            this.reductionDataGridView.TabIndex = 0;
            this.reductionDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReductionCellClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dateTimeFin);
            this.groupBox2.Controls.Add(this.dateTimeDebut);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.reduProduitDataGridView);
            this.groupBox2.Controls.Add(this.txtReduProNom);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(953, 178);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Produit en reduction";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // dateTimeFin
            // 
            this.dateTimeFin.CalendarFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeFin.Location = new System.Drawing.Point(842, 40);
            this.dateTimeFin.Name = "dateTimeFin";
            this.dateTimeFin.Size = new System.Drawing.Size(101, 21);
            this.dateTimeFin.TabIndex = 14;
            this.dateTimeFin.ValueChanged += new System.EventHandler(this.dateTimeFin_ValueChanged);
            // 
            // dateTimeDebut
            // 
            this.dateTimeDebut.CalendarFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeDebut.Location = new System.Drawing.Point(719, 40);
            this.dateTimeDebut.Name = "dateTimeDebut";
            this.dateTimeDebut.Size = new System.Drawing.Size(105, 21);
            this.dateTimeDebut.TabIndex = 14;
            this.dateTimeDebut.ValueChanged += new System.EventHandler(this.dateTimeDebut_ValueChanged);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(660, 132);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(125, 37);
            this.button8.TabIndex = 10;
            this.button8.Text = "  Modifier";
            this.button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.ModifierRedutionProduit);
            // 
            // reduProduitDataGridView
            // 
            this.reduProduitDataGridView.AllowUserToAddRows = false;
            this.reduProduitDataGridView.AllowUserToDeleteRows = false;
            this.reduProduitDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.reduProduitDataGridView.Location = new System.Drawing.Point(6, 19);
            this.reduProduitDataGridView.Name = "reduProduitDataGridView";
            this.reduProduitDataGridView.ReadOnly = true;
            this.reduProduitDataGridView.Size = new System.Drawing.Size(481, 150);
            this.reduProduitDataGridView.TabIndex = 0;
            this.reduProduitDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RecutionProduitCellClicked);
            // 
            // txtReduProNom
            // 
            this.txtReduProNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReduProNom.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReduProNom.Location = new System.Drawing.Point(551, 40);
            this.txtReduProNom.Multiline = true;
            this.txtReduProNom.Name = "txtReduProNom";
            this.txtReduProNom.Size = new System.Drawing.Size(154, 21);
            this.txtReduProNom.TabIndex = 8;
            this.txtReduProNom.TextChanged += new System.EventHandler(this.txtReduProNom_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label5.Location = new System.Drawing.Point(549, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Nom produit";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(802, 132);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(142, 37);
            this.button7.TabIndex = 11;
            this.button7.Text = "  Rechercher";
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.ChercherReductionProduit);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(660, 80);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 37);
            this.button5.TabIndex = 13;
            this.button5.Text = "  Ajouter";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.AjouterReductionProduit);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(802, 80);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(142, 37);
            this.button6.TabIndex = 12;
            this.button6.Text = "  Supprimer";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.SupprimerReductionProduit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(839, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Date fin";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(716, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Date debut";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button9);
            this.groupBox3.Controls.Add(this.txtProduitNom);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.produitDataGridView);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBox3.Location = new System.Drawing.Point(3, 373);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(953, 233);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Produits";
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(797, 117);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(142, 37);
            this.button9.TabIndex = 12;
            this.button9.Text = "  Rechercher";
            this.button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.ChercherProduit);
            // 
            // txtProduitNom
            // 
            this.txtProduitNom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProduitNom.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduitNom.Location = new System.Drawing.Point(686, 77);
            this.txtProduitNom.Multiline = true;
            this.txtProduitNom.Name = "txtProduitNom";
            this.txtProduitNom.Size = new System.Drawing.Size(253, 27);
            this.txtProduitNom.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label6.Location = new System.Drawing.Point(682, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Nom";
            // 
            // produitDataGridView
            // 
            this.produitDataGridView.AllowUserToAddRows = false;
            this.produitDataGridView.AllowUserToDeleteRows = false;
            this.produitDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.produitDataGridView.Location = new System.Drawing.Point(6, 20);
            this.produitDataGridView.Name = "produitDataGridView";
            this.produitDataGridView.ReadOnly = true;
            this.produitDataGridView.Size = new System.Drawing.Size(481, 207);
            this.produitDataGridView.TabIndex = 0;
            this.produitDataGridView.SelectionChanged += new System.EventHandler(this.produitDataGridViewSelectionChanged);
            // 
            // PromoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PromoControl";
            this.Size = new System.Drawing.Size(983, 660);
            this.Load += new System.EventHandler(this.PromoControlLoad);
            this.VisibleChanged += new System.EventHandler(this.PromoControlVisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reductionDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reduProduitDataGridView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.produitDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView reductionDataGridView;
        private System.Windows.Forms.TextBox txtReductionNom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReductionMontant;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.DataGridView reduProduitDataGridView;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimeFin;
        private System.Windows.Forms.DateTimePicker dateTimeDebut;
        private System.Windows.Forms.TextBox txtReduProNom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView produitDataGridView;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox txtProduitNom;
        private System.Windows.Forms.Label label6;
    }
}
