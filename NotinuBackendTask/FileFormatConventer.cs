using Newtonsoft.Json;
using Notino.Homework.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Notino.Homework
{
    public class FileFormatConventer<T>
    {
        private string SourceFileName { get; set; }

        private string TargetFileName { get; set; }
        private Format SourceFormat => GetFormat(SourceFileName);
        private Format TargetFormat => GetFormat(TargetFileName);

        public T Data { get; private set; }

        private IFormatManager<T> sourceFormatManager { get; set; }

        private string SupportedFormatPattern => $"^.+\\.(?<extension>{string.Join("|", Enum.GetNames(typeof(Format))).ToUpper()})$";

        public FileFormatConventer(string sourceFileName)
        {
            SourceFileName = CheckFile(sourceFileName) ? sourceFileName : throw new ArgumentException(sourceFileName);

            sourceFormatManager = FormatManagerFactory<T>.CreateFormatManager(SourceFormat);

            Data = SetData();
        }

        private bool CheckFile(string fileName)
        {
            return File.Exists(fileName) && CheckFileFormat(fileName);
        }

        private bool CheckFileFormat(string fileName)
        {
            Regex regex = new Regex(SupportedFormatPattern);
            return regex.IsMatch(fileName.ToUpper());
        }

        public void SaveConvertedData(string targetFileName)
        {
            TargetFileName = CheckFileFormat(targetFileName) ? targetFileName : throw new ArgumentException(targetFileName);
            var targetFormatManager = FormatManagerFactory<T>.CreateFormatManager(TargetFormat);
            targetFormatManager.SaveFile(Data, targetFileName);
        }

        private T SetData()
        {
            var content = GetFileContent(SourceFileName);
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception($"Source file {SourceFileName} is empty");
            }

            return sourceFormatManager.GetDeserializedData(content);
        }

        private Format GetFormat(string fileName)
        {
            var extension = Regex.Match(fileName.ToUpper(), SupportedFormatPattern).Groups["extension"].Value;
            return (Format)Enum.Parse(typeof(Format), extension.ToUpper());
        }

        private string GetFileContent(string fileName)
        {
            try
            {
                FileStream sourceStream = File.Open(fileName, FileMode.Open);
                var reader = new StreamReader(sourceStream);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
