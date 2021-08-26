using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace XMLDataHandler
{
    class DataLoaderClass
    {
        public Boolean validateIs = true;

        public static DataLoaderClass giveMeDataLoader()
        {
            DataLoaderClass dataLoader = new DataLoaderClass();
            return dataLoader;
        }

        private DataLoaderClass() { }

        public void loadData(String path)
        {
            XmlTextReader reader = new XmlTextReader(path);
            XmlValidatingReader valreader = new XmlValidatingReader(reader);
            Console.WriteLine("Press any buttom to check DTD");

            valreader.ValidationType = ValidationType.DTD;
            Program vx = new Program();
            valreader.ValidationEventHandler += new ValidationEventHandler(vx.HandleEvent);

            while (valreader.Read()) { }
            Console.ReadKey();
            Console.WriteLine("\nCompleted\n");
            reader.Dispose();

            if (validateIs)
            {
                XmlDocument Doc = new XmlDocument();
                Doc.Load(path);
                XmlElement Root = Doc.DocumentElement;
                Boolean loop = true;

                while (loop)
                {
                    Console.WriteLine("Choose action");
                    Console.WriteLine("\t1 - Show info");
                    Console.WriteLine("\t2 - Add info");
                    Console.WriteLine("\t3 - Change info");
                    Console.WriteLine("\t4 - Delete info");
                    Console.WriteLine("\t0 - Exit programm");
                    Console.WriteLine("\nYour Action: ");

                    int value = 0;
                    if (Int32.TryParse(Console.ReadLine(), out value))
                    {
                        if (value >= 0 && value <= 4)
                        {
                            switch (value)
                            {
                                case 0:
                                    loop = false;
                                    break;
                                case 1:
                                    this.showInfo(Root);
                                    break;
                                case 2:
                                    this.addInfo(Doc, Root);
                                    break;
                                case 3:
                                    this.changeInfo(Doc, Root);
                                    break;
                                case 4:
                                    this.deleteInfo(Doc,Root);
                                    break;
                                default:
                                    Console.WriteLine("\nWrite correct value\n");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nWrite correct value\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Write correct value");
                    }
                }
            }
        }

        private void showInfo(XmlElement Root)
        {
            Console.WriteLine("Right now you have " + Root.ChildNodes.Count + " elements in your XML\n");
            int counter = 0;
            foreach (XmlNode node in Root)
            {
                Console.WriteLine(counter);
                counter++;
                foreach (XmlNode childnode in node.ChildNodes)
                {
                    if (childnode.Name == "WeaponId")
                    {
                        Console.WriteLine("Weapon ID: {0}", childnode.InnerText);
                    }
                    if (childnode.Name == "Name")
                    {
                        Console.WriteLine("Name: {0}", childnode.InnerText);
                    }
                    if (childnode.Name == "ProductionYear")
                    {
                        Console.WriteLine("Production Year: {0}", childnode.InnerText);
                    }
                    if (childnode.Name == "Caliber")
                    {
                        Console.WriteLine("Caliber: {0}", childnode.InnerText);
                    }
                    if (childnode.Name == "MagazineCapacity")
                    {
                        Console.WriteLine("Magazine Capacity: {0}", childnode.InnerText);
                    }
                }
                Console.WriteLine();
            }
        }

        private void addInfo(XmlDocument Doc, XmlElement Root)
        {
            XmlElement Weapon = Doc.CreateElement("Weapon");

            XmlElement WeaponId = Doc.CreateElement("WeaponId");
            Console.WriteLine("Weapon ID:");
            string temp1 = this.check();
            XmlText TextWeaponid = Doc.CreateTextNode(temp1);

            XmlElement Name = Doc.CreateElement("Name");
            Console.WriteLine("Name:");
            string temp2 = Convert.ToString(Console.ReadLine());
            XmlText TextName = Doc.CreateTextNode(temp2);

            XmlElement ProductionYear = Doc.CreateElement("ProductionYear");
            Console.WriteLine("Production Year:");
            string temp3 = this.check();
            XmlText TextProductionYear = Doc.CreateTextNode(temp3);

            XmlElement Caliber = Doc.CreateElement("Caliber");
            Console.WriteLine("Caliber:");
            string temp4 = Convert.ToString(Console.ReadLine());
            XmlText TextCaliber = Doc.CreateTextNode(temp4);

            XmlElement MagazineCapacity = Doc.CreateElement("MagazineCapacity");
            Console.WriteLine("Magazine Capacity:");
            string temp5 = this.check();
            XmlText TextMagazineCapacity = Doc.CreateTextNode(temp5);

            WeaponId.AppendChild(TextWeaponid);
            Name.AppendChild(TextName);
            ProductionYear.AppendChild(TextProductionYear);
            Caliber.AppendChild(TextCaliber);
            MagazineCapacity.AppendChild(TextMagazineCapacity);

            Weapon.AppendChild(WeaponId);
            Weapon.AppendChild(Name);
            Weapon.AppendChild(ProductionYear);
            Weapon.AppendChild(Caliber);
            Weapon.AppendChild(MagazineCapacity);

            Root.AppendChild(Weapon);
            Doc.Save("Source.xml");
        }

        private void changeInfo(XmlDocument Doc, XmlElement Root)
        {
            int choice = 0;
            Console.WriteLine("\nNumber");
            if (Int32.TryParse(Console.ReadLine(), out choice))
            {
                if (choice >= 0 && choice <= Root.ChildNodes.Count)
                {
                    XmlNode FirstTurn = Root.ChildNodes[choice];
                    Boolean FirstCheck = true;
                    while (FirstCheck)
                    {
                        Console.WriteLine("\nChoose");
                        Console.WriteLine("\t1 - Weapon ID");
                        Console.WriteLine("\t2 - Name");
                        Console.WriteLine("\t3 - Production Year");
                        Console.WriteLine("\t4 - Caliber");
                        Console.WriteLine("\t5 - Magazine Capacity");
                        Console.WriteLine("\t0 - Exit");
                        Console.WriteLine("\nYour choice");

                        int SecondCheck;
                        if (Int32.TryParse(Console.ReadLine(), out SecondCheck))
                        {
                            switch (SecondCheck)
                            {
                                case 0:
                                    FirstCheck = false;
                                    break;
                                case 1:
                                    XmlNode XmlWeaponId = FirstTurn.ChildNodes[0];
                                    Console.WriteLine("\nWeapon ID:");
                                    string temp_1 = check();
                                    XmlWeaponId.InnerText = temp_1;
                                    Doc.Save("Source.xml");
                                    break;
                                case 2:
                                    XmlNode XmlName = FirstTurn.ChildNodes[1];
                                    Console.WriteLine("\nName:");
                                    string temp_2 = Convert.ToString(Console.ReadLine());
                                    XmlName.InnerText = temp_2;
                                    Doc.Save("Source.xml");
                                    break;
                                case 3:
                                    XmlNode XmlProductionYear = FirstTurn.ChildNodes[2];
                                    Console.WriteLine("\nProduction Year:");
                                    string temp_3 = check();
                                    XmlProductionYear.InnerText = temp_3;
                                    Doc.Save("Source.xml");
                                    break;
                                case 4:
                                    XmlNode XmlCaliber = FirstTurn.ChildNodes[3];
                                    Console.WriteLine("\nCaliber:");
                                    string temp_4 = Convert.ToString(Console.ReadLine());
                                    XmlCaliber.InnerText = temp_4;
                                    Doc.Save("Source.xml");
                                    break;
                                case 5:
                                    XmlNode XmlMagazineCapacity = FirstTurn.ChildNodes[4];
                                    Console.WriteLine("\nMagazine Capacity:");
                                    string temp_5 = check();
                                    XmlMagazineCapacity.InnerText = temp_5;
                                    Doc.Save("Source.xml");
                                    break;
                                default:
                                    Console.WriteLine("\nIncorrect value\n");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nWrite correct value\n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nWrite correct value\n");
                }
            }
            else
            {
                Console.WriteLine("\nWrite correct value\n");
            }
        }

        private void deleteInfo(XmlDocument Doc, XmlElement Root)
        {
            Console.WriteLine("\nNumber: ");
            int index;
            if (Int32.TryParse(Console.ReadLine(), out index))
            {
                if (index >= 0 && index <= Root.ChildNodes.Count)
                {
                    XmlNode SecondTurn = Root.ChildNodes[index];
                    Root.RemoveChild(SecondTurn);
                    Doc.Save("Source.xml");
                }
                else
                {
                    Console.WriteLine("\nWrite correct value\n");
                }
            }
            else
            {
                Console.WriteLine("\nWrite correct value\n");
            }
        }

        private string check()
        {
            int value;

            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out value))
                {
                    string answer = Convert.ToString(value);
                    return answer;
                }
                else
                {

                    Console.WriteLine("\nYou can write here only numerical value\n");

                }
            }
        }

    }
}
