namespace eidca.sample
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            token_challenge = new TextBox();
            lblToken = new Label();
            txtChallenge = new TextBox();
            button1 = new Button();
            signature = new TextBox();
            button2 = new Button();
            lblFullName = new Label();
            picAvatar = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            SuspendLayout();
            // 
            // token_challenge
            // 
            token_challenge.Location = new Point(12, 32);
            token_challenge.Multiline = true;
            token_challenge.Name = "token_challenge";
            token_challenge.Size = new Size(483, 74);
            token_challenge.TabIndex = 0;
            token_challenge.Text = resources.GetString("token_challenge.Text");
            // 
            // lblToken
            // 
            lblToken.AutoSize = true;
            lblToken.Location = new Point(13, 13);
            lblToken.Name = "lblToken";
            lblToken.Size = new Size(125, 15);
            lblToken.TabIndex = 1;
            lblToken.Text = "token_challenge (JWT)";
            // 
            // txtChallenge
            // 
            txtChallenge.Location = new Point(230, 126);
            txtChallenge.Name = "txtChallenge";
            txtChallenge.Size = new Size(265, 23);
            txtChallenge.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(13, 126);
            button1.Name = "button1";
            button1.Size = new Size(211, 23);
            button1.TabIndex = 3;
            button1.Text = "2. Giải mã lấy Challenge";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // signature
            // 
            signature.Location = new Point(231, 169);
            signature.Multiline = true;
            signature.Name = "signature";
            signature.Size = new Size(264, 118);
            signature.TabIndex = 4;
            signature.TextChanged += signature_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(13, 168);
            button2.Name = "button2";
            button2.Size = new Size(211, 23);
            button2.TabIndex = 5;
            button2.Text = "3. Gửi căn cước Ký chuỗi challenge";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFullName.Location = new Point(550, 76);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(68, 15);
            lblFullName.TabIndex = 6;
            lblFullName.Text = "HỌ VÀ TÊN";
            // 
            // picAvatar
            // 
            picAvatar.Location = new Point(550, 104);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(159, 183);
            picAvatar.TabIndex = 7;
            picAvatar.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 307);
            label1.Name = "label1";
            label1.Size = new Size(408, 15);
            label1.TabIndex = 8;
            label1.Text = "Bước 1: Đọc căn cước - Bước 2: Giải mã lấy challenge - Bước 3:  Ký challenge";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(550, 51);
            label2.Name = "label2";
            label2.Size = new Size(111, 15);
            label2.TabIndex = 9;
            label2.Text = "1. Đọc thẻ căn cước";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(756, 335);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(picAvatar);
            Controls.Add(lblFullName);
            Controls.Add(button2);
            Controls.Add(signature);
            Controls.Add(button1);
            Controls.Add(txtChallenge);
            Controls.Add(lblToken);
            Controls.Add(token_challenge);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox token_challenge;
        private Label lblToken;
        private TextBox txtChallenge;
        private Button button1;
        private TextBox signature;
        private Button button2;
        private Label lblFullName;
        private PictureBox picAvatar;
        private Label label1;
        private Label label2;
    }
}
