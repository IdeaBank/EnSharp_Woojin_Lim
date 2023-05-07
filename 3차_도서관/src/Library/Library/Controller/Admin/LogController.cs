using System;
using System.Collections.Generic;
using System.Net;
using Library.Constant;
using Library.Model;
using Library.Model.DAO;
using Library.Model.DTO;
using Library.Utility;

namespace Library.Controller.Admin
{
    public class LogController
    {
        private int currentSelectionIndex;
        
        public LogController()
        {
            this.currentSelectionIndex = 0;
        }

        public void SelectLogMenu()
        {
            Console.Clear();
            ReturnedValue result = new ReturnedValue(ResultCode.SUCCESS, -1);
            
            while (result.ResultCode != ResultCode.ESC_PRESSED)
            {
                // UI 출력
                View.Admin.MenuView.getInstance.PrintAdminMenuContour();
                result = MenuSelector.getInstance.ChooseMenu(0, Constant.Menu.Count.LOG, Constant.Menu.Type.LOG);

                // ESC키가 눌렸으면 반환
                if (result.ResultCode == ResultCode.ESC_PRESSED)
                {
                    return;
                }

                // 커서 위치 저장
                this.currentSelectionIndex = result.ReturnedInt;

                // 다음 메뉴 진입
                EnterNextMenu();
            }
        }

        public void EnterNextMenu()
        {
            switch (this.currentSelectionIndex)
            {
                case Constant.Menu.Selection.EDIT_LOG:
                    EditLog();
                    break;
                
                case Constant.Menu.Selection.SAVE_LOG:
                    SaveLog();
                    break;
                
                case Constant.Menu.Selection.DELETE_LOG:
                    DeleteLog();
                    break;
                
                case Constant.Menu.Selection.RESET_LOG:
                    ResetLog();
                    break;

            }
        }

        private void EditLog()
        {
            Console.Clear();

            List<LogDTO> logs = LogDAO.getInstance.GetAllLog();
            
            View.Admin.LogView.getInstance.PrintLogs(logs);
            
            Console.Write("삭제할 로그 ID: ".PadLeft(ConsoleWriter.getInstance.GetPadCount("삭제할 로그 ID", 15)));

            UserInput input = new UserInput(ResultCode.NO, "");

            if (input.ResultCode != ResultCode.SUCCESS)
            {
                Console.SetCursorPosition(15, Console.CursorTop);
                
                input = UserInputManager.getInstance.GetRemoveLogWithId();

                if (input.ResultCode == ResultCode.ESC_PRESSED || !UserInputManager.getInstance.IsNumber(input.Input))
                {
                    return;
                }
            }

            switch(LogDAO.getInstance.DeleteLog(int.Parse(input.Input)))
            {
                case ResultCode.NO_LOG:
                    View.Admin.LogView.getInstance.PrintLogResult("ID: " + input.Input + "에 해당하는 로그가 존재하지 않습니다!", true);
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", input.Input, "로그 삭제 시도"));
                    break;
                case ResultCode.SUCCESS:
                    View.Admin.LogView.getInstance.PrintLogResult("ID: " + input.Input + "에 해당하는 로그를 삭제했습니다!");
                    LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", input.Input, "로그 삭제"));
                    break;
            }

            Console.ReadKey();
            
        }

        private void SaveLog()
        {
            Console.Clear();
            
            View.Admin.LogView.getInstance.PrintLogConfirmContour();
            View.Admin.LogView.getInstance.PrintLogResult("저장하시겠습니까? Y: 예, N: 아니오");

            if (UserInputManager.getInstance.InputYesOrNo() == ResultCode.YES)
            {
                LogDAO.getInstance.SaveFile();
            }
            
            Console.Clear();
            View.Admin.LogView.getInstance.PrintLogConfirmContour();
            View.Admin.LogView.getInstance.PrintLogResult("성공적으로 저장했습니다!");
            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "로그 파일 저장", "로그 파일 저장"));
            Console.ReadKey(true);
        }

        private void DeleteLog()
        {
            Console.Clear();
            
            View.Admin.LogView.getInstance.PrintLogConfirmContour();
            View.Admin.LogView.getInstance.PrintLogResult("로그파일을 삭제하시겠습니까? Y: 예, N: 아니오");

            if (UserInputManager.getInstance.InputYesOrNo() == ResultCode.YES)
            {
                LogDAO.getInstance.DeleteFile();
            }
            
            Console.Clear();
            View.Admin.LogView.getInstance.PrintLogConfirmContour();
            View.Admin.LogView.getInstance.PrintLogResult("성공적으로 삭제했습니다!");
            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "로그 파일 삭제", "로그 파일 삭제"));
            Console.ReadKey(true);
        }

        private void ResetLog()
        {
            Console.Clear();
            
            View.Admin.LogView.getInstance.PrintLogConfirmContour();
            View.Admin.LogView.getInstance.PrintLogResult("로그를 초기화하시겠습니까? Y: 예, N: 아니오");

            if (UserInputManager.getInstance.InputYesOrNo() == ResultCode.YES)
            {
                LogDAO.getInstance.ResetLog();
            }

            Console.Clear();
            View.Admin.LogView.getInstance.PrintLogConfirmContour();
            View.Admin.LogView.getInstance.PrintLogResult("성공적으로 초기화했습니다!");
            LogDAO.getInstance.InsertLog(new LogDTO(0, DateTime.Now.ToString(), "ADMIN", "로그 초기화", "로그 초기화"));
            Console.ReadKey(true);
        }
    }
}