using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using TemplateEngineProject.tables;
using TemplateEngineProject.utilities;


namespace TemplateEngineProject
{
    class Program
    {
        static void Main(String[] args)
        {
            StreamReader data = new StreamReader(args[1].Trim('"'));

            ContextReader reader = new ContextReader(args[0].Trim('"'));
            ContextTable context = reader.Read();

            Template template = new Template(data);
            template.Parse();


            string result = template.Execute(context);

            StreamWriter writer = new StreamWriter(args[2].Trim('"'));
            writer.Write(result);
            writer.Close();



            Console.ReadLine();
        }
    }
}