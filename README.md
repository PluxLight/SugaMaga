# SugaMaga - 3인칭 멀티 플레이 배틀로얄 게임

![logo](./images/logo.png)

## 목차
  - [프로젝트 진행 기간](#프로젝트-진행-기간)
  - [SugaMaga 배경](#sugamaga-배경)
  - [SugaMaga 개요](#sugamaga-개요)
  - [메뉴 구조](#메뉴-구조)
  - [와이어 프레임 (게임)](#와이어-프레임-게임)
  - [와이어 프레임 (웹)](#와이어-프레임-웹)
  - [주요 기술 (Service Architecture)](#주요-기술-service-architecture)
  - [협업 툴 및 환경](#협업-툴-및-환경)
  - [팀원](#팀원)
  - [프로젝트 산출물](#프로젝트-산출물)
  - [서비스 화면 (웹)](#서비스-화면-웹)
  - [서비스 화면 (게임)](#서비스-화면-게임)  
<br>

## 프로젝트 진행 기간
---
2023.02.20(월) ~ 2023.04.07(금) (총 7주)  
<br>

## SugaMaga 배경
---
SugaMaga(이하 슈가마가)는 기존 배틀로얄 장르의 유사점을 극복하고자 기획하게 되었습니다. 기존 배틀로얄 장르 게임의 대부분은  
- 실사 그래픽
- 원거리 전투
- 획일화된 무기군  

등의 공통점을 가지고 있었습니다.  
  
저희 슈가마가는
- 캐쥬얼 그래픽
- 근거리 전투
- 다양한 무기군  

을 차별점으로 가지고있습니다.  
<br>

## SugaMaga 개요
---
슈가마가는 Sugar(설탕) + Maga(마녀의 라틴어)를 조합하여 만들어진 이름입니다.  
슈가는 맵 곳곳에 배치된 장식들과 무기로 사용되는 과자들을 뜻하며  
마가는 해당 게임의 배틀로얄이 진행되는 원인인 마녀를 뜻합니다.  
<br>


## 메뉴 구조
---
![menu_structure](./images/menu_structure.png)

## 와이어 프레임 (게임)
---
![wireframe_game](./images/wireframe_game.png)

## 와이어 프레임 (웹)
---
![wireframe_web](./images/wireframe_web.png)

## 주요 기술 (Service Architecture)
---
![system_architecture](./images/system_architecture.png)

## 협업 툴 및 환경
---
### Git

- 코드 버전 관리
- Convention을 정하여 규칙에 맞게 Commit 메세지 작성

### Jira

- 매주 월요일 이슈 등록하여 Sprint 진행
- Story Point 설정하여 1주 40point 기준으로 진행
- In-Progress -> Done 순으로 진행

### Notion

- 이슈와 진행상황 공유 및 회의록 작성
- Convention 정리
- 공유해야 하는 문서 정리

### 그 외
- MatterMost
- Webex

## 팀원
---
### 전은수
- 팀장
- 플레이어 (조작, 물리)

### 김학철
- 게임서버
- CI/CD

### 박해준
- 맵
- 플레이어 코스튬

### 이석기
- UI
- 인벤토리

### 전상하
- DB
- Web (Front, Back)

### 정진욱
- 스킬 구현
- 몬스터

## 프로젝트 산출물
---
- 메뉴 구조
- Git 전략
- Service Architecture
- Convention
- API 명세서
- ERD
- 회의록
- 1차 발표자료
- 2차 발표자료

## 서비스 화면 (웹)
---
### 홈페이지
웹페이지의 홈 화면입니다.  
게임을 이용하기 위한 회원가입, 로그인, 정보수정이 가능합니다.  
그리고 게임 소개 내용을 보거나 게임 런처를 다운로드 할 수 있습니다.<br><br>
![web_home](./images/web_home.png)
<br><br>

### 회원가입
회원가입을 진행합니다.  
회원가입 요청이 성공한다면 이메일을 확인 요청 안내페이지가 발생합니다.
![web_signup](./images/web_signup.png)
![web_signupsuccess](./images/web_signupsuccess.png)
<br><br>

### 로그인
로그인 화면입니다.
![web_](./images/web_login.png)
<br><br>

### 나의정보
로그인한 유저 정보를 확인할 수 있습니다.  
닉네임 및 비밀번호 수정이 가능합니다.
![web_myinfo](./images/web_myinfo.png)
<br><br>

### 게임소개
  1. 설명  
    게임의 이미지와 배경을 확인할 수 있습니다.
    ![web_](./images/web_intro.png)
    <br><br>
  2. 조작법  
     게임의 조작방법을 확인할 수 있습니다.
    ![web_](./images/web_manual.png)
    <br><br>
  3. 아트워크  
     게임에서 사용된 그림 및 사진을 확인할 수 있습니다.
    ![web_](./images/web_artwork.png)
<br><br>

### 다운로드
게임실행에 필요한 게임런처 파일을 다운로드 할 수 있습니다.
![web_](./images/web_download.png)  
<br>

## 서비스 화면 (게임)
---

### 게임 다운로드 및 실행
1. 다운로드 페이지에서 게임런처를 다운받습니다.
![game_down](./images/game_01_download.png)  

2. 다운로드 받은 런처를 실행합니다. 런처가 실행되며 자동으로 본 게임 파일을 다운로드 합니다.
![game_down](./images/game_02_launcher.png)  
![game_down](./images/game_03_launcher.png)  
![game_down](./images/game_04_launcher.png)  
<br>

### 게임 실행
1. 다운로드가 완료되면 버튼이 실행으로 변경됩니다. 버튼을 눌러 게임을 실행합니다.
![game_run](./images/game_05_launcher.png)  


2. 게임을 실행하면 가장 먼저 나오는 화면입니다. 로그인을 진행합니다. 회원가입은 웹 사이트에서 지원하고 있습니다.
![game_run](./images/game_06_login.png)  

3. 로그인 후 진입한 메인화면입니다. 바로 게임을 실행하거나 코스튬을 변경할 수 있습니다.
![game_run](./images/game_07_main.png)  
<br>

### 코스튬 변경
1. 코스튬을 변경합니다. 변경할 수 있는 코스튬의 종류는 총 9가지 입니다. 몸통, 머리, 악세사리, 등, 헤어스타일, 모자, 눈, 입, 눈썹을 변경할 수 있습니다.
![game_costume](./images/game_08_costume.png)  
![game_costume](./images/game_09_costume.png)  
2. 변경을 마치고 메인화면으로 돌아왔습니다.
![game_costume](./images/game_10_main.png)  
<br>

### 로비 참가
1. 게임에 참가하기 위해 로비에 참가합니다. 로비에서는 채팅으로 같은 로비에 있는 플레이어와 대화할 수도 있습니다.
![game_lobby](./images/game_11_lobby.png)  

### 게임 시작
1. 게임을 시작한 직후의 모습입니다. 게임화면에서는 다음의 내용들을 확인할 수 있습니다.
   1. 왼쪽상단에서 처치 수와 생존자 수를 확인할 수 있습니다.  
   2. 가운데상단에서 필드 요소 중 하나인 자기장이 접근하는 시간을 알 수 있습니다.
   3. 왼쪽 하단의 미니맵을 통해 플레이어의 위치를 확인할 수 있습니다.
   4. 가운데 하단에서 HP바를 통해 플레이어의 현재 체력을 알 수 있습니다.
   5. HP바 양 옆의 인벤토리 슬롯이 있습니다. 습득한 장비 아이템과, 소비 아이템을 확인할 수 있습니다
   6. 우측 하단의 스킬 아이콘을 통해 스킬을 사용한 후 남은 재사용 대기시간을 확인할 수 있습니다.

![game_run](./images/game_12_game_start.png)  
2. 장비를 습득하고 인벤토리 슬롯에 대응하는 숫자 키보드를 누를면 아이템을 장비할 수 있습니다.
![game_run](./images/game_13_get_equip.png)  
3. 아이템 근처에 다가가면 화면 중앙에 상호작용 방법이 표시됩니다. 안내에 따라 키를 누르면 아이템을 습득할 수 있습니다.
![game_run](./images/game_14_get_potion.png)  
![game_run](./images/game_15_get_potion.png)  
4. 장비 아이템에는 각자 고유한 스킬이 있습니다. 그러나 사용방법은 마우스 우클릭으로 모두 동일합니다.
![game_run](./images/game_16_use_skill.png)  
5. 게임이 시작되고 일정 시간이 지나면 플레이어의 전투를 강제하는 요소인 자기장이 움직입니다. 자기장에 닿으면 HP가 감소되며 화면이 푸르게 변합니다.
![game_run](./images/game_17_curse.png)  
![game_run](./images/game_18_curse.png)  
![game_run](./images/game_19_curse.png)  