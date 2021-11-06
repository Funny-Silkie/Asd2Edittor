using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Test
{
    [TestFixture]
    public class ReflectionTest
    {
        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory("Export");
        }
        [Test]
        public void AllAssemblies()
        {
            _ = Altseed2.AlphaBlend.Add;
            using var writer = new StreamWriter(@"Export\Assemblies.csv", false);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().OrderBy(x => x.FullName))
            {
                writer.WriteLine(assembly.FullName);
                foreach (var type in assembly.GetTypes().Where(x => x.IsPublic))
                {
                    writer.Write(',');
                    writer.WriteLine(type.FullName);
                }
            }
        }
    }
}