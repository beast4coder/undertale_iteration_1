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
            lblPlayerHealth = new Label();
            ((System.ComponentModel.ISupportInitialize)pbArena).BeginInit();
            SuspendLayout();
            // 
            // pbArena
            // 
            pbArena.BackColor = Color.Transparent;
            pbArena.Location = new Point(-1, -1);
            pbArena.Name = "pbArena";
            pbArena.Size = new Size(964, 543);
            pbArena.TabIndex = 0;
            pbArena.TabStop = false;
            // 
            // tmrGameTimer
            // 
            tmrGameTimer.Enabled = true;
            tmrGameTimer.Interval = 10;
            tmrGameTimer.Tick += tmrGameTimer_Tick;
            // 
            // lblPlayerHealth
            // 
            lblPlayerHealth.AutoSize = true;
            lblPlayerHealth.ForeColor = SystemColors.Control;
            lblPlayerHealth.Location = new Point(411, 363);
            lblPlayerHealth.Name = "lblPlayerHealth";
            lblPlayerHealth.Size = new Size(36, 15);
            lblPlayerHealth.TabIndex = 1;
            lblPlayerHealth.Text = "20/20";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Desktop;
            ClientSize = new Size(960, 540);
            Controls.Add(lblPlayerHealth);
            Controls.Add(pbArena);
            MinimumSize = new Size(976, 579);
            Name = "GameForm";
            Text = "undertale";
            Load += GameForm_Load;
            Paint += Update_Sprites;
            KeyDown += GameForm_KeyDown;
            KeyUp += GameForm_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pbArena).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pbArena;
        private System.Windows.Forms.Timer tmrGameTimer;
        private Label lblPlayerHealth;
    }
}