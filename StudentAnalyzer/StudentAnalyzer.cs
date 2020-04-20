using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using StudentLibrary;
using System.IO;

namespace StudentAnalyzer
{
    class StudentAnalyzer
    {
        static void Main(string[] args)
        {
            var students = Deserialize(); // Вызов метода десериализации.

            // Создание коллекции, состоящей из студентов факультета МИЭМ и вывод кол-ва студентов.
            Console.WriteLine(students.Where(x => x.Faculty == Faculty.MIEM).ToList().Count);
            
            Console.WriteLine("");
            SortByDescending(students); // Вызов метода сортировки студентов по убыванию.
            var ListOfStudents = GroupOfStudents(students); // Вызов метода для группировки студентов.

            Console.WriteLine("");
            // Вывод в консоль информации об усредненных супер-студентах.
            foreach (var student in ListOfStudents)
                Console.WriteLine(student.ToString());

            // Сортировка супер-студентов по степени их крутости.
            ListOfStudents.Sort((x, y) =>
                x.Mark < y.Mark ? 1 : x.Mark != y.Mark ? -1 : x.Name.CompareTo(y.Name));

            Console.WriteLine("");
            // Вывод на экран отсортированных супер-студентов.
            foreach (var student in ListOfStudents)
                Console.WriteLine(student.ToString());
        }

        /// <summary>
        /// Метод, который разбивает студентов на группы по факультетам и создает список из усредненных студентов из каждой группы.
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        static List<Student> GroupOfStudents(List<Student> students)
        {
            var ListOfStudents = new List<Student>();
            var GroupedStudents = students.GroupBy(x => x.Faculty).ToArray();
            for (int i = 0; i < 3; ++i)
            {
                var group = GroupedStudents[i].ToList();
                var result = group[0];
                for (int j = 1; j < GroupedStudents.Length; ++j)
                    result = result + group[j];
                ListOfStudents.Add(result);
            }
            return ListOfStudents;
        }
        /// <summary>
        /// Метод, который сортирует получаемую коллекцию студентов по убыванию их оценок.
        /// </summary>
        /// <param name="students">Коллекция студентов</param>
        static void SortByDescending(List<Student> students)
        {
            var sortedList = students.OrderByDescending(x => x.Mark).ToList();

            for (int i = 0; i < 10; ++i)
                Console.WriteLine(sortedList[i].ToString());
        }
        /// <summary>
        /// Метод, который десериализует коллекцию студентов из файла и возвращает ее.
        /// </summary>
        /// <returns>Коллекция студентов</returns>
        static List<Student> Deserialize()
        {
            List<Student> students;
            using (var stream = new StreamReader("../../../students.json"))
            {
                string file = stream.ReadToEnd();
                students = JsonConvert.DeserializeObject<List<Student>>(file);
            }
            return students;
        }
    }
}
