namespace Solution.LogicalCode
{
    public class OpinionFirst
    {

        /*
         1. How about your opinion..?
            if (application != null)
            {
                if (application.protected != null)
                {
                    return application.protected.shieldLastRun;
                }
            }

            Key:
            cleaner and easier to read code.
        
        
        My Opinion:
        I think the code is cleaner and easier to read if you use the null-conditional operator in application object.
        The null-conditional operator is a new feature in C# 6.0 that allows you to access members and elements only when the receiver is not null.

        In my opinion it would be simpler if written as follows:

        return application?.protected?.shieldLastRun;
         */
    }
}
