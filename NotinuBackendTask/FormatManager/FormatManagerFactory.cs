using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notino.Homework
{
    public static class FormatManagerFactory<T>
    {
        public static IFormatManager<T> CreateFormatManager(Format format)
        {
            switch (format)
            {
                case Format.XML:
                    return new XmlManager<T>();
                case Format.JSON:
                    return new JsonManager<T>();
                default:
                    throw new Exception();
            }
        }
    }
}
