namespace TgmTasHelper
{
    partial class MainForm
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
            this.m_CurrentBoardRenderer = new TgmTasHelper.BoardRenderer();
            this.m_Choices = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // m_CurrentBoardRenderer
            // 
            this.m_CurrentBoardRenderer.Location = new System.Drawing.Point(12, 12);
            this.m_CurrentBoardRenderer.MaximumSize = new System.Drawing.Size(200, 400);
            this.m_CurrentBoardRenderer.MinimumSize = new System.Drawing.Size(200, 400);
            this.m_CurrentBoardRenderer.Name = "m_CurrentBoardRenderer";
            this.m_CurrentBoardRenderer.Size = new System.Drawing.Size(200, 400);
            this.m_CurrentBoardRenderer.TabIndex = 0;
            this.m_CurrentBoardRenderer.Text = "gameStateRenderer1";
            // 
            // m_Choices
            // 
            this.m_Choices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Choices.AutoScroll = true;
            this.m_Choices.Location = new System.Drawing.Point(218, 12);
            this.m_Choices.Name = "m_Choices";
            this.m_Choices.Size = new System.Drawing.Size(1248, 823);
            this.m_Choices.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1478, 847);
            this.Controls.Add(this.m_Choices);
            this.Controls.Add(this.m_CurrentBoardRenderer);
            this.Name = "Form1";
            this.Text = "TGM TAS Helper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BoardRenderer m_CurrentBoardRenderer;
        private System.Windows.Forms.FlowLayoutPanel m_Choices;
    }
}

