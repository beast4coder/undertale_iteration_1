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
            pbArena = new PictureBox();
            tmrGameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pbArena).BeginInit();
            SuspendLayout();
            // 
            // pbArena
            // 
            pbArena.BackColor = SystemColors.ButtonHighlight;
            pbArena.Location = new Point(365, 173);
            pbArena.Name = "pbArena";
            pbArena.Size = new Size(250, 250);
            pbArena.TabIndex = 0;
            pbArena.TabStop = false;
            pbArena.Paint += Update_Sprites;
            // 
            // tmrGameTimer
            // 
            tmrGameTimer.Enabled = true;
            tmrGameTimer.Tick += tmrGameTimer_Tick;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Desktop;
            ClientSize = new Size(960, 540);
            Controls.Add(pbArena);
            MinimumSize = new Size(976, 579);
            Name = "GameForm";
            Text = "undertale";
            Load += GameForm_Load;
            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pbArena).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbArena;
        private System.Windows.Forms.Timer tmrGameTimer;
    }
}