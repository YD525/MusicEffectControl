using JsonCore;
using MusicEffectControl.MusicManage;
using MusicEffectControl.SQLManager;
using MusicEffectControl.UIManage;
using Sound_Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicEffectControl
{
    public class DeFine
    {
        public static bool CanReSet = true;

        public static MainForm ActiveEdit = null;
        public static SqlCore<SQLiteHelper> GlobalDB = null;
        public static LocalSetting Setting = new LocalSetting();

        public const int DefMusicViewColumnCount = 5;
        public static MainWindow WorkingWin = null;

        public static void Init(MainWindow Win)
        {
            DeFine.Setting = Setting.GetLocalSetting();
            GlobalDB = new SqlCore<SQLiteHelper>(GetFullPath(@"\") + "Local.db");

            if (WorkingWin == null)
            {
                WorkingWin = Win;
            }
        }

        public static string GetFullPath(string Path)
        {
            string GetShellPath = System.Windows.Forms.Application.StartupPath;
            return GetShellPath + Path;
        }
    }

    public class LocalSetting
    {
        public bool UsingHotKey = false;
        public bool CanLoop = false;

        public void SetLocalSetting()
        {
            string GetJson = JsonHelper.CreatJson(this);
            DataHelper.WriteFile(DeFine.GetFullPath(@"\Config.ini"), Encoding.UTF8.GetBytes(GetJson));
        }

        public LocalSetting GetLocalSetting()
        {
            string GetStr = Encoding.UTF8.GetString(DataHelper.GetBytesByFilePath(DeFine.GetFullPath(@"\Config.ini")));

            var GetLocal = JsonHelper.JsonParse<LocalSetting>(GetStr);

            if (GetLocal == null)
            {
                GetLocal = new LocalSetting();
            }

            return GetLocal;
        }
    }
}
