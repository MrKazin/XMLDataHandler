using System;
using System.Xml.Schema;

namespace XMLDataHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            DataLoaderClass dataLoader = DataLoaderClass.giveMeDataLoader();
            dataLoader.loadData("Source.xml");
        }

        internal void HandleEvent(object sender, ValidationEventArgs args)
        {
            Console.WriteLine("Error Validation");
            Console.WriteLine("What happend  :{0}", args.Message);
            DataLoaderClass dataLoader = DataLoaderClass.giveMeDataLoader();
            dataLoader.validateIs = false;
        }
    }
}
