using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Extensions;

namespace T4
{
    class Program
    {
        static void Main(string[] args)
        {
            //IterateDirectories(new DirectoryInfo("G:/Sandboxes/T4"));
            TestExtensions();
        }

        private static void TestExtensions()
        {
            foreach (var fileName in GetXmlFileNames())
            {
                var xDoc = XDocument.Load(fileName);
                var controllers = xDoc.Controllers();

                foreach (var controller in controllers)
                {
                    var actions = controller.Actions();
                    foreach (var action in actions)
                    {
                        var testFileName = string.Format("{0}_{1}Tests.generated.cs", controller.Attribute("name").Value, action.Attribute("name").Value);

                        foreach (var path in action.Paths())
                        {
                            var testMethodName = path.Attribute("name").Value;
                            var inputParams = path.Descendants("Setups").First().Descendants().ToList();
                            var outputParams = path.Descendants("Expects").First().Descendants().ToList();
                        }

                        //manager.StartNewFile(string.Format("{0}_{1}Tests.generated.cs", controller.Attribute["name"], action.Attribute["name"]));

                        //manager.StartHeader();


                        //manager.EndBlock();
                    }
                }
            }
        }

        private static IEnumerable<string> GetXmlFileNames()
        {
            var path = "G:\\Sandboxes\\T4\\T4 Toolbox Experiements\\Resources";//Host.ResolvePath(string.Empty);
            return Directory.GetFiles(path, "*Tests.xml");
        }

        private static void IterateDirectories(DirectoryInfo dir)
        {
            Array.ForEach(dir.GetFiles("*Tests.xml"), GenerateTests);
            Array.ForEach(dir.GetDirectories(), IterateDirectories);
        }

        private static void GenerateTests(FileInfo file)
        {
            var xmlDoc = XDocument.Load(file.FullName);
            var tests = xmlDoc.Descendants().Where(x => x.Name == "Path").ToList();
        }
    }
} 
