

namespace Quiz {
    class Program {
        public static void Main() {
            Console.WriteLine("Test Data:");
            Console.Write("Enter First Number: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("Enter Second Number: ");
            int y = int.Parse(Console.ReadLine());
            Console.Write("Enter Third Number: ");
            int z = int.Parse(Console.ReadLine());
            int maths1 = (x+y)*z;
            int maths2 = (x*y + y*z);
            Console.WriteLine("Expected Output:");
            Console.WriteLine($"Result of specified numbers {x}, {y}, and {z}, (x+y)*z is {maths1} and x*y + y*z is {maths2}");
        }
    }
}