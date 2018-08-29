using System;
using System.IO;
using System.Windows.Forms;

namespace SSHCHANGERFREE
{
    class CTLError
    {
        public static string _path = Application.StartupPath;
        //public static bool WriteError(string file_name,string Title,string ex)
        //{
        //    try
        //    {
        //        FileInfo fileInfo=new FileInfo(Path.Combine(_path,file_name));
        //        if (!fileInfo.Exists)
        //            fileInfo.Create();
        //        FileStream fileStream=new FileStream(fileInfo.FullName,FileMode.Append);
        //        StreamWriter writer=new StreamWriter(fileStream);
        //        writer.WriteLine("");
        //        writer.WriteLine("");
        //        writer.WriteLine("");
        //        writer.WriteLine("");
        //        writer.WriteLine("-------------------------"+DateTime.Now+"---------------------------------");
        //        writer.WriteLine(Title);
        //        writer.WriteLine(ex);
        //        writer.Dispose();
        //        writer.Close();
        //        fileStream.Dispose();
        //        fileStream.Close();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
        //    }
            
        //}
        public static bool WriteError(string title, string ex)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(_path, "ErrorLog"+DateTime.Now.ToString("ddMMyy")));
                if (!fileInfo.Exists)
                {
                    fileInfo.Create();
                    
                }
                FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Append);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("-------------------------" + DateTime.Now + "---------------------------------");
                writer.WriteLine("Có vấn đề sảy ra tại " + title);
                writer.WriteLine(ex);
                writer.Dispose();
                writer.Close();
                fileStream.Dispose();
                fileStream.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public static void XoaErrorlog()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(Application.StartupPath);
                foreach (FileInfo fileerr in d.GetFiles("ErrorLog*"))
                {
                    DateTime ngayxoa=DateTime.Now.AddDays(-7);
                    if (fileerr.LastWriteTime < ngayxoa)
                    {
                        fileerr.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                CTLError.WriteError("Loi Xoa Errorlog", ex.Message);
                return;
            }
            
        }
    }
}
