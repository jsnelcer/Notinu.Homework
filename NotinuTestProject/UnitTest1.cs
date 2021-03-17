using System;
using Xunit;
using Notino.Homework;
using Notino.Homework.Model;
using System.IO;

namespace NotinuTestProject
{
    public class UnitTests
    {
        private string GetFilePath(string fileName) => Path.Combine("Resources", fileName);
        private const string JsonTestFile = "jsonTestFile.json";
        private const string XmlTestFile = "xmlTestFile.xml";
        private const string XmlTestFileExtension = "xmlTestFile_extension.xml";
        private const string JsonTestFileExtension = "jsonTestFile_extension.json";

        [Fact]
        public void DeserializeJsonFileTest()
        {
            var filePath = GetFilePath(JsonTestFileExtension);
            var converter = new FileFormatConventer<DocumentExtension>(filePath);

            Assert.Equal(1, converter.Data.Id);
            Assert.Equal("jsonDocumentAutor", converter.Data.Autor);
            Assert.Equal("jsonText", converter.Data.Text);
            Assert.Equal("jsonTitleText", converter.Data.Title);
        }

        [Fact]
        public void DeserializeXmlFileTest()
        {
            var filePath = GetFilePath(XmlTestFileExtension);
            var converter = new FileFormatConventer<DocumentExtension>(filePath);

            Assert.Equal(111, converter.Data.Id);
            Assert.Equal("xmlDocumentAutor", converter.Data.Autor);
            Assert.Equal("xmlText", converter.Data.Text);
            Assert.Equal("xmlTitleText", converter.Data.Title);
        }

        [Fact]
        public void FormatConverterTest()
        {
            var filePath = GetFilePath(JsonTestFile);
            var converter = new FileFormatConventer<Document>(filePath);

            var xmlFormatManager = FormatManagerFactory<Document>.CreateFormatManager(Format.XML);
            var xmlTempFilePath = GetFilePath("tempFile.xml");

            // Save data from json file to xmlFile
            converter.SaveConvertedData(xmlTempFilePath);
            
            // read xml file
            var content = File.Open(xmlTempFilePath, FileMode.Open);
            var reader = new StreamReader(content);

            // get data from xml file
            var data = xmlFormatManager.GetDeserializedData(reader.ReadToEnd());

            // compare data from converter and xml file
            Assert.Equal(converter.Data.Text, data.Text);
            Assert.Equal(converter.Data.Title, data.Title);
        }

        [Fact]
        public void FormatConverterTest_2()
        {
            var filePath = GetFilePath(XmlTestFile);
            var converter = new FileFormatConventer<Document>(filePath);

            var jsonFormatManager = FormatManagerFactory<Document>.CreateFormatManager(Format.JSON);
            var jsonTempFilePath = GetFilePath("tempFile.json");

            // Save data from json file to xmlFile
            converter.SaveConvertedData(jsonTempFilePath);

            // read json file
            var content = File.Open(jsonTempFilePath, FileMode.Open);
            var reader = new StreamReader(content);

            // get data from json file
            var data = jsonFormatManager.GetDeserializedData(reader.ReadToEnd());

            // compare data from converter and json file
            Assert.Equal(converter.Data.Text, data.Text);
            Assert.Equal(converter.Data.Title, data.Title);
        }
    }
}
