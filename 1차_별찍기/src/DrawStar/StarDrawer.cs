using System;

namespace DrawStar
{
    static class Constants
    {
        public const int MAX_INPUT = 30; // 최대로 입력할 수 있는 줄의 수
        public const int MAX_WIDTH = (MAX_INPUT * 2 + 1) + 4; // 테두리 너비
        public const int MAX_HEIGHT = MAX_INPUT * 2 + 4; // 테두리 높이
    }
    public class StarDrawer
    {
        public StarDrawer()
        {
            // 빈 생성자
        }

        private bool IsDigitsOnly(string str)
        {
            if (str.Length == 0)
                return false;
            // str이 비어 있을 경우, false 반환
            
            foreach (char c in str)
                if (c < '0' || c > '9')
                    return false;
            // 길이가 0이거나, 숫자가 아닌 문자가 있으면 false 반환
            
            return true;
            // 숫자로 이루어졌을 경우에는 true 반환
        }

        private void DrawContour()
        {
            // 외곽선 출력을 위한 함수
            Console.WriteLine(new string('#', Constants.MAX_WIDTH));
            
            for (int i = 0; i < Constants.MAX_HEIGHT - 2; ++i)
            {
                Console.Write("#");
                Console.Write(new string(' ', Constants.MAX_WIDTH - 2));
                Console.Write("#");
                Console.WriteLine();
            }
            
            Console.WriteLine(new string('#', Constants.MAX_WIDTH));
        }
        
