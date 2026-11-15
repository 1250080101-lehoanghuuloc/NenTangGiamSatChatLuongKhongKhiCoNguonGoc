namespace khongkhi
{
    partial class FrmXemDuLieu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cboTram = new System.Windows.Forms.ComboBox();
            this.cboCamBien = new System.Windows.Forms.ComboBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnXemNguonGoc = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chartThoiGian = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartThoiGian)).BeginInit();
            this.SuspendLayout();
            // 
            // cboTram
            // 
            this.cboTram.FormattingEnabled = true;
            this.cboTram.Location = new System.Drawing.Point(80, 25);
            this.cboTram.Name = "cboTram";
            this.cboTram.Size = new System.Drawing.Size(200, 24);
            this.cboTram.TabIndex = 0;
            this.cboTram.SelectedIndexChanged += new System.EventHandler(this.cboTram_SelectedIndexChanged);
            // 
            // cboCamBien
            // 
            this.cboCamBien.FormattingEnabled = true;
            this.cboCamBien.Location = new System.Drawing.Point(370, 25);
            this.cboCamBien.Name = "cboCamBien";
            this.cboCamBien.Size = new System.Drawing.Size(200, 24);
            this.cboCamBien.TabIndex = 1;
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(20, 110);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(950, 250);
            this.dgvData.TabIndex = 2;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "yyyy-MM-dd";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(80, 72);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(150, 22);
            this.dtpTuNgay.TabIndex = 3;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "yyyy-MM-dd";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(310, 72);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(150, 22);
            this.dtpDenNgay.TabIndex = 4;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(480, 68);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 30);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnXemNguonGoc
            // 
            this.btnXemNguonGoc.Location = new System.Drawing.Point(590, 68);
            this.btnXemNguonGoc.Name = "btnXemNguonGoc";
            this.btnXemNguonGoc.Size = new System.Drawing.Size(130, 30);
            this.btnXemNguonGoc.TabIndex = 6;
            this.btnXemNguonGoc.Text = "Xem nguồn gốc";
            this.btnXemNguonGoc.UseVisualStyleBackColor = true;
            this.btnXemNguonGoc.Click += new System.EventHandler(this.btnXemNguonGoc_Click);
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(880, 645);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(90, 30);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Trạm:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Cảm biến:";
            // 
            // chartThoiGian
            // 
            chartArea4.Name = "ChartArea1";
            this.chartThoiGian.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartThoiGian.Legends.Add(legend4);
            this.chartThoiGian.Location = new System.Drawing.Point(20, 395);
            this.chartThoiGian.Name = "chartThoiGian";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "GiaTriDo";
            this.chartThoiGian.Series.Add(series4);
            this.chartThoiGian.Size = new System.Drawing.Size(950, 235);
            this.chartThoiGian.TabIndex = 10;
            this.chartThoiGian.Text = "chart1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(20, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Biểu đồ giá trị theo thời gian";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Từ ngày:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Đến ngày:";
            // 
            // FrmXemDuLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 690);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chartThoiGian);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXemNguonGoc);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.cboCamBien);
            this.Controls.Add(this.cboTram);
            this.Name = "FrmXemDuLieu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem Dữ Liệu Quan Trắc";
            this.Load += new System.EventHandler(this.FrmXemDuLieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartThoiGian)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTram;
        private System.Windows.Forms.ComboBox cboCamBien;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnXemNguonGoc;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartThoiGian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}