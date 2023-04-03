using System;

namespace WriteStar
{
    public class StarWriter
    {
        public StarWriter()
        {
            // 빈 생성자
        }

        public bool InputType()
        {
            this.type = Int32.Parse(Console.ReadLine());
            // 출력할 별의 형태를 입력 받음.

            if (!(0 <= this.type && this.type <= 4))
                this.InputType();
            // 0~4가 아닐 경우, 계속 입력 받음

            if (this.type == 0)
                return false;
            // 0일 경우, 프로그램을 종료하기 위해 false(비정상 입력) 반환

            return true;
            // 1~4일 경우, 정상 입력으로 판단해 true 반환
        }

        public bool InputLines()
        {
            this.totalLines = Int32.Parse(Console.ReadLine());
            // 출력할 별의 형태를 입력 받음.

            if (this.totalLines < 0)
                return false;
            // 0 이하일 경우, 비정상적인 입력이므로 false 반환

            return true;
            // 1 이상일 경우, 정상 입력으로 판단해 true 반환
        }

        public void PrintStar()
        {
            switch (type) // 타입에 맞는 별 출력
            {
                case 1:
                    PrintPyramid();
                    break;
                case 2:
                    PrintRevPyramid();
                    break;
                case 3:
                    PrintHourGlass();
                    break;
                case 4:
                    PrintDiamond();
                    break;
                default:
                    return;
            }
        }

        public void PrintPyramid()
        {
            for (int i = 0; i < this.totalLines; ++i)
            {
                for (int j = 0; j < this.totalLines - i - 1; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < i * 2 + 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.WriteLine();
                // 한 줄 내림
            }
        }

        public void PrintRevPyramid()
        {
            for (int i = 0; i < this.totalLines; ++i)
            {
                for (int j = 0; j < i; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < (this.totalLines - i) * 2 - 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.WriteLine();
                // 한 줄 내림
            }
        }

        public void PrintHourGlass()
        {
            PrintRevPyramid();
            PrintPyramid();
            // 역피라미드-피라미드 순으로 합쳐서 출력
        }

        public void PrintDiamond()
        {
            // 모래시계와 달리, 가운데 있는 줄은 한 번만 등장하기 때문에 직접 출력
            
            for (int i = 0; i < this.totalLines; ++i)
            {
                for (int j = 0; j < this.totalLines - i - 1; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < i * 2 + 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.WriteLine();
                // 한 줄 내림
            }
            
            for (int i = 1; i < this.totalLines; ++i)
            {
                for (int j = 0; j < i; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < (this.totalLines - i) * 2 - 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.WriteLine();
                // 한 줄 내림
            }
        }

        private int type; // 출력 형태
        private int totalLines; // 출력할 줄 수
    }
}