using MusicEffectControl.ConvertManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static MusicEffectControl.MusicManage.MusicsAdo;

namespace MusicEffectControl.MusicManage
{
   
    public class MusicsAdo
    {
        public static bool CanBinDingHotKey(string Type,string HotKey,ref int Rowid)
        {
            string SqlOrder = "Select Rowid From Musics Where Type = '{0}' And HotKey = '{1}'";
            int GetRowid = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder,Type,HotKey)));
            if (GetRowid >= 0)
            {
                Rowid = GetRowid;
                return false;
            }
            return true;
        }

        public static List<MusicItem> QueryMusicHaveHotKey(string Type)
        {
            List<MusicItem> MusicItems = new List<MusicItem>();

            string SqlOrder = "Select Rowid,* From Musics Where Type = '{0}' And HotKey != '' order by SortNo desc";

            DataTable NTable = DeFine.GlobalDB.ExecuteQuery(string.Format(SqlOrder, Type));

            for (int i = 0; i < NTable.Rows.Count; i++)
            {
                MusicItems.Add(new MusicItem(
                    NTable.Rows[i]["Rowid"],
                    NTable.Rows[i]["Type"],
                    NTable.Rows[i]["MusicName"],
                    NTable.Rows[i]["Len"],
                    NTable.Rows[i]["SortNo"],
                    NTable.Rows[i]["HotKey"],
                    NTable.Rows[i]["Source"],
                    NTable.Rows[i]["TimeRange"]
                ));
            }
            return MusicItems;
        }

        public static bool CheckMusic(string Type,string MusicName)
        {
            string SqlOrder = "Select Rowid From Musics Where Type = '{0}' And MusicName = '{1}'";
            int GetRowid = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder,Type,MusicName)));

            if (GetRowid >= 0)
            {
                return true;
            }

            return false;
        }

        public static bool AddMusic(MusicItem Item)
        {
            if (!CheckMusic(Item.Type, Item.MusicName))
            {
                string SqlOrder = "Insert Into Musics(Type,MusicName,Len,SortNo,HotKey,Source,TimeRange)Values('{0}','{1}',{2},{3},'{4}','{5}','{6}')";
                int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,Item.Type,Item.MusicName,Item.Len,Item.SortNo,Item.HotKey,Item.Source,Item.TimeRange));
                if (State != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<MusicItem> QueryMusicByType(string Type)
        {
            List<MusicItem> MusicItems = new List<MusicItem>();

            string SqlOrder = "Select Rowid,* From Musics Where Type = '{0}' order by SortNo desc";

            DataTable NTable = DeFine.GlobalDB.ExecuteQuery(string.Format(SqlOrder, Type));

            for (int i = 0; i < NTable.Rows.Count; i++)
            {
                MusicItems.Add(new MusicItem(
                    NTable.Rows[i]["Rowid"],
                    NTable.Rows[i]["Type"],
                    NTable.Rows[i]["MusicName"],
                    NTable.Rows[i]["Len"],
                    NTable.Rows[i]["SortNo"],
                    NTable.Rows[i]["HotKey"],
                    NTable.Rows[i]["Source"],
                    NTable.Rows[i]["TimeRange"]
                ));
            }
            return MusicItems;
        }

        public static int GetMusicCountByType(string Type)
        {
            string SqlOrder = "Select Count(*) From Musics Where Type ='{0}'";
            int GetCount = ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder,Type)));
            return GetCount;
        }

        public static bool SetMusicSortNo(int Rowid, int SortNo)
        {
            string SqlOrder = "UPDate Musics Set SortNo = {1} Where Rowid = {0}";
            int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,Rowid,SortNo));
            if (State != 0)
            {
                return true;
            }
            return false;
        }

        public static bool DeleteMusic(int Rowid)
        {
            string SqlOrder = "Delete From Musics Where Rowid = {0}";
            int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,Rowid));
            if (State != 0)
            {
                return true;
            }

            return false;
        }

        public static bool ModifyMusic(MusicItem Item)
        {
            string SqlOrder = "UPDate Musics Set Type = '{1}',MusicName = '{2}',Len = {3},SortNo = {4},HotKey = '{5}',Source = '{6}',TimeRange = '{7}' Where Rowid = {0}";
            int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,Item.Rowid,Item.Type,Item.MusicName,Item.Len,Item.SortNo,Item.HotKey,Item.Source,Item.TimeRange));
            if (State != 0)
            {
                return true;
            }
            return false;
        }

        public static int GetMaxSortByType(string Type)
        {
            string SqlOrder = "Select MAX(SortNo) From Musics Where Type = '{0}'";

            return ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder,Type)));
        }

        public static int GetMinSortByType(string Type)
        {
            string SqlOrder = "Select MIN(SortNo) From Musics Where Type = '{0}'";

            return ConvertHelper.ObjToInt(DeFine.GlobalDB.ExecuteScalar(string.Format(SqlOrder, Type)));
        }

        public static MusicItem GetFullInFoByRowid(int Rowid)
        {
            string SqlOrder = "Select Rowid,* From Musics Where Rowid = {0}";

            DataTable NTable = DeFine.GlobalDB.ExecuteQuery(string.Format(SqlOrder,Rowid));

            if (NTable.Rows.Count > 0)
            {
                return new MusicItem(
                         NTable.Rows[0]["Rowid"],
                         NTable.Rows[0]["Type"],
                         NTable.Rows[0]["MusicName"],
                         NTable.Rows[0]["Len"],
                         NTable.Rows[0]["SortNo"],
                         NTable.Rows[0]["HotKey"],
                         NTable.Rows[0]["Source"],
                         NTable.Rows[0]["TimeRange"]
                     );
            }

            return new MusicItem();
        }

        public class MusicItem
        {
            public int Rowid = 0;
            public string Type = "";
            public string MusicName = "";
            public int Len = 0;
            public int SortNo = 0;
            public string HotKey = "";
            public string Source = "";
            public string TimeRange = "";

            public MusicItem() { }

            public MusicItem(string Type,string MusicName,int Len,int SortNo,string HotKey,string Source,string TimeRange)
            {
                this.Type = Type;
                this.MusicName = MusicName;
                this.Len = Len;
                this.SortNo = SortNo;
                this.HotKey = HotKey;
                this.Source = Source;
                this.TimeRange = TimeRange;
            }

            public MusicItem(object Rowid, object Type, object MusicName, object Len, object SortNo, object HotKey, object Source,object TimeRange)
            {
                this.Rowid = ConvertHelper.ObjToInt(Rowid);
                this.Type = ConvertHelper.ObjToStr(Type);
                this.MusicName = ConvertHelper.ObjToStr(MusicName);
                this.Len = ConvertHelper.ObjToInt(Len);
                this.SortNo = ConvertHelper.ObjToInt(SortNo);
                this.HotKey = ConvertHelper.ObjToStr(HotKey);
                this.Source = ConvertHelper.ObjToStr(Source);
                this.TimeRange = ConvertHelper.ObjToStr(TimeRange);
            }
        }

    }
    public class MusicHelper
    {
        public static void AddMusicByPath(string Path, string Tag)
        {
            string SqlOrder = "Delete From Musics Where Type = '{0}'";
            int State = DeFine.GlobalDB.ExecuteNonQuery(string.Format(SqlOrder,Tag));

            DeFine.GlobalDB.ExecuteNonQuery("Vacuum");

            foreach (var GetFile in DataHelper.GetAllFile(Path, new List<string>() { ".mp3" }))
            {
                string FilePath = GetFile.FilePath;
                string FileName = FilePath.Substring(FilePath.LastIndexOf(@"\") + @"\".Length);
                string GetName = FileName.Split('.')[0];

                MusicsAdo.AddMusic(new MusicItem(
                                          Tag,
                                          GetName,
                                          ConvertHelper.ObjToInt(new FileInfo(FilePath).Length / 1024),
                                          0,
                                          "",
                                          FilePath,
                                          ""
                                          ));
            }
        }

        public static List<MusicItem> GetMusicList(string Tag)
        {
            return MusicsAdo.QueryMusicByType(Tag);
        }
    }
}
