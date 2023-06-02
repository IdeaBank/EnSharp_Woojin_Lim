package commands;

import commandInterface.CommandInterface;
import constant.CommandResultType;
import model.PromptData;
import view.PromptView;

public class Help implements CommandInterface {
    @Override
    public void executeCommand(PromptData promptData, String command) {
        String[] commandToken = command.split(" ");

        // 명령어가 제대로 되어있으면 help 문구 출력
        if(isCommandValid(command) == CommandResultType.SUCCESS) {
            PromptView.getInstance().printHelp();
        }

        // 명령어가 이상하면 오류 문구 출력
        else {
            PromptView.getInstance().printMessage("'" + commandToken[0] +"'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
        }
    }

    @Override
    public CommandResultType isCommandValid(String command) {
        // help이거나 help로 시작하고 띄어쓰기가 있으면 정상 명령어로 판단
        if(command.equals("help") || command.startsWith("help ")) {
            return CommandResultType.SUCCESS;
        }

        return CommandResultType.COMMAND_NOT_EXIST;
    }

    @Override
    public String[] getCommandToken(String command) {
        return command.split(" ");
    }
}
