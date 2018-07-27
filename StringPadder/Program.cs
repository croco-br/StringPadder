using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace StringPadder
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = new List<string>(1024);

            if (File.Exists(ConfigurationManager.AppSettings["InputFilePath"]))
            {
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["InputFilePath"]))
                {
                    using (StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["OutputFilePath"]))
                    {
                        var expectedLength = int.Parse(ConfigurationManager.AppSettings["ExpectedStringLength"]);
                        var paddingChar = char.Parse(ConfigurationManager.AppSettings["PaddingChar"]);

                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            line = new string(line.Where(char.IsDigit).ToArray());
                            var filledLine = "";

                            //fill
                            if (line?.Length < expectedLength)
                            {
                                filledLine = line.PadLeft(expectedLength, paddingChar);
                            }
                            else
                            {
                                filledLine = line;
                            }

                            //distinct
                            if (!inputs.Contains(filledLine))
                            {
                                inputs.Add(filledLine);
                            }
                        }

                        foreach (var item in inputs)
                        {
                            sw.WriteLine(string.Concat("'", item, "',"));
                        }
                    }
                }
            }
        }
    }
}
