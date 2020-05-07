using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetExplorer
{
    public class AssemblyLoader
    {
        public static async Task<string[]> ShowOpenDialog(Window parent)
        { 
            var dialog = new OpenFileDialog();
            dialog.AllowMultiple = true;

            var extNet = new FileDialogFilter();
            extNet.Name = ".NET Assembles";
            extNet.Extensions.AddRange(new string[] { "dll", "exe" });

            var extAll = new FileDialogFilter();
            extAll.Name = "All Files";
            extAll.Extensions.Add("*");

            dialog.Filters.AddRange(new FileDialogFilter[] { extNet, extAll });

            return await dialog.ShowAsync(parent);
        }

        public static Assembly Load(string file)
        {
            var fileInfo = new FileInfo(file);
            var asm = Assembly.LoadFile(fileInfo.FullName);

            // TODO: add here some assembly resolving events

            return asm;
        }

        public static async void ShowExceptionDialog(Window parent, string file, Exception ex)
        {
            var b = new StringBuilder();
            b.AppendLine("Unable to load assembly.");
            b.AppendLine();
            b.AppendLine("File: " + file);
            b.AppendLine("Exception: " + ex.GetType().Name);
            b.AppendLine("Message: " + ex.Message);
            await new MessageBox(b.ToString(), ".NET Explorer: Error").ShowDialog(parent);
        }
    }
}
