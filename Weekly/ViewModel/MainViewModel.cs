using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Weekly.Model;
using Weekly.Public;

namespace Weekly.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        #region 私有变量
        private int _totalCnt = 0;
        string xlxsname; //xlxs名称
        IXmlWriter _xmlWriter = new Public.XmlWriter();
        IXmlReader _xmlReader = new Public.XmlReader();        
        #endregion

        #region 常量
        readonly string xmlpath;  //xml常量
        const string ROOTNODE = "Weekly"; //根节点
        const string DATETIME = "DateTime"; //日期节点
        const string CONTENT = "Content"; //内容
        const string NUMBER = "Number"; //编号
        const string COSTTIME = "CostTime"; //耗时
        const string CURRENTDATE = "CurrentDate"; //当前时间
        const string CATEGORY = "CATEGORY";//分类
        const string STATE = "STATE";//是否完成

        const string DATENOTE = @"/Weekly/DateTime";
        const string CONTENTNOTE = @"/Weekly/DateTime/Content";
        readonly string EXEPAHT = AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        #region 构造器
        public MainViewModel()
        {
            xmlpath = EXEPAHT + "week.xml";
        }
        #endregion

        #region 属性

        #region [Daily]
        private ObservableCollection<DailyContent> _daily = new ObservableCollection<DailyContent>();

        public ObservableCollection<DailyContent> Daily
        {
            get { return _daily; }
            set
            {
                _daily = value;
                OnPropertyChanged("Daily");
            }

        }
        #endregion

        #region [SelectedItem]
        private DailyContent _selectedItem = null;

        public DailyContent SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }

        }
        #endregion


        #region [SelectedDate]
        private DateTime _selectedDate = DateTime.Today;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }

        }
        #endregion


        #region [MyName]
        private string _myName = "王奇阳";

        public string MyName
        {
            get { return _myName; }
            set
            {
                _myName = value;
                OnPropertyChanged("MyName");
            }

        }
        #endregion



        #endregion //属性

        #region 命令

        public ICommand AddCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Daily.Add(new DailyContent() { Number = _totalCnt++, Content = string.Empty, Category = "开发", Hour = 2, State = true});
                });
            }
        }

        public ICommand CopyCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string tmpContent = string.Empty;
                    foreach(DailyContent dc in Daily)
                    {
                        tmpContent = tmpContent + dc.Number.ToString() + "、" + dc.Content + "\r\n";
                    }
                    Clipboard.SetDataObject(tmpContent);
                    MessageBox.Show("拷贝成功");
                });
            }
        }

        public ICommand DelCommand
        {
            get
            {
                return new RelayCommand<DailyContent>((dc) =>
                {
                    Daily.Remove(dc);
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (File.Exists(xmlpath))
                    {
                        _xmlWriter.Load(xmlpath);
                    }
                    else
                    {
                        _xmlWriter.AddXmlRootNode(ROOTNODE);
                    }

                    DateTimeNode();
                    _xmlWriter.Save(xmlpath);
                    MessageBox.Show("保存成功");
                });
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Daily.Clear();
                    _totalCnt = 0;
                    if (!File.Exists(xmlpath)) return;
                    _xmlReader.Load(xmlpath);
                    XmlNodeList xnlContent = _xmlReader.GetXmlNodeList(CONTENTNOTE);
                    for (int j = 0; j < xnlContent.Count; j++)
                    {
                        if (xnlContent[j].ParentNode.Attributes[CURRENTDATE].InnerText == SelectedDate.ToShortDateString())
                        {
                            Daily.Add(new DailyContent()
                            {
                                Category = xnlContent[j].Attributes[CATEGORY].InnerText,
                                Content = xnlContent[j].InnerText,
                                State = xnlContent[j].Attributes[STATE].InnerText == "是",
                                Hour = Convert.ToDouble(xnlContent[j].Attributes[COSTTIME].InnerText),
                                Number = Convert.ToInt32(xnlContent[j].Attributes[NUMBER].InnerText)
                            });
                            _totalCnt = Convert.ToInt32(xnlContent[j].Attributes[NUMBER].InnerText) + 1;
                        }
                    }
                
                });
            }
        }


        #region [ExportCommand]
        public ICommand ExportCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        int dayYear = SelectedDate.DayOfYear;
                        xlxsname = EXEPAHT + SelectedDate.Year.ToString() + "第"+ ((int)Math.Ceiling(dayYear * 1.0 / 7)).ToString() + "周周报.xlsx";
                        int rowcnt = 0, curRow = 0;
                        if (File.Exists(xlxsname)) File.Delete(xlxsname);
                        IWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();
                        ISheet sheet = book.CreateSheet("二开组");

                        #region 第一行
                        IRow row = sheet.CreateRow(rowcnt++);
                        ICell cell = null;
                        IFont font = book.CreateFont();
                        font.FontHeight = 24;
                        font.Boldweight = short.MaxValue;
                        FillCell(sheet, row, cell, book, font, 0, 0, 1, 11, "项目周报", HeaderColor.White);
                        cell = row.GetCell(rowcnt);
                        cell.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        #endregion

                        #region 第二行
                        curRow = rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 2, "提交人", HeaderColor.Aque);
                        FillCell(sheet, row, cell, book, curRow, curRow, 3, 5, MyName, HeaderColor.White);
                        FillCell(sheet, row, cell, book, curRow, curRow, 6, 7, "提交日期", HeaderColor.Aque);
                        FillCell(sheet, row, cell, book, curRow, curRow, 8, 11, SelectedDate.ToShortDateString(), HeaderColor.White);
                        #endregion

                        #region 第三行
                        curRow = rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 11, "项目进展情况", HeaderColor.Aque);
                        #endregion

                        #region 第四行\第五行
                        curRow = rowcnt++;
                        row = sheet.CreateRow(curRow);
                        sheet.CreateRow(curRow + 1);
                        FillCell(sheet, row, cell, book, curRow, curRow + 1, 1, 2, "项目总体进展情况", HeaderColor.Aque);
                        FillCell(sheet, row, cell, book, curRow, curRow, 3, 4, "所处阶段", HeaderColor.White);
                        FillCell(sheet, row, cell, book, curRow, curRow, 5, 7, "企业客户端定制项目", HeaderColor.White);

                        curRow = rowcnt++;
                        row = sheet.GetRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 3, 4, "下一发版日期", HeaderColor.White);
                        #endregion

                        #region 第六行
                        curRow = rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 11, "本周工作总结", HeaderColor.Aque);
                        #endregion

                        #region 第七行
                        curRow = rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 2, "项目活动描述", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 3, 3, "参加人", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 4, 4, "开始时间", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 5, 5, "结束时间", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 6, 6, "工作量（人时）", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 7, 7, "是否按计划完成", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 8, 11, "要关注的重点、要采取的措施", HeaderColor.Pink);
                        #endregion

                        int DataRowLogicWeek =  InsertCurWeekData(sheet, row, cell, book, curRow);

                        #region 第八行
                        curRow = DataRowLogicWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 5, "本周合计工作量（人时）", HeaderColor.Aque);
                        FillCell(sheet, row, cell, book, curRow, curRow, 6, 11, "",HeaderColor.White);
                        #endregion

                        #region 第九行
                        curRow = DataRowLogicWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 11, "计划未完成的总体原因", HeaderColor.Aque);
                        #endregion

                        #region 第十行
                        curRow = DataRowLogicWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 11, "", HeaderColor.White);
                        #endregion

                        #region 第十行
                        curRow = DataRowLogicWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 11, "下周工作计划", HeaderColor.Aque);
                        #endregion

                        #region 第十一行
                        curRow = DataRowLogicWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 2, "项目活动描述", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 3, 3, "参加人", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 4, 4, "开始时间", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 5, 5, "结束时间", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 6, 6, "工作量（人时）", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 7, 7, "是否本周遗留问题", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 8, 8, "是否关键路径", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 9, 11, "要关注的重点、要采取的措施", HeaderColor.Pink);
                        #endregion

                        int DataRowNextWeek = 1 + DataRowLogicWeek;

                        #region 第十二行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 11, "", HeaderColor.White);
                        #endregion

                        #region 第十三行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 5, "计划截至下周完成整个项目任务的百分比",HeaderColor.Aque);
                        FillCell(sheet, row, cell, book, curRow, curRow, 6, 11, "", HeaderColor.White);
                        #endregion

                        #region 第十四行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 11, "目前项目存在的主要问题和解决措施", HeaderColor.Aque);
                        #endregion

                        #region 第十五行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 6, "描述", HeaderColor.Pink);
                        FillCell(sheet, row, cell, book, curRow, curRow, 7, 11, "解决措施", HeaderColor.Pink);
                        #endregion

                        #region 第十六行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 6, "", HeaderColor.White);
                        FillCell(sheet, row, cell, book,curRow, curRow, 7, 11, "",HeaderColor.White);
                        #endregion

                        #region 第十七行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 11, "", HeaderColor.White);
                        #endregion

                        #region 第十八行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 6, "目前主要风险", HeaderColor.Aque);
                        #endregion

                        #region 第十九行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 6, "描述", HeaderColor.Aque);
                        FillCell(sheet, row, cell, book, curRow, curRow, 7, 11, "解决措施",HeaderColor.Aque);
                        #endregion

                        #region 第二十行
                        curRow = DataRowNextWeek + rowcnt++;
                        row = sheet.CreateRow(curRow);
                        FillCell(sheet, row, cell, book, curRow, curRow, 1, 6, "", HeaderColor.White);
                        FillCell(sheet, row, cell, book, curRow, curRow, 7, 11, "", HeaderColor.White);
                        #endregion

                        using (FileStream fs = new FileStream(xlxsname, FileMode.Create, FileAccess.Write))
                        {
                            book.Write(fs);
                        }

                        System.Diagnostics.Process.Start(xlxsname);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //throw;
                    }

                    
                });
            }
        }
        #endregion

        #endregion

        #region 方法
        private void DateTimeNode()
        {
            //判断是否存在同名的节点
            SameDateTime();
            //判断是否是根节点
            if (_xmlWriter.CurrentNode.Name != ROOTNODE)
            {
                _xmlWriter.Save(xmlpath);
                _xmlReader.Load(xmlpath);
                var nodelList = _xmlReader.GetXmlNodeList(@"/Weekly");
                if (nodelList.Count <= 0) return;
                _xmlWriter.MoveToNode(@"/Weekly", nodelList.Count - 1);
            }
            _xmlWriter.AddSubXmlNode(DATETIME);
            _xmlWriter.AddXmlAtrribute(CURRENTDATE, SelectedDate.ToShortDateString());

            foreach (DailyContent dc in Daily)
            {
                if (_xmlWriter.CurrentNode.Name != DATETIME)
                {
                    _xmlWriter.AddSibXmlNode(CONTENT, dc.Content);
                }
                else
                {
                    _xmlWriter.AddSubXmlNode(CONTENT, dc.Content);
                }
                _xmlWriter.AddXmlAtrribute(NUMBER, dc.Number.ToString());
                _xmlWriter.AddXmlAtrribute(COSTTIME, dc.Hour.ToString());
                _xmlWriter.AddXmlAtrribute(CATEGORY, dc.Category.ToString());
                _xmlWriter.AddXmlAtrribute(STATE, dc.State ? "是" : "否");
            }
        }

        private bool SameDateTime()
        {
            _xmlWriter.Save(xmlpath);
            _xmlReader.Load(xmlpath);
            var nodeList = _xmlReader.GetXmlNodeList(DATENOTE);
            if (nodeList.Count <= 0) return false;
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlAttribute xab = _xmlReader.GetXmlAttribute(DATENOTE, i, CURRENTDATE);
                if (xab == null) return false;
                if (xab.InnerText == SelectedDate.ToShortDateString())
                {
                    _xmlWriter.RemoveChild(DATENOTE, i);
                    return true;
                }
            }
            return false;
        }

        private void FillCell(ISheet sheet, IRow row, ICell cell, IWorkbook book, IFont font, int rowidx, int endrow,
            int colidx, int endcol, string content, HeaderColor header)
        {
            List<ICell> cells = FillCell(sheet, row, cell, book, rowidx, endrow, colidx, endcol, content, header);
            foreach (ICell item in cells)
            {
                item.CellStyle.SetFont(font);
            }
        }

        private List<ICell> FillCell(ISheet sheet,IRow row, ICell cell, IWorkbook book, int rowidx, int endrow, 
            int colidx, int endcol, string content, HeaderColor header)
        {
            List<ICell> cells = new List<ICell>();
            ICellStyle style = book.CreateCellStyle();
            cell = row.CreateCell(colidx);
            cell.SetCellValue(content);
            if (colidx == endcol && rowidx == endrow)
            {
                sheet.AddMergedRegion(new CellRangeAddress(rowidx, endrow, colidx, endcol));
                cell.CellStyle = CustomStyle(style,header);
                cells.Add(cell);
            }
            else
            {
                for (int j = rowidx; j <= endrow; j++)
                {
                    IRow rowregion = sheet.GetRow(j);
                    if (rowregion != row)
                        cell = rowregion.CreateCell(colidx);
                    for (int i = colidx + 1; i <= endcol; i++)
                    {
                        cell = rowregion.CreateCell(i);
                    }
                }
                CellRangeAddress region = new CellRangeAddress(rowidx, endrow, colidx, endcol);
                sheet.AddMergedRegion(region);
                for (int i = region.FirstRow; i <= region.LastRow; i++)
                {
                    IRow rowregion = sheet.GetRow(i);
                    for (int j = region.FirstColumn; j <= region.LastColumn; j++)
                    {
                        ICell singleCell = rowregion.GetCell(j);
                        singleCell.CellStyle = CustomStyle(style, header);
                        cells.Add(singleCell);
                    }
                }
            }
            return cells;
        }

        private int InsertCurWeekData(ISheet sheet, IRow row, ICell cell, IWorkbook book, int rowNumber)
        {
            int dayCnt = 1; //天计数
            int dayRowCnt = dayCnt; //行号
            int dailyCnt = 1; //日志条数
            int rowMerge = 0; //要合并多少行

            //算一周的时间
            int dayweek = (int)SelectedDate.DayOfWeek;
            if (dayweek <= 0) return 0;
            string sCurDay = SelectedDate.AddDays(-dayweek + 1).ToShortDateString();

            //加载xml
            _xmlReader.Load(xmlpath);
            XmlNodeList xnlDate = _xmlReader.GetXmlNodeList(DATENOTE);
            for (int i = 0; i < xnlDate.Count; i++)
            {
                if (xnlDate[i].Attributes[CURRENTDATE].InnerText == sCurDay)
                {
                    XmlNodeList xmlContent = _xmlReader.GetXmlNodeList(CONTENTNOTE);
                    for (int j = 0; j < xmlContent.Count; j++)
                    {
                        if (xmlContent[j].ParentNode.Attributes[CURRENTDATE].InnerText != sCurDay) continue;
                        dayRowCnt = rowNumber + dailyCnt;
                        row = sheet.CreateRow(dayRowCnt);
                        FillCell(sheet, row, cell, book, dayRowCnt, dayRowCnt, 1, 1, xmlContent[j].Attributes[CATEGORY].InnerText, HeaderColor.White);
                        FillCell(sheet, row, cell, book, dayRowCnt, dayRowCnt, 2, 2, xmlContent[j].InnerText, HeaderColor.White);
                        FillCell(sheet, row, cell, book, dayRowCnt, dayRowCnt, 6, 6, xmlContent[j].Attributes[COSTTIME].InnerText, HeaderColor.White);
                        FillCell(sheet, row, cell, book, dayRowCnt, dayRowCnt, 7, 7, xmlContent[j].Attributes[STATE].InnerText, HeaderColor.White);
                        rowMerge++;
                        dailyCnt++;
                    }

                    int rowRange = dayRowCnt - rowMerge + 1;
                    row = sheet.GetRow(rowRange);
                    FillCell(sheet, row, cell, book, rowRange, dayRowCnt, 4, 4, sCurDay, HeaderColor.White);
                    cell = row.GetCell(4);
                    cell.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    cell.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    FillCell(sheet, row, cell, book, rowRange, dayRowCnt, 5, 5, sCurDay, HeaderColor.White);
                    cell = row.GetCell(5);
                    cell.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    cell.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                    rowMerge = 0;
                }
                if (i == xnlDate.Count - 1 && dayCnt <= dayweek)
                {  
                    i = -1;
                    dayCnt++;
                    sCurDay = SelectedDate.AddDays(-dayweek + dayCnt).ToShortDateString();
                }
            }

            //填充全单元格内容
            row = sheet.GetRow(rowNumber + 1);
            FillCell(sheet, row, cell, book, rowNumber + 1, dayRowCnt, 3, 3, MyName, HeaderColor.White);
            cell = row.GetCell(3);
            cell.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            cell.CellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            return dailyCnt;
        }

        private ICellStyle CustomStyle(ICellStyle style, IFont font, HeaderColor header)
        {

            style = CustomStyle(style, header);
            style.SetFont(font);
            return style;
        }

        private ICellStyle CustomStyle(ICellStyle style, HeaderColor header)
        {
            style.FillForegroundColor = (short)header;
            style.FillPattern = FillPattern.SolidForeground;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;

            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;


            return style;
        }

        #endregion
    }

}
