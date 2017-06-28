using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Program_launcher
{
    class Programs
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public ImageSource FileIcon { get; set; } 

        public Programs(string Path, string Name)
        {
            FilePath = Path;
            FileName = Name;
            FileIcon = ConvertToSource(Path);
        }

        public static List<Programs> LoadPrograms()
        {
            List<Programs> TempList = new List<Programs>();
            string[] ProgramChunks = Properties.Settings.Default.ProgramList.Split(';');
            foreach (var item in ProgramChunks)
            {
                string[] ProgramParts = item.Split(',');
                TempList.Add(new Programs(ProgramParts[0], ProgramParts[1]));
            }
            return TempList;
        }

        public static ImageSource ConvertToSource(string Path)
        {
            return Imaging.CreateBitmapSourceFromHIcon(
            Icon.ExtractAssociatedIcon(Path).Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
        }

        public static void AddProgram()
        {
            OpenFileDialog OpenProgram = new OpenFileDialog();
            OpenProgram.Filter = "Programs (*.exe)|*.exe|Batch script (*.bat)|*.bat|Command script (*.cmd)|*.cmd|COM programs (*.com)|*.com|All files (*.*)|*.*";
            if (OpenProgram.ShowDialog() == true)
            {
                var Alias = new AliasDialog();
                if (Alias.ShowDialog() == true)
                {
                    Properties.Settings.Default.ProgramList += ";" + OpenProgram.FileName + "," + Alias.ResponceText;
                    Properties.Settings.Default.Save();
                }
            }
        }

        public static void DeleteProgram(int CharIndex)
        {
            int index = FindStringLocation(Properties.Settings.Default.ProgramList, CharIndex);
            if (index != -1)
            {
                int indexPlus = FindStringLocation(Properties.Settings.Default.ProgramList, CharIndex + 1);
                if (indexPlus != -1)
                    Properties.Settings.Default.ProgramList = Properties.Settings.Default.ProgramList.Remove(index, indexPlus - index);
                else
                    Properties.Settings.Default.ProgramList = Properties.Settings.Default.ProgramList.Remove(index);
            }
            else
                Properties.Settings.Default.ProgramList = "";
            Properties.Settings.Default.Save();
        }

        public static int FindStringLocation(string Settings, int n)
        {
            int count = 0;
            for (int i = 0; i < Settings.Length; i++)
            {
                if (Settings[i] == ';')
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static void LaunchProgram(string Command)
        {
            Process.Start(Command);
        }

        public override string ToString()
        {
            return FilePath;
        }
    }
}
