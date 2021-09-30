
namespace View
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.worldField = new System.Windows.Forms.DataGridView();
            this.startButton = new System.Windows.Forms.Button();
            this.step = new System.Windows.Forms.Label();
            this.next = new System.Windows.Forms.Button();
            this.prev = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.timeoutUpDown = new System.Windows.Forms.NumericUpDown();
            this.toFirstStepButton = new System.Windows.Forms.Button();
            this.toLastStepButton = new System.Windows.Forms.Button();
            this.bestOfButton = new System.Windows.Forms.Button();
            this.countOfGenerationsUpDown = new System.Windows.Forms.NumericUpDown();
            this.countOfSnakesLable = new System.Windows.Forms.Label();
            this.progressSimulations = new View.ProgressWithText();
            ((System.ComponentModel.ISupportInitialize)(this.worldField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countOfGenerationsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // worldField
            // 
            this.worldField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.worldField.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.worldField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.worldField.Location = new System.Drawing.Point(12, 68);
            this.worldField.Name = "worldField";
            this.worldField.RowHeadersWidth = 51;
            this.worldField.RowTemplate.Height = 29;
            this.worldField.Size = new System.Drawing.Size(951, 371);
            this.worldField.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(156, 40);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start new simulation";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // step
            // 
            this.step.AutoSize = true;
            this.step.Location = new System.Drawing.Point(392, 23);
            this.step.Name = "step";
            this.step.Size = new System.Drawing.Size(17, 20);
            this.step.TabIndex = 2;
            this.step.Text = "0";
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(174, 12);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(94, 40);
            this.next.TabIndex = 3;
            this.next.Text = "Next step";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // prev
            // 
            this.prev.Location = new System.Drawing.Point(274, 13);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(112, 39);
            this.prev.TabIndex = 4;
            this.prev.Text = "Previous step";
            this.prev.UseVisualStyleBackColor = true;
            this.prev.Click += new System.EventHandler(this.prev_Click);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.continueButton.Location = new System.Drawing.Point(688, 15);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(173, 37);
            this.continueButton.TabIndex = 5;
            this.continueButton.Text = "Continue with timeout";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // timeoutUpDown
            // 
            this.timeoutUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeoutUpDown.Location = new System.Drawing.Point(867, 21);
            this.timeoutUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.timeoutUpDown.Name = "timeoutUpDown";
            this.timeoutUpDown.Size = new System.Drawing.Size(96, 27);
            this.timeoutUpDown.TabIndex = 6;
            this.timeoutUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // toFirstStepButton
            // 
            this.toFirstStepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toFirstStepButton.Location = new System.Drawing.Point(588, 15);
            this.toFirstStepButton.Name = "toFirstStepButton";
            this.toFirstStepButton.Size = new System.Drawing.Size(94, 37);
            this.toFirstStepButton.TabIndex = 7;
            this.toFirstStepButton.Text = "First step";
            this.toFirstStepButton.UseVisualStyleBackColor = true;
            this.toFirstStepButton.Click += new System.EventHandler(this.toFirstStepButton_Click);
            // 
            // toLastStepButton
            // 
            this.toLastStepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toLastStepButton.Location = new System.Drawing.Point(488, 15);
            this.toLastStepButton.Name = "toLastStepButton";
            this.toLastStepButton.Size = new System.Drawing.Size(94, 37);
            this.toLastStepButton.TabIndex = 8;
            this.toLastStepButton.Text = "Last step";
            this.toLastStepButton.UseVisualStyleBackColor = true;
            this.toLastStepButton.Click += new System.EventHandler(this.toLastStepButton_Click);
            // 
            // bestOfButton
            // 
            this.bestOfButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bestOfButton.Location = new System.Drawing.Point(12, 445);
            this.bestOfButton.Name = "bestOfButton";
            this.bestOfButton.Size = new System.Drawing.Size(102, 40);
            this.bestOfButton.TabIndex = 9;
            this.bestOfButton.Text = "Best of";
            this.bestOfButton.UseVisualStyleBackColor = true;
            this.bestOfButton.Click += new System.EventHandler(this.bestOfButton_Click);
            // 
            // countOfGenerationsUpDown
            // 
            this.countOfGenerationsUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countOfGenerationsUpDown.Location = new System.Drawing.Point(120, 452);
            this.countOfGenerationsUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.countOfGenerationsUpDown.Name = "countOfGenerationsUpDown";
            this.countOfGenerationsUpDown.Size = new System.Drawing.Size(121, 27);
            this.countOfGenerationsUpDown.TabIndex = 10;
            // 
            // countOfSnakesLable
            // 
            this.countOfSnakesLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countOfSnakesLable.AutoSize = true;
            this.countOfSnakesLable.Location = new System.Drawing.Point(251, 455);
            this.countOfSnakesLable.Name = "countOfSnakesLable";
            this.countOfSnakesLable.Size = new System.Drawing.Size(0, 20);
            this.countOfSnakesLable.TabIndex = 11;
            // 
            // progressSimulations
            // 
            this.progressSimulations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressSimulations.BackColor = System.Drawing.SystemColors.Control;
            this.progressSimulations.BorderColor = System.Drawing.Color.Black;
            this.progressSimulations.BorderWidth = 2;
            this.progressSimulations.ForeColor = System.Drawing.Color.Black;
            this.progressSimulations.Location = new System.Drawing.Point(488, 455);
            this.progressSimulations.MaxValue = 100;
            this.progressSimulations.MinValue = 0;
            this.progressSimulations.Name = "progressSimulations";
            this.progressSimulations.ProgressColor = System.Drawing.Color.Green;
            this.progressSimulations.ProgressTextType = View.ProgressWithText.FsProgressTextType.AsIs;
            this.progressSimulations.ShowProgressText = true;
            this.progressSimulations.Size = new System.Drawing.Size(475, 24);
            this.progressSimulations.TabIndex = 12;
            this.progressSimulations.Text = "progressWithText1";
            this.progressSimulations.Value = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 491);
            this.Controls.Add(this.progressSimulations);
            this.Controls.Add(this.countOfSnakesLable);
            this.Controls.Add(this.countOfGenerationsUpDown);
            this.Controls.Add(this.bestOfButton);
            this.Controls.Add(this.toLastStepButton);
            this.Controls.Add(this.toFirstStepButton);
            this.Controls.Add(this.timeoutUpDown);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.prev);
            this.Controls.Add(this.next);
            this.Controls.Add(this.step);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.worldField);
            this.Name = "MainForm";
            this.Text = "Worms";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.worldField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countOfGenerationsUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView worldField;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label step;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button prev;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.NumericUpDown timeoutUpDown;
        private System.Windows.Forms.Button toFirstStepButton;
        private System.Windows.Forms.Button toLastStepButton;
        private System.Windows.Forms.Button bestOfButton;
        private System.Windows.Forms.NumericUpDown countOfGenerationsUpDown;
        private System.Windows.Forms.Label countOfSnakesLable;
        private ProgressWithText progressSimulations;
    }
}

