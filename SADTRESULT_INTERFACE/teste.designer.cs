namespace SADTRESULT_INTERFACE
{
    partial class teste
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(teste));
            this.picboxSair = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblogin = new System.Windows.Forms.Label();
            this.btPesquisar = new System.Windows.Forms.Button();
            this.tbErro = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxSair)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picboxSair
            // 
            this.picboxSair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picboxSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picboxSair.Image = ((System.Drawing.Image)(resources.GetObject("picboxSair.Image")));
            this.picboxSair.Location = new System.Drawing.Point(482, 0);
            this.picboxSair.Name = "picboxSair";
            this.picboxSair.Size = new System.Drawing.Size(17, 20);
            this.picboxSair.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxSair.TabIndex = 0;
            this.picboxSair.TabStop = false;
            this.picboxSair.Click += new System.EventHandler(this.picboxSair_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.tbErro);
            this.panel1.Controls.Add(this.lblogin);
            this.panel1.Controls.Add(this.picboxSair);
            this.panel1.Controls.Add(this.btPesquisar);
            this.panel1.Location = new System.Drawing.Point(3, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 202);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblogin
            // 
            this.lblogin.AutoSize = true;
            this.lblogin.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblogin.ForeColor = System.Drawing.Color.White;
            this.lblogin.Location = new System.Drawing.Point(9, 9);
            this.lblogin.Name = "lblogin";
            this.lblogin.Size = new System.Drawing.Size(58, 23);
            this.lblogin.TabIndex = 26;
            this.lblogin.Text = "ERRO";
            // 
            // btPesquisar
            // 
            this.btPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.btPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btPesquisar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btPesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.btPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPesquisar.ForeColor = System.Drawing.Color.White;
            this.btPesquisar.Location = new System.Drawing.Point(12, 165);
            this.btPesquisar.Name = "btPesquisar";
            this.btPesquisar.Size = new System.Drawing.Size(464, 33);
            this.btPesquisar.TabIndex = 1;
            this.btPesquisar.Text = "Voltar";
            this.btPesquisar.UseVisualStyleBackColor = false;
            this.btPesquisar.Click += new System.EventHandler(this.btPesquisar_Click);
            // 
            // tbErro
            // 
            this.tbErro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.tbErro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbErro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbErro.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbErro.ForeColor = System.Drawing.Color.DimGray;
            this.tbErro.Location = new System.Drawing.Point(12, 35);
            this.tbErro.Multiline = true;
            this.tbErro.Name = "tbErro";
            this.tbErro.ReadOnly = true;
            this.tbErro.Size = new System.Drawing.Size(464, 124);
            this.tbErro.TabIndex = 24;
            this.tbErro.Enter += new System.EventHandler(this.tbLogin_Enter);
            this.tbErro.Leave += new System.EventHandler(this.tbLogin_Leave);
            // 
            // teste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.ClientSize = new System.Drawing.Size(506, 207);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "teste";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SADTRESULT_INTERFACE";
            ((System.ComponentModel.ISupportInitialize)(this.picboxSair)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picboxSair;
        private System.Windows.Forms.Button btPesquisar;
        private System.Windows.Forms.Label lblogin;
        private System.Windows.Forms.TextBox tbErro;
    }
}

