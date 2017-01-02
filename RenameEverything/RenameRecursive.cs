using System.Collections.Generic;
using System.IO;

namespace RenameEverything
{
    public class RenameRecursive
    {
        private string renameFrom;
        private string renameTo;

        public List<string> Execute(string directory, string renameFrom, string renameTo)
        {
            this.renameFrom = renameFrom;
            this.renameTo = renameTo;
            
            List<string> dir = RenameDirectoryRecursive(directory);
            List<string> file = RenameFileRecursive(directory.Replace(renameFrom, renameTo));
            dir.AddRange(file);

            return dir;
        }

        private List<string> RenameDirectoryRecursive(string directory)
        {
            List<string> output = new List<string>();

            //these are lists of full file paths
            var dirs = Directory.EnumerateDirectories(directory);
            foreach(var dir in dirs)
            {
                if (dir.Contains(renameFrom))
                {
                    Directory.Move(dir, dir.Replace(renameFrom, renameTo));
                }

                RenameDirectoryRecursive(dir);
            }
            
            return output;
        }

        private List<string> RenameFileRecursive(string directory)
        {
            List<string> output = new List<string>();

            var files = Directory.EnumerateFiles(directory);
            foreach (var file in files)
            {
                //open the file and rename contents
                RenameFileContents(file);

                //move the file
                if (file.Contains(renameFrom))
                {
                    File.Move(file, file.Replace(renameFrom, renameTo));
                }
            }

            return output;
        }

        private void RenameFileContents(string file)
        {
            //open the file and parse it's contents looking for the key
            string text = File.ReadAllText(file);
            text = text.Replace(renameFrom, renameTo);
            File.WriteAllText(file, text);
        }
    }
}