        public bool InputMenu()
        {
            this._totalLines = 3;
            // 3줄씩 출력하기 위해 임시로 설정
            
            while(true)
            {
                Console.Clear();
                DrawContour();
                // 외곽선 그려주기
                
                string str;
                
                // 메뉴 각각 출력
                str = "1. 가운데 정렬 별 찍기";
                Console.SetCursorPosition(Constants.MAX_WIDTH / 4 - str.Length / 2 - 3, Constants.MAX_HEIGHT / 4 + 3);
                Console.Write(str);
                DrawPyramid(Constants.MAX_WIDTH / 4 - 3, Constants.MAX_HEIGHT / 4);

                str = "2. 1번의 반대로 찍기";
                Console.SetCursorPosition(Constants.MAX_WIDTH / 4 * 3 - str.Length / 2 - 3, Constants.MAX_HEIGHT / 4 + 3);
                Console.Write(str);
                DrawReversePyramid(Constants.MAX_WIDTH / 4 * 3 - 3, Constants.MAX_HEIGHT / 4);
                
                str = "3. 모래시계";
                Console.SetCursorPosition(Constants.MAX_WIDTH / 4 - str.Length / 2 - 3, Constants.MAX_HEIGHT / 4 * 3 + 3);
                Console.Write(str);
                DrawHourGlass(Constants.MAX_WIDTH / 4 - 3, Constants.MAX_HEIGHT / 4 * 3 - 3);
                
                str = "4. 다이아";
                Console.SetCursorPosition(Constants.MAX_WIDTH / 4 * 3 - str.Length / 2 - 3, Constants.MAX_HEIGHT / 4 * 3 + 3);
                Console.Write(str);
                DrawDiamond(Constants.MAX_WIDTH / 4 * 3 - 3, Constants.MAX_HEIGHT / 4 * 3 - 3);

                str = "Enter Menu";
                Console.SetCursorPosition(Constants.MAX_WIDTH / 2 - str.Length / 2 - 3, Constants.MAX_HEIGHT - 4);
                Console.Write(str);

                
                // 입력창
                str = "_________";
                Console.SetCursorPosition(Constants.MAX_WIDTH / 2 - str.Length / 2 - 3, Constants.MAX_HEIGHT - 3);
                Console.Write(str);
                
                Console.SetCursorPosition(Constants.MAX_WIDTH / 2 - 3, Constants.MAX_HEIGHT - 3);

                // 임시로 입력 값 저장
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

        public void InputLines()
        {
            while (true)
            {
                Console.SetCursorPosition(0, Constants.MAX_HEIGHT);
                // 외곽선 밖으로 커서 옮겨주기
                
                Console.WriteLine("********************");
                Console.WriteLine("출력할 줄의 수를 입력하세요");
                Console.WriteLine("********************");

                // 임시로 입력한 값 저장
                string tempInput = Console.ReadLine();

                if (IsDigitsOnly(tempInput) == false)
                    continue;
                // 문자가 있거나 입력 값이 없을 경우, 다시 입력 받음

                this._totalLines = Int32.Parse(tempInput);
                // 출력할 별의 줄 수 저장

                if (0 < this._totalLines && this._totalLines <= Constants.MAX_INPUT)
                {
                    Console.Clear();
                    return;
                }
                // 1 이상 MAX_INPUT 이하일 때만 화면을 지우고 return, 아닐 시 다시 입력 받음
            }
        }

        private void DrawPyramid(int x, int y)
        {
            Console.SetCursorPosition(x, y);

            for (int i = 0; i < this._totalLines; ++i)
            {
                for (int j = 0; j < this._totalLines - i - 1; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < i * 2 + 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.SetCursorPosition(x, Console.CursorTop + 1);
                // 한 줄 내림
            }
        }

        private void DrawReversePyramid(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            
            for (int i = 0; i < this._totalLines; ++i)
            {
                for (int j = 0; j < i; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < (this._totalLines - i) * 2 - 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.SetCursorPosition(x, Console.CursorTop + 1);
                // 한 줄 내림
            }
        }

        private void DrawHourGlass(int x, int y)
        {
            DrawReversePyramid(x, y);
            DrawPyramid(x, y + this._totalLines);
            // 역피라미드-피라미드 순으로 합쳐서 출력
        }

        private void DrawDiamond(int x, int y)
        {
            // 모래시계와 달리, 가운데 있는 줄은 한 번만 등장하기 때문에 직접 출력
            Console.SetCursorPosition(x, y);
            
            for (int i = 0; i < this._totalLines; ++i)
            {
                for (int j = 0; j < this._totalLines - i - 1; ++j)
                    Console.Write(" ");
                // 왼쪽 칸 띄워줌
                
                for(int j = 0; j < i * 2 + 1; ++j)
                    Console.Write("*");
                // 별 찍기
                
                Console.SetCursorPosition(x, Console.CursorTop + 1);
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
                
                Console.SetCursorPosition(x, Console.CursorTop + 1);
                // 한 줄 내림
            }
        }
        
        public int DrawStar()
        {
            Console.Clear();
            DrawContour();
            // 별을 출력하기 위해 미리 콘솔창 깔끔하게 정리
            
            switch (_type) // 타입에 맞는 별 출력
            {
                case 1:
                    DrawPyramid(Constants.MAX_WIDTH / 2 - this._totalLines + 1, 2);
                    break;
                case 2:
                    DrawReversePyramid(Constants.MAX_WIDTH / 2 - this._totalLines + 1, 2);
                    break;
                case 3:
                    DrawHourGlass(Constants.MAX_WIDTH / 2 - this._totalLines + 1, 2);
                    break;
                case 4:
                    DrawDiamond(Constants.MAX_WIDTH / 2 - this._totalLines + 1, 2);
                    break;
                default:
                    return 1;
            }
            
            string str = "1. 다시하기, 2: 메뉴로, 3. EXIT";
            Console.SetCursorPosition(Constants.MAX_WIDTH / 2 - str.Length / 2, Constants.MAX_HEIGHT - 4);
            Console.Write(str);

            string tempInput;
            int retry;

            while (true)
            {
                str = new string('_', 10);
                Console.SetCursorPosition(Constants.MAX_WIDTH / 2 - str.Length / 2, Constants.MAX_HEIGHT - 3);
                Console.Write(str);

                Console.SetCursorPosition(Constants.MAX_WIDTH / 2, Constants.MAX_HEIGHT - 3);

                tempInput = Console.ReadLine();

                if (IsDigitsOnly(tempInput))
                {
                    retry = Int32.Parse(tempInput);

                    if(retry == 1 || retry == 2 || retry == 3)
                       return retry;
                }
            }

            return 2;
        }

        private int _type; // 출력 형태
        private int _totalLines; // 출력할 줄 수
    }
}