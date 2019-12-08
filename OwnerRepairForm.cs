﻿//文件名：OwnerRepairForm.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyCommunity
{
  public partial class OwnerRepairForm : Form
  {
    public OwnerRepairForm()
    {
      InitializeComponent();
    }
    public string MyCommunity;
    private void 业主维修BindingNavigatorSaveItem_Click(object sender, EventArgs e)
    {
      if (Convert.ToDouble(this.修理费用TextBox.Text) + Convert.ToDouble(this.材料费用TextBox.Text) != Convert.ToDouble(this.费用合计TextBox.Text))
      {
        MessageBox.Show("修理费用、材料费用或费用合计金额有误，请仔细检查后再保存！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }
      this.Validate();
      this.业主维修BindingSource.EndEdit();
      this.业主维修TableAdapter.Update(this.dBCommunityDataSet.业主维修);
    }
    private void OwnerRepairForm_Load(object sender, EventArgs e)
    {
      // TODO: 这行代码将数据加载到表“dBCommunityDataSet.楼栋信息”中
      this.楼栋信息TableAdapter.Fill(this.dBCommunityDataSet.楼栋信息);
      // TODO: 这行代码将数据加载到表“dBCommunityDataSet.业主维修”中
      this.业主维修TableAdapter.Fill(this.dBCommunityDataSet.业主维修);            
    }
    private void 查询ToolStripButton_Click(object sender, EventArgs e)
    {
      this.业主维修BindingSource.Filter = "维修编号 LIKE '" + this.维修编号ToolStripTextBox.Text + "'";
    }
    private void 打印ToolStripButton_Click(object sender, EventArgs e)
    {//打印业主维修处理单
      this.printPreviewDialog1.Document = this.printDocument1;
      this.printPreviewDialog1.ShowDialog();     
    }
    private void 楼栋名称ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {//获取指定楼栋的业主姓名
      this.业主信息TableAdapter.Fill(this.dBCommunityDataSet.业主信息);
      this.业主信息BindingSource.Filter = "楼栋名称 = '" + 楼栋名称ComboBox.Text + "'";
    }
    private void 业主姓名ComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {//获取指定业主的编号
      if (this.业主姓名ComboBox.SelectedValue.ToString() != "System.Data.DataRowView")
      {
        this.业主编号TextBox.Text = this.业主姓名ComboBox.SelectedValue.ToString();
      }
    }
    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
      e.Graphics.DrawString(this.MyCommunity + "业主维修处理单", new Font("宋体", 20), Brushes.Black, 200, 90);
      e.Graphics.DrawString("维修编号：" + this.业主维修DataGridView.CurrentRow.Cells[0].Value.ToString(), new Font("宋体", 10), Brushes.Black, 100, 145);
      e.Graphics.DrawString("打印日期：" + DateTime.Now.ToLongDateString(), new Font("宋体", 10), Brushes.Black, 550, 145);
      e.Graphics.DrawLine(new Pen(Color.Black, (float)3.00), 100, 165, 720, 165);
      e.Graphics.DrawString("楼栋名称：" + this.业主维修DataGridView.CurrentRow.Cells[1].Value.ToString(), new Font("宋体", 10), Brushes.Black, 100, 175);
      e.Graphics.DrawString("业主编号：" + this.业主维修DataGridView.CurrentRow.Cells[2].Value.ToString(), new Font("宋体", 10), Brushes.Black, 320, 175);
      e.Graphics.DrawString("业主姓名：" + this.业主维修DataGridView.CurrentRow.Cells[3].Value.ToString(), new Font("宋体", 10), Brushes.Black, 550, 175);
      e.Graphics.DrawLine(new Pen(Color.Black), 100, 195, 720, 195);
      e.Graphics.DrawString("报修日期：" + DateTime.Parse(this.业主维修DataGridView.CurrentRow.Cells[4].Value.ToString()).ToShortDateString(), new Font("宋体", 10), Brushes.Black, 100, 200);
      e.Graphics.DrawString("接待人员：" + this.业主维修DataGridView.CurrentRow.Cells[5].Value.ToString(), new Font("宋体", 10), Brushes.Black, 400, 200);
      e.Graphics.DrawLine(new Pen(Color.Black), 100, 220, 720, 220);
      e.Graphics.DrawString("故障现象：", new Font("宋体", 10), Brushes.Black, 100, 225);
      e.Graphics.DrawString(this.业主维修DataGridView.CurrentRow.Cells[6].Value.ToString(), new Font("宋体", 10), Brushes.Black, new RectangleF(110, 245, 620, 90));
      e.Graphics.DrawLine(new Pen(Color.Black), 100, 335, 720, 335);
      e.Graphics.DrawString("处理意见：", new Font("宋体", 10), Brushes.Black, 100, 340);
      e.Graphics.DrawString(this.业主维修DataGridView.CurrentRow.Cells[7].Value.ToString(), new Font("宋体", 10), Brushes.Black, new RectangleF(110, 360, 620, 90));
      e.Graphics.DrawLine(new Pen(Color.Black), 100, 450, 720, 450);
      e.Graphics.DrawString("修理日期：" + DateTime.Parse(this.业主维修DataGridView.CurrentRow.Cells[8].Value.ToString()).ToShortDateString(), new Font("宋体", 10), Brushes.Black, 100, 455);
      e.Graphics.DrawString("修理人员：" + this.业主维修DataGridView.CurrentRow.Cells[9].Value.ToString(), new Font("宋体", 10), Brushes.Black, 250, 455);
      e.Graphics.DrawString("修理费用：" + this.业主维修DataGridView.CurrentRow.Cells[10].Value.ToString() + "元", new Font("宋体", 10), Brushes.Black, 370, 455);
      e.Graphics.DrawString("材料费用：" + this.业主维修DataGridView.CurrentRow.Cells[11].Value.ToString() + "元", new Font("宋体", 10), Brushes.Black, 480, 455);
      double My费用合计 = Convert.ToDouble(this.业主维修DataGridView.CurrentRow.Cells[10].Value.ToString()) + Convert.ToDouble(this.业主维修DataGridView.CurrentRow.Cells[11].Value.ToString());
      e.Graphics.DrawString("费用合计：" + My费用合计.ToString() + "元", new Font("宋体", 10), Brushes.Black, 590, 455);
      e.Graphics.DrawLine(new Pen(Color.Black), 100, 475, 720, 475);
      e.Graphics.DrawString("修理结果：", new Font("宋体", 10), Brushes.Black, 100, 480);
      e.Graphics.DrawString(this.业主维修DataGridView.CurrentRow.Cells[12].Value.ToString(), new Font("宋体", 10), Brushes.Black, new RectangleF(110, 500, 620, 240));
      e.Graphics.DrawLine(new Pen(Color.Black), 100, 765, 720, 765);
      e.Graphics.DrawString("补充说明：", new Font("宋体", 10), Brushes.Black, 100, 770);
      e.Graphics.DrawString(this.业主维修DataGridView.CurrentRow.Cells[13].Value.ToString(), new Font("宋体", 10), Brushes.Black, new RectangleF(110, 790, 620, 205));
      e.Graphics.DrawLine(new Pen(Color.Black, (float)3.00), 100, 1000, 720, 1000);       
    }
    private void 业主编号TextBox_TextChanged(object sender, EventArgs e)
    {
        this.业主姓名ComboBox.SelectedValue = this.业主编号TextBox.Text;
    }
  }
}