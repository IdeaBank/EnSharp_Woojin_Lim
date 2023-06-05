package view;


import util.ItemVerifier;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.text.DateFormat;
import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

public class PromptView {

    // Singleton 사용
    private static PromptView _instance;

    public static PromptView getInstance() {
        if(_instance == null) {
            _instance = new PromptView();
        }

        return _instance;
    }

    private PromptView() {

    }

    public void printPromptInfo(String buildVersion) {
        System.out.println(buildVersion);
        System.out.println("(c) Microsoft Corporation. All rights reserved.");
    }

    public void printWorkingDirectory(String workingDirectoryPath) {
        System.out.println(workingDirectoryPath);
    }

    public void printDriveInfo(String driveInfo) {
        System.out.println(driveInfo + "\n");
    }

    private String getFormattedNumber(long number) {
        DecimalFormat formatter = new DecimalFormat("#,###");

        return formatter.format(number);
    }

    public void printItemList(long availableSpace, File[] files, String location) {
        int fileCount = 0, directoryCount = 0, totalSize = 0;

        location = ItemVerifier.getInstance().getActualPath(location);

        System.out.println(" " + location + " 디렉터리\n");

        for(File file: files) {
            if(!file.isHidden() && !Files.isSymbolicLink(file.toPath())) {
                if (file.isFile()) {
                    printItemInfo(file);
                    fileCount += 1;
                    totalSize += file.length();
                    System.out.println(file.getName());
                }

                else if (file.isDirectory()) {
                    printItemInfo(file);
                    directoryCount += 1;
                    System.out.println(file.getName());
                }

            }
        }

        System.out.printf("              %d개 파일                 %d 바이트\n", fileCount, totalSize);
        System.out.printf("              %d개 디렉터리  %s 바이트 남음\n", directoryCount, getFormattedNumber(availableSpace));
    }

    private void printItemInfo(File file) {
        long lastModifiedTimestamp = file.lastModified();
        Date lastModifiedDate = new Date(lastModifiedTimestamp);
        DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd  a hh:mm    ");
        String lastModified = dateFormat.format(lastModifiedDate);

        if(file.isFile()) {
            System.out.print(lastModified);
            System.out.printf("%14s ", getFormattedNumber(file.length()));
        }

        else if(file.isDirectory()) {
            System.out.print(lastModified);
            System.out.printf("%-14s ", "<DIR>");
        }
    }

    public void clearPrompt() {
        for(int i = 0; i < 50; ++i) {
            System.out.println();
        }
    }

    public void printMessage(String str) {
        System.out.println(str);
    }

    public void printMessageWithNoNewline(String str) {
        System.out.print(str);
    }

    public void printNoCommand(String command) {
        PromptView.getInstance().printMessage("'" + command + "'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는\n배치 파일이 아닙니다.");
    }

