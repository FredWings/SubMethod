namespace WndFormEventLoadOnce
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnAdd = new System.Windows.Forms.Button();
            this.txtOnce = new System.Windows.Forms.TextBox();
            this.showOnce = new System.Windows.Forms.TextBox();
            this.txtMore = new System.Windows.Forms.TextBox();
            this.showMore = new System.Windows.Forms.TextBox();
            this.BtnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(28, 22);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 0;
            this.BtnAdd.Text = "添加事件";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // txtOnce
            // 
            this.txtOnce.Location = new System.Drawing.Point(125, 22);
            this.txtOnce.Name = "txtOnce";
            this.txtOnce.Size = new System.Drawing.Size(149, 21);
            this.txtOnce.TabIndex = 1;
            // 
            // showOnce
            // 
            this.showOnce.Location = new System.Drawing.Point(295, 22);
            this.showOnce.Name = "showOnce";
            this.showOnce.Size = new System.Drawing.Size(273, 21);
            this.showOnce.TabIndex = 2;
            // 
            // txtMore
            // 
            this.txtMore.Location = new System.Drawing.Point(125, 60);
            this.txtMore.Name = "txtMore";
            this.txtMore.Size = new System.Drawing.Size(149, 21);
            this.txtMore.TabIndex = 3;
            // 
            // showMore
            // 
            this.showMore.Location = new System.Drawing.Point(295, 60);
            this.showMore.Name = "showMore";
            this.showMore.Size = new System.Drawing.Size(273, 21);
            this.showMore.TabIndex = 4;
            // 
            // BtnClear
            // 
            this.BtnClear.Location = new System.Drawing.Point(31, 60);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(72, 21);
            this.BtnClear.TabIndex = 5;
            this.BtnClear.Text = "清除";
            this.BtnClear.UseVisualStyleBackColor = true;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 132);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.showMore);
            this.Controls.Add(this.txtMore);
            this.Controls.Add(this.showOnce);
            this.Controls.Add(this.txtOnce);
            this.Controls.Add(this.BtnAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.TextBox txtOnce;
        private System.Windows.Forms.TextBox showOnce;
        private System.Windows.Forms.TextBox txtMore;
        private System.Windows.Forms.TextBox showMore;
        private System.Windows.Forms.Button BtnClear;
    }
}

