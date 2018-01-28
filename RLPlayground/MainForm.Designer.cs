namespace RLPlayground
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
            this.gridView = new System.Windows.Forms.DataGridView();
            this.stateTypeComboBox = new System.Windows.Forms.ComboBox();
            this.runButton = new System.Windows.Forms.Button();
            this.iterationCountLabel = new System.Windows.Forms.Label();
            this.iterationCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.singleStepButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.iterationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView
            // 
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Location = new System.Drawing.Point(31, 66);
            this.gridView.Name = "gridView";
            this.gridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gridView.Size = new System.Drawing.Size(417, 309);
            this.gridView.TabIndex = 0;
            // 
            // stateTypeComboBox
            // 
            this.stateTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stateTypeComboBox.FormattingEnabled = true;
            this.stateTypeComboBox.ItemHeight = 18;
            this.stateTypeComboBox.Location = new System.Drawing.Point(31, 25);
            this.stateTypeComboBox.Name = "stateTypeComboBox";
            this.stateTypeComboBox.Size = new System.Drawing.Size(168, 26);
            this.stateTypeComboBox.TabIndex = 1;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(196, 389);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // iterationCountLabel
            // 
            this.iterationCountLabel.AutoSize = true;
            this.iterationCountLabel.Location = new System.Drawing.Point(28, 394);
            this.iterationCountLabel.Name = "iterationCountLabel";
            this.iterationCountLabel.Size = new System.Drawing.Size(78, 13);
            this.iterationCountLabel.TabIndex = 3;
            this.iterationCountLabel.Text = "Iteration count:";
            // 
            // iterationCountNumericUpDown
            // 
            this.iterationCountNumericUpDown.Location = new System.Drawing.Point(112, 392);
            this.iterationCountNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.iterationCountNumericUpDown.Name = "iterationCountNumericUpDown";
            this.iterationCountNumericUpDown.Size = new System.Drawing.Size(78, 20);
            this.iterationCountNumericUpDown.TabIndex = 5;
            // 
            // singleStepButton
            // 
            this.singleStepButton.Location = new System.Drawing.Point(277, 389);
            this.singleStepButton.Name = "singleStepButton";
            this.singleStepButton.Size = new System.Drawing.Size(75, 23);
            this.singleStepButton.TabIndex = 6;
            this.singleStepButton.Text = "Single step";
            this.singleStepButton.UseVisualStyleBackColor = true;
            this.singleStepButton.Click += new System.EventHandler(this.singleStepButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(196, 419);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // iterationLabel
            // 
            this.iterationLabel.AutoSize = true;
            this.iterationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.iterationLabel.Location = new System.Drawing.Point(28, 424);
            this.iterationLabel.Name = "iterationLabel";
            this.iterationLabel.Size = new System.Drawing.Size(102, 13);
            this.iterationLabel.TabIndex = 8;
            this.iterationLabel.Text = "Current iteration:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 468);
            this.Controls.Add(this.iterationLabel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.singleStepButton);
            this.Controls.Add(this.iterationCountNumericUpDown);
            this.Controls.Add(this.iterationCountLabel);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.stateTypeComboBox);
            this.Controls.Add(this.gridView);
            this.Name = "MainForm";
            this.Text = "RL Playground";
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iterationCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridView;
        private System.Windows.Forms.ComboBox stateTypeComboBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label iterationCountLabel;
        private System.Windows.Forms.NumericUpDown iterationCountNumericUpDown;
        private System.Windows.Forms.Button singleStepButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label iterationLabel;
    }
}

