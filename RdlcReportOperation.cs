using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using Microsoft.Reporting.WebForms;

namespace ReportViewDemo
{
    /// <summary>
    /// RDLC操作类
    /// </summary>
    public class RdlcReportOperation
    {
        public string reportType = "PDF";
        /// <summary>
        /// 生成Rdlc文件类型
        /// </summary>
        public string ReportType
        {
            get
            {
                return reportType;
            }
            set
            {
                reportType = value;
            }
        }
        private string mimeType = string.Empty;
        private string encoding = string.Empty;
        private string fileNameExtension = string.Empty;
        private Warning[] warnings;
        private string[] streams;
        /// <summary>
        /// 设置RDlc格式 默认是按A4字设置
        /// </summary>
        public System.Text.StringBuilder sbDeviceInfo = new System.Text.StringBuilder();

        public RdlcReportOperation()
        {
            sbDeviceInfo.Append("<DeviceInfo>");
            sbDeviceInfo.Append("<OutputFormat>jpeg</OutputFormat>");
            sbDeviceInfo.Append("<PageWidth>29.7cm</PageWidth>");
            sbDeviceInfo.Append("<PageHeight>21cm</PageHeight>");
            sbDeviceInfo.Append("<MarginTop>0cm</MarginTop>");
            sbDeviceInfo.Append("<MarginLeft>0cm</MarginLeft>");
            sbDeviceInfo.Append("<MarginRight>0cm</MarginRight>");
            sbDeviceInfo.Append("<MarginBottom>0cm</MarginBottom>");
            sbDeviceInfo.Append("</DeviceInfo>");
        }

        /// <summary>
        /// 生成暂列金额分析表
        /// </summary>
        /// <param name="table"></param>
        /// <param name="project"></param>
        /// <param name="pdfPath"></param>
        public void CreateZlje(DataTable table, string pdfPath)
        {
            LocalReport localReport = new LocalReport();
            string rdlcPath = System.AppDomain.CurrentDomain.BaseDirectory + "Report\\zljefxbReport.rdlc";
            //获取RDLC 流
            var fileIo = GetRdlcFile(rdlcPath);
            localReport.LoadReportDefinition(fileIo);
            //加载数据
            //List<string> listProject = new List<string>();
            //ReportDataSource dataProjetc = new ReportDataSource();
            //dataProjetc.Name = "project";
            //dataProjetc.Value = listProject;
            //localReport.DataSources.Add(dataProjetc);
            
            //Render the report            
            var renderedBytes = localReport.Render(reportType, sbDeviceInfo.ToString(), out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            writeFile(renderedBytes, pdfPath);
        }


        /// <summary>
        /// 根据RDLC文件路径获取文件流
        /// </summary>
        /// <param name="rdlcPath"></param>
        /// <returns></returns>
        public MemoryStream GetRdlcFile(string rdlcPath)
        {
            if (!File.Exists(rdlcPath))
            {
                throw new Exception("文件不存在！");
            }
            string fileContent = File.ReadAllText(rdlcPath);
            MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent));
            return stream;
        }

        public static void writeFile(byte[] pReadByte, string fileName)
        {
            FileStream pFileStream = null;
            try
            {
                pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                pFileStream.Write(pReadByte, 0, pReadByte.Length);
            }
            catch
            {
            }
            finally
            {
                if (pFileStream != null)
                {
                    pFileStream.Close();
                }
            }

        }

    }
    
}
