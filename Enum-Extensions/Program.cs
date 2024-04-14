using Core;
using Core.Infrastructure;
using Core.Managers;
using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace Enum_Extensions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            string title = "Taska xoş geldiniz rahat olun berk birşeydi :)";
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);
            Console.ResetColor();

            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.Title = "C# Project";


            StudentManager studentMgr = new StudentManager();
            ClassroomManager classMgr = new ClassroomManager();

            #region variables
            string name;
            int searchId;
            int count = 1;
        #endregion

        readmenu:
            PrintMenu();

            Menu menu = HelpManager.ReadMenu("Choose from Menu: ");
            switch (menu)
            {
                case Menu.ClassroomAdd:
                    Console.Clear();
                    Classroom c = new Classroom();
                    do
                    {
                        name = HelpManager.ReadString("Write The Name of Classroom: ");
                        c.Name = name;
                    } while (!HelpManager.ClassNameChecker(name));

                l4:
                    PrintClassroomTypeMenu();
                    ClassroomType classroomType = HelpManager.ReadClassroomMenu("Choose from ClassroomType Menu: ");

                    switch (classroomType)
                    {
                        case ClassroomType.Backend:
                            c.ClassroomType = ClassroomType.Backend;
                            break;
                        case ClassroomType.Frontend:
                            c.ClassroomType = ClassroomType.Frontend;
                            break;
                        default:
                            HelpManager.PrintError("Wrong choice, Try again!!!");
                            goto l4;
                    }
                    classMgr.Add(c);
                    Console.Clear();

                    goto readmenu;
                case Menu.ClassroomAll:
                    Console.Clear();
                    classMgr.ShowAll();
                    goto readmenu;

                case Menu.ClassroomSingle:
                    Console.Clear();
                    classMgr.ShowAll();
                    searchId = HelpManager.ReadInteger("Enter Id of Classroom: ");
                    classMgr.FindId(searchId);
                    goto readmenu;

                case Menu.ClassroomRemove:
                    Console.Clear();
                    classMgr.ShowAll();
                    searchId = HelpManager.ReadInteger("Enter Id of Classroom: ");
                    classMgr.Remove(searchId);
                    classMgr.ShowAll();
                    goto readmenu;
                case Menu.StudentAdd:
                    Console.Clear();
                  
                    Student s = new Student();
                    
                   
                    do
                    {
                        name = HelpManager.ReadString("Write The Name of Student: ");
                        s.Name = name;
                    } while (!HelpManager.FullnameChecker(name));

                    do
                    {
                        name = HelpManager.ReadString("Write The Surname of Student: ");
                        s.Surname = name;
                    } while (!HelpManager.FullnameChecker(name));
                    if (classMgr.GetAll().Length == 0)
                    {
                        HelpManager.PrintError("There is No Classroom");
                        string answer;
                        do
                        {
                            Console.WriteLine("Do you Want to Add Classroom?   y/n");
                            answer = HelpManager.ReadString("Enter you Choice: ");
                        } while (!(answer.ToLower() == "y" || answer.ToLower() == "n"));

                        if (answer.ToLower() == "y")
                        {
                            Console.Clear();
                            Classroom c1 = new Classroom();
                            do
                            {
                                name = HelpManager.ReadString("Write The Name of Classroom: ");
                                c1.Name = name;
                            } while (!HelpManager.ClassNameChecker(name));

                        l5:
                            PrintClassroomTypeMenu();
                            ClassroomType classroomTypeHandle = HelpManager.ReadClassroomMenu("Choose from ClassroomType Menu: ");

                            switch (classroomTypeHandle)
                            {
                                case ClassroomType.Backend:
                                    c1.ClassroomType = ClassroomType.Backend;
                                    break;
                                case ClassroomType.Frontend:
                                    c1.ClassroomType = ClassroomType.Frontend;
                                    break;
                                default:
                                    HelpManager.PrintError("Wrong choice, Try again!!!");
                                    goto l5;
                            }
                   
                            classMgr.Add(c1);
                            Console.Clear();

                        }
                        else if (answer.ToLower() == "n")
                        {
                            goto readmenu;
                        }
                    }
                classId:
                    classMgr.ShowAll();
                    s.ClassroomId = HelpManager.ReadInteger("Write The Id of Classroom: ");
                    var classroomIdForStudent = classMgr.GetAll().FirstOrDefault(item => item.Id == s.ClassroomId);
                    if (classroomIdForStudent == null)
                    {
                        HelpManager.PrintError("Wrong ClassroomId, Try again!!");
                        goto classId;
                    }

                    Classroom classForTypes = classMgr.GetAll().FirstOrDefault(item => item.Id ==s.ClassroomId);
                    int studentClassTypesCount = studentMgr.GetAll().Where(item=>item.ClassroomId == classForTypes.Id).Count();

                    if (studentClassTypesCount + 1 <= classForTypes.StudentLimit)
                    {
                        studentMgr.Add(s);
                    }
                    else
                    {
                        HelpManager.PrintError("Out of Bounds");
                    }


                    goto readmenu;
                case Menu.StudentAll:
                    Console.Clear();
                    studentMgr.ShowAll();
                    goto readmenu;

                case Menu.StudentSingle:
                    Console.Clear();
                    studentMgr.ShowAll();
                    searchId = HelpManager.ReadInteger("Enter Id of Student: ");
                    studentMgr.FindId(searchId);
                    goto readmenu;

                case Menu.StudentRemove:
                    Console.Clear();
                    studentMgr.ShowAll();
                    searchId = HelpManager.ReadInteger("Enter Id of Student: ");
                    studentMgr.Remove(searchId);
                    studentMgr.ShowAll();
                    goto readmenu;
                case Menu.Exit:

                    goto lEnd;
                default:
                    Console.Clear();
                    HelpManager.PrintError("Wrong Choice, Try again!!");
                    Console.Clear();
                    goto readmenu;
            }

        lEnd:
            Console.WriteLine("End......");
            Console.WriteLine("Click the Random BUTTON for Quit.");
            Console.ReadKey();
        }




        static void PrintMenu()
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            foreach (var item in Enum.GetNames(typeof(Menu)))
            {
                Menu m = (Menu)Enum.Parse(typeof(Menu), item);
                Console.WriteLine($"{((byte)m).ToString().PadLeft(2)}. {item}");
            }
            Console.WriteLine(new string('-', Console.WindowWidth));
        }
        static void PrintClassroomTypeMenu()
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine("############ ClassroomTypes  ############");
            foreach (var item in Enum.GetNames(typeof(ClassroomType)))
            {
                ClassroomType f = (ClassroomType)Enum.Parse(typeof(ClassroomType), item);
                Console.WriteLine($"{((byte)f).ToString().PadLeft(2)}. {item}");
            }
            Console.WriteLine(new string('-', Console.WindowWidth));
        }
    }
}
