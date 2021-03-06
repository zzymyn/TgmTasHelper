﻿namespace TgmTasHelper
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
            this.m_Choices = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_NewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_UndoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_RedoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_Time = new System.Windows.Forms.Label();
            this.m_Level = new System.Windows.Forms.Label();
            this.m_NextButton = new System.Windows.Forms.Button();
            this.m_PrevButton = new System.Windows.Forms.Button();
            this.m_SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_HideHolesCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_Preview = new TgmTasHelper.SimulationRenderer();
            this.m_PreviewStrip = new TgmTasHelper.SimulationRenderer();
            this.m_CurrentBoardRenderer = new TgmTasHelper.SimulationRenderer();
            this.m_MaxMoves = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PreviewStrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_CurrentBoardRenderer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_MaxMoves)).BeginInit();
            this.SuspendLayout();
            // 
            // m_Choices
            // 
            this.m_Choices.AutoScroll = true;
            this.m_Choices.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_Choices.Location = new System.Drawing.Point(0, 524);
            this.m_Choices.Name = "m_Choices";
            this.m_Choices.Size = new System.Drawing.Size(1484, 400);
            this.m_Choices.TabIndex = 1;
            this.m_Choices.WrapContents = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.editToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1484, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_NewMenuItem,
            this.m_OpenMenuItem,
            this.m_SaveMenuItem,
            this.m_SaveAsMenuItem,
            this.toolStripMenuItem1,
            this.m_ExitMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // m_NewMenuItem
            // 
            this.m_NewMenuItem.Image = global::TgmTasHelper.Properties.Resources.page;
            this.m_NewMenuItem.Name = "m_NewMenuItem";
            this.m_NewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.m_NewMenuItem.Size = new System.Drawing.Size(195, 22);
            this.m_NewMenuItem.Text = "&New";
            this.m_NewMenuItem.Click += new System.EventHandler(this.m_NewMenuItem_Click);
            // 
            // m_OpenMenuItem
            // 
            this.m_OpenMenuItem.Image = global::TgmTasHelper.Properties.Resources.folder_page;
            this.m_OpenMenuItem.Name = "m_OpenMenuItem";
            this.m_OpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.m_OpenMenuItem.Size = new System.Drawing.Size(195, 22);
            this.m_OpenMenuItem.Text = "&Open";
            this.m_OpenMenuItem.Click += new System.EventHandler(this.m_OpenMenuItem_Click);
            // 
            // m_SaveMenuItem
            // 
            this.m_SaveMenuItem.Image = global::TgmTasHelper.Properties.Resources.disk;
            this.m_SaveMenuItem.Name = "m_SaveMenuItem";
            this.m_SaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.m_SaveMenuItem.Size = new System.Drawing.Size(195, 22);
            this.m_SaveMenuItem.Text = "&Save";
            this.m_SaveMenuItem.Click += new System.EventHandler(this.m_SaveMenuItem_Click);
            // 
            // m_SaveAsMenuItem
            // 
            this.m_SaveAsMenuItem.Name = "m_SaveAsMenuItem";
            this.m_SaveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.m_SaveAsMenuItem.Size = new System.Drawing.Size(195, 22);
            this.m_SaveAsMenuItem.Text = "Save As...";
            this.m_SaveAsMenuItem.Click += new System.EventHandler(this.m_SaveAsMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
            // 
            // m_ExitMenuItem
            // 
            this.m_ExitMenuItem.Name = "m_ExitMenuItem";
            this.m_ExitMenuItem.Size = new System.Drawing.Size(195, 22);
            this.m_ExitMenuItem.Text = "&Exit";
            this.m_ExitMenuItem.Click += new System.EventHandler(this.m_ExitMenuItem_Click);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_UndoMenuItem,
            this.m_RedoMenuItem});
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem1.Text = "&Edit";
            // 
            // m_UndoMenuItem
            // 
            this.m_UndoMenuItem.Image = global::TgmTasHelper.Properties.Resources.arrow_undo;
            this.m_UndoMenuItem.Name = "m_UndoMenuItem";
            this.m_UndoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.m_UndoMenuItem.Size = new System.Drawing.Size(174, 22);
            this.m_UndoMenuItem.Text = "&Undo";
            this.m_UndoMenuItem.Click += new System.EventHandler(this.m_UndoMenuItem_Click);
            // 
            // m_RedoMenuItem
            // 
            this.m_RedoMenuItem.Image = global::TgmTasHelper.Properties.Resources.arrow_redo;
            this.m_RedoMenuItem.Name = "m_RedoMenuItem";
            this.m_RedoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.m_RedoMenuItem.Size = new System.Drawing.Size(174, 22);
            this.m_RedoMenuItem.Text = "&Redo";
            this.m_RedoMenuItem.Click += new System.EventHandler(this.m_RedoMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // m_Time
            // 
            this.m_Time.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_Time.Location = new System.Drawing.Point(78, 396);
            this.m_Time.Name = "m_Time";
            this.m_Time.Size = new System.Drawing.Size(150, 23);
            this.m_Time.TabIndex = 8;
            this.m_Time.Text = "0";
            this.m_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_Level
            // 
            this.m_Level.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_Level.Location = new System.Drawing.Point(78, 419);
            this.m_Level.Name = "m_Level";
            this.m_Level.Size = new System.Drawing.Size(150, 23);
            this.m_Level.TabIndex = 9;
            this.m_Level.Text = "0";
            this.m_Level.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // m_NextButton
            // 
            this.m_NextButton.Image = global::TgmTasHelper.Properties.Resources.arrow_right;
            this.m_NextButton.Location = new System.Drawing.Point(234, 215);
            this.m_NextButton.Name = "m_NextButton";
            this.m_NextButton.Size = new System.Drawing.Size(60, 60);
            this.m_NextButton.TabIndex = 11;
            this.m_NextButton.UseVisualStyleBackColor = true;
            this.m_NextButton.Click += new System.EventHandler(this.m_NextButton_Click);
            // 
            // m_PrevButton
            // 
            this.m_PrevButton.Image = global::TgmTasHelper.Properties.Resources.arrow_left;
            this.m_PrevButton.Location = new System.Drawing.Point(12, 215);
            this.m_PrevButton.Name = "m_PrevButton";
            this.m_PrevButton.Size = new System.Drawing.Size(60, 60);
            this.m_PrevButton.TabIndex = 10;
            this.m_PrevButton.UseVisualStyleBackColor = true;
            this.m_PrevButton.Click += new System.EventHandler(this.m_BackButton_Click);
            // 
            // m_SaveFileDialog
            // 
            this.m_SaveFileDialog.DefaultExt = "xml.gz";
            this.m_SaveFileDialog.Filter = "Compressed XML files|*.xml.gz|All Files|*.*";
            this.m_SaveFileDialog.RestoreDirectory = true;
            this.m_SaveFileDialog.SupportMultiDottedExtensions = true;
            // 
            // m_OpenFileDialog
            // 
            this.m_OpenFileDialog.DefaultExt = "xml.gz";
            this.m_OpenFileDialog.Filter = "Compressed XML files|*.xml.gz|All Files|*.*";
            this.m_OpenFileDialog.SupportMultiDottedExtensions = true;
            // 
            // m_HideHolesCheckbox
            // 
            this.m_HideHolesCheckbox.Location = new System.Drawing.Point(6, 19);
            this.m_HideHolesCheckbox.Name = "m_HideHolesCheckbox";
            this.m_HideHolesCheckbox.Size = new System.Drawing.Size(211, 24);
            this.m_HideHolesCheckbox.TabIndex = 12;
            this.m_HideHolesCheckbox.Text = "Hide options that will result in holes";
            this.m_HideHolesCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.m_MaxMoves);
            this.groupBox1.Controls.Add(this.m_HideHolesCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(300, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 182);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solver Options:";
            // 
            // m_Preview
            // 
            this.m_Preview.BackColor = System.Drawing.Color.Black;
            this.m_Preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_Preview.Location = new System.Drawing.Point(78, 27);
            this.m_Preview.Name = "m_Preview";
            this.m_Preview.Size = new System.Drawing.Size(150, 60);
            this.m_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_Preview.TabIndex = 3;
            this.m_Preview.TabStop = false;
            // 
            // m_PreviewStrip
            // 
            this.m_PreviewStrip.BackColor = System.Drawing.Color.Black;
            this.m_PreviewStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_PreviewStrip.Location = new System.Drawing.Point(234, 27);
            this.m_PreviewStrip.Name = "m_PreviewStrip";
            this.m_PreviewStrip.Size = new System.Drawing.Size(750, 60);
            this.m_PreviewStrip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_PreviewStrip.TabIndex = 3;
            this.m_PreviewStrip.TabStop = false;
            // 
            // m_CurrentBoardRenderer
            // 
            this.m_CurrentBoardRenderer.BackColor = System.Drawing.Color.Black;
            this.m_CurrentBoardRenderer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_CurrentBoardRenderer.Location = new System.Drawing.Point(78, 93);
            this.m_CurrentBoardRenderer.Name = "m_CurrentBoardRenderer";
            this.m_CurrentBoardRenderer.Size = new System.Drawing.Size(150, 300);
            this.m_CurrentBoardRenderer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_CurrentBoardRenderer.TabIndex = 0;
            this.m_CurrentBoardRenderer.TabStop = false;
            this.m_CurrentBoardRenderer.Text = "gameStateRenderer1";
            // 
            // m_MaxMoves
            // 
            this.m_MaxMoves.Location = new System.Drawing.Point(181, 49);
            this.m_MaxMoves.Name = "m_MaxMoves";
            this.m_MaxMoves.Size = new System.Drawing.Size(36, 20);
            this.m_MaxMoves.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 23);
            this.label1.TabIndex = 14;
            this.label1.Text = "Max moves to show:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 924);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_NextButton);
            this.Controls.Add(this.m_PrevButton);
            this.Controls.Add(this.m_Level);
            this.Controls.Add(this.m_Time);
            this.Controls.Add(this.m_Preview);
            this.Controls.Add(this.m_PreviewStrip);
            this.Controls.Add(this.m_Choices);
            this.Controls.Add(this.m_CurrentBoardRenderer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "TGM TAS Helper";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_PreviewStrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_CurrentBoardRenderer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_MaxMoves)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SimulationRenderer m_CurrentBoardRenderer;
        private System.Windows.Forms.FlowLayoutPanel m_Choices;
        private SimulationRenderer m_PreviewStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem m_UndoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_RedoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_NewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_OpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem m_ExitMenuItem;
        private SimulationRenderer m_Preview;
        private System.Windows.Forms.Label m_Time;
        private System.Windows.Forms.Label m_Level;
        private System.Windows.Forms.Button m_PrevButton;
        private System.Windows.Forms.Button m_NextButton;
        private System.Windows.Forms.SaveFileDialog m_SaveFileDialog;
        private System.Windows.Forms.OpenFileDialog m_OpenFileDialog;
        private System.Windows.Forms.CheckBox m_HideHolesCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown m_MaxMoves;
    }
}

