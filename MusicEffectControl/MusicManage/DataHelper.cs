using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicEffectControl.MusicManage
{
    public class DataHelper
    {
        public static byte[] GetBytesByFilePath(string strFile)
        {
            byte[] photo_byte = null;

            if (File.Exists(strFile))
            {
                using (FileStream fs =
                          new FileStream(strFile, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        photo_byte = br.ReadBytes((int)fs.Length);
                    }
                }
            }
            else
            {
                return new byte[0];
            }

            return photo_byte;
        }

        public static void WriteFile(string TargetPath, byte[] Data)
        {
            FileStream FS = new FileStream(TargetPath, FileMode.Create);
            FS.Write(Data, 0, Data.Length);
            FS.Close();
            FS.Dispose();
        }

        public static List<FileInformation> GetAllFile(string filepath, List<string> filetype = null)
        {
            DirectoryAllFiles.FileList.Clear();
            List<FileInformation> list = DirectoryAllFiles.GetAllFiles(new System.IO.DirectoryInfo(filepath));
            List<FileInformation> nlist = new List<FileInformation>();

            if (filetype == null == false)
            {
                nlist.AddRange(list);
                foreach (var autoget in list)
                {
                    if (filetype.Contains(autoget.Filetype) == false)
                    {
                        nlist.Remove(autoget);
                    }
                }

                return nlist;
            }

            return list;
        }
    }

    public class FileInformation
    {
        public string Filetype = "";
        public string FileName = "";
        public string FilePath = "";

        public List<string> FileCode = new List<string>();
    }

    public class DirectoryAllFiles
    {
        public static List<FileInformation> FileList = new List<FileInformation>();
        public static List<FileInformation> GetAllFiles(DirectoryInfo dir)
        {

            List<FileInfo> allFile = new List<FileInfo>(); ;

            try
            {
                allFile = dir.GetFiles().ToList();
            }
            catch { }

            foreach (FileInfo fi in allFile)
            {
                FileList.Add(new FileInformation { FileName = fi.Name, FilePath = fi.FullName, Filetype = fi.Extension });
            }

            List<DirectoryInfo> allDir = new List<DirectoryInfo>();

            try
            {
                allDir = dir.GetDirectories().ToList();
            }
            catch { }

            foreach (DirectoryInfo d in allDir)
            {
                GetAllFiles(d);
            }
            return FileList;
        }
    }
}
