/*
 * Created by SharpDevelop.
 * User: HP
 * Date: 15.09.2017
 * Time: 21:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WMIScanner
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox ip1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox query;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn sfd;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RichTextBox rich;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button Wyszukaj;
		private System.Windows.Forms.Button stop;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveQueryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveResultToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.Label label5;

		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		public void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.ip1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.query = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Wyszukaj = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.sfd = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.label4 = new System.Windows.Forms.Label();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.stop = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveResultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label5 = new System.Windows.Forms.Label();
			this.rich = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label1.Location = new System.Drawing.Point(26, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(419, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "Type IP address/subnet mask:";
			// 
			// ip1
			// 
			this.ip1.Location = new System.Drawing.Point(26, 52);
			this.ip1.Name = "ip1";
			this.ip1.Size = new System.Drawing.Size(217, 20);
			this.ip1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label2.Location = new System.Drawing.Point(26, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(208, 27);
			this.label2.TabIndex = 2;
			this.label2.Text = "Type your query:";
			// 
			// query
			// 
			this.query.Location = new System.Drawing.Point(26, 122);
			this.query.Name = "query";
			this.query.Size = new System.Drawing.Size(217, 20);
			this.query.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label3.Location = new System.Drawing.Point(26, 199);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(216, 31);
			this.label3.TabIndex = 4;
			this.label3.Text = "Result:";
			// 
			// Wyszukaj
			// 
			this.Wyszukaj.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Wyszukaj.Location = new System.Drawing.Point(34, 157);
			this.Wyszukaj.Name = "Wyszukaj";
			this.Wyszukaj.Size = new System.Drawing.Size(170, 29);
			this.Wyszukaj.TabIndex = 6;
			this.Wyszukaj.Text = "SEARCH";
			this.Wyszukaj.UseVisualStyleBackColor = true;
			this.Wyszukaj.Click += new System.EventHandler(this.Button1Click);
			// 
			// dataGridView
			// 
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.Column6,
			this.sfd,
			this.Column5,
			this.Column1,
			this.Column2,
			this.Column3,
			this.Column4});
			this.dataGridView.Location = new System.Drawing.Point(19, 229);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.Size = new System.Drawing.Size(875, 191);
			this.dataGridView.TabIndex = 7;
			// 
			// Column6
			// 
			this.Column6.FillWeight = 25F;
			this.Column6.HeaderText = "Lp.";
			this.Column6.Name = "Column6";
			this.Column6.Visible = false;
			// 
			// sfd
			// 
			this.sfd.FillWeight = 87.05584F;
			this.sfd.HeaderText = "Username";
			this.sfd.Name = "sfd";
			// 
			// Column5
			// 
			this.Column5.FillWeight = 87.05584F;
			this.Column5.HeaderText = "Error";
			this.Column5.Name = "Column5";
			// 
			// Column1
			// 
			this.Column1.FillWeight = 87.05584F;
			this.Column1.HeaderText = "ComputerName";
			this.Column1.Name = "Column1";
			// 
			// Column2
			// 
			this.Column2.FillWeight = 87.05584F;
			this.Column2.HeaderText = "Name";
			this.Column2.Name = "Column2";
			// 
			// Column3
			// 
			this.Column3.FillWeight = 87.05584F;
			this.Column3.HeaderText = "Version";
			this.Column3.Name = "Column3";
			// 
			// Column4
			// 
			this.Column4.FillWeight = 87.05584F;
			this.Column4.HeaderText = "SerialNumber";
			this.Column4.Name = "Column4";
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(397, 52);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(287, 26);
			this.progressBar.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label4.Location = new System.Drawing.Point(397, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(286, 33);
			this.label4.TabIndex = 9;
			this.label4.Text = "Progress";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// stop
			// 
			this.stop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.stop.Location = new System.Drawing.Point(250, 157);
			this.stop.Name = "stop";
			this.stop.Size = new System.Drawing.Size(165, 28);
			this.stop.TabIndex = 12;
			this.stop.Text = "STOP";
			this.stop.UseVisualStyleBackColor = true;
			this.stop.Click += new System.EventHandler(this.stopp);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.saveToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1282, 24);
			this.menuStrip1.TabIndex = 13;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.saveQueryToolStripMenuItem,
			this.saveResultToolStripMenuItem});
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// saveQueryToolStripMenuItem
			// 
			this.saveQueryToolStripMenuItem.Name = "saveQueryToolStripMenuItem";
			this.saveQueryToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.saveQueryToolStripMenuItem.Text = " Save query";
			this.saveQueryToolStripMenuItem.Click += new System.EventHandler(this.saveq);
			// 
			// saveResultToolStripMenuItem
			// 
			this.saveResultToolStripMenuItem.Name = "saveResultToolStripMenuItem";
			this.saveResultToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.saveResultToolStripMenuItem.Text = "Save result";
			this.saveResultToolStripMenuItem.Click += new System.EventHandler(this.saver);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.label5.Location = new System.Drawing.Point(1027, 89);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(219, 30);
			this.label5.TabIndex = 16;
			this.label5.Text = "Saved queries";
			// 
			// rich
			// 
			this.rich.Location = new System.Drawing.Point(940, 126);
			this.rich.Name = "rich";
			this.rich.Size = new System.Drawing.Size(317, 294);
			this.rich.TabIndex = 17;
			this.rich.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1282, 467);
			this.Controls.Add(this.rich);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.stop);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.Wyszukaj);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.query);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ip1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menuStrip1);
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "WMI Scanner";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		}
		}
		