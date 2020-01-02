using AkApp.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            _plugins = ReadExtensions();
            Console.WriteLine($"{_plugins.Count} plugin(s) found"); 
            // Print 
            foreach (var plugin in _plugins)
            {
                Console.WriteLine($"{plugin.Title} | {plugin.Description}");
            }
            Console.WriteLine("-----------------------");
            foreach (var plugin in _plugins)
            {
                plugin.DoSomething(); 
            }

            Console.WriteLine("Hello World!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(); 
        }

        static List<IPlugin> _plugins = null; 

        static List<IPlugin> ReadExtensions()
        {
            var pluginsLists = new List<IPlugin>(); 
            // 1- Read the dll files from the extensions folder
            var files = Directory.GetFiles("extensions", "*.dll");

            // 2- Read the assembly from files 
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), file));

                // 3- Exteract all the types that implements IPlugin 
                var pluginTypes = assembly.GetTypes().Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface).ToArray();

                foreach (var pluginType in pluginTypes)
                {
                    // 4- Create an instance from the extracted type 
                    var pluginInstance = Activator.CreateInstance(pluginType) as IPlugin;
                    pluginsLists.Add(pluginInstance);
                }
            }

            return pluginsLists;
        }

    }
}
