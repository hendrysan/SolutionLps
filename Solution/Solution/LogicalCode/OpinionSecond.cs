namespace Solution.LogicalCode
{
    public class OpinionSecond
    {
        /* 
         * original code
        public ApplicationInfo GetInfo()
        {
            var application = new ApplicationInfo
            {
                Path = "C:/apps/",
                Name = "Shield.exe"
            };
            return application;
        }

        Key:
return more than one value from a class method

        My Opinion:
        I think the code can return more than one value from a class method if you use a tuple or list object.

          In my opinion it would be simpler if written as follows:

        public (string, string) GetInfo()
        {
            var path = "C:/apps/";
            var name = "Shield.exe";
            return (path, name);
        }

        OR 

        public List<ApplicationInfo> GetInfo()
        {
            var applications = new List<ApplicationInfo>
            {
                new ApplicationInfo
                {
                    Path = "C:/apps/",
                    Name = "Shield.exe"
                },
                new ApplicationInfo
                {
                    Path = "C:/apps/",
                    Name = "Shield.exe"
                }
            };
            return applications;
        }
        */

        public List<ApplicationInfo> GetInfo()
        {
            var applications = new List<ApplicationInfo>
            {
                new ApplicationInfo
                {
                    Path = $"C:/apps/",
                    Name = "Shield.exe"
                },
                new ApplicationInfo
                {
                    Path = $"C:/apps/",
                    Name = "Shield.exe"
                }
            };
            return applications;
        }



    }

    public class ApplicationInfo
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
