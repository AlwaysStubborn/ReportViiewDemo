using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportViewDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RdlcReportOperation rdlcReport = new RdlcReportOperation();
            try
            {
                rdlcReport.CreateZlje(null, "d:\\test.pdf");
                Console.WriteLine("生成pdf文件到d:\\test.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.ToString() : ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
