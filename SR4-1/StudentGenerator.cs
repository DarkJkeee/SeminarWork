using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StudentLibrary;
using System.IO;

namespace SR4_1
{
    class StudentGenerator
    {
        static Random random = new Random(); // Статический экземпляр класса Random для работы с генерацией случайных параметров.
        static void Main(string[] args)
        {
            do
            {
                Console.Clear(); // Очистка консоли после цикла повтора решения.
                var students = CreateStudents(); // Вызов метода CreateStudents() в котором создается список из 30 студентов.

                // Вывод информации о каждом студенте в консоль.
                foreach (var student in students)
                    Console.WriteLine(student.ToString());

                var json = new JsonSerializer(); // Создание экземпляра JsonSerializer.
                Serialize(json, students); // Вызов метода для сериализации списка студентов.

                Console.WriteLine("Press F to recreate...");
            } while (Console.ReadKey().Key == ConsoleKey.F); // Условие для работы цикла повтора решения.
        }

        /// <summary>
        /// Метод, который работает с json сериализацией и сохраняет список из студентов.
        /// </summary>
        /// <param name="json"></param>
        /// <param name="students"></param>
        static void Serialize(JsonSerializer json, List<Student> students)
        {
            using (var stream = new StreamWriter("../../../students.json"))
                json.Serialize(stream, students);
        }
        /// <summary>
        /// Метод, который создает список из 30 студентов по необходимой спецификации.
        /// </summary>
        /// <returns>List<Student></returns>
        static List<Student> CreateStudents()
        {
            var students = new List<Student>();

            for (int i = 0; i < 30; ++i)
            {
                var name = CreateName();
                double mark = random.NextDouble() + random.Next(4, 10);
                var faculty = CreateFaculty();

                try
                {
                    students.Add(new Student(name, mark, faculty)); // Добавление студента в список.
                }
                catch(Exception ex)
                {
                    Console.WriteLine("wtf?!?!?!!?");
                }
            }
            return students;
        }
        /// <summary>
        /// Метод, который создает имя по необходимой спецификации.
        /// </summary>
        /// <returns>Имя</returns>
        static string CreateName()
        {
            var name = "";
            var capacity = random.Next(6, 10);
            for (int j = 0; j < capacity; ++j)
            {
                char symb = random.Next(0, 2) == 0 ? (char)random.Next('a', 'z' + 1) : (char)random.Next('A', 'Z' + 1);
                name += symb;
            }
            return name;
        }
        /// <summary>
        /// Метод, который генерирует с равной вероятностью факультет.
        /// </summary>
        /// <returns>Факультет</returns>
        static Faculty CreateFaculty()
        {
            var rand = random.Next(0, 3);
            if (rand == 0)
                return Faculty.CS;
            else if (rand == 1)
                return Faculty.MIEM;
            else
                return Faculty.Design;
        }
    }
}
