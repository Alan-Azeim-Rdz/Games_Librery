namespace Games_Librery
{
    partial class ApiFrom
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
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            lstJuegos = new ListView();
            Nombre = new ColumnHeader();
            Lansamiento = new ColumnHeader();
            picPortada = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picPortada).BeginInit();
            SuspendLayout();
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(12, 9);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(38, 15);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "label1";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(12, 57);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(218, 23);
            txtBuscar.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(12, 119);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "button1";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // lstJuegos
            // 
            lstJuegos.Columns.AddRange(new ColumnHeader[] { Nombre, Lansamiento });
            lstJuegos.Location = new Point(536, 12);
            lstJuegos.Name = "lstJuegos";
            lstJuegos.Size = new Size(603, 597);
            lstJuegos.TabIndex = 3;
            lstJuegos.UseCompatibleStateImageBehavior = false;
            lstJuegos.SelectedIndexChanged += lstJuegos_SelectedIndexChanged;
            // 
            // picPortada
            // 
            picPortada.Location = new Point(318, 30);
            picPortada.Name = "picPortada";
            picPortada.Size = new Size(161, 160);
            picPortada.TabIndex = 4;
            picPortada.TabStop = false;
            // 
            // ApiFrom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1151, 621);
            Controls.Add(picPortada);
            Controls.Add(lstJuegos);
            Controls.Add(btnBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Name = "ApiFrom";
            Text = "ApiFrom";
            ((System.ComponentModel.ISupportInitialize)picPortada).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private ListView lstJuegos;
        private PictureBox picPortada;
        private ColumnHeader Nombre;
        private ColumnHeader Lansamiento;
    }
}