namespace PackageHandler
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.updateButton = new System.Windows.Forms.Button();
            this.NewGraph = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(191, 121);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(134, 46);
            this.updateButton.TabIndex = 4;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // NewGraph
            // 
            this.NewGraph.Location = new System.Drawing.Point(29, 121);
            this.NewGraph.Name = "NewGraph";
            this.NewGraph.Size = new System.Drawing.Size(134, 46);
            this.NewGraph.TabIndex = 9;
            this.NewGraph.Text = "NewGraph";
            this.NewGraph.UseVisualStyleBackColor = true;
            this.NewGraph.Click += new System.EventHandler(this.NewGraph_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 310);
            this.Controls.Add(this.NewGraph);
            this.Controls.Add(this.updateButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button NewGraph;
    }
}

