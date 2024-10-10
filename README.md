## 해골 멀리가기 게임 SIMPLE IS BEST

### 개요
싸피에서 팀원들과 이야기 하면서 게임을 만들고 싶었습니다.
하지만 프로젝트가 바빠서 실제로 구현하지는 못했고 제 머릿속에 저장만 되어 있었습니다.
싸피가 끝난 후 "아 맞다 한 번 만들어볼까?" 라는 생각이 스쳤고 드디어 세상에 나오게 되었습니다.
이름하여 <bold>해골멀리가기</bold> 게임

<br> 해골의 머리의 중심을 잡아서 최대한 멀리가는 게임입니다.


https://github.com/user-attachments/assets/599c259e-bb85-4664-93f5-57bc1236803f


### 기능
- 해골 머리 좌우로 움직이기
- GPGS를 이용한 랭킹 기록 및 조회
- 애드몹 광고


### 클래스 설명

#### GameManager.cs 
- 게임에 관련된 스코어 상태 저장

#### NotificationManager.cs
- DG.Tweening 을 이용한 토클 창 띄우기
- 메세지 데이터관련 로직

#### AdManager.cs
- 구글 에드센스 관련 설정


#### AudioEffectManager.cs
- 오디오 관련 메니저
- 음악 재생 및 플레이 버튼 바꾸는 로직

#### BackgroundScroller.cs
- 백그라운드 이미지 반복재생

#### ButtonController.cs
- 게임 종료 버튼

#### ButtonController.cs
- 게임 재시작 중지 로직
- 멈출 때 패널을 이용해 배경 어둡게 하기

#### Head.cs
- 머리충돌 감지

#### LeaderBoardManager.cs
- GPGS 리더보드 불러오기 및 기록하기

#### StorkController.cs
- 머리 좌우 랜덤으로 움직이게 하기
- storkController -> SkeletonController , 해골의 모든 부분을 컨트롤하는 클래스

