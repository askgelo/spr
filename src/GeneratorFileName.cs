using System;
using System.IO;
using System.Linq;

namespace src
{
    public class GeneratorFileName
    {
        public const string BaseName = "mank";
        public const int MaxCountByDate = 3;

        public int CurrentNumber { get; private set; } = 0;

        public string CurrentName
        {
            get
            {
                string fileName = string.Join("", BaseName, TimeProvider.Current.Now.ToString("yyyyMMdd"), 
                    CurrentNumber);
                return fileName;
            }
        }

        public void GenerateNextName()
        {
            CurrentNumber++;
            if (CurrentNumber >= MaxCountByDate) CurrentNumber = 0;
        }

        internal void GenerateFirstName()
        {
            CurrentNumber = 0;
        }
    }

}
