using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

// Handles printouts of the piece pictures
namespace ChessForms.graphics
{
    class ImageHandler
    {
        private string path = "";
        private string ending = "";
        Dictionary<string, Image> images = new Dictionary<string,Image>();

        public ImageHandler(string path, string fileType)
        {
            this.path = path;
            ending = fileType;
        }

        // Get image from map, or load it from memory and put it in the map.
        public Image getImage(string filename)
        {
            string fullName = path + "/" + filename + "." + ending;
            if (images.ContainsKey(filename))
            {
                return images[filename];
            } else {
                Image i = Image.FromFile(fullName, true);
                images.Add(filename, i);
                return i;
            }
        }

        // Removes a stored image. Returns true if the image was removed,
        // false if the image was not found.
        public bool removeImage(string fileName)
        {
            if (images.ContainsKey(fileName))
            {
                images[fileName].Dispose();
                images.Remove(fileName);
                return true;
            }
            return false;
        }

        // Empties the map
        public void clear()
        {
            foreach (System.Collections.Generic.KeyValuePair<string, Image> pair in images)
            {
                pair.Value.Dispose();
            }
            images.Clear();
        }
    }
}
