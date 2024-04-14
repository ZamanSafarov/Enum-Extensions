using Core.CustomExceptions;

namespace Core.Managers
{
    public class StudentManager
    {
        Student[] data = new Student[0];


        public void Add(Student entity) 
        {
            int len = data.Length;
            Array.Resize(ref data, len + 1);
            data[len] = entity;
        }
        public Student[] GetAll()
        {
            return data;
        }
         public void ShowAll() 
        {
            Console.WriteLine("##################Students#################");
            foreach (Student item in GetAll()) 
            {
                Console.WriteLine(item);
            }
        }
        public void Remove(int id)
        {
            Student[] filteredStudent = new Student[0];
            bool found = true;
            foreach (var item in data)
            {
                if (!(item.Id == id))
                {
                    int len = filteredStudent.Length;
                    Array.Resize(ref filteredStudent, len + 1);
                    filteredStudent[len] = item;
                    found = false;
                }
            }
            if (found != false)
            {
                throw new StudentNotFoundException("No Student Found by Id");
            }
            data = filteredStudent;
        }
        public void FindId(int id) 
        {
            bool found = true;
            foreach (Student student in data)
            {
                if (student.Id == id) 
                {
                    Console.WriteLine(student);
                    found = false;
                }
            }
            if (found != false)
            {
                throw new StudentNotFoundException("No Student Found by Id");
            }
        }
    }
}
