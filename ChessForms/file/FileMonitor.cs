using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ChessForms.src;

namespace ChessForms.file
{
    public class FileMonitor
    {

        private string filePath = AppDomain.CurrentDomain.BaseDirectory + "../../saveFiles/current_state";
        

        public delegate void monitorFunc();
        monitorFunc change;

        public FileMonitor(monitorFunc onChange)
        {
            change = onChange;
            //onChange = chan;
            // Create a new FileSystemWatcher and set its properties.
            // Create directory if not exists
            bool exists = System.IO.Directory.Exists(filePath);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(filePath);
            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = filePath;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            // Only watch text files.
            watcher.Filter = "*.txt";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChange);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private void OnChange(object source, FileSystemEventArgs e)
        {
            change();
        }
    }
}
