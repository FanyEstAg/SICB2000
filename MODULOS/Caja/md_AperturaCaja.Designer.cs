
namespace Punto_de_Venta.MODULOS.Caja
{
    partial class md_AperturaCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(md_AperturaCaja));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.txtfecha = new System.Windows.Forms.DateTimePicker();
            this.txtip = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmInicio = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOmitir = new System.Windows.Forms.ToolStripMenuItem();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.White;
            this.Panel1.Controls.Add(this.Panel2);
            this.Panel1.Controls.Add(this.txtMonto);
            this.Panel1.Controls.Add(this.MenuStrip1);
            this.Panel1.Controls.Add(this.Label9);
            this.Panel1.Controls.Add(this.Label1);
            this.Panel1.Location = new System.Drawing.Point(226, 42);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(408, 267);
            this.Panel1.TabIndex = 567;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.White;
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.Button1);
            this.Panel2.Controls.Add(this.txtfecha);
            this.Panel2.Controls.Add(this.txtip);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(408, 72);
            this.Panel2.TabIndex = 565;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.Label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 33.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(0, 0);
            this.Label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(408, 72);
            this.Label2.TabIndex = 532;
            this.Label2.Text = "Dinero en Caja";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button1
            // 
            this.Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(197)))), ((int)(((byte)(76)))));
            this.Button1.FlatAppearance.BorderSize = 0;
            this.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.ForeColor = System.Drawing.Color.White;
            this.Button1.Location = new System.Drawing.Point(378, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(27, 32);
            this.Button1.TabIndex = 540;
            this.Button1.Text = "X";
            this.Button1.UseVisualStyleBackColor = false;
            // 
            // txtfecha
            // 
            this.txtfecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtfecha.Location = new System.Drawing.Point(137, 6);
            this.txtfecha.Name = "txtfecha";
            this.txtfecha.Size = new System.Drawing.Size(74, 20);
            this.txtfecha.TabIndex = 566;
            // 
            // txtip
            // 
            this.txtip.AutoSize = true;
            this.txtip.BackColor = System.Drawing.Color.Transparent;
            this.txtip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtip.ForeColor = System.Drawing.Color.White;
            this.txtip.Location = new System.Drawing.Point(90, 38);
            this.txtip.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtip.Name = "txtip";
            this.txtip.Size = new System.Drawing.Size(90, 13);
            this.txtip.TabIndex = 527;
            this.txtip.Text = "tu nomvbre de pc";
            // 
            // txtMonto
            // 
            this.txtMonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.txtMonto.Location = new System.Drawing.Point(63, 145);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(284, 30);
            this.txtMonto.TabIndex = 564;
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.AutoSize = false;
            this.MenuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmInicio,
            this.tsmOmitir});
            this.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuStrip1.Location = new System.Drawing.Point(63, 194);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.ShowItemToolTips = true;
            this.MenuStrip1.Size = new System.Drawing.Size(216, 43);
            this.MenuStrip1.TabIndex = 562;
            this.MenuStrip1.Text = "MenuStrip7";
            // 
            // tsmInicio
            // 
            this.tsmInicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(19)))), ((int)(((byte)(32)))));
            this.tsmInicio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tsmInicio.ForeColor = System.Drawing.Color.White;
            this.tsmInicio.Name = "tsmInicio";
            this.tsmInicio.Size = new System.Drawing.Size(65, 39);
            this.tsmInicio.Text = "Inicio";
            this.tsmInicio.ToolTipText = "Iniciar";
            // 
            // tsmOmitir
            // 
            this.tsmOmitir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tsmOmitir.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tsmOmitir.ForeColor = System.Drawing.Color.Black;
            this.tsmOmitir.Name = "tsmOmitir";
            this.tsmOmitir.Size = new System.Drawing.Size(71, 39);
            this.tsmOmitir.Text = "Omitir";
            this.tsmOmitir.ToolTipText = "Omitir";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(12, 75);
            this.Label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(0, 13);
            this.Label9.TabIndex = 533;
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(58, 97);
            this.Label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(289, 31);
            this.Label1.TabIndex = 511;
            this.Label1.Text = "Efectivo Inicial en Caja";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // md_AperturaCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 378);
            this.Controls.Add(this.Panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "md_AperturaCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.DateTimePicker txtfecha;
        internal System.Windows.Forms.Label txtip;
        internal System.Windows.Forms.TextBox txtMonto;
        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem tsmInicio;
        internal System.Windows.Forms.ToolStripMenuItem tsmOmitir;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label1;
    }
}