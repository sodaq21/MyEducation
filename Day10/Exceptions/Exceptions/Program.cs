using System.Reflection.Metadata;

namespace Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public decimal AttemptedAmount { get; }
        public decimal AvailableBalance { get; }

        public InsufficientFundsException(decimal attempted, decimal available)
            : base($"Cannot withdraw {attempted}: only {available} available.")
        {
            AttemptedAmount = attempted;
            AvailableBalance = available;
        }
    }

    public class NullParameterException : Exception
    {
        public NullParameterException() : base("Given parameter is null."){}
    }

    public class DivisionByZeroException : Exception
    {
        public DivideByZeroException() : base("Division by zero is forbidden.")
        {
            
        }
    }

    public class IncorrectParameterException : Exception
    {
        public IncorrectParameterException() : base("Given parameter is incorrect.")
        {
            
        }
    }

    internal class Program
    {

        static double ParseAndDivide(string a, string b)
        {
            double new_a;
            double new_b;
            try
            {
                if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                    throw new NullParameterException();
                if (!(double.TryParse(a, out new_a) && double.TryParse(b, out new_b)))
                    throw new IncorrectParameterException();

                if (new_b == 0)
                    throw new DivisionByZeroException();

                return new_a / new_b;

            }
            catch (IncorrectParameterException ex)
            {
                Console.WriteLine(ex.Message);
                return double.NaN;
            }
            catch (NullParameterException ex)
            {
                Console.WriteLine(ex.Message);
                return double.NaN;
            }
            catch (DivisionByZeroException ex)
            {
                Console.WriteLine(ex.Message);
                return double.NaN;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown exception: " + ex.Message);
                return double.NaN;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(ParseAndDivide("", "0"));
            Console.WriteLine(ParseAndDivide("", "."));
            Console.WriteLine(ParseAndDivide("5", "0"));
            Console.WriteLine(ParseAndDivide("41", "as"));
        
            //         #region Theory
            //         try
            //         {
            //	Directory.Delete(@"asd");
            //}
            //catch (Exception ex)
            //{
            //             Console.WriteLine(ex.StackTrace);
            //	// throw; <- just 'throw' during debugging will redirect you to the line where exception occurs
            //	throw ex; // <- this 'throw ex' will redirect you to itself
            //         }

            //         try
            //         {
            //             // code where can be exception
            //         }
            //         catch (ArgumentNullException ex)
            //         {
            //             // if it didn't catch - going to the next catch{}
            //         }
            //         catch (ArgumentException ex)
            //         {

            //         }
            //         catch (Exception ex)
            //         {
            //             // catch order: more specific -> more general
            //         }
            //         finally
            //         {
            //             // code executes anyway
            //         }

            //         using (var reader = new StreamReader("data.txt"))
            //         {
            //             var line = reader.ReadLine(); // if execution occurs - reader will be closed
            //         } // Dispose() calls automatically 
            //         #endregion


        }
    }
}
