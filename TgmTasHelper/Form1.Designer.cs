﻿namespace TgmTasHelper
{
    partial class Form1
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
            this.gameStateRenderer1 = new TgmTasHelper.BoardRenderer();
            this.SuspendLayout();
            // 
            // gameStateRenderer1
            // 
            this.gameStateRenderer1.Location = new System.Drawing.Point(12, 12);
            this.gameStateRenderer1.MaximumSize = new System.Drawing.Size(200, 400);
            this.gameStateRenderer1.MinimumSize = new System.Drawing.Size(200, 400);
            this.gameStateRenderer1.Name = "gameStateRenderer1";
            this.gameStateRenderer1.Size = new System.Drawing.Size(200, 400);
            this.gameStateRenderer1.TabIndex = 0;
            this.gameStateRenderer1.Text = "gameStateRenderer1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1478, 847);
            this.Controls.Add(this.gameStateRenderer1);
            this.Name = "Form1";
            this.Text = "TGM TAS Helper";
            this.ResumeLayout(false);

        }

        #endregion

        private BoardRenderer gameStateRenderer1;
    }
}

