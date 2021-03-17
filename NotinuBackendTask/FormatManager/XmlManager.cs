using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Notino.Homework
{
    internal class XmlManager<T> : IFormatManager<T>
    {
        public T GetDeserializedData(string input)
        {
            var xDoc = XDocument.Parse(input);
            var xmlSerializer = new XmlSerializer(typeof(T));

            // tu by som ešte vytvoril xsd schému a overil validáciu xml súboru
            // niečo také napríklad
            // xDoc.Validate(LoadValidSchema(), (sender, args) => throw args.Exception))

            return (T)xmlSerializer.Deserialize(xDoc.CreateReader());
        }

        public void SaveFile(T data, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            
            using (FileStream writer = File.Open(fileName, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}