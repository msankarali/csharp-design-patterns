using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator mediator = new Mediator();
            Teacher muharrem = new Teacher(mediator) { Name = "Muharrem" };
            mediator.Teacher = muharrem;

            Student servet = new Student(mediator) { Name = "Servet" };
            Student samet = new Student(mediator) { Name = "Samet" };
            mediator.Students = new List<Student> { servet, samet };

            muharrem.SendNewImageUrl("slide1.png");
            muharrem.ReceiveQuestion("gj", servet);


            Console.ReadLine();

        }
    }

    abstract class CourseMember
    {
        protected Mediator Mediator;

        protected CourseMember(Mediator mediator)
        {
            Mediator = mediator;
        }
    }

    class Teacher : CourseMember
    {
        public Teacher(Mediator mediator) : base(mediator)
        {

        }

        public string Name { get; set; }

        public void ReceiveQuestion(string question, Student student)
        {
            Console.WriteLine("Teacher received question from {0} - {1}", student.Name, question);
        }

        public void SendNewImageUrl(string url)
        {
            Console.WriteLine("Teacher changed slide: {0}", url);
            Mediator.UploadImage(url);
        }

        public void AnswerQuestion(string answer, Student student)
        {
            Console.WriteLine("Teacher answered question {0} - {1}", student.Name, answer);
        }
    }

    class Student : CourseMember
    {
        public Student(Mediator mediator) : base(mediator)
        {

        }

        public string Name { get; set; }
        public void ReceiveImage(string url)
        {
            Console.WriteLine("{0} received image: {1}", Name, url);
        }

        public void ReceiveAnswer(string answer)
        {
            Console.WriteLine("{0} received answer: {1}", Name, answer);
        }
    }

    class Mediator
    {
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        public void UploadImage(string url)
        {
            foreach (var student in Students)
            {
                student.ReceiveImage(url);
            }
        }

        public void SendQuestion(string question, Student student)
        {
            Teacher.ReceiveQuestion(question, student);
        }

        public void SendAnswer(string answer, Student student)
        {
            student.ReceiveAnswer(answer);
        }
    }
}
