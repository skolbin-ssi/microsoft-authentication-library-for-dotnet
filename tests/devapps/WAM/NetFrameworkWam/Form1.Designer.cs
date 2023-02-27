﻿namespace NetDesktopWinForms
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
            this.resultTbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.authorityCbx = new System.Windows.Forms.ComboBox();
            this.clientIdCbx = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.loginHintTxt = new System.Windows.Forms.TextBox();
            this.promptCbx = new System.Windows.Forms.ComboBox();
            this.atsBtn = new System.Windows.Forms.Button();
            this.atiBtn = new System.Windows.Forms.Button();
            this.atsAtiBtn = new System.Windows.Forms.Button();
            this.accBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.btnClearCache = new System.Windows.Forms.Button();
            this.cbxScopes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxAccount = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbxMsaPt = new System.Windows.Forms.CheckBox();
            this.btnExpire = new System.Windows.Forms.Button();
            this.btnRemoveAccount = new System.Windows.Forms.Button();
            this.cbxBackgroundThread = new System.Windows.Forms.CheckBox();
            this.cbxListOsAccounts = new System.Windows.Forms.CheckBox();
            this.cbxUseWam = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudAutocancelSeconds = new System.Windows.Forms.NumericUpDown();
            this.chxEnableRuntimeLogs = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudAutocancelSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // resultTbx
            // 
            this.resultTbx.Location = new System.Drawing.Point(15, 342);
            this.resultTbx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resultTbx.Multiline = true;
            this.resultTbx.Name = "resultTbx";
            this.resultTbx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTbx.Size = new System.Drawing.Size(910, 584);
            this.resultTbx.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Authority";
            // 
            // authorityCbx
            // 
            this.authorityCbx.FormattingEnabled = true;
            this.authorityCbx.Items.AddRange(new object[] {
            "https://login.microsoftonline.com/common",
            "https://login.microsoftonline.com/organizations",
            "https://login.microsoftonline.com/consumers",
            "https://login.microsoftonline.com/49f548d0-12b7-4169-a390-bb5304d24462",
            "https://login.microsoftonline.com/f645ad92-e38d-4d1a-b510-d1b09a74a8ca",
            "https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47",
            "https://login.microsoftonline.com/f8cdef31-a31e-4b4a-93e4-5f571e91255a",
            "https://login.windows-ppe.net/organizations",
            "https://login.windows-ppe.net/72f988bf-86f1-41af-91ab-2d7cd011db47",
            "https://login.partner.microsoftonline.cn/organizations",
            "https://login.microsoftonline.us/organizations"});
            this.authorityCbx.Location = new System.Drawing.Point(110, 65);
            this.authorityCbx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.authorityCbx.Name = "authorityCbx";
            this.authorityCbx.Size = new System.Drawing.Size(625, 28);
            this.authorityCbx.TabIndex = 3;
            this.authorityCbx.Text = "https://login.microsoftonline.com/common";
            // 
            // clientIdCbx
            // 
            this.clientIdCbx.FormattingEnabled = true;
            this.clientIdCbx.Location = new System.Drawing.Point(110, 23);
            this.clientIdCbx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clientIdCbx.Name = "clientIdCbx";
            this.clientIdCbx.Size = new System.Drawing.Size(625, 28);
            this.clientIdCbx.TabIndex = 4;
            this.clientIdCbx.Text = "1d18b3b0-251b-4714-a02a-9956cec86c2d";
            this.clientIdCbx.SelectedIndexChanged += new System.EventHandler(this.clientIdCbx_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "ClientId";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 151);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Login Hint ";
            // 
            // loginHintTxt
            // 
            this.loginHintTxt.Location = new System.Drawing.Point(110, 148);
            this.loginHintTxt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loginHintTxt.Name = "loginHintTxt";
            this.loginHintTxt.Size = new System.Drawing.Size(328, 26);
            this.loginHintTxt.TabIndex = 8;
            // 
            // promptCbx
            // 
            this.promptCbx.FormattingEnabled = true;
            this.promptCbx.Items.AddRange(new object[] {
            "",
            "select_account",
            "force_login",
            "no_prompt",
            "consent",
            "never"});
            this.promptCbx.Location = new System.Drawing.Point(746, 203);
            this.promptCbx.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.promptCbx.Name = "promptCbx";
            this.promptCbx.Size = new System.Drawing.Size(180, 28);
            this.promptCbx.TabIndex = 10;
            // 
            // atsBtn
            // 
            this.atsBtn.Location = new System.Drawing.Point(18, 297);
            this.atsBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.atsBtn.Name = "atsBtn";
            this.atsBtn.Size = new System.Drawing.Size(112, 35);
            this.atsBtn.TabIndex = 11;
            this.atsBtn.Text = "ATS";
            this.atsBtn.UseVisualStyleBackColor = true;
            this.atsBtn.Click += new System.EventHandler(this.atsBtn_Click);
            // 
            // atiBtn
            // 
            this.atiBtn.Location = new System.Drawing.Point(140, 297);
            this.atiBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.atiBtn.Name = "atiBtn";
            this.atiBtn.Size = new System.Drawing.Size(112, 35);
            this.atiBtn.TabIndex = 12;
            this.atiBtn.Text = "ATI";
            this.atiBtn.UseVisualStyleBackColor = true;
            this.atiBtn.Click += new System.EventHandler(this.atiBtn_Click);
            // 
            // atsAtiBtn
            // 
            this.atsAtiBtn.Location = new System.Drawing.Point(261, 297);
            this.atsAtiBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.atsAtiBtn.Name = "atsAtiBtn";
            this.atsAtiBtn.Size = new System.Drawing.Size(112, 35);
            this.atsAtiBtn.TabIndex = 13;
            this.atsAtiBtn.Text = "ATS + ATI";
            this.atsAtiBtn.UseVisualStyleBackColor = true;
            this.atsAtiBtn.Click += new System.EventHandler(this.atsAtiBtn_Click);
            // 
            // accBtn
            // 
            this.accBtn.Location = new System.Drawing.Point(472, 297);
            this.accBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.accBtn.Name = "accBtn";
            this.accBtn.Size = new System.Drawing.Size(120, 35);
            this.accBtn.TabIndex = 15;
            this.accBtn.Text = "Get Accounts";
            this.accBtn.UseVisualStyleBackColor = true;
            this.accBtn.Click += new System.EventHandler(this.getAccountsBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(825, 937);
            this.clearBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(102, 35);
            this.clearBtn.TabIndex = 16;
            this.clearBtn.Text = "Clear Log";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // btnClearCache
            // 
            this.btnClearCache.Location = new System.Drawing.Point(765, 297);
            this.btnClearCache.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClearCache.Name = "btnClearCache";
            this.btnClearCache.Size = new System.Drawing.Size(162, 35);
            this.btnClearCache.TabIndex = 17;
            this.btnClearCache.Text = "Clear MSAL Cache";
            this.btnClearCache.UseVisualStyleBackColor = true;
            this.btnClearCache.Click += new System.EventHandler(this.btnClearCache_Click);
            // 
            // cbxScopes
            // 
            this.cbxScopes.FormattingEnabled = true;
            this.cbxScopes.Items.AddRange(new object[] {
            "User.Read",
            "User.Read User.Read.All",
            "https://management.core.windows.net//.default",
            "https://graph.microsoft.com/.default",
            "499b84ac-1321-427f-aa17-267ca6975798/vso.code_full",
            "api://51eb3dd6-d8b5-46f3-991d-b1d4870de7de/myaccess",
            "https://management.core.chinacloudapi.cn//.default",
            "https://management.core.usgovcloudapi.net//.default"});
            this.cbxScopes.Location = new System.Drawing.Point(110, 106);
            this.cbxScopes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxScopes.Name = "cbxScopes";
            this.cbxScopes.Size = new System.Drawing.Size(816, 28);
            this.cbxScopes.TabIndex = 18;
            this.cbxScopes.Text = "User.Read";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Scopes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(676, 208);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Prompt";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(448, 151);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Or Account";
            // 
            // cbxAccount
            // 
            this.cbxAccount.FormattingEnabled = true;
            this.cbxAccount.Location = new System.Drawing.Point(549, 146);
            this.cbxAccount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxAccount.Name = "cbxAccount";
            this.cbxAccount.Size = new System.Drawing.Size(376, 28);
            this.cbxAccount.TabIndex = 22;
            // 
            // cbxMsaPt
            // 
            this.cbxMsaPt.AutoSize = true;
            this.cbxMsaPt.Location = new System.Drawing.Point(273, 206);
            this.cbxMsaPt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxMsaPt.Name = "cbxMsaPt";
            this.cbxMsaPt.Size = new System.Drawing.Size(165, 24);
            this.cbxMsaPt.TabIndex = 23;
            this.cbxMsaPt.Text = "MSA-Passthrough";
            this.cbxMsaPt.UseVisualStyleBackColor = true;
            // 
            // btnExpire
            // 
            this.btnExpire.Location = new System.Drawing.Point(765, 252);
            this.btnExpire.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExpire.Name = "btnExpire";
            this.btnExpire.Size = new System.Drawing.Size(162, 35);
            this.btnExpire.TabIndex = 24;
            this.btnExpire.Text = "Expire ATs";
            this.btnExpire.UseVisualStyleBackColor = true;
            this.btnExpire.Click += new System.EventHandler(this.btnExpire_Click);
            // 
            // btnRemoveAccount
            // 
            this.btnRemoveAccount.Location = new System.Drawing.Point(603, 297);
            this.btnRemoveAccount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRemoveAccount.Name = "btnRemoveAccount";
            this.btnRemoveAccount.Size = new System.Drawing.Size(134, 35);
            this.btnRemoveAccount.TabIndex = 25;
            this.btnRemoveAccount.Text = "Remove Acc";
            this.btnRemoveAccount.UseVisualStyleBackColor = true;
            this.btnRemoveAccount.Click += new System.EventHandler(this.btnRemoveAcc_Click);
            // 
            // cbxBackgroundThread
            // 
            this.cbxBackgroundThread.AutoSize = true;
            this.cbxBackgroundThread.Location = new System.Drawing.Point(448, 206);
            this.cbxBackgroundThread.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxBackgroundThread.Name = "cbxBackgroundThread";
            this.cbxBackgroundThread.Size = new System.Drawing.Size(214, 24);
            this.cbxBackgroundThread.TabIndex = 26;
            this.cbxBackgroundThread.Text = "Force background thread";
            this.cbxBackgroundThread.UseVisualStyleBackColor = true;
            // 
            // cbxListOsAccounts
            // 
            this.cbxListOsAccounts.AutoSize = true;
            this.cbxListOsAccounts.Location = new System.Drawing.Point(273, 238);
            this.cbxListOsAccounts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxListOsAccounts.Name = "cbxListOsAccounts";
            this.cbxListOsAccounts.Size = new System.Drawing.Size(156, 24);
            this.cbxListOsAccounts.TabIndex = 27;
            this.cbxListOsAccounts.Text = "List OS accounts";
            this.cbxListOsAccounts.UseVisualStyleBackColor = true;
            // 
            // cbxUseWam
            // 
            this.cbxUseWam.FormattingEnabled = true;
            this.cbxUseWam.Location = new System.Drawing.Point(18, 200);
            this.cbxUseWam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxUseWam.Name = "cbxUseWam";
            this.cbxUseWam.Size = new System.Drawing.Size(232, 28);
            this.cbxUseWam.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 268);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 20);
            this.label7.TabIndex = 29;
            this.label7.Text = "Autocancel seconds";
            // 
            // nudAutocancelSeconds
            // 
            this.nudAutocancelSeconds.Location = new System.Drawing.Point(183, 265);
            this.nudAutocancelSeconds.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudAutocancelSeconds.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudAutocancelSeconds.Name = "nudAutocancelSeconds";
            this.nudAutocancelSeconds.Size = new System.Drawing.Size(69, 26);
            this.nudAutocancelSeconds.TabIndex = 30;
            // 
            // chxEnableRuntimeLogs
            // 
            this.chxEnableRuntimeLogs.AutoSize = true;
            this.chxEnableRuntimeLogs.Location = new System.Drawing.Point(448, 238);
            this.chxEnableRuntimeLogs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chxEnableRuntimeLogs.Name = "chxEnableRuntimeLogs";
            this.chxEnableRuntimeLogs.Size = new System.Drawing.Size(210, 24);
            this.chxEnableRuntimeLogs.TabIndex = 31;
            this.chxEnableRuntimeLogs.Text = "Enable Runtime Logging";
            this.chxEnableRuntimeLogs.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 988);
            this.Controls.Add(this.chxEnableRuntimeLogs);
            this.Controls.Add(this.nudAutocancelSeconds);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxUseWam);
            this.Controls.Add(this.cbxListOsAccounts);
            this.Controls.Add(this.cbxBackgroundThread);
            this.Controls.Add(this.btnRemoveAccount);
            this.Controls.Add(this.btnExpire);
            this.Controls.Add(this.cbxMsaPt);
            this.Controls.Add(this.cbxAccount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxScopes);
            this.Controls.Add(this.btnClearCache);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.accBtn);
            this.Controls.Add(this.atsAtiBtn);
            this.Controls.Add(this.atiBtn);
            this.Controls.Add(this.atsBtn);
            this.Controls.Add(this.promptCbx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.loginHintTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clientIdCbx);
            this.Controls.Add(this.authorityCbx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resultTbx);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudAutocancelSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox resultTbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox authorityCbx;
        private System.Windows.Forms.ComboBox clientIdCbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox loginHintTxt;
        private System.Windows.Forms.ComboBox promptCbx;
        private System.Windows.Forms.Button atsBtn;
        private System.Windows.Forms.Button atiBtn;
        private System.Windows.Forms.Button atsAtiBtn;
        private System.Windows.Forms.Button accBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button btnClearCache;
        private System.Windows.Forms.ComboBox cbxScopes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxAccount;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbxMsaPt;
        private System.Windows.Forms.Button btnExpire;
        private System.Windows.Forms.Button btnRemoveAccount;
        private System.Windows.Forms.CheckBox cbxBackgroundThread;
        private System.Windows.Forms.CheckBox cbxListOsAccounts;
        private System.Windows.Forms.ComboBox cbxUseWam;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudAutocancelSeconds;
        private System.Windows.Forms.CheckBox chxEnableRuntimeLogs;
    }
}

