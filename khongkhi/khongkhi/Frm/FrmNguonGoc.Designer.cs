namespace khongkhi
{
    partial class FrmNguonGoc
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

        private void InitializeComponent()
        {
            this.labelTieuDe = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaPhepDo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtThoiDiemDo = new System.Windows.Forms.TextBox();

            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTram = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCamBien = new System.Windows.Forms.TextBox();

            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPhuongPhap = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPhienBan = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDoTre = new System.Windows.Forms.TextBox();

            this.btnDong = new System.Windows.Forms.Button();

            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTieuDe
            // 
            this.labelTieuDe.AutoSize = true;
            this.labelTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTieuDe.ForeColor = System.Drawing.Color.Navy;
            this.labelTieuDe.Location = new System.Drawing.Point(240, 15);
            this.labelTieuDe.Name = "labelTieuDe";
            this.labelTieuDe.Size = new System.Drawing.Size(324, 29);
            this.labelTieuDe.TabIndex = 0;
            this.labelTieuDe.Text = "NGUỒN GỐC CỦA CHỈ SỐ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMaPhepDo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGiaTri);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtThoiDiemDo);
            this.groupBox1.Location = new System.Drawing.Point(30, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chỉ số";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã phép đo:";
            // 
            // txtMaPhepDo
            // 
            this.txtMaPhepDo.Location = new System.Drawing.Point(120, 27);
            this.txtMaPhepDo.Name = "txtMaPhepDo";
            this.txtMaPhepDo.ReadOnly = true;
            this.txtMaPhepDo.Size = new System.Drawing.Size(220, 22);
            this.txtMaPhepDo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(380, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Giá trị:";
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Location = new System.Drawing.Point(480, 27);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.ReadOnly = true;
            this.txtGiaTri.Size = new System.Drawing.Size(230, 22);
            this.txtGiaTri.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Thời điểm đo:";
            // 
            // txtThoiDiemDo
            // 
            this.txtThoiDiemDo.Location = new System.Drawing.Point(120, 62);
            this.txtThoiDiemDo.Name = "txtThoiDiemDo";
            this.txtThoiDiemDo.ReadOnly = true;
            this.txtThoiDiemDo.Size = new System.Drawing.Size(220, 22);
            this.txtThoiDiemDo.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtTram);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtCamBien);
            this.groupBox2.Location = new System.Drawing.Point(30, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 70);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vị trí đo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Trạm:";
            // 
            // txtTram
            // 
            this.txtTram.Location = new System.Drawing.Point(120, 27);
            this.txtTram.Name = "txtTram";
            this.txtTram.ReadOnly = true;
            this.txtTram.Size = new System.Drawing.Size(220, 22);
            this.txtTram.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(380, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Cảm biến:";
            // 
            // txtCamBien
            // 
            this.txtCamBien.Location = new System.Drawing.Point(480, 27);
            this.txtCamBien.Name = "txtCamBien";
            this.txtCamBien.ReadOnly = true;
            this.txtCamBien.Size = new System.Drawing.Size(230, 22);
            this.txtCamBien.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtTrangThai);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtLyDo);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtPhuongPhap);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtPhienBan);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtDoTre);
            this.groupBox3.Location = new System.Drawing.Point(30, 245);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(740, 195);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bốn thuộc tính của chỉ số";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Trạng thái hợp lệ:";
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.Location = new System.Drawing.Point(180, 27);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.ReadOnly = true;
            this.txtTrangThai.Size = new System.Drawing.Size(530, 22);
            this.txtTrangThai.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Lý do:";
            // 
            // txtLyDo
            // 
            this.txtLyDo.Location = new System.Drawing.Point(180, 57);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.ReadOnly = true;
            this.txtLyDo.Size = new System.Drawing.Size(530, 22);
            this.txtLyDo.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "Phương pháp đo:";
            // 
            // txtPhuongPhap
            // 
            this.txtPhuongPhap.Location = new System.Drawing.Point(180, 92);
            this.txtPhuongPhap.Name = "txtPhuongPhap";
            this.txtPhuongPhap.ReadOnly = true;
            this.txtPhuongPhap.Size = new System.Drawing.Size(530, 22);
            this.txtPhuongPhap.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Phiên bản hiệu chuẩn:";
            // 
            // txtPhienBan
            // 
            this.txtPhienBan.Location = new System.Drawing.Point(180, 127);
            this.txtPhienBan.Name = "txtPhienBan";
            this.txtPhienBan.ReadOnly = true;
            this.txtPhienBan.Size = new System.Drawing.Size(530, 22);
            this.txtPhienBan.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 165);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 17);
            this.label10.TabIndex = 8;
            this.label10.Text = "Độ trễ:";
            // 
            // txtDoTre
            // 
            this.txtDoTre.Location = new System.Drawing.Point(180, 162);
            this.txtDoTre.Name = "txtDoTre";
            this.txtDoTre.ReadOnly = true;
            this.txtDoTre.Size = new System.Drawing.Size(530, 22);
            this.txtDoTre.TabIndex = 9;
            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(340, 450);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 35);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmNguonGoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelTieuDe);
            this.Name = "FrmNguonGoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nguồn gốc của chỉ số";

            // 🔥 THÊM DÒNG NÀY ĐỂ KÍCH HOẠT SỰ KIỆN LOAD CHO FORM 🔥
            this.Load += new System.EventHandler(this.FrmNguonGoc_Load);

            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelTieuDe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaPhepDo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGiaTri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtThoiDiemDo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTram;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCamBien;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPhuongPhap;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPhienBan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDoTre;
        private System.Windows.Forms.Button btnDong;
    }
}