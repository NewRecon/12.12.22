using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _12._12._22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Realizator a = new Realizator();
            //((IA)a).Foo();
            //IA ia = new Realizator();
            //ia.Foo();
            //IB ib = new Realizator();
            //ib.Foo();
            //IC ic = new Realizator();
            //ic.Foo();

            //// интерфейс IComparable
            //Auditory auditory = new Auditory();

            //auditory.Sort();

            //// интерфейс IEnumerable
            //foreach (Student e in auditory)
            //{
            //    Console.WriteLine(e.FirstName + " " + e.FirstName);
            //}

            //// интерфейс IComparer
            //auditory.Sort(new ArrayComparer());

            
            Calculator calculator = new Calculator();
            string expresson = Console.ReadLine();
            char znak = ' ';
            foreach(var e in expresson)
            {
                if (e == '+' || e == '-' || e == '*' || e == '/')
                {
                    znak = e;
                    break;
                }
            }
            var numbers = expresson.Split(znak);
            CalcDelegate calcDelegate = null;
            //switch (znak)
            //{
            //    case '+':
            //        calcDelegate = new CalcDelegate(calculator.Add);
            //        break;
            //    case '-':
            //        calcDelegate = new CalcDelegate(Calculator.Sub);
            //        break;
            //    case '*':
            //        calcDelegate = calculator.Mult;
            //        break;
            //    case '/':
            //        calcDelegate = calculator.Div;
            //        break;
            //    default:
            //        throw new InvalidOperationException();
            //}
            //Console.WriteLine($"Result: {calcDelegate(double.Parse(numbers[0]), double.Parse(numbers[1]))}");

            // мультикаст
            calcDelegate += calculator.Add;
            calcDelegate += Calculator.Sub;
            calcDelegate += calculator.Mult;
            calcDelegate += calculator.Div;

            foreach(CalcDelegate e in calcDelegate.GetInvocationList())
            {
                Console.WriteLine($"Result: {e(double.Parse(numbers[0]), double.Parse(numbers[1]))}");
            }
        }

        // делегаты
        //System.Delegate
        //System.MulticastDelegate
        //delegate
        // object.Clone()
        // Combine(delegate, delegate) - создаёт мультикаст
        // CreateDelegate(Type, MethodInfo)
        //public delegate int IntDelegate(double d);
        //public delegate void VoidDelegate(int i);
        // 1. делегат - основа для события
        // 2. событие не существует без делегата
        // 3. делегат - основа для анонимного метода
        // 4. могут быть вызваны как в синхронном, так и в асинхронном режиме
        // 5. делегаты используются для определения метода обратного вызова
    }
    interface IA
    {
        void Foo();
    }
    interface IB
    {
        void Foo();
    }
    interface IC
    {
        void Foo();
    }
    class Realizator : IA, IB, IC
    {
        
        void IA.Foo()
        {
            Console.WriteLine("Implict realization IA");
        }
        void IB.Foo()
        {
            Console.WriteLine("Implict realization IB");
        }
        void IC.Foo()
        {
            Console.WriteLine("Implict realization IC");
        }
    }

    class Student : IComparable
    {
        public string FirstName;
        public string LastName;
        public int CompareTo(object obj)
        {
            // мой вариант с перегрузкой ToString
            return ToString().CompareTo(obj.ToString());

            // вариант препода
            //if (obj is Student)
            //    return LastName.CompareTo((obj as Student).LastName);

            throw new NotFiniteNumberException();
        }
        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
    class Auditory : IEnumerable
    {
        Student[] students =
        {
            new Student
            {
                FirstName = "Иван",
                LastName = "Иванов"
            },
            new Student
            {
                FirstName = "Сидор",
                LastName = "Сидоров"
            },
            new Student
            {
                FirstName = "Петр",
                LastName = "Метров"
            },
            new Student
            {
                FirstName = "Алексей",
                LastName = "Алексеев"
            }
        };
        IEnumerator IEnumerable.GetEnumerator()
        {
            return students.GetEnumerator();
        }
        public void Sort ()
        {
            Array.Sort(students);
        }
        public void Sort (IComparer comparer)
        {
            Array.Sort(students, comparer);
        }
    }
    class ArrayComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is Student && y is Student)
            {
                return string.Compare((x as Student).LastName, (y as Student).LastName);
            }
            throw new NotImplementedException();
        }
    }



    // делегаты

    public delegate double CalcDelegate(double x, double y);

    class Calculator
    {
        public double Add(double x, double y)
        {
            return x + y;
        }
        public static double Sub(double x, double y)
        {
            return x - y;
        }
        public double Mult(double x, double y)
        {
            return x * y;
        }
        public double Div(double x, double y)
        {
            if (y != 0) 
                return x / y;
            throw new DivideByZeroException();
        }
    }
}
