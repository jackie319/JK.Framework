using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Manager
{
    public class Exp
    {
        //Main
        //static class Program
        //{
        //    /// <summary>
        //    /// 应用程序的主入口点。
        //    /// </summary>
        //    [STAThread]
        //    static void Main()
        //    {
        //        Application.EnableVisualStyles();
        //        Application.SetCompatibleTextRenderingDefault(false);

        //        UpdateManager manager = new UpdateManager("");
        //        string fileName = Application.StartupPath + @"\update\HouseManage.Container.AutoUpdate.exe";
        //        if (manager.IsNeedUpdate)
        //        {
        //            manager.StartExe(fileName);
        //        }
        //        Application.Run(new MainForm());

        //    }

        //}

        //Update

        //public partial class MainForm : Form
        //{
        //    public MainForm()
        //    {
        //        InitializeComponent();
        //    }

        //    private void MainForm_Load(object sender, EventArgs e)
        //    {
        //        //string path = @"..\VersionConfig.json";//自己用
        //        string parentPath = VersionJsonUtility.GetParentPath(Application.StartupPath);
        //        string path = parentPath + @"\VersionConfig.json";//被调
        //        UpdateManager manager = new UpdateManager(path);
        //        this.msgLable.Text = "正在检查更新……";
        //        try
        //        {
        //            if (manager.IsNeedUpdate)
        //            {
        //                this.msgLable.Text = "正在下载更新……";
        //                //manager.DownLoadZipAsync();
        //                manager.DownLoadZip();
        //                this.msgLable.Text = "正在解压……";
        //                manager.UnZip();
        //                manager.ChangeVersionNum();
        //                this.msgLable.Text = "更新完毕";

        //                string fileName = parentPath + @"\HouseManage.Container.exe";
        //                manager.StartExe(fileName);
        //            }
        //            else
        //            {
        //                this.msgLable.Text = "已是最新版，不需要更新";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            this.msgLable.Text = "更新失败！" + ex.Message;
        //        }

        //    }

        //}
    }
}
