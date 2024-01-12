# 🕹️ SimTools Controller
> 모션 시뮬레이터를 조작하는 소프트웨어

## 🔦 원리
 + **SimTools GameManager**가 프로세스를 탐지(Running)하면, **UDP** 프로토콜로 통신이 가능
 + **UDP** 통신으로 데이터를 입력받으면 **Serial** 통신으로 하드웨어를 조작
 + 프로세스명, 아이피, 포트, 데이터 처리 방식 등은 **Plugin**에 정의

## ⚒️ 플러그인(Plugin) 빌드
 1) "**SimToolsController_GamePlugin/GamePlugin.sln**" 솔루션 파일 열기
 2) 솔루션 탐색기에서 **"My Project"** 폴더 클릭
 3) 왼쪽 "**애플리케이션**" 탭에서 "**어셈블리 이름**"과 "**루트 네임스페이스**"를 "**(게임명)_GamePlugin**"으로 변경
 4) 왼쪽 "**참조**" 탭의 경로들이 깨지지 않도록 주의
 5) **GamePlugin.vb**
    + 변수
       - `_PluginAuthorsName` : GameManager에 표시되는 플러그인 **저자**
       - `_GameName` : GameManager에서 선택할 때 표시되는 **이름**
       - `_ProcessName` : 탐지하고자 하는 **프로세스 이름**, 작업 관리자에 표시되는 이름을 기재
       - `_Port` : 통신할 포트 번호. 기본값은 **4123**
    + 함수
        - `Process_PacketRecieved` : 패킷을 받아 처리하는 함수, 현재는 ','으로 구분하여 9개의 데이터를 수신
        - `PatchGame` : 패치할 게임 경로를 지정하는 과정, 필요없다면 무조건 True를 반환
        - `ValidatePatchPath` : 패치 경로 유효성을 확인하는 과정, 필요없다면 무조건 True를 반환
 6) "**빌드**" 탭에서 "**솔루션 정리**", "**GamePlugin 정리**" 눌러서 기존 플러그인 제거
 7) "**솔루션 빌드**"를 클릭하여, "**bin/Debug**" 경로에 **"(게임명)_GamePlugin.dll"** 생성
 8) "**PluginValidator.exe**" 실행해서 "**(게임명)_GamePlugin.dll is a Valid Plugin!**" 확인
 9) **dll** 파일을 **SimTools Game Plugin Updater**에 드래그 앤 드랍하여 **GameManager**에 추가

## 🚩 테스트 프로그램(SimToolsController_Project)
+ "**Assets/Scripts/ControlSimTools.cs**"
    - 아이피와 포트를 가지고 **UDPClient**를 생성하는 스크립트
    - **플러그인**에 기재한 방식으로 9개의 데이터를 처리하여 패킷을 전송
    - 외부에서 아이피와 포트를 변경할 수 있도록 **XML** 로드 추가
+ "**Assets/Scripts/UIController.cs**"
    - 위 9개의 데이터를 조작하는 스크립트
    - **Slider** 컴포넌트로 조작
    
<div align="center">
    <img src="https://github.com/Giseung30/SimTools_Controller/assets/60832219/4300ac8e-2592-41e3-8808-a1bcea7500cf"/>
</div>