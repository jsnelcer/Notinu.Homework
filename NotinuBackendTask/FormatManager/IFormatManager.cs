using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notino.Homework
{
    public interface IFormatManager<T>
    {
        T GetDeserializedData(string input);
        void SaveFile(T data, string fileName);
    }
}
