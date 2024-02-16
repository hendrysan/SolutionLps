namespace Solution.LogicalCode
{
    public class OpinionThrid
    {

        /*
         
        class Laptop
        {
            public string Os{ get; set; } // can be modified
            public Laptop(string os)
            {
                Os= os;
            }
        }
        var laptop = new Laptop("macOs");
        Console.WriteLine(Laptop.Os); // Laptop os: macOs

         */


        class  Laptop
        {
            public string Os { get; } // can be modified
            public Laptop(string os)
            {
                Os = os;
            }
        }

        public string Main()
        {
            var laptop = new Laptop("macOs");
            var data = laptop.Os;
            Console.WriteLine(data); // Laptop os: macOs

            return data;
        }
    }
}
