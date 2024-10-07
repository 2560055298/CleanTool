namespace CleanTool;
using System;
using System.Diagnostics;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    // 定义界面控件
    private Button cleanButton;
    private CheckBox checkBoxWeChat;
    private CheckBox checkBoxQQ;
    private CheckBox checkBoxBrowser;
    // 版本号
    private Label versionLabel;

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
        cleanButton = new Button();
        checkBoxWeChat = new CheckBox();
        checkBoxQQ = new CheckBox();
        checkBoxBrowser = new CheckBox();
        versionLabel = new Label();
        SuspendLayout();
        // 
        // cleanButton
        // 
        cleanButton.FlatAppearance.BorderColor = Color.DarkGray;
        cleanButton.FlatStyle = FlatStyle.Flat;
        cleanButton.Font = new Font("Microsoft Sans Serif", 11F);
        cleanButton.Location = new Point(160, 140);
        cleanButton.Name = "cleanButton";
        cleanButton.Size = new Size(110, 32);
        cleanButton.TabIndex = 0;
        cleanButton.Text = "立即清理";
        cleanButton.Click += CleanButton_Click;
        // 
        // checkBoxWeChat
        // 
        checkBoxWeChat.AutoSize = true;
        checkBoxWeChat.Checked = true;
        checkBoxWeChat.CheckState = CheckState.Checked;
        checkBoxWeChat.Font = new Font("Microsoft Sans Serif", 11F);
        checkBoxWeChat.Location = new Point(24, 32);
        checkBoxWeChat.Name = "checkBoxWeChat";
        checkBoxWeChat.Size = new Size(117, 22);
        checkBoxWeChat.TabIndex = 1;
        checkBoxWeChat.Text = "清理微信缓存";
        checkBoxWeChat.CheckedChanged += checkBoxWeChat_CheckedChanged;
        // 
        // checkBoxQQ
        // 
        checkBoxQQ.AutoSize = true;
        checkBoxQQ.Checked = true;
        checkBoxQQ.CheckState = CheckState.Checked;
        checkBoxQQ.Font = new Font("Microsoft Sans Serif", 11F);
        checkBoxQQ.Location = new Point(24, 60);
        checkBoxQQ.Name = "checkBoxQQ";
        checkBoxQQ.Size = new Size(111, 22);
        checkBoxQQ.TabIndex = 2;
        checkBoxQQ.Text = "清理QQ缓存";
        // 
        // checkBoxBrowser
        // 
        checkBoxBrowser.AutoSize = true;
        checkBoxBrowser.Checked = true;
        checkBoxBrowser.CheckState = CheckState.Checked;
        checkBoxBrowser.Font = new Font("Microsoft Sans Serif", 11F);
        checkBoxBrowser.Location = new Point(24, 88);
        checkBoxBrowser.Name = "checkBoxBrowser";
        checkBoxBrowser.Size = new Size(325, 22);
        checkBoxBrowser.TabIndex = 3;
        checkBoxBrowser.Text = "清理（Edge、google、360系列）浏览器缓存";
        // 
        // versionLabel
        // 
        versionLabel.Anchor = AnchorStyles.Bottom;
        versionLabel.AutoSize = true;
        versionLabel.Location = new Point(381, 235);
        versionLabel.Name = "versionLabel";
        versionLabel.Size = new Size(68, 17);
        versionLabel.TabIndex = 4;
        versionLabel.Text = "版本号: 1.0";
        // 
        // Form1
        // 
        ClientSize = new Size(471, 261);
        Controls.Add(cleanButton);
        Controls.Add(checkBoxWeChat);
        Controls.Add(checkBoxQQ);
        Controls.Add(checkBoxBrowser);
        Controls.Add(versionLabel);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "缓存清理工具：漫漫求出品";
        ResumeLayout(false);
        PerformLayout();
    }

    private void CleanButton_Click(object sender, EventArgs e)
    {
        // 弹出确认框
        DialogResult result = MessageBox.Show("确定要清理缓存吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        // 如果用户选择 "是"，则执行清理操作
        if (result == DialogResult.Yes)
        {
            // 微信缓存清理
            if (checkBoxWeChat.Checked)
            {
                ClearWeChatCache();
            }

            // QQ缓存清理
            if (checkBoxQQ.Checked)
            {
                ClearQQCache();
            }

            // 浏览器缓存清理
            if (checkBoxBrowser.Checked)
            {
                ClearBrowserCache();
            }

            MessageBox.Show("清理完成！");
        }
    }

    /*
        微信缓存清理
    */
    private void ClearWeChatCache()
    {
        string userName = Environment.UserName;
        string weChatCachePath = $@"C:\Users\{Environment.UserName}\Documents\WeChat Files";
        Console.WriteLine("微信路径: " + weChatCachePath);

        if (Directory.Exists(weChatCachePath))
        {
            CloseBrowser("WeChat");
            Directory.Delete(weChatCachePath, true);
        }
    }

    /*
       QQ缓存清理
    */
    private void ClearQQCache()
    {
        string userName = Environment.UserName;
        string qqCachePath = $@"C:\Users\{Environment.UserName}\Documents\Tencent Files";
        if (Directory.Exists(qqCachePath))
        {
            CloseBrowser("QQ");
            Directory.Delete(qqCachePath, true);
        }
    }

    /*
       游览器缓存清理
    */
    private void ClearBrowserCache()
    {

        string userName = Environment.UserName;

        // Edge 游览器
        string edgeCachePath = $@"C:\Users\{Environment.UserName}\AppData\Local\Microsoft\Edge";
        if (Directory.Exists(edgeCachePath))
        {
            CloseBrowser("msedge");
            Directory.Delete(edgeCachePath, true);
        }

        // Google游览器
        string googleCachePath = $@"C:\Users\{Environment.UserName}\AppData\Local\Google\Chrome";
        if (Directory.Exists(googleCachePath))
        {
            CloseBrowser("chrome");
            Directory.Delete(googleCachePath, true);
        }

        // 360 极速游览器
        string speed360CachePath = $@"C:\Users\{Environment.UserName}\AppData\Local\360Chrome\Chrome\User Data";
        if (Directory.Exists(speed360CachePath))
        {
            CloseBrowser("360chrome");
            Directory.Delete(speed360CachePath, true);
        }


        // 360 安全游览器
        string safe360CachePath = $@"C:\Users\{Environment.UserName}\AppData\Roaming\360se6\User Data";
        if (Directory.Exists(safe360CachePath))
        {
            CloseBrowser("360se");
            Directory.Delete(safe360CachePath, true);
        }
    }

    /// <summary>
    /// 关闭指定名称的浏览器进程
    /// </summary>
    /// <param name="browserName">浏览器进程名称</param>
    static void CloseBrowser(string browserName)
    {
        try
        {
            // 获取指定名称的进程
            Process[] processes = Process.GetProcessesByName(browserName);

            // 关闭进程
            foreach (Process process in processes)
            {
                process.Kill(); // 终止进程
                process.WaitForExit(); // 等待进程退出
                Console.WriteLine($"{browserName} 已关闭");
            }

            if (processes.Length == 0)
            {
                Console.WriteLine($"{browserName} 未运行");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"无法关闭 {browserName}: {ex.Message}");
        }
    }
    #endregion
}
