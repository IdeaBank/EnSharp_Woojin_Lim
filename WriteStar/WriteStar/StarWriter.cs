using System;

namespace WriteStar
{
    public class StarWriter
    {
        public StarWriter()
        {
            // 빈 생성자
        }

        private bool IsDigitsOnly(string str)
        {
            if (str.Length == 0)
                return false;
            
            foreach (char c in str)
                if (c < '0' || c > '9')
                    return false;
            // 길이가 0이거나, 숫자가 아닌 문자가 있으면 false 반환
            
            return true;
            // 숫자로 이루어졌을 경우에는 true 반환
        }
        
        public bool InputType()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("*****************************");
                Console.WriteLine("출력할 별의 형태를 숫자로 입력해주세요.");
                Console.WriteLine("1. 가운데 정렬 별 찍기");
                Console.WriteLine("2. 1번의 반대로 찍기");
                Console.WriteLine("3. 모래시계");
                Console.WriteLine("4. 다이아");
                Console.WriteLine("0. 프로그램 종료");
                Console.WriteLine("*****************************");

                string tempInput = Console.ReadLine();

                if (IsDigitsOnly(tempInput) == false)
                    continue;
                // 문자가 있거나 입력 값이 없을 경우, 다시 입력 받음

                this._type = Int32.Parse(tempInput);
                // 출력할 별의 형태 저장

                if (!(0 <= this._type && this._type <= 4))
                    continue;
                // 0~4가 아닐 경우, 다시 입력 받음

                if (this._type == 0)
                    return false;
                // 0일 경우, 프로그램을 종료하기 위해 false(비정상 입력) 반환

                return true;
                // 1~4일 경우, 정상 입력으로 판단해 true 반환
            }
        }

        public bool InputLines()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("********************");
                Console.WriteLine("출력할 줄의 수를 입력하세요");
                Console.WriteLine("********************");

                string tempInput = Console.ReadLine();

                if (IsDigitsOnly(tempInput) == false)
                    continue;
                // 문자가 있거나 입력 값이 없을 경우, 다시 입력 받음

                this._totalLines = Int32.Parse(tempInput);
                // 출력할 별의 줄 수 저장

                if (this._totalLines == 0)
                    return false;
                // 0일 경우, 비정상적인 입력이므로 false 반환 (음수일 경우에는 -가 들어가 있으므로 문자로 취급되어, 다시 입력 받음)

                return true;
                // 1 이상일 경우, 정상 입력으로 판단해 true 반환
            }
        }

        private void PrintPyramid()
        {
            for (int i = 0; i < this._totalLines; ++i)
            {
                for (int j = 0; j < this._totalLines - i - 1; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < i * 2 + 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.WriteLine();
                // 한 줄 내림
            }
        }

        private void PrintRevPyramid()
        {
            for (int i = 0; i < this._totalLines; ++i)
            {
                for (int j = 0; j < i; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < (this._totalLines - i) * 2 - 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.WriteLine();
                // 한 줄 내림
            }
        }

        private void PrintHourGlass()
        {
            PrintRevPyramid();
            PrintPyramid();
            // 역피라미드-피라미드 순으로 합쳐서 출력
        }

        private void PrintDiamond()
        {
            // 모래시계와 달리, 가운데 있는 줄은 한 번만 등장하기 때문에 직접 출력
            
            for (int i = 0; i < this._totalLines; ++i)
            {
                for (int j = 0; j < this._totalLines - i - 1; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < i * 2 + 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.WriteLine();
                // 한 줄 내림
            }
            
            for (int i = 1; i < this._totalLines; ++i)
            {
                for (int j = 0; j < i; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < (this._totalLines - i) * 2 - 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.WriteLine();
                // 한 줄 내림
            }
        }
        
        public void PrintStar()
        {
            Console.Clear();
            // 별을 출력하기 위해 미리 콘솔창 깔끔하게 정리
            
            switch (_type) // 타입에 맞는 별 출력
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
            
            Console.WriteLine("계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
            Console.Clear();
        }

        private int _type; // 출력 형태
        private int _totalLines; // 출력할 줄 수
    }
}