using System.IO;
using System.Linq;

namespace src
{
    public class Storage
    {
        GeneratorFileName generator;
        string path;

        public Storage(string path, GeneratorFileName generator)
        {
            this.generator = generator;
            this.path = path;
        }

        public string[] GetFiles()
        {
            var files = Directory.GetFiles(path, string.Format("{0}*", GeneratorFileName.BaseName)).ToList();
            files.Sort();
            return files.Select(fileName => Path.GetFileNameWithoutExtension(fileName)).ToArray();
        }

        public void SaveData(byte[] data)
        {
            string prevName = generator.CurrentName;
            while (File.Exists(Path.Combine(path, generator.CurrentName)))
            {
                generator.GenerateNextName();
                if (generator.CurrentName == prevName)
                {
                    generator.GenerateFirstName();
                }
            }

            File.WriteAllBytes(Path.Combine(path, generator.CurrentName), data);
        }

        public byte[] GetData(string filename = null)
        {
            byte[] data = null;
            if (filename != null)
            {
                if (File.Exists(filename)) data = File.ReadAllBytes(filename);
            }
            else
            {
                var files = GetFiles();
                if (files.Length > 0)
                {
                    data = File.ReadAllBytes(files.Last());
                }
            }
            return data;
        }
    }

}
