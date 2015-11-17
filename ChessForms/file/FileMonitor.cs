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

        private string filePath = AppDomain.CurrentDomain.BaseDirectory + "../../saveFiles/";
        private const string FILE_NAME = "current_state.txt";

        //public delegate void monitorFunc();
        //monitorFunc change;

        private bool change = false;
        private DateTime ignoreEnd;

        public FileMonitor(/*monitorFunc onChange*/)
        {
            //change = onChange;
            //onChange = chan;
            // Create a new FileSystemWatcher and set its properties.
            // Create directory if not exists
            /*bool exists = Directory.Exists(filePath);
            if (!exists)
            {
                Directory.CreateDirectory(filePath);
            }*/

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = filePath;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            // Only watch text files.
            watcher.Filter = FILE_NAME;

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChange);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        // Called when file changed
        private void OnChange(object source, FileSystemEventArgs e)
        {
            if (DateTime.Now.CompareTo(ignoreEnd) > 0)
            {
                change = true;
            }
        }

        // Check and reset change flag
        public bool changeDetected()
        {
            if (change)
            {
                change = false;
                return true;
            }

            return false;
        }

        public void ignoreFor(int millis)
        {
            ignoreEnd = DateTime.Now.AddMilliseconds(millis);
        }
    }
}
