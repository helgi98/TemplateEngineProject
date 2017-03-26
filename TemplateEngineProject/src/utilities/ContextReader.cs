using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TemplateEngineProject.exceptions;
using TemplateEngineProject.model;
using TemplateEngineProject.tables;

namespace TemplateEngineProject.utilities
{
    class ContextReader
    {
        private ContextTable _context;
        private String _xmlPath;

        public ContextReader(String xmlPath)
        {
            _context = new ContextTable();
            _xmlPath = xmlPath;
        }

        private void AddUser(XElement contextObject)
        {
            try
            {
                XmlSerializer deSerializer = new XmlSerializer(typeof(User));
                User user = (User) deSerializer.Deserialize(
                    new StringReader(contextObject.ToString()));
                _context.AddProperty("User", user);
            }
            catch 
            {
                throw new ParserException("[ContextReader]Invalid User configuration");
            }
        }

        public ContextTable Read()
        {
            XDocument doc = XDocument.Load(_xmlPath);

            var contextObjects = doc
                .Root
                .Elements();

            foreach (XElement contextObject in contextObjects)
            {
                if (contextObject.Name == "User")
                    AddUser(contextObject);
                else
                    _context.AddProperty(contextObject.Name.ToString(), contextObject.Value);
            }

            return _context;
        }
    }
}