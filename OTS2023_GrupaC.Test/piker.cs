using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS2026_GrupaD.Test
{
    internal class piker
    {
        public static IEnumerable<TestCaseData>Piker(string imefajla)
        {
            string path= $@"{AppDomain.CurrentDomain.BaseDirectory}/{imefajla}";
            string[] lines =File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] CHARS = line.Split(null);

            }
        }
    }
}
