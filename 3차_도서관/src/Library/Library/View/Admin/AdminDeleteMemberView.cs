using System;

namespace Library.View.Admin
{
    public class AdminDeleteMemberView: ViewFrame
    {
        public static void Print()
        {
            Console.Clear();

            Console.Write("Enter user ID to delete: ");
        }
    }
}