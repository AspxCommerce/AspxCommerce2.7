/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Xml;

namespace SageFrame.Dashboard
{
    /// <summary>
    /// This class helps to execute remote webservice through code behind.
    /// </summary>
    public class WebServiceInvoker
    {
        Dictionary<string, Type> availableTypes;
        /// <summary>
        /// Gets or sets available services.
        /// </summary>
        public List<string> AvailableServices
        {
            get { return this.services; }
        }
        /// <summary>
        /// Initializes new instance of WebServiceInvoker class
        /// </summary>
        /// <param name="webServiceUri">Uri object.</param>
        public WebServiceInvoker(Uri webServiceUri)
        {
            this.services = new List<string>();
            this.availableTypes = new Dictionary<string, Type>();
            XmlTextReader xmlreader = new XmlTextReader(webServiceUri.ToString() + "?wsdl");
            if (ServiceDescription.CanRead(xmlreader))
            {
                this.webServiceAssembly = BuildAssemblyFromWSDL(xmlreader);

                if (this.webServiceAssembly != null)
                {
                    Type[] types = this.webServiceAssembly.GetExportedTypes();
                    foreach (Type type in types)
                    {
                        services.Add(type.FullName);
                        availableTypes.Add(type.FullName, type);
                    }
                }
            }
        }
        /// <summary>
        /// Returns List of strings of the web service methods
        /// </summary>
        /// <param name="serviceName">Service Name</param>
        /// <returns>list of string.</returns>
        public List<string> EnumerateServiceMethods(string serviceName)
        {
            List<string> methods = new List<string>();

            if (!this.availableTypes.ContainsKey(serviceName))
                throw new Exception("Service Not Available");
            else
            {
                Type type = this.availableTypes[serviceName];

                foreach (MethodInfo minfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
                    methods.Add(minfo.Name);
                return methods;
            }
        }

        /// <summary>
        /// Invokes provided method with array of object and webservice name.
        /// </summary>
        /// <typeparam name="T">Type of the object implementing.</typeparam>
        /// <param name="serviceName">WebserviceName</param>
        /// <param name="methodName">MethodName inside that web service</param>
        /// <param name="args">Array of object.</param>
        /// <returns>Type of the object implementing.</returns>
        public T InvokeMethod<T>(string serviceName, string methodName, params object[] args)
        {
            object obj = this.webServiceAssembly.CreateInstance(serviceName);
            Type type = obj.GetType();
            return (T)type.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, args);
        }
        /// <summary>
        /// Builds service description importer
        /// </summary>
        /// <param name="xmlreader">XmlTextReader object.</param>
        /// <returns>ServiceDescriptionImporter object.</returns>
        private ServiceDescriptionImporter BuildServiceDescriptionImporter(XmlTextReader xmlreader)
        {
            ServiceDescriptionImporter descriptionImporter = null;
            ServiceDescription serviceDescription = ServiceDescription.Read(xmlreader);
            descriptionImporter = new ServiceDescriptionImporter();
            descriptionImporter.ProtocolName = "Soap";
            descriptionImporter.AddServiceDescription(serviceDescription, null, null);
            descriptionImporter.Style = ServiceDescriptionImportStyle.Client;
            descriptionImporter.CodeGenerationOptions =
                System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;
            return descriptionImporter;
        }

        /// <summary>
        /// Compiles assembly for given codes.
        /// </summary>
        /// <param name="descriptionImporter">ServiceDescriptionImporter object containing the code.</param>
        /// <returns>Assembly.</returns>
        private Assembly CompileAssembly(ServiceDescriptionImporter descriptionImporter)
        {
            CodeNamespace codeNamespace = new CodeNamespace();
            CodeCompileUnit codeUnit = new CodeCompileUnit();
            codeUnit.Namespaces.Add(codeNamespace);
            ServiceDescriptionImportWarnings importWarnings = descriptionImporter.Import(codeNamespace, codeUnit);
            if (importWarnings == 0)
            {
                CodeDomProvider compiler = CodeDomProvider.CreateProvider("CSharp");
                string[] references = new string[2] { "System.Web.Services.dll", "System.Xml.dll" };
                CompilerParameters parameters = new CompilerParameters(references);
                CompilerResults results = compiler.CompileAssemblyFromDom(parameters, codeUnit);
                foreach (CompilerError oops in results.Errors)
                {
                    throw new Exception("Compilation Error Creating Assembly");
                }
                return results.CompiledAssembly;
            }
            else
            {
                throw new Exception("Invalid WSDL");
            }
        }
        /// <summary>
        /// Builds assembly from WSDL.
        /// </summary>
        /// <param name="xmlreader">XmlTextReader object.</param>
        /// <returns>Assembly.</returns>
        private Assembly BuildAssemblyFromWSDL(XmlTextReader xmlreader)
        {
            ServiceDescriptionImporter descriptionImporter = BuildServiceDescriptionImporter(xmlreader);
            return CompileAssembly(descriptionImporter);
        }

        private Assembly webServiceAssembly;
        private List<string> services;
    }
}
