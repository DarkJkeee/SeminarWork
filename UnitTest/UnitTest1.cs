using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentLibrary;
using Newtonsoft.Json;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class StudentTest
    {
        /// <summary>
        /// Метод, который проверяет ToString().
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            const string Expected = "CS Student testfas: Mark = 9,043";
            var student = new Student("testfas", 9.043, Faculty.CS);
            Assert.AreEqual(Expected, student.ToString());
        }
        /// <summary>
        /// Метод, который проверяет переопределение оператора сложения.
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            var student1 = new Student("qwerty", 6.0, Faculty.CS);
            var student2 = new Student("adiosi", 4.0, Faculty.CS);
            var student3 = new Student("qweosi", 5.0, Faculty.CS);

            Assert.AreEqual(student1 + student2, student3);
        }

        /// <summary>
        /// Тест, который проверяет работу сериализации и десериализации.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            var json = new JsonSerializer();
            var student1 = new Student("qwerty", 6.0, Faculty.CS);
            Student student2;
            using (var stream = new StreamWriter("../../../StudentForTest.json"))
                json.Serialize(stream, student1);
            using (var stream = new StreamReader("../../../StudentForTest.json"))
            {
                string file = stream.ReadToEnd();
                student2 = JsonConvert.DeserializeObject<Student>(file);
            }
            Assert.AreEqual(student1, student2);
        }
    }
}
