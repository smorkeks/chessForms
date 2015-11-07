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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.drawingArea = new System.Windows.Forms.PictureBox();
            this.AiScoreTextBox = new System.Windows.Forms.TextBox();
            this.graphicsTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // consoleInput
            // 
            this.consoleInput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleInput.Location = new System.Drawing.Point(24, 552);
            this.consoleInput.Name = "consoleInput";
            this.consoleInput.Size = new System.Drawing.Size(363, 20);
            this.consoleInput.TabIndex = 0;
            // 
            // consoleOutput
            // 
            this.consoleOutput.Enabled = false;
            this.consoleOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleOutput.Location = new System.Drawing.Point(24, 12);
            this.consoleOutput.Multiline = true;
            this.consoleOutput.Name = "consoleOutput";
            this.consoleOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleOutput.Size = new System.Drawing.Size(363, 534);
            this.consoleOutput.TabIndex = 1;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(406, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(175, 23);
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
            "AI"});
            this.whiteAgentDropDown.Location = new System.Drawing.Point(406, 41);
            this.whiteAgentDropDown.Name = "whiteAgentDropDown";
            this.whiteAgentDropDown.Size = new System.Drawing.Size(175, 21);
            this.whiteAgentDropDown.TabIndex = 3;
            // 
            // chessTextBox
            // 
            this.chessTextBox.Enabled = false;
            this.chessTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chessTextBox.Location = new System.Drawing.Point(6, 6);
            this.chessTextBox.Multiline = true;
            this.chessTextBox.Name = "chessTextBox";
            this.chessTextBox.Size = new System.Drawing.Size(520, 520);
            this.chessTextBox.TabIndex = 4;
            // 
            // playerTurn
            // 
            this.playerTurn.Enabled = false;
            this.playerTurn.Location = new System.Drawing.Point(406, 285);
            this.playerTurn.Name = "playerTurn";
            this.playerTurn.Size = new System.Drawing.Size(175, 20);
            this.playerTurn.TabIndex = 5;
            this.playerTurn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // turnText
            // 
            this.turnText.Enabled = false;
            this.turnText.Location = new System.Drawing.Point(406, 311);
            this.turnText.Name = "turnText";
            this.turnText.Size = new System.Drawing.Size(175, 20);
            this.turnText.TabIndex = 6;
            this.turnText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // blackAgentDropDown
            // 
            this.blackAgentDropDown.FormattingEnabled = true;
            this.blackAgentDropDown.Items.AddRange(new object[] {
            "Terminal Agent",
            "Graphics Agent",
            "AI"});
            this.blackAgentDropDown.Location = new System.Drawing.Point(406, 68);
            this.blackAgentDropDown.Name = "blackAgentDropDown";
            this.blackAgentDropDown.Size = new System.Drawing.Size(175, 21);
            this.blackAgentDropDown.TabIndex = 7;
            // 
            // graphicsTab
            // 
            this.graphicsTab.Controls.Add(this.tabPage1);
            this.graphicsTab.Controls.Add(this.tabPage2);
            this.graphicsTab.Location = new System.Drawing.Point(598, 12);
            this.graphicsTab.Name = "graphicsTab";
            this.graphicsTab.SelectedIndex = 0;
            this.graphicsTab.Size = new System.Drawing.Size(539, 560);
            this.graphicsTab.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chessTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(531, 534);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text GUI";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.drawingArea);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(531, 534);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graphics GUI";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // drawingArea
            // 
            this.drawingArea.Location = new System.Drawing.Point(7, 7);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(520, 520);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.TabStop = false;
            this.drawingArea.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onDrawingClick);
            // 
            // AiScoreTextBox
            // 
            this.AiScoreTextBox.Location = new System.Drawing.Point(406, 170);
            this.AiScoreTextBox.Name = "AiScoreTextBox";
            this.AiScoreTextBox.Size = new System.Drawing.Size(175, 20);
            this.AiScoreTextBox.TabIndex = 9;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 594);
            this.Controls.Add(this.AiScoreTextBox);
            this.Controls.Add(this.graphicsTab);
            this.Controls.Add(this.blackAgentDropDown);
            this.Controls.Add(this.turnText);
            this.Controls.Add(this.playerTurn);
            this.Controls.Add(this.whiteAgentDropDown);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.consoleOutput);
            this.Controls.Add(this.consoleInput);
            this.Name = "GUI";
            this.Text = "Chess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
            this.Load += new System.EventHandler(this.GUI_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.onPaint);
            this.graphicsTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawingArea)).EndInit();
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
    }
}

