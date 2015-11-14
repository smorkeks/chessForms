namespace ChessForms
{
    partial class GUI
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
            this.consoleInput = new System.Windows.Forms.TextBox();
            this.consoleOutput = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.whiteAgentDropDown = new System.Windows.Forms.ComboBox();
            this.chessTextBox = new System.Windows.Forms.TextBox();
            this.playerTurn = new System.Windows.Forms.TextBox();
            this.turnText = new System.Windows.Forms.TextBox();
            this.blackAgentDropDown = new System.Windows.Forms.ComboBox();
            this.graphicsTab = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.drawingArea = new System.Windows.Forms.PictureBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.AiScoreTextBox = new System.Windows.Forms.TextBox();
            this.scoreBoard = new System.Windows.Forms.TextBox();
            this.pauseButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.blackPlayerLabel = new System.Windows.Forms.Label();
            this.whitePlayerLabel = new System.Windows.Forms.Label();
            this.playbackCheckBox = new System.Windows.Forms.CheckBox();
            this.blackAiDiffTrackBar = new System.Windows.Forms.TrackBar();
            this.blackAiDiffLabel = new System.Windows.Forms.Label();
            this.whiteAiDiffLabel = new System.Windows.Forms.Label();
            this.whiteAiDiffTrackBar = new System.Windows.Forms.TrackBar();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.blackPlaybackFilenameTextBox = new System.Windows.Forms.TextBox();
            this.whitePlaybackFilenameTextBox = new System.Windows.Forms.TextBox();
            this.blackPlaybackSleepTime = new System.Windows.Forms.TrackBar();
            this.whitePlaybackSleepTime = new System.Windows.Forms.TrackBar();
            this.blackSleepTimeLabel = new System.Windows.Forms.Label();
            this.whiteSleepTimeLabel = new System.Windows.Forms.Label();
            this.graphicsTab.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blackAiDiffTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whiteAiDiffTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPlaybackSleepTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitePlaybackSleepTime)).BeginInit();
            this.SuspendLayout();
            // 
            // consoleInput
            // 
            this.consoleInput.Enabled = false;
            this.consoleInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleInput.Location = new System.Drawing.Point(32, 679);
            this.consoleInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.consoleInput.Name = "consoleInput";
            this.consoleInput.Size = new System.Drawing.Size(483, 23);
            this.consoleInput.TabIndex = 0;
            // 
            // consoleOutput
            // 
            this.consoleOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleOutput.Location = new System.Drawing.Point(32, 15);
            this.consoleOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.consoleOutput.Multiline = true;
            this.consoleOutput.Name = "consoleOutput";
            this.consoleOutput.ReadOnly = true;
            this.consoleOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleOutput.Size = new System.Drawing.Size(483, 656);
            this.consoleOutput.TabIndex = 1;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(539, 191);
            this.startButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(233, 28);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // whiteAgentDropDown
            // 
            this.whiteAgentDropDown.FormattingEnabled = true;
            this.whiteAgentDropDown.Items.AddRange(new object[] {
            "Terminal Agent",
            "Graphics Agent",
            "AI",
            "Playback Agent"});
            this.whiteAgentDropDown.Location = new System.Drawing.Point(539, 605);
            this.whiteAgentDropDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.whiteAgentDropDown.Name = "whiteAgentDropDown";
            this.whiteAgentDropDown.Size = new System.Drawing.Size(232, 24);
            this.whiteAgentDropDown.TabIndex = 3;
            this.whiteAgentDropDown.SelectedValueChanged += new System.EventHandler(this.onWhiteAgentChange);
            // 
            // chessTextBox
            // 
            this.chessTextBox.Enabled = false;
            this.chessTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chessTextBox.Location = new System.Drawing.Point(8, 7);
            this.chessTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chessTextBox.Multiline = true;
            this.chessTextBox.Name = "chessTextBox";
            this.chessTextBox.Size = new System.Drawing.Size(692, 639);
            this.chessTextBox.TabIndex = 4;
            // 
            // playerTurn
            // 
            this.playerTurn.Location = new System.Drawing.Point(539, 263);
            this.playerTurn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerTurn.Name = "playerTurn";
            this.playerTurn.ReadOnly = true;
            this.playerTurn.Size = new System.Drawing.Size(232, 22);
            this.playerTurn.TabIndex = 5;
            this.playerTurn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // turnText
            // 
            this.turnText.Location = new System.Drawing.Point(539, 295);
            this.turnText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.turnText.Name = "turnText";
            this.turnText.ReadOnly = true;
            this.turnText.Size = new System.Drawing.Size(232, 22);
            this.turnText.TabIndex = 6;
            this.turnText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // blackAgentDropDown
            // 
            this.blackAgentDropDown.FormattingEnabled = true;
            this.blackAgentDropDown.Items.AddRange(new object[] {
            "Terminal Agent",
            "Graphics Agent",
            "AI",
            "Playback Agent"});
            this.blackAgentDropDown.Location = new System.Drawing.Point(539, 40);
            this.blackAgentDropDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.blackAgentDropDown.Name = "blackAgentDropDown";
            this.blackAgentDropDown.Size = new System.Drawing.Size(232, 24);
            this.blackAgentDropDown.TabIndex = 7;
            this.blackAgentDropDown.SelectedValueChanged += new System.EventHandler(this.onBlackAgentChange);
            // 
            // graphicsTab
            // 
            this.graphicsTab.Controls.Add(this.tabPage2);
            this.graphicsTab.Controls.Add(this.tabPage1);
            this.graphicsTab.Location = new System.Drawing.Point(797, 15);
            this.graphicsTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.graphicsTab.Name = "graphicsTab";
            this.graphicsTab.SelectedIndex = 0;
            this.graphicsTab.Size = new System.Drawing.Size(719, 689);
            this.graphicsTab.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.drawingArea);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(711, 660);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graphics GUI";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // drawingArea
            // 
            this.drawingArea.Location = new System.Drawing.Point(9, 9);
            this.drawingArea.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(693, 640);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.TabStop = false;
            this.drawingArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onDrawingClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chessTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(711, 660);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text GUI";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // AiScoreTextBox
            // 
            this.AiScoreTextBox.Location = new System.Drawing.Point(539, 359);
            this.AiScoreTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AiScoreTextBox.Name = "AiScoreTextBox";
            this.AiScoreTextBox.Size = new System.Drawing.Size(232, 22);
            this.AiScoreTextBox.TabIndex = 9;
            // 
            // scoreBoard
            // 
            this.scoreBoard.Location = new System.Drawing.Point(539, 327);
            this.scoreBoard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scoreBoard.Name = "scoreBoard";
            this.scoreBoard.ReadOnly = true;
            this.scoreBoard.Size = new System.Drawing.Size(232, 22);
            this.scoreBoard.TabIndex = 10;
            this.scoreBoard.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(539, 227);
            this.pauseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(233, 28);
            this.pauseButton.TabIndex = 11;
            this.pauseButton.Text = "Pause Game";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(539, 426);
            this.loadButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(233, 28);
            this.loadButton.TabIndex = 12;
            this.loadButton.Text = "Load Game State";
            this.loadButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(539, 462);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(233, 28);
            this.saveButton.TabIndex = 13;
            this.saveButton.Text = "Save Game State";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(539, 162);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 2);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(539, 405);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 2);
            this.label2.TabIndex = 15;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(539, 571);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 2);
            this.label3.TabIndex = 16;
            this.label3.Text = "label3";
            // 
            // blackPlayerLabel
            // 
            this.blackPlayerLabel.AutoSize = true;
            this.blackPlayerLabel.Location = new System.Drawing.Point(537, 17);
            this.blackPlayerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.blackPlayerLabel.Name = "blackPlayerLabel";
            this.blackPlayerLabel.Size = new System.Drawing.Size(141, 17);
            this.blackPlayerLabel.TabIndex = 17;
            this.blackPlayerLabel.Text = "Black Player Settings";
            // 
            // whitePlayerLabel
            // 
            this.whitePlayerLabel.AutoSize = true;
            this.whitePlayerLabel.Location = new System.Drawing.Point(537, 585);
            this.whitePlayerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.whitePlayerLabel.Name = "whitePlayerLabel";
            this.whitePlayerLabel.Size = new System.Drawing.Size(143, 17);
            this.whitePlayerLabel.TabIndex = 18;
            this.whitePlayerLabel.Text = "White Player Settings";
            // 
            // playbackCheckBox
            // 
            this.playbackCheckBox.AutoSize = true;
            this.playbackCheckBox.Location = new System.Drawing.Point(541, 498);
            this.playbackCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playbackCheckBox.Name = "playbackCheckBox";
            this.playbackCheckBox.Size = new System.Drawing.Size(107, 21);
            this.playbackCheckBox.TabIndex = 19;
            this.playbackCheckBox.Text = "Save moves";
            this.playbackCheckBox.UseVisualStyleBackColor = true;
            // 
            // blackAiDiffTrackBar
            // 
            this.blackAiDiffTrackBar.Location = new System.Drawing.Point(541, 74);
            this.blackAiDiffTrackBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.blackAiDiffTrackBar.Maximum = 6;
            this.blackAiDiffTrackBar.Minimum = 1;
            this.blackAiDiffTrackBar.Name = "blackAiDiffTrackBar";
            this.blackAiDiffTrackBar.Size = new System.Drawing.Size(232, 56);
            this.blackAiDiffTrackBar.TabIndex = 20;
            this.blackAiDiffTrackBar.Value = 2;
            this.blackAiDiffTrackBar.Visible = false;
            this.blackAiDiffTrackBar.Scroll += new System.EventHandler(this.onBlackAiDiffChange);
            // 
            // blackAiDiffLabel
            // 
            this.blackAiDiffLabel.AutoSize = true;
            this.blackAiDiffLabel.Location = new System.Drawing.Point(541, 112);
            this.blackAiDiffLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.blackAiDiffLabel.Name = "blackAiDiffLabel";
            this.blackAiDiffLabel.Size = new System.Drawing.Size(77, 17);
            this.blackAiDiffLabel.TabIndex = 21;
            this.blackAiDiffLabel.Text = "Difficulty: 2";
            this.blackAiDiffLabel.Visible = false;
            // 
            // whiteAiDiffLabel
            // 
            this.whiteAiDiffLabel.AutoSize = true;
            this.whiteAiDiffLabel.Location = new System.Drawing.Point(539, 676);
            this.whiteAiDiffLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.whiteAiDiffLabel.Name = "whiteAiDiffLabel";
            this.whiteAiDiffLabel.Size = new System.Drawing.Size(77, 17);
            this.whiteAiDiffLabel.TabIndex = 23;
            this.whiteAiDiffLabel.Text = "Difficulty: 2";
            this.whiteAiDiffLabel.Visible = false;
            // 
            // whiteAiDiffTrackBar
            // 
            this.whiteAiDiffTrackBar.Location = new System.Drawing.Point(539, 638);
            this.whiteAiDiffTrackBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.whiteAiDiffTrackBar.Maximum = 6;
            this.whiteAiDiffTrackBar.Minimum = 1;
            this.whiteAiDiffTrackBar.Name = "whiteAiDiffTrackBar";
            this.whiteAiDiffTrackBar.Size = new System.Drawing.Size(232, 56);
            this.whiteAiDiffTrackBar.TabIndex = 22;
            this.whiteAiDiffTrackBar.Value = 2;
            this.whiteAiDiffTrackBar.Visible = false;
            this.whiteAiDiffTrackBar.Scroll += new System.EventHandler(this.onWhiteAiDiffChange);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(539, 527);
            this.fileNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.fileNameTextBox.MaxLength = 40;
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(232, 22);
            this.fileNameTextBox.TabIndex = 24;
            // 
            // blackPlaybackFilenameTextBox
            // 
            this.blackPlaybackFilenameTextBox.Location = new System.Drawing.Point(539, 72);
            this.blackPlaybackFilenameTextBox.Name = "blackPlaybackFilenameTextBox";
            this.blackPlaybackFilenameTextBox.Size = new System.Drawing.Size(232, 22);
            this.blackPlaybackFilenameTextBox.TabIndex = 25;
            this.blackPlaybackFilenameTextBox.Visible = false;
            // 
            // whitePlaybackFilenameTextBox
            // 
            this.whitePlaybackFilenameTextBox.Location = new System.Drawing.Point(539, 636);
            this.whitePlaybackFilenameTextBox.Name = "whitePlaybackFilenameTextBox";
            this.whitePlaybackFilenameTextBox.Size = new System.Drawing.Size(232, 22);
            this.whitePlaybackFilenameTextBox.TabIndex = 26;
            this.whitePlaybackFilenameTextBox.Visible = false;
            // 
            // blackPlaybackSleepTime
            // 
            this.blackPlaybackSleepTime.Location = new System.Drawing.Point(539, 100);
            this.blackPlaybackSleepTime.Maximum = 1000;
            this.blackPlaybackSleepTime.Name = "blackPlaybackSleepTime";
            this.blackPlaybackSleepTime.Size = new System.Drawing.Size(232, 56);
            this.blackPlaybackSleepTime.SmallChange = 100;
            this.blackPlaybackSleepTime.TabIndex = 27;
            this.blackPlaybackSleepTime.TickFrequency = 100;
            this.blackPlaybackSleepTime.Visible = false;
            this.blackPlaybackSleepTime.Scroll += new System.EventHandler(this.onBlackSleepTimeChange);
            // 
            // whitePlaybackSleepTime
            // 
            this.whitePlaybackSleepTime.Location = new System.Drawing.Point(539, 664);
            this.whitePlaybackSleepTime.Maximum = 1000;
            this.whitePlaybackSleepTime.Name = "whitePlaybackSleepTime";
            this.whitePlaybackSleepTime.Size = new System.Drawing.Size(232, 56);
            this.whitePlaybackSleepTime.SmallChange = 100;
            this.whitePlaybackSleepTime.TabIndex = 28;
            this.whitePlaybackSleepTime.TickFrequency = 100;
            this.whitePlaybackSleepTime.Visible = false;
            this.whitePlaybackSleepTime.Scroll += new System.EventHandler(this.onWhiteSleepTimeChange);
            // 
            // blackSleepTimeLabel
            // 
            this.blackSleepTimeLabel.AutoSize = true;
            this.blackSleepTimeLabel.Location = new System.Drawing.Point(544, 133);
            this.blackSleepTimeLabel.Name = "blackSleepTimeLabel";
            this.blackSleepTimeLabel.Size = new System.Drawing.Size(95, 17);
            this.blackSleepTimeLabel.TabIndex = 29;
            this.blackSleepTimeLabel.Text = "Sleep Time: 0";
            this.blackSleepTimeLabel.Visible = false;
            // 
            // whiteSleepTimeLabel
            // 
            this.whiteSleepTimeLabel.AutoSize = true;
            this.whiteSleepTimeLabel.Location = new System.Drawing.Point(541, 698);
            this.whiteSleepTimeLabel.Name = "whiteSleepTimeLabel";
            this.whiteSleepTimeLabel.Size = new System.Drawing.Size(95, 17);
            this.whiteSleepTimeLabel.TabIndex = 30;
            this.whiteSleepTimeLabel.Text = "Sleep Time: 0";
            this.whiteSleepTimeLabel.Visible = false;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1544, 731);
            this.Controls.Add(this.whiteSleepTimeLabel);
            this.Controls.Add(this.blackSleepTimeLabel);
            this.Controls.Add(this.whitePlaybackFilenameTextBox);
            this.Controls.Add(this.blackPlaybackFilenameTextBox);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.whiteAiDiffLabel);
            this.Controls.Add(this.whiteAiDiffTrackBar);
            this.Controls.Add(this.blackAiDiffLabel);
            this.Controls.Add(this.blackAiDiffTrackBar);
            this.Controls.Add(this.playbackCheckBox);
            this.Controls.Add(this.whitePlayerLabel);
            this.Controls.Add(this.blackPlayerLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.scoreBoard);
            this.Controls.Add(this.AiScoreTextBox);
            this.Controls.Add(this.graphicsTab);
            this.Controls.Add(this.blackAgentDropDown);
            this.Controls.Add(this.turnText);
            this.Controls.Add(this.playerTurn);
            this.Controls.Add(this.whiteAgentDropDown);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.consoleOutput);
            this.Controls.Add(this.consoleInput);
            this.Controls.Add(this.blackPlaybackSleepTime);
            this.Controls.Add(this.whitePlaybackSleepTime);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GUI";
            this.Text = "Chess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
            this.Load += new System.EventHandler(this.GUI_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.onPaint);
            this.graphicsTab.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blackAiDiffTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.whiteAiDiffTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPlaybackSleepTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitePlaybackSleepTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox consoleInput;
        private System.Windows.Forms.TextBox consoleOutput;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox whiteAgentDropDown;
        private System.Windows.Forms.TextBox chessTextBox;
        private System.Windows.Forms.TextBox playerTurn;
        private System.Windows.Forms.TextBox turnText;
        private System.Windows.Forms.ComboBox blackAgentDropDown;
        private System.Windows.Forms.TabControl graphicsTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox drawingArea;
        private System.Windows.Forms.TextBox AiScoreTextBox;
        private System.Windows.Forms.TextBox scoreBoard;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label blackPlayerLabel;
        private System.Windows.Forms.Label whitePlayerLabel;
        private System.Windows.Forms.CheckBox playbackCheckBox;
        private System.Windows.Forms.TrackBar blackAiDiffTrackBar;
        private System.Windows.Forms.Label blackAiDiffLabel;
        private System.Windows.Forms.Label whiteAiDiffLabel;
        private System.Windows.Forms.TrackBar whiteAiDiffTrackBar;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.TextBox blackPlaybackFilenameTextBox;
        private System.Windows.Forms.TextBox whitePlaybackFilenameTextBox;
        private System.Windows.Forms.TrackBar blackPlaybackSleepTime;
        private System.Windows.Forms.TrackBar whitePlaybackSleepTime;
        private System.Windows.Forms.Label blackSleepTimeLabel;
        private System.Windows.Forms.Label whiteSleepTimeLabel;
    }
}

