using DefectChecker.View;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Aqrose.Framework.Utility.MessageManager;

namespace DefectChecker
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += OnGlobalError;
            AppDomain.CurrentDomain.UnhandledException += OnDomainError;

            Process[] app = Process.GetProcessesByName("DefectChecker");
            if (app.Length > 1)
            {
                MessageBox.Show("软件已运行，本次启动将退出");
                System.Environment.Exit(0);
            }

            try
            {
                MessageManager.Instance();
                MessageManagerLogger.Instance().Init(Application.StartupPath + @"\Log\", "DefectChecker", 30);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                ErrorMessage form = new ErrorMessage();
                form.Name = "异常提醒";
                form.Show("打开过程中出现异常, 异常信息：" + e.Message + "\r\nStackTrace\r\n" + e.StackTrace);
                form.Close();
                throw;
            }

            MessageManagerLogger.Instance().ShutDown();
            MessageManager.Instance().ShutDown();
        }

        static void OnGlobalError(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            MessageManager.Instance().Alarm("OnGlobalError: " + sender.ToString() + exception.Message + " stack: " + exception.StackTrace);
        }

        static void OnDomainError(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;
            MessageManager.Instance().Alarm("OnDomainError: " + sender.ToString() + exception.Message + " stack: " + exception.StackTrace);
        }
    }
}
