using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Notino.Homework.Model;

namespace Notino.Homework
{

    class Program
    {
        static void Main(string[] args)
        {
            var supportedFormatPattern = $"^.+\\.(?<extension>{string.Join("|", Enum.GetNames(typeof(Format))).ToUpper()})$";
            CheckArguments(args, supportedFormatPattern);

            var sourceFileName = Path.Combine(args[0]);
            var targetFileName = Path.Combine(args[1]);

            var fileFormatConverter = new FileFormatConventer<DocumentExtension>(sourceFileName);
            fileFormatConverter.SaveConvertedData(targetFileName);


            Console.WriteLine($"{targetFileName} file was generated.");
        }

        private static void CheckArguments(string[] args, string supportedFormatPattern)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("Missing argument(s)");
            }
        }
    }
}
