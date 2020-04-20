using System;

namespace StudentLibrary
{
    public class Student
    {
        public string Name { get; } // Свойство только для чтения, сохраняющее имя студента.
        public Faculty Faculty { get; } // Свойство только для чтения, сохраняющее факультет студента.
        public double Mark { get; } // Свойство только для чтения, сохраняющее оценку студента.

        /// <summary>
        /// Конструктор, который проверяет параметры на соответствие спецификации.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mark"></param>
        /// <param name="faculty"></param>
        public Student(string name, double mark, Faculty faculty)
        {
            if (!(name.Length >= 6 && name.Length < 10))
                throw new Exception();
            for (int i = 0; i < name.Length; ++i)
                if (!((name[i] >= 'A' && name[i] <= 'Z') || (name[i] >= 'a' && name[i] <= 'z')))
                    throw new Exception();
            if (!(mark >= 4 && mark < 10))
                throw new Exception();

            Name = name;
            Mark = mark;
            Faculty = faculty;
        }

        /// <summary>
        /// Переопределенный метод ToString, который выводит информацию о студенте.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Faculty} Student {Name}: Mark = {Mark:F3}";
        }
        /// <summary>
        /// Переопределенный оператор сложения
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Student operator +(Student A, Student B)
        {
            int middleA = A.Name.Length / 2;
            int middleB = B.Name.Length / 2;
            string name = A.Name.Length >= B.Name.Length ? A.Name.Substring(0, middleA) + B.Name.Substring(middleB) : B.Name.Substring(0, middleB) + A.Name.Substring(middleA);

            Faculty faculty = A.Faculty == B.Faculty ? A.Faculty : throw new ArgumentException("Факультеты не равны"); 

            double mark = (A.Mark + B.Mark) / 2;

            return new Student(name, mark, faculty);
        }
        /// <summary>
        /// Переопределенный метод Equals, который корректно сравнивает два объекта.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Student other = obj as Student;
            if (Name == other.Name)
                if (Mark == other.Mark)
                    if (Faculty == other.Faculty)
                        return true;
            return false;
        }
    }
}