    public void printHelp() {
        System.out.println("특정 명령어에 대한 자세한 내용이 필요하면 HELP 명령어 이름을 입력하십시오.\n" +
                "ASSOC    파일 확장명 연결을 보여주거나 수정합니다.\n" +
                "ATTRIB   파일 속성을 표시하거나 바꿉니다.\n" +
                "BREAK    확장된 CTRL+C 검사를 설정하거나 지웁니다.\n" +
                "BCDEDIT        부팅 로딩을 제어하기 위해 부팅 데이터베이스에서 속성을 설정합니다.\n" +
                "CACLS    파일의 액세스 컨트롤 목록(ACL)을 표시하거나 수정합니다.\n" +
                "CALL     한 일괄 프로그램에서 다른 일괄 프로그램을 호출합니다.\n" +
                "CD       현재 디렉터리 이름을 보여주거나 바꿉니다.\n" +
                "CHCP     활성화된 코드 페이지의 번호를 표시하거나 설정합니다.\n" +
                "CHDIR    현재 디렉터리 이름을 보여주거나 바꿉니다.\n" +
                "CHKDSK   디스크를 검사하고 상태 보고서를 표시합니다.\n" +
                "CHKNTFS  부팅하는 동안 디스크 확인을 화면에 표시하거나 변경합니다.\n" +
                "CLS      화면을 지웁니다.\n" +
                "CMD      Windows 명령 인터프리터의 새 인스턴스를 시작합니다.\n" +
                "COLOR    콘솔의 기본색과 배경색을 설정합니다.\n" +
                "COMP     두 개 또는 여러 개의 파일을 비교합니다.\n" +
                "COMPACT  NTFS 분할 영역에 있는 파일의 압축을 표시하거나 변경합니다.\n" +
                "CONVERT  FAT 볼륨을 NTFS로 변환합니다. 현재 드라이브는\n" +
                "         변환할 수 없습니다.\n" +
                "COPY     하나 이상의 파일을 다른 위치로 복사합니다.\n" +
                "DATE     날짜를 보여주거나 설정합니다.\n" +
                "DEL      하나 이상의 파일을 지웁니다.\n" +
                "DIR      디렉터리에 있는 파일과 하위 디렉터리 목록을 보여줍니다.\n" +
                "DISKPART       디스크 파티션 속성을 표시하거나 구성합니다.\n" +
                "DOSKEY       명령줄을 편집하고, Windows 명령을 다시 호출하고,\n" +
                "               매크로를 만듭니다.\n" +
                "DRIVERQUERY    현재 장치 드라이버 상태와 속성을 표시합니다.\n" +
                "ECHO           메시지를 표시하거나 ECHO를 켜거나 끕니다.\n" +
                "ENDLOCAL       배치 파일에서 환경 변경의 지역화를 끝냅니다.\n" +
                "ERASE          하나 이상의 파일을 지웁니다.\n" +
                "EXIT           CMD.EXE 프로그램(명령 인터프리터)을 종료합니다.\n" +
                "FC             두 파일 또는 파일 집합을 비교하여 다른 점을\n" +
                "         표시합니다.\n" +
                "FIND           파일에서 텍스트 문자열을 검색합니다.\n" +
                "FINDSTR        파일에서 문자열을 검색합니다.\n" +
                "FOR            파일 집합의 각 파일에 대해 지정된 명령을 실행합니다.\n" +
                "FORMAT         Windows에서 사용할 디스크를 포맷합니다.\n" +
                "FSUTIL         파일 시스템 속성을 표시하거나 구성합니다.\n" +
                "FTYPE          파일 확장명 연결에 사용되는 파일 형식을 표시하거나\n" +
                "               수정합니다.\n" +
                "GOTO           Windows 명령 인터프리터가 일괄 프로그램에서\n" +
                "               이름표가 붙여진 줄로 이동합니다.\n" +
                "GPRESULT       컴퓨터 또는 사용자에 대한 그룹 정책 정보를 표시합니다.\n" +
                "GRAFTABL       Windows가 그래픽 모드에서 확장 문자 세트를 표시할\n" +
                "         수 있게 합니다.\n" +
                "HELP           Windows 명령에 대한 도움말 정보를 제공합니다.\n" +
                "ICACLS         파일과 디렉터리에 대한 ACL을 표시, 수정, 백업 또는\n" +
                "               복원합니다.\n" +
                "IF             일괄 프로그램에서 조건 처리를 수행합니다.\n" +
                "LABEL          디스크의 볼륨 이름을 만들거나, 바꾸거나, 지웁니다.\n" +
                "MD             디렉터리를 만듭니다.\n" +
                "MKDIR          디렉터리를 만듭니다.\n" +
                "MKLINK         바로 가기 링크와 하드 링크를 만듭니다.\n" +
                "MODE           시스템 장치를 구성합니다.\n" +
                "MORE           출력을 한번에 한 화면씩 표시합니다.\n" +
                "MOVE           하나 이상의 파일을 한 디렉터리에서 다른 디렉터리로\n" +
                "               이동합니다.\n" +
                "OPENFILES      파일 공유에서 원격 사용자에 의해 열린 파일을 표시합니다.\n" +
                "PATH           실행 파일의 찾기 경로를 표시하거나 설정합니다.\n" +
                "PAUSE          배치 파일의 처리를 일시 중단하고 메시지를 표시합니다.\n" +
                "POPD           PUSHD에 의해 저장된 현재 디렉터리의 이전 값을\n" +
                "               복원합니다.\n" +
                "PRINT          텍스트 파일을 인쇄합니다.\n" +
                "PROMPT         Windows 명령 프롬프트를 변경합니다.\n" +
                "PUSHD          현재 디렉터리를 저장한 다음 변경합니다.\n" +
                "RD             디렉터리를 제거합니다.\n" +
                "RECOVER        불량이거나 결함이 있는 디스크에서 읽을 수 있는 정보를 복구합니다.\n" +
                "REM            배치 파일 또는 CONFIG.SYS에 주석을 기록합니다.\n" +
                "REN            파일 이름을 바꿉니다.\n" +
                "RENAME         파일 이름을 바꿉니다.\n" +
                "REPLACE        파일을 바꿉니다.\n" +
                "RMDIR          디렉터리를 제거합니다.\n" +
                "ROBOCOPY       파일과 디렉터리 트리를 복사할 수 있는 고급 유틸리티입니다.\n" +
                "SET            Windows 환경 변수를 표시, 설정 또는 제거합니다.\n" +
                "SETLOCAL       배치 파일에서 환경 변경의 지역화를 시작합니다.\n" +
                "SC             서비스(백그라운드 프로세스)를 표시하거나 구성합니다.\n" +
                "SCHTASKS       컴퓨터에서 실행할 명령과 프로그램을 예약합니다.\n" +
                "SHIFT          배치 파일에서 바꿀 수 있는 매개 변수의 위치를 바꿉니다.\n" +
                "SHUTDOWN       컴퓨터의 로컬 또는 원격 종료를 허용합니다.\n" +
                "SORT           입력을 정렬합니다.\n" +
                "START          지정한 프로그램이나 명령을 실행할 별도의 창을 시작합니다.\n" +
                "SUBST          경로를 드라이브 문자에 연결합니다.\n" +
                "SYSTEMINFO     컴퓨터별 속성과 구성을 표시합니다.\n" +
                "TASKLIST       서비스를 포함하여 현재 실행 중인 모든 작업을 표시합니다.\n" +
                "TASKKILL       실행 중인 프로세스나 응용 프로그램을 중단합니다.\n" +
                "TIME           시스템 시간을 표시하거나 설정합니다.\n" +
                "TITLE          CMD.EXE 세션에 대한 창 제목을 설정합니다.\n" +
                "TREE           드라이브 또는 경로의 디렉터리 구조를 그래픽으로\n" +
                "               표시합니다.\n" +
                "TYPE           텍스트 파일의 내용을 표시합니다.\n" +
                "VER            Windows 버전을 표시합니다.\n" +
                "VERIFY         파일이 디스크에 올바로 기록되었는지 검증할지\n" +
                "         여부를 지정합니다.\n" +
                "VOL            디스크 볼륨 레이블과 일련 번호를 표시합니다.\n" +
                "XCOPY          파일과 디렉터리 트리를 복사합니다.\n" +
                "WMIC           대화형 명령 셸 내의 WMI 정보를 표시합니다.\n" +
                "\n" +
                "도구에 대한 자세한 내용은 온라인 도움말의 명령줄 참조를 참조하십시오.");
    }
}
