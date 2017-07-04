using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.Common
{
    public class NPOI_excel
    {
        HSSFWorkbook hssfworkbook;
        ISheet sheet1;
#region 原始邮政
		 

        //public void BuildExcel(List<string[]> array, string path)
        //{
        //    string[] title = new string[] { "序号", "客户编号", "大宗用户编号", "寄达局邮编", "寄达局名称", "收件人姓名", "收件人地址(国际邮件填英文)", "收件人电话", "邮件重量", "邮件号码", "邮件备注", "保险金额", "保价金额", "寄件人城市名（英文）", "寄件人地址（英文）", "寄件人电话", "内件类型代码", "验关报关标志", "验关物品类型", "内件名称", "内件数量", "单件重量", "单价", "产地", "协调号", "物品英文说明", "	寄件人省名（英文）", "寄件人姓名（英文）", "英文城市名", "英文州名	", "英文国家名", "回执单号码", "已贴票金额", "封发标志", "总资费", "保值金额", "内件成分说明", "图象文件名", "代收款" };

        //    hssfworkbook = new HSSFWorkbook();
        //    // 新建一个Excel页签
        //    sheet1 = hssfworkbook.CreateSheet("Sheet1");
        //    IRow row0 = sheet1.CreateRow(0);

        //    for (int i = 0; i < title.Length; i++)
        //    {
        //        //新建单元格
        //        ICell cell = row0.CreateCell(i);
        //        // 单元格赋值
        //        cell.SetCellValue(title[i]);
        //    }
        //    for (int i = 0; i < array.Count; i++)
        //    {
        //        IRow row1 = sheet1.CreateRow(i+1);
        //        row1.CreateCell(0).SetCellValue(i);//批次号
        //        row1.CreateCell(9).SetCellValue(array[i][0]);//批次号
        //        row1.CreateCell(27).SetCellValue(array[i][1]);//寄件人姓名
        //        row1.CreateCell(15).SetCellValue(array[i][2]);//寄件人电话
        //        row1.CreateCell(14).SetCellValue(array[i][3]);//中心名称 记做寄件人地址
        //        row1.CreateCell(5).SetCellValue(array[i][4]);//收件人姓名
        //        row1.CreateCell(7).SetCellValue(array[i][5]);//收件人电话
        //        row1.CreateCell(6).SetCellValue(array[i][6]);//收件人地址
        //        float weight = 0;
        //        float.TryParse(array[i][7].ToString(), out weight);
        //        if (weight == 0)
        //        {
        //            row1.CreateCell(8).SetCellValue("5000");//物品重量
        //        }
        //        else
        //        {

        //            if (weight < 50)
        //            {
        //                weight = weight * 1000;
        //            }
        //            row1.CreateCell(8).SetCellValue((weight).ToString("f0"));//物品重量
        //        }
        //        //  row1.CreateCell(37).SetCellValue(array[i][8]);//guid
        //    }
        //    //// 创建新增行
        //    //for (var i = 1; i < 10; i++)
        //    //{
        //    //    IRow row1 = sheet1.CreateRow(i);
        //    //    for (var j = 0; j < 10; j++)
        //    //    {
        //    //        //新建单元格
        //    //        ICell cell = row1.CreateCell(j);

        //    //        // 单元格赋值
        //    //        cell.SetCellValue("单元格" + j.ToString());
        //    //    }
        //    //}
        //    for (int columnNum = 0; columnNum <= 38; columnNum++)
        //    {
        //        int columnWidth = sheet1.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
        //        for (int rowNum = 1; rowNum <= sheet1.LastRowNum; rowNum++)//在这一列上循环行  
        //        {
        //            IRow currentRow = sheet1.GetRow(rowNum);
        //            ICell currentCell = currentRow.GetCell(columnNum);
        //            if (currentCell != null)
        //            {
        //                int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度  
        //                if (columnWidth < length + 1)
        //                {
        //                    columnWidth = length + 1;
        //                }//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符  
        //            }

        //        }
        //        sheet1.SetColumnWidth(columnNum, columnWidth * 256);
        //    }
        //    // 设置行宽度
        //    sheet1.SetColumnWidth(2, 10 * 256);

        //    //sheet1.SetColumnWidth()
        //    // ICellStyle styless=hssfworkbook.CreateCellStyle()
        //    // 获取单元格 并设置样式
        //    ICellStyle styleCell = hssfworkbook.CreateCellStyle();
        //    //居中
        //    styleCell.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        //    ////垂直居中
        //    //styleCell.VerticalAlignment = VerticalAlignment.Top;
        //    //ICellStyle cellStyle = hssfworkbook.CreateCellStyle();

        //    ////设置字体
        //    //IFont fontColorRed = hssfworkbook.CreateFont();
        //    //fontColorRed.Color = HSSFColor.OliveGreen.Red.Index;

        //    //styleCell.SetFont(fontColorRed);

        //    //  sheet1.GetRow(2).GetCell(2).CellStyle = styleCell;

        //    // 合并单元格
        //    // sheet1.AddMergedRegion(new CellRangeAddress(2, 4, 2, 5));


        //    // 输出Excel
        //    //string filename = "cnblogs.rhythmk.com.导出.xls";
        //    //var context = HttpContext.Current;
        //    //context.Response.ContentType = "application/vnd.ms-excel";
        //    //context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", context.Server.UrlEncode(filename)));
        //    //context.Response.Clear();


        //    MemoryStream file = new MemoryStream();
        //    hssfworkbook.Write(file);
        //    FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        //    BinaryWriter w = new BinaryWriter(fs);
        //    w.Write(file.ToArray());
        //    fs.Close();
        //    file.Close();


        //    //context.Response.BinaryWrite(file.GetBuffer());
        //    //context.Response.End();

        //}
        	#endregion
               public void BuildExcel(List<string[]> array, string path)
        {
            string[] title = new string[] { "记录序号", "用户自编号", "邮编", "寄达局", "收件人", "收件人电话", "收件人地址", "重量", "邮件号码", "保价金额", "保值金额", "保险费" };

            hssfworkbook = new HSSFWorkbook();
            // 新建一个Excel页签
            sheet1 = hssfworkbook.CreateSheet("Sheet1");
            IRow row0 = sheet1.CreateRow(0);

            for (int i = 0; i < title.Length; i++)
            {
                //新建单元格
                ICell cell = row0.CreateCell(i);
                // 单元格赋值
                cell.SetCellValue(title[i]);
            }
            for (int i = 0; i < array.Count; i++)
            {
                IRow row1 = sheet1.CreateRow(i+1);
                row1.CreateCell(0).SetCellValue(i+1);//批次号
                row1.CreateCell(8).SetCellValue(array[i][0]);//批次号
               // row1.CreateCell(27).SetCellValue(array[i][1]);//寄件人姓名
               // row1.CreateCell(15).SetCellValue(array[i][2]);//寄件人电话
                //row1.CreateCell(14).SetCellValue(array[i][3]);//中心名称 记做寄件人地址
                row1.CreateCell(4).SetCellValue(array[i][4]);//收件人姓名
                row1.CreateCell(5).SetCellValue(array[i][5]);//收件人电话
                row1.CreateCell(6).SetCellValue(array[i][6]);//收件人地址
                float weight = 0;
                float.TryParse(array[i][7]==null?"0":array[i][7].ToString(), out weight);
               // row1.CreateCell(7).SetCellValue(array[i][7].ToString());
                //if (weight == 0)
                //{
                //    row1.CreateCell(7).SetCellValue("5000");//物品重量
                //}
                //else
                //{

                    if (weight < 100)
                    {
                        weight = weight * 1000;
                    }
                    row1.CreateCell(7).SetCellValue((weight).ToString("f0"));//物品重量
                //}
                  row1.CreateCell(2).SetCellValue(array[i][8]);//邮编
                //  row1.CreateCell(37).SetCellValue(array[i][8]);//guid
            }
            //// 创建新增行
            //for (var i = 1; i < 10; i++)
            //{
            //    IRow row1 = sheet1.CreateRow(i);
            //    for (var j = 0; j < 10; j++)
            //    {
            //        //新建单元格
            //        ICell cell = row1.CreateCell(j);

            //        // 单元格赋值
            //        cell.SetCellValue("单元格" + j.ToString());
            //    }
            //}
            for (int columnNum = 0; columnNum <= 12; columnNum++)
            {
                int columnWidth = sheet1.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
                for (int rowNum = 1; rowNum <= sheet1.LastRowNum; rowNum++)//在这一列上循环行  
                {
                    IRow currentRow = sheet1.GetRow(rowNum);
                    ICell currentCell = currentRow.GetCell(columnNum);
                    if (currentCell != null)
                    {
                        int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度  
                        if (columnWidth < length + 1)
                        {
                            columnWidth = length + 1;
                        }//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符  
                    }

                }
                sheet1.SetColumnWidth(columnNum, columnWidth * 256);
            }
            // 设置行宽度
            sheet1.SetColumnWidth(2, 10 * 256);

            //sheet1.SetColumnWidth()
            // ICellStyle styless=hssfworkbook.CreateCellStyle()
            // 获取单元格 并设置样式
            ICellStyle styleCell = hssfworkbook.CreateCellStyle();
            //居中
            styleCell.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            ////垂直居中
            //styleCell.VerticalAlignment = VerticalAlignment.Top;
            //ICellStyle cellStyle = hssfworkbook.CreateCellStyle();

            ////设置字体
            //IFont fontColorRed = hssfworkbook.CreateFont();
            //fontColorRed.Color = HSSFColor.OliveGreen.Red.Index;

            //styleCell.SetFont(fontColorRed);

            //  sheet1.GetRow(2).GetCell(2).CellStyle = styleCell;

            // 合并单元格
            // sheet1.AddMergedRegion(new CellRangeAddress(2, 4, 2, 5));


            // 输出Excel
            //string filename = "cnblogs.rhythmk.com.导出.xls";
            //var context = HttpContext.Current;
            //context.Response.ContentType = "application/vnd.ms-excel";
            //context.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", context.Server.UrlEncode(filename)));
            //context.Response.Clear();


            MemoryStream file = new MemoryStream();
            hssfworkbook.Write(file);
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            BinaryWriter w = new BinaryWriter(fs);
            w.Write(file.ToArray());
            fs.Close();
            file.Close();


            //context.Response.BinaryWrite(file.GetBuffer());
            //context.Response.End();

        }
        public void BuildExcel_shjj(List<string[]> array, string path)
        {
            try
            {

                string[] title = new string[] { "运单号", "收件人", "收件邮箱", "收件省州", "收件城市", "收件单位", "收件地址", "收件邮编", "收件电话", "物品描述", "重量", "发件人", "发件电话", "备注" };

                hssfworkbook = new HSSFWorkbook();
                // 新建一个Excel页签
                sheet1 = hssfworkbook.CreateSheet("Sheet1");
                IRow row0 = sheet1.CreateRow(0);

                for (int i = 0; i < title.Length; i++)
                {
                    //新建单元格
                    ICell cell = row0.CreateCell(i);
                    // 单元格赋值
                    cell.SetCellValue(title[i]);
                }
                for (int i = 1; i <= array.Count; i++)
                {
                    IRow row1 = sheet1.CreateRow(i);
                    for (int j = 0; j < 14; j++)
                    {
                        row1.CreateCell(j).SetCellValue(array[i - 1][j]);
                    }

                }

                for (int columnNum = 0; columnNum <= 14; columnNum++)
                {
                    int columnWidth = sheet1.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
                    for (int rowNum = 1; rowNum <= sheet1.LastRowNum; rowNum++)//在这一列上循环行  
                    {
                        IRow currentRow = sheet1.GetRow(rowNum);
                        ICell currentCell = currentRow.GetCell(columnNum);
                        if (currentCell != null)
                        {
                            int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度  
                            if (columnWidth < length + 1)
                            {
                                columnWidth = length + 1;
                            }//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符  
                        }

                    }
                    sheet1.SetColumnWidth(columnNum, columnWidth * 256);
                }
                // 设置行宽度
                sheet1.SetColumnWidth(2, 10 * 256);

                //sheet1.SetColumnWidth()
                // ICellStyle styless=hssfworkbook.CreateCellStyle()
                // 获取单元格 并设置样式
                ICellStyle styleCell = hssfworkbook.CreateCellStyle();
                //居中
                styleCell.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;


                MemoryStream file = new MemoryStream();
                hssfworkbook.Write(file);
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(file.ToArray());
                fs.Close();
                file.Close();

            }
            catch (Exception ex)
            {
                
                //filterContext.Exception.Message可获取错误信息
                WriteLog.Write2Log(ex, "", "c:\\dz_datainput.log");
            }
        }
    }
}
