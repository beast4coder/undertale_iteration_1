namespace undertale_iteration_1
{
    partial class GameForm
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
            components = new System.ComponentModel.Container();
            pbBackground = new PictureBox();
            tmrGameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pbBackground).BeginInit();
            SuspendLayout();
            // 
            // pbBackground
            // 
            pbBackground.BackColor = Color.Transparent;
            pbBackground.Location = new Point(-1, -1);
            pbBackground.Name = "pbBackground";
            pbBackground.Size = new Size(964, 543);
            pbBackground.TabIndex = 0;
            pbBackground.TabStop = false;
            // 
            // tmrGameTimer
            // 
            tmrGameTimer.Enabled = true;
            tmrGameTimer.Interval = 20;
            tmrGameTimer.Tick += tmrGameTimer_Tick;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = false;
            BackColor = Color.Black;
            ClientSize = new Size(640, 480);
            Controls.Add(pbBackground);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimumSize = new Size(640, 480);
            Name = "GameForm";
            Text = "undertale";
            Load += GameForm_Load;
            Paint += Update_Sprites;
            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pbBackground).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbBackground;
        private System.Windows.Forms.Timer tmrGameTimer;
    }
}