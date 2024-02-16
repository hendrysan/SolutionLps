namespace Solution.LogicalCode
{
    public class MemoryLeakFourth
    {
        class Cache
        {
            private static Dictionary<int, object> _cache = new Dictionary<int, object>();
            public static void Add(int key, object value)
            {
                _cache.Add(key, value);
            }
            public static object Get(int key)
            {
                return _cache[key];
            }
        }
        public void Main()
        {
            for (int i = 0; i < 1000000; i++)
            {
                Cache.Add(i, new object());
            }
            Console.WriteLine("Cache populated");
            Console.WriteLine("please input key = ");
            string input = Console.ReadLine();
            Cache.Get(int.Parse(input));
        }
    }
}
