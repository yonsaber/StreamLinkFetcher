namespace StreamLinkFetcher
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txt_TwitchLink = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_GetStreamUrl = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.txt_StreamVideoLink = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_CopyToClipboard = new System.Windows.Forms.Button();
            this.lst_Diag = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_RingBuffer = new System.Windows.Forms.ComboBox();
            this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_TwitchLink
            // 
            this.txt_TwitchLink.Location = new System.Drawing.Point(15, 25);
            this.txt_TwitchLink.Name = "txt_TwitchLink";
            this.txt_TwitchLink.Size = new System.Drawing.Size(313, 20);
            this.txt_TwitchLink.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stream URL / DJ Twitch Name";
            // 
            // btn_GetStreamUrl
            // 
            this.btn_GetStreamUrl.Location = new System.Drawing.Point(15, 92);
            this.btn_GetStreamUrl.Name = "btn_GetStreamUrl";
            this.btn_GetStreamUrl.Size = new System.Drawing.Size(152, 23);
            this.btn_GetStreamUrl.TabIndex = 2;
            this.btn_GetStreamUrl.Text = "Get Stream URL";
            this.btn_GetStreamUrl.UseVisualStyleBackColor = true;
            this.btn_GetStreamUrl.Click += new System.EventHandler(this.btn_GetStreamUrl_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(173, 92);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(155, 23);
            this.btn_Clear.TabIndex = 3;
            this.btn_Clear.Text = "Clear Input";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // txt_StreamVideoLink
            // 
            this.txt_StreamVideoLink.Enabled = false;
            this.txt_StreamVideoLink.Location = new System.Drawing.Point(15, 169);
            this.txt_StreamVideoLink.Name = "txt_StreamVideoLink";
            this.txt_StreamVideoLink.Size = new System.Drawing.Size(313, 20);
            this.txt_StreamVideoLink.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Stream Video Link";
            // 
            // btn_CopyToClipboard
            // 
            this.btn_CopyToClipboard.Location = new System.Drawing.Point(15, 195);
            this.btn_CopyToClipboard.Name = "btn_CopyToClipboard";
            this.btn_CopyToClipboard.Size = new System.Drawing.Size(313, 23);
            this.btn_CopyToClipboard.TabIndex = 6;
            this.btn_CopyToClipboard.Text = "Copy To Clipboard";
            this.btn_CopyToClipboard.UseVisualStyleBackColor = true;
            this.btn_CopyToClipboard.Click += new System.EventHandler(this.btn_CopyToClipboard_Click);
            // 
            // lst_Diag
            // 
            this.lst_Diag.Enabled = false;
            this.lst_Diag.Location = new System.Drawing.Point(15, 239);
            this.lst_Diag.Multiline = true;
            this.lst_Diag.Name = "lst_Diag";
            this.lst_Diag.Size = new System.Drawing.Size(313, 258);
            this.lst_Diag.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "RingBuffer Size";
            // 
            // cmb_RingBuffer
            // 
            this.cmb_RingBuffer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_RingBuffer.FormattingEnabled = true;
            this.cmb_RingBuffer.Location = new System.Drawing.Point(15, 65);
            this.cmb_RingBuffer.Name = "cmb_RingBuffer";
            this.cmb_RingBuffer.Size = new System.Drawing.Size(313, 21);
            this.cmb_RingBuffer.TabIndex = 9;
            // 
            // programBindingSource
            // 
            this.programBindingSource.DataSource = typeof(StreamLinkFetcher.Program);
            // 
            // Form1
            // 
            this.AcceptButton = this.btn_GetStreamUrl;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 509);
            this.Controls.Add(this.cmb_RingBuffer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lst_Diag);
            this.Controls.Add(this.btn_CopyToClipboard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_StreamVideoLink);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_GetStreamUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_TwitchLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Stream Extractor";
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_TwitchLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_GetStreamUrl;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.TextBox txt_StreamVideoLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_CopyToClipboard;
        private System.Windows.Forms.TextBox lst_Diag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_RingBuffer;
        private System.Windows.Forms.BindingSource programBindingSource;
    }
}

