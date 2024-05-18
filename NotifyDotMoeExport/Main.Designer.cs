
namespace NotifyDotMoeExport
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.textBoxJsonPath = new System.Windows.Forms.TextBox();
            this.labelJsonPath = new System.Windows.Forms.Label();
            this.buttonSelectJsonPath = new System.Windows.Forms.Button();
            this.openFileDialogMenu = new System.Windows.Forms.OpenFileDialog();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.progressBarConvert = new System.Windows.Forms.ProgressBar();
            this.buttonSelectConsAnimeDatPath = new System.Windows.Forms.Button();
            this.labelConsumeAnimePath = new System.Windows.Forms.Label();
            this.textBoxConsAnimeDatPath = new System.Windows.Forms.TextBox();
            this.buttonDownloadConsAnimeDat = new System.Windows.Forms.Button();
            this.saveFileDialogMenu = new System.Windows.Forms.SaveFileDialog();
            this.buttonMalOutputXML = new System.Windows.Forms.Button();
            this.labelMalOutput = new System.Windows.Forms.Label();
            this.textBoxMalOutputXML = new System.Windows.Forms.TextBox();
            this.buttonDownloadJsonExport = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.buttonCacheOutputPath = new System.Windows.Forms.Button();
            this.buttonSelectCacheInputPath = new System.Windows.Forms.Button();
            this.radioButtonModeMALFormat = new System.Windows.Forms.RadioButton();
            this.radioButtonModeAnilistAPI = new System.Windows.Forms.RadioButton();
            this.checkBoxIgnoreAnilistRules = new System.Windows.Forms.CheckBox();
            this.labelCacheOutput = new System.Windows.Forms.Label();
            this.textBoxCacheOutputPath = new System.Windows.Forms.TextBox();
            this.labelCacheInputPath = new System.Windows.Forms.Label();
            this.textBoxCacheInputPath = new System.Windows.Forms.TextBox();
            this.labelExportMode = new System.Windows.Forms.Label();
            this.panelCacheOutputPath = new System.Windows.Forms.Panel();
            this.panelMalOutputPath = new System.Windows.Forms.Panel();
            this.panelCachePath = new System.Windows.Forms.Panel();
            this.panelConsumeAnimePath = new System.Windows.Forms.Panel();
            this.panelJsonExportPath = new System.Windows.Forms.Panel();
            this.panelCacheOutputPath.SuspendLayout();
            this.panelMalOutputPath.SuspendLayout();
            this.panelCachePath.SuspendLayout();
            this.panelConsumeAnimePath.SuspendLayout();
            this.panelJsonExportPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxJsonPath
            // 
            this.textBoxJsonPath.Location = new System.Drawing.Point(136, 3);
            this.textBoxJsonPath.Name = "textBoxJsonPath";
            this.textBoxJsonPath.ReadOnly = true;
            this.textBoxJsonPath.Size = new System.Drawing.Size(328, 20);
            this.textBoxJsonPath.TabIndex = 0;
            // 
            // labelJsonPath
            // 
            this.labelJsonPath.AutoSize = true;
            this.labelJsonPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJsonPath.Location = new System.Drawing.Point(2, 3);
            this.labelJsonPath.Name = "labelJsonPath";
            this.labelJsonPath.Size = new System.Drawing.Size(131, 16);
            this.labelJsonPath.TabIndex = 1;
            this.labelJsonPath.Text = "Json Export Path :";
            // 
            // buttonSelectJsonPath
            // 
            this.buttonSelectJsonPath.Location = new System.Drawing.Point(549, 3);
            this.buttonSelectJsonPath.Name = "buttonSelectJsonPath";
            this.buttonSelectJsonPath.Size = new System.Drawing.Size(75, 20);
            this.buttonSelectJsonPath.TabIndex = 2;
            this.buttonSelectJsonPath.Tag = "json|textBoxJsonPath";
            this.buttonSelectJsonPath.Text = "Select";
            this.toolTipInfo.SetToolTip(this.buttonSelectJsonPath, "Select a notify anime list (.json)\r\nThe json export from notify website\r\nand the " +
        "download option in-app are the same");
            this.buttonSelectJsonPath.UseVisualStyleBackColor = true;
            this.buttonSelectJsonPath.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // openFileDialogMenu
            // 
            this.openFileDialogMenu.Filter = "Notify JSON file (*.json)|*.json";
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(20, 201);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(617, 32);
            this.buttonConvert.TabIndex = 3;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // progressBarConvert
            // 
            this.progressBarConvert.Location = new System.Drawing.Point(10, 208);
            this.progressBarConvert.MarqueeAnimationSpeed = 10;
            this.progressBarConvert.Name = "progressBarConvert";
            this.progressBarConvert.Size = new System.Drawing.Size(632, 29);
            this.progressBarConvert.TabIndex = 4;
            // 
            // buttonSelectConsAnimeDatPath
            // 
            this.buttonSelectConsAnimeDatPath.Location = new System.Drawing.Point(549, 3);
            this.buttonSelectConsAnimeDatPath.Name = "buttonSelectConsAnimeDatPath";
            this.buttonSelectConsAnimeDatPath.Size = new System.Drawing.Size(75, 20);
            this.buttonSelectConsAnimeDatPath.TabIndex = 7;
            this.buttonSelectConsAnimeDatPath.Tag = "dat|textBoxConsAnimeDatPath";
            this.buttonSelectConsAnimeDatPath.Text = "Select";
            this.toolTipInfo.SetToolTip(this.buttonSelectConsAnimeDatPath, "Select the ActivityConsumeAnime database file of notify");
            this.buttonSelectConsAnimeDatPath.UseVisualStyleBackColor = true;
            this.buttonSelectConsAnimeDatPath.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // labelConsumeAnimePath
            // 
            this.labelConsumeAnimePath.AutoSize = true;
            this.labelConsumeAnimePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConsumeAnimePath.Location = new System.Drawing.Point(3, 3);
            this.labelConsumeAnimePath.Name = "labelConsumeAnimePath";
            this.labelConsumeAnimePath.Size = new System.Drawing.Size(192, 16);
            this.labelConsumeAnimePath.TabIndex = 6;
            this.labelConsumeAnimePath.Text = "ConsumeAnime DAT Path :";
            // 
            // textBoxConsAnimeDatPath
            // 
            this.textBoxConsAnimeDatPath.Location = new System.Drawing.Point(201, 3);
            this.textBoxConsAnimeDatPath.Name = "textBoxConsAnimeDatPath";
            this.textBoxConsAnimeDatPath.ReadOnly = true;
            this.textBoxConsAnimeDatPath.Size = new System.Drawing.Size(264, 20);
            this.textBoxConsAnimeDatPath.TabIndex = 5;
            // 
            // buttonDownloadConsAnimeDat
            // 
            this.buttonDownloadConsAnimeDat.Location = new System.Drawing.Point(471, 3);
            this.buttonDownloadConsAnimeDat.Name = "buttonDownloadConsAnimeDat";
            this.buttonDownloadConsAnimeDat.Size = new System.Drawing.Size(75, 20);
            this.buttonDownloadConsAnimeDat.TabIndex = 8;
            this.buttonDownloadConsAnimeDat.Tag = "dat";
            this.buttonDownloadConsAnimeDat.Text = "Download";
            this.toolTipInfo.SetToolTip(this.buttonDownloadConsAnimeDat, "Download the ActivityConsumeAnime database file using notify api");
            this.buttonDownloadConsAnimeDat.UseVisualStyleBackColor = true;
            this.buttonDownloadConsAnimeDat.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // saveFileDialogMenu
            // 
            this.saveFileDialogMenu.Filter = "Notify file (*.dat)|*.dat";
            // 
            // buttonMalOutputXML
            // 
            this.buttonMalOutputXML.Location = new System.Drawing.Point(549, 4);
            this.buttonMalOutputXML.Name = "buttonMalOutputXML";
            this.buttonMalOutputXML.Size = new System.Drawing.Size(75, 20);
            this.buttonMalOutputXML.TabIndex = 11;
            this.buttonMalOutputXML.Tag = "xml";
            this.buttonMalOutputXML.Text = "Select";
            this.toolTipInfo.SetToolTip(this.buttonMalOutputXML, "Select where to save your anime list in MAL format (xml)");
            this.buttonMalOutputXML.UseVisualStyleBackColor = true;
            this.buttonMalOutputXML.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // labelMalOutput
            // 
            this.labelMalOutput.AutoSize = true;
            this.labelMalOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMalOutput.Location = new System.Drawing.Point(3, 3);
            this.labelMalOutput.Name = "labelMalOutput";
            this.labelMalOutput.Size = new System.Drawing.Size(128, 16);
            this.labelMalOutput.TabIndex = 10;
            this.labelMalOutput.Text = "MAL Output Path :";
            // 
            // textBoxMalOutputXML
            // 
            this.textBoxMalOutputXML.Location = new System.Drawing.Point(136, 3);
            this.textBoxMalOutputXML.Name = "textBoxMalOutputXML";
            this.textBoxMalOutputXML.ReadOnly = true;
            this.textBoxMalOutputXML.Size = new System.Drawing.Size(410, 20);
            this.textBoxMalOutputXML.TabIndex = 9;
            // 
            // buttonDownloadJsonExport
            // 
            this.buttonDownloadJsonExport.Location = new System.Drawing.Point(470, 3);
            this.buttonDownloadJsonExport.Name = "buttonDownloadJsonExport";
            this.buttonDownloadJsonExport.Size = new System.Drawing.Size(75, 20);
            this.buttonDownloadJsonExport.TabIndex = 12;
            this.buttonDownloadJsonExport.Tag = "json";
            this.buttonDownloadJsonExport.Text = "Download";
            this.toolTipInfo.SetToolTip(this.buttonDownloadJsonExport, "Download your anime list from notify api.\r\n\r\nWARNING : Download made from notify " +
        "\r\napi won\'t include animes set as private.\r\nUse the export feature in your setti" +
        "ngs\r\nto include your private animes.");
            this.buttonDownloadJsonExport.UseVisualStyleBackColor = true;
            this.buttonDownloadJsonExport.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // toolTipInfo
            // 
            this.toolTipInfo.AutoPopDelay = 60000;
            this.toolTipInfo.InitialDelay = 800;
            this.toolTipInfo.ReshowDelay = 100;
            this.toolTipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipInfo.ToolTipTitle = "Info";
            // 
            // buttonCacheOutputPath
            // 
            this.buttonCacheOutputPath.Location = new System.Drawing.Point(549, 2);
            this.buttonCacheOutputPath.Name = "buttonCacheOutputPath";
            this.buttonCacheOutputPath.Size = new System.Drawing.Size(75, 20);
            this.buttonCacheOutputPath.TabIndex = 15;
            this.buttonCacheOutputPath.Tag = "json-cache";
            this.buttonCacheOutputPath.Text = "Select";
            this.toolTipInfo.SetToolTip(this.buttonCacheOutputPath, "Select where to save cached anime list.\r\nThis file will be used by this app to re" +
        "sume\r\nimport by ignoring previous entry, allowing\r\nfaster operations when conver" +
        "ting your list\r\nagain");
            this.buttonCacheOutputPath.UseVisualStyleBackColor = true;
            this.buttonCacheOutputPath.Click += new System.EventHandler(this.buttonCacheOutputPath_Click);
            // 
            // buttonSelectCacheInputPath
            // 
            this.buttonSelectCacheInputPath.Location = new System.Drawing.Point(549, 3);
            this.buttonSelectCacheInputPath.Name = "buttonSelectCacheInputPath";
            this.buttonSelectCacheInputPath.Size = new System.Drawing.Size(75, 20);
            this.buttonSelectCacheInputPath.TabIndex = 18;
            this.buttonSelectCacheInputPath.Tag = "json-cache|textBoxCacheInputPath";
            this.buttonSelectCacheInputPath.Text = "Select";
            this.toolTipInfo.SetToolTip(this.buttonSelectCacheInputPath, resources.GetString("buttonSelectCacheInputPath.ToolTip"));
            this.buttonSelectCacheInputPath.UseVisualStyleBackColor = true;
            this.buttonSelectCacheInputPath.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // radioButtonModeMALFormat
            // 
            this.radioButtonModeMALFormat.AutoSize = true;
            this.radioButtonModeMALFormat.Checked = true;
            this.radioButtonModeMALFormat.Location = new System.Drawing.Point(122, 181);
            this.radioButtonModeMALFormat.Name = "radioButtonModeMALFormat";
            this.radioButtonModeMALFormat.Size = new System.Drawing.Size(82, 17);
            this.radioButtonModeMALFormat.TabIndex = 20;
            this.radioButtonModeMALFormat.TabStop = true;
            this.radioButtonModeMALFormat.Text = "MAL Format";
            this.toolTipInfo.SetToolTip(this.radioButtonModeMALFormat, resources.GetString("radioButtonModeMALFormat.ToolTip"));
            this.radioButtonModeMALFormat.UseVisualStyleBackColor = true;
            // 
            // radioButtonModeAnilistAPI
            // 
            this.radioButtonModeAnilistAPI.AutoSize = true;
            this.radioButtonModeAnilistAPI.Location = new System.Drawing.Point(213, 181);
            this.radioButtonModeAnilistAPI.Name = "radioButtonModeAnilistAPI";
            this.radioButtonModeAnilistAPI.Size = new System.Drawing.Size(72, 17);
            this.radioButtonModeAnilistAPI.TabIndex = 21;
            this.radioButtonModeAnilistAPI.Text = "Anilist API";
            this.toolTipInfo.SetToolTip(this.radioButtonModeAnilistAPI, resources.GetString("radioButtonModeAnilistAPI.ToolTip"));
            this.radioButtonModeAnilistAPI.UseVisualStyleBackColor = true;
            this.radioButtonModeAnilistAPI.CheckedChanged += new System.EventHandler(this.radioButtonModeAnilistAPI_CheckedChanged);
            // 
            // checkBoxIgnoreAnilistRules
            // 
            this.checkBoxIgnoreAnilistRules.AutoSize = true;
            this.checkBoxIgnoreAnilistRules.Enabled = false;
            this.checkBoxIgnoreAnilistRules.Location = new System.Drawing.Point(291, 181);
            this.checkBoxIgnoreAnilistRules.Name = "checkBoxIgnoreAnilistRules";
            this.checkBoxIgnoreAnilistRules.Size = new System.Drawing.Size(81, 17);
            this.checkBoxIgnoreAnilistRules.TabIndex = 22;
            this.checkBoxIgnoreAnilistRules.Text = "Ignore rules";
            this.toolTipInfo.SetToolTip(this.checkBoxIgnoreAnilistRules, "If checked, animes won\'t be skipped even if they match\r\nthe following rules :\r\n- " +
        "The anime is not set as private\r\n- The anime score is not a decimal");
            this.checkBoxIgnoreAnilistRules.UseVisualStyleBackColor = true;
            // 
            // labelCacheOutput
            // 
            this.labelCacheOutput.AutoSize = true;
            this.labelCacheOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCacheOutput.Location = new System.Drawing.Point(3, 2);
            this.labelCacheOutput.Name = "labelCacheOutput";
            this.labelCacheOutput.Size = new System.Drawing.Size(142, 16);
            this.labelCacheOutput.TabIndex = 14;
            this.labelCacheOutput.Text = "Cache Output Path :";
            // 
            // textBoxCacheOutputPath
            // 
            this.textBoxCacheOutputPath.Location = new System.Drawing.Point(150, 2);
            this.textBoxCacheOutputPath.Name = "textBoxCacheOutputPath";
            this.textBoxCacheOutputPath.ReadOnly = true;
            this.textBoxCacheOutputPath.Size = new System.Drawing.Size(396, 20);
            this.textBoxCacheOutputPath.TabIndex = 13;
            // 
            // labelCacheInputPath
            // 
            this.labelCacheInputPath.AutoSize = true;
            this.labelCacheInputPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCacheInputPath.Location = new System.Drawing.Point(3, 3);
            this.labelCacheInputPath.Name = "labelCacheInputPath";
            this.labelCacheInputPath.Size = new System.Drawing.Size(94, 16);
            this.labelCacheInputPath.TabIndex = 17;
            this.labelCacheInputPath.Text = "Cache Path :";
            // 
            // textBoxCacheInputPath
            // 
            this.textBoxCacheInputPath.Location = new System.Drawing.Point(103, 3);
            this.textBoxCacheInputPath.Name = "textBoxCacheInputPath";
            this.textBoxCacheInputPath.ReadOnly = true;
            this.textBoxCacheInputPath.Size = new System.Drawing.Size(443, 20);
            this.textBoxCacheInputPath.TabIndex = 16;
            // 
            // labelExportMode
            // 
            this.labelExportMode.AutoSize = true;
            this.labelExportMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExportMode.Location = new System.Drawing.Point(14, 181);
            this.labelExportMode.Name = "labelExportMode";
            this.labelExportMode.Size = new System.Drawing.Size(102, 16);
            this.labelExportMode.TabIndex = 19;
            this.labelExportMode.Text = "Export Mode :";
            // 
            // panelCacheOutputPath
            // 
            this.panelCacheOutputPath.Controls.Add(this.buttonCacheOutputPath);
            this.panelCacheOutputPath.Controls.Add(this.textBoxCacheOutputPath);
            this.panelCacheOutputPath.Controls.Add(this.labelCacheOutput);
            this.panelCacheOutputPath.Location = new System.Drawing.Point(9, 147);
            this.panelCacheOutputPath.Name = "panelCacheOutputPath";
            this.panelCacheOutputPath.Size = new System.Drawing.Size(627, 31);
            this.panelCacheOutputPath.TabIndex = 23;
            // 
            // panelMalOutputPath
            // 
            this.panelMalOutputPath.Controls.Add(this.buttonMalOutputXML);
            this.panelMalOutputPath.Controls.Add(this.textBoxMalOutputXML);
            this.panelMalOutputPath.Controls.Add(this.labelMalOutput);
            this.panelMalOutputPath.Location = new System.Drawing.Point(9, 117);
            this.panelMalOutputPath.Name = "panelMalOutputPath";
            this.panelMalOutputPath.Size = new System.Drawing.Size(627, 31);
            this.panelMalOutputPath.TabIndex = 24;
            // 
            // panelCachePath
            // 
            this.panelCachePath.Controls.Add(this.buttonSelectCacheInputPath);
            this.panelCachePath.Controls.Add(this.textBoxCacheInputPath);
            this.panelCachePath.Controls.Add(this.labelCacheInputPath);
            this.panelCachePath.Location = new System.Drawing.Point(10, 68);
            this.panelCachePath.Name = "panelCachePath";
            this.panelCachePath.Size = new System.Drawing.Size(627, 31);
            this.panelCachePath.TabIndex = 25;
            // 
            // panelConsumeAnimePath
            // 
            this.panelConsumeAnimePath.Controls.Add(this.buttonSelectConsAnimeDatPath);
            this.panelConsumeAnimePath.Controls.Add(this.buttonDownloadConsAnimeDat);
            this.panelConsumeAnimePath.Controls.Add(this.textBoxConsAnimeDatPath);
            this.panelConsumeAnimePath.Controls.Add(this.labelConsumeAnimePath);
            this.panelConsumeAnimePath.Location = new System.Drawing.Point(10, 39);
            this.panelConsumeAnimePath.Name = "panelConsumeAnimePath";
            this.panelConsumeAnimePath.Size = new System.Drawing.Size(627, 31);
            this.panelConsumeAnimePath.TabIndex = 26;
            // 
            // panelJsonExportPath
            // 
            this.panelJsonExportPath.Controls.Add(this.buttonSelectJsonPath);
            this.panelJsonExportPath.Controls.Add(this.buttonDownloadJsonExport);
            this.panelJsonExportPath.Controls.Add(this.textBoxJsonPath);
            this.panelJsonExportPath.Controls.Add(this.labelJsonPath);
            this.panelJsonExportPath.Location = new System.Drawing.Point(10, 8);
            this.panelJsonExportPath.Name = "panelJsonExportPath";
            this.panelJsonExportPath.Size = new System.Drawing.Size(627, 31);
            this.panelJsonExportPath.TabIndex = 27;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(649, 245);
            this.Controls.Add(this.panelJsonExportPath);
            this.Controls.Add(this.panelConsumeAnimePath);
            this.Controls.Add(this.panelCachePath);
            this.Controls.Add(this.panelMalOutputPath);
            this.Controls.Add(this.panelCacheOutputPath);
            this.Controls.Add(this.checkBoxIgnoreAnilistRules);
            this.Controls.Add(this.radioButtonModeAnilistAPI);
            this.Controls.Add(this.radioButtonModeMALFormat);
            this.Controls.Add(this.labelExportMode);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.progressBarConvert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notify.moe Exporter";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_OnClosing);
            this.panelCacheOutputPath.ResumeLayout(false);
            this.panelCacheOutputPath.PerformLayout();
            this.panelMalOutputPath.ResumeLayout(false);
            this.panelMalOutputPath.PerformLayout();
            this.panelCachePath.ResumeLayout(false);
            this.panelCachePath.PerformLayout();
            this.panelConsumeAnimePath.ResumeLayout(false);
            this.panelConsumeAnimePath.PerformLayout();
            this.panelJsonExportPath.ResumeLayout(false);
            this.panelJsonExportPath.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxJsonPath;
        private System.Windows.Forms.Label labelJsonPath;
        private System.Windows.Forms.Button buttonSelectJsonPath;
        private System.Windows.Forms.OpenFileDialog openFileDialogMenu;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.ProgressBar progressBarConvert;
        private System.Windows.Forms.Button buttonSelectConsAnimeDatPath;
        private System.Windows.Forms.Label labelConsumeAnimePath;
        private System.Windows.Forms.TextBox textBoxConsAnimeDatPath;
        private System.Windows.Forms.Button buttonDownloadConsAnimeDat;
        private System.Windows.Forms.SaveFileDialog saveFileDialogMenu;
        private System.Windows.Forms.Button buttonMalOutputXML;
        private System.Windows.Forms.Label labelMalOutput;
        private System.Windows.Forms.TextBox textBoxMalOutputXML;
        private System.Windows.Forms.Button buttonDownloadJsonExport;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button buttonCacheOutputPath;
        private System.Windows.Forms.Label labelCacheOutput;
        private System.Windows.Forms.TextBox textBoxCacheOutputPath;
        private System.Windows.Forms.Button buttonSelectCacheInputPath;
        private System.Windows.Forms.Label labelCacheInputPath;
        private System.Windows.Forms.TextBox textBoxCacheInputPath;
        private System.Windows.Forms.Label labelExportMode;
        private System.Windows.Forms.RadioButton radioButtonModeMALFormat;
        private System.Windows.Forms.RadioButton radioButtonModeAnilistAPI;
        private System.Windows.Forms.CheckBox checkBoxIgnoreAnilistRules;
        private System.Windows.Forms.Panel panelCacheOutputPath;
        private System.Windows.Forms.Panel panelMalOutputPath;
        private System.Windows.Forms.Panel panelCachePath;
        private System.Windows.Forms.Panel panelConsumeAnimePath;
        private System.Windows.Forms.Panel panelJsonExportPath;
    }
}

