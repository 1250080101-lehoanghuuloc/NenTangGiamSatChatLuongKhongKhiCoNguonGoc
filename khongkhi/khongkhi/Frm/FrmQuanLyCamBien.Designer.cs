namespace khongkhi
{
    partial class FrmQuanLyCamBien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaCamBien = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTram = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPhuongPhap = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLoaiChiSo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDonViDo = new System.Windows.Forms.TextBox();

            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvCamBien = new System.Windows.Forms.DataGridView();

            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCamBien)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDonViDo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtLoaiChiSo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboPhuongPhap);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboTram);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMaCamBien);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(840, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin cảm biến";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã cảm biến:";
            // 
            // txtMaCamBien
            // 
            this.txtMaCamBien.Location = new System.Drawing.Point(130, 32);
            this.txtMaCamBien.Name = "txtMaCamBien";
            this.txtMaCamBien.Size = new System.Drawing.Size(250, 22);
            this.txtMaCamBien.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(440, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trạm:";
            // 
            // cboTram
            // 
            this.cboTram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTram.FormattingEnabled = true;
            this.cboTram.Location = new System.Drawing.Point(550, 32);
            this.cboTram.Name = "cboTram";
            this.cboTram.Size = new System.Drawing.Size(260, 24);
            this.cboTram.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Phương pháp đo:";
            // 
            // cboPhuongPhap
            // 
            this.cboPhuongPhap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongPhap.FormattingEnabled = true;
            this.cboPhuongPhap.Location = new System.Drawing.Point(130, 77);
            this.cboPhuongPhap.Name = "cboPhuongPhap";
            this.cboPhuongPhap.Size = new System.Drawing.Size(250, 24);
            this.cboPhuongPhap.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(440, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Loại chỉ số:";
            // 
            // txtLoaiChiSo
            // 
            this.txtLoaiChiSo.Location = new System.Drawing.Point(550, 77);
            this.txtLoaiChiSo.Name = "txtLoaiChiSo";
            this.txtLoaiChiSo.Size = new System.Drawing.Size(120, 22);
            this.txtLoaiChiSo.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(685, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Đơn vị:";
            // 
            // txtDonViDo
            // 
            this.txtDonViDo.Location = new System.Drawing.Point(740, 77);
            this.txtDonViDo.Name = "txtDonViDo";
            this.txtDonViDo.Size = new System.Drawing.Size(70, 22);
            this.txtDonViDo.TabIndex = 9;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(140, 165);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(110, 35);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(275, 165);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 35);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(410, 165);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 35);
            this.btnXoa.TabIndex = 3;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(545, 165);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 35);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(680, 165);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 35);
            this.btnDong.TabIndex = 5;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvCamBien);
            this.groupBox2.Location = new System.Drawing.Point(20, 215);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(840, 240);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách cảm biến";
            // 
            // dgvCamBien
            // 
            this.dgvCamBien.AllowUserToAddRows = false;
            this.dgvCamBien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCamBien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCamBien.Location = new System.Drawing.Point(15, 25);
            this.dgvCamBien.Name = "dgvCamBien";
            this.dgvCamBien.ReadOnly = true;
            this.dgvCamBien.RowHeadersWidth = 51;
            this.dgvCamBien.RowTemplate.Height = 24;
            this.dgvCamBien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCamBien.Size = new System.Drawing.Size(810, 200);
            this.dgvCamBien.TabIndex = 0;
            this.dgvCamBien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCamBien_CellClick);
            // 
            // FrmQuanLyCamBien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 475);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmQuanLyCamBien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý cảm biến";
            this.Load += new System.EventHandler(this.FrmQuanLyCamBien_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCamBien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaCamBien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboTram;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboPhuongPhap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLoaiChiSo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDonViDo;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvCamBien;
    }
}