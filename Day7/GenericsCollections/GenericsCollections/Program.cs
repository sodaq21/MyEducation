namespace GenericsCollections
{
   
    class Base
    {
        public virtual void M<T1, T2>() 
            where T1 : struct 
            where T2 : class
        { }
    }

    class Derived : Base
    {
        public override void M<T1, T2>() 
            where T1: struct 
            // where T2 : Delegate // Error
        { }
        
    }


    class Item
    {

    }
    class Pen : Item
    {

    }
    class Notebook : Item
    {

    }

    internal class Program
    {
        static bool EqualsValues<T>(T val)
        {
            T temp = val;
            Console.WriteLine(val.ToString());
            bool b = temp.Equals(val); // any type can call ToString() and Equals() because all types have inherited from object
            return b;
        }

        static T Min<T>(T val1, T val2) where T : IComparable<T>, IConvertible // method can only get types that implement each interface
        {
            if (val1.CompareTo(val2) < 0) return val1;
            return val2;
        }

        static void Function1<T1, T2>(T1 val1, T2 val2)
        {

        }
        static void Function1(int val1, int val2)
        {

        }
        //static void Function1<T3, T4>(T3 val1, T4 val2) // Error, method with 2 parameters type T already exists
        //{

        //}

        static void Func1<T>(T val)
        {

        }
        //static void Func1<T>(T val) where T : IComparable<T> // Error
        //{

        //}

        static void RefFunc<T>(T referenceVar) where T : class // T can be only reference type
        {

        }
        static void ValFunc<T>(T valueVar) where T : struct // T can be only value type
        {

        }
        static void ItemFunc<T>(T valueVar) where T : Item // T can be any class that inherited from Item
        {

        }
        static void SettingToNull<T>(T value)
        {
            //value = null; // Error, convert null to type parameter 'T' because it could be a non-nullable value type
            value = default(T); // can do this way  
        }
        static void Main(string[] args)
        {
            EqualsValues(12);
            EqualsValues("Hello");
            EqualsValues(16.2);
            int min = Min(6, 1);
            double mind = Min(5.15, 5.6);
            string mins = Min("twe", "gasd");
            // Exception ex = Min(ArgumentException, TypeAccessException); // Error, Exception class doesn't implement IComparable<> and IConvertible

            Item i = new Item();
            Pen p = new Pen();
            ItemFunc(i);
            ItemFunc(p);
            Base b = new Base();
            //ItemFunc(b); // Error
            
        }
    }
}
