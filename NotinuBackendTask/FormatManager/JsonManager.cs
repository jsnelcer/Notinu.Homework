using Newtonsoft.Json;
using System;
using System.IO;

namespace Notino.Homework
{
    internal class JsonManager<T> : IFormatManager<T>
    {
        public T GetDeserializedData(string input)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch
            {
                throw new Exception("Deserialization from Json fail");
            }
        }

        public void SaveFile(T data, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                writer.Write(JsonConvert.SerializeObject(data));
            }
        }
    }
}