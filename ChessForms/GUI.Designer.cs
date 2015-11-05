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
            this.SuspendLayout();
            // 
            // consoleInput
            // 
            this.consoleInput.Location = new System.Drawing.Point(24, 339);
            this.consoleInput.Name = "consoleInput";
            this.consoleInput.Size = new System.Drawing.Size(363, 20);
            this.consoleInput.TabIndex = 0;
            // 
            // consoleOutput
            // 
            this.consoleOutput.Enabled = false;
            this.consoleOutput.Location = new System.Drawing.Point(24, 12);
            this.consoleOutput.Multiline = true;
            this.consoleOutput.Name = "consoleOutput";
            this.consoleOutput.Size = new System.Drawing.Size(363, 321);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 380);
            this.Controls.Add(this.whiteAgentDropDown);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.consoleOutput);
            this.Controls.Add(this.consoleInput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox consoleInput;
        private System.Windows.Forms.TextBox consoleOutput;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ComboBox whiteAgentDropDown;
    }
}

