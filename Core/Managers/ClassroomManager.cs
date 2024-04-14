using Core.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Managers
{
    public class ClassroomManager
    {
        Classroom[] data = new Classroom[0];


        public void Add(Classroom entity)
        {

            int len = data.Length;
            Array.Resize(ref data, len + 1);
            data[len] = entity;
        }
        public Classroom[] GetAll()
        {
            return data;
        }
        public void ShowAll()
        {
            Console.WriteLine("##################Classrooms#################");
            foreach (Classroom item in GetAll())
            {
                Console.WriteLine(item);
            }
        }
        public void Remove(int id)
        {
            Classroom[] filteredClassroom = new Classroom[0];
            bool found = true;
            foreach (var item in data)
            {
                if (!(item.Id == id))
                {
                    int len = filteredClassroom.Length;
                    Array.Resize(ref filteredClassroom, len + 1);
                    filteredClassroom[len] = item;
                    found = false;
                }
            }
            if (found != false)
            {
                throw new ClasroomNotFoundException("No Classroom Found by Id");
            }
            data = filteredClassroom;
        }
        public void FindId(int id)
        {
            bool found = true;
            foreach (Classroom classroom in data)
            {
                if (classroom.Id == id)
                {
                    Console.WriteLine(classroom);
                    found = false;
                }
            }
            if (found != false)
            {
                throw new ClasroomNotFoundException("No Classroom Found by Id");
            }
        }
    }
}
