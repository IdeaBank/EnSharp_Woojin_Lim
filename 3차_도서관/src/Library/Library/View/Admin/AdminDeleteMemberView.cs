using System;

namespace Library.View.Admin
{
    public class AdminDeleteMemberView: ViewFrame
    {
        public static void Print()
        {
            Console.Write("삭제할 유저의 ID를 입력하세요: ");
        }
    }
}