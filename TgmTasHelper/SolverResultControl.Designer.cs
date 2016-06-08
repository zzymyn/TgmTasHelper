namespace TgmTasHelper
{
    partial class SolverResultControl
    {

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.m_BoardRenderer = new TgmTasHelper.SimulationRenderer();
            this.m_Time = new System.Windows.Forms.Label();
            this.m_Level = new System.Windows.Forms.Label();
            this.m_BMT = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_BoardRenderer)).BeginInit();
            this.SuspendLayout();
            // 
            // m_BoardRenderer
            // 
            this.m_BoardRenderer.BackColor = System.Drawing.Color.Black;
            this.m_BoardRenderer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_BoardRenderer.Location = new System.Drawing.Point(0, 0);
            this.m_BoardRenderer.Name = "m_BoardRenderer";
            this.m_BoardRenderer.Size = new System.Drawing.Size(150, 300);
            this.m_BoardRenderer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_BoardRenderer.TabIndex = 0;
            this.m_BoardRenderer.TabStop = false;
            this.m_BoardRenderer.Text = "boardRenderer1";
            // 
            // m_Time
            // 
            this.m_Time.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_Time.Location = new System.Drawing.Point(3, 303);
            this.m_Time.Name = "m_Time";
            this.m_Time.Size = new System.Drawing.Size(143, 23);
            this.m_Time.TabIndex = 2;
            this.m_Time.Text = "0";
            this.m_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_Level
            // 
            this.m_Level.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_Level.Location = new System.Drawing.Point(3, 326);
            this.m_Level.Name = "m_Level";
            this.m_Level.Size = new System.Drawing.Size(143, 23);
            this.m_Level.TabIndex = 4;
            this.m_Level.Text = "0";
            this.m_Level.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_BMT
            // 
            this.m_BMT.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_BMT.Location = new System.Drawing.Point(3, 349);
            this.m_BMT.Name = "m_BMT";
            this.m_BMT.Size = new System.Drawing.Size(143, 23);
            this.m_BMT.TabIndex = 5;
            this.m_BMT.Text = "0";
            this.m_BMT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SolverResultControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_BMT);
            this.Controls.Add(this.m_Level);
            this.Controls.Add(this.m_Time);
            this.Controls.Add(this.m_BoardRenderer);
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Name = "SolverResultControl";
            this.Size = new System.Drawing.Size(150, 376);
            ((System.ComponentModel.ISupportInitialize)(this.m_BoardRenderer)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private SimulationRenderer m_BoardRenderer;
        private System.Windows.Forms.Label m_Time;
        private System.Windows.Forms.Label m_Level;
        private System.Windows.Forms.Label m_BMT;
    }
}
