using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessForms.file
{
    public class FileMonitor
    {

        private static string filePath = AppDomain.CurrentDomain.BaseDirectory + "../../saveFiles/current_state";
        private delegate void OnChange(object source, FileSystemEventArgs e);

        public FileMonitor(OnChange callback)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = filePath;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            // Only watch text files.
            watcher.Filter = "*.txt";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(callback);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }
    }
}
