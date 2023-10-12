# QuickWaifu2x
waifu2x GUI, 그런데 이제 클립보드를 곁드린

#### 릴리즈 버전은 [여기](https://github.com/Soju06/QuickWaifu2x/releases/download/v1.1.2/quickWaifu2x-1.1.2_net5.0-windows10.0.20348.0.zip)에서 다운받을 수 있습니다.

## Getting started on Windows

1. 시작하기 전에

   [waifu2x-ncnn-vulkan](https://github.com/nihui/waifu2x-ncnn-vulkan)에 의존되어있습니다.

   파라미터 형식이 변하지 않았다면 최신버전으로도 작동 합니다.
   

   다음의 프로그램은 윈도우 10 이상에서만 작동할껍니다.

2. 파일을 다운받고 압축을 푸세요!

3. 다음과 같은 이름의 프로그램을 실행시키세요!

   ![대충 QuickWaifu2x.exe 파일 사진](https://user-images.githubusercontent.com/34199905/134859570-07b79168-784e-4584-b6aa-f0ce9a80fc51.png)

   **실행을 해도 아무것도 안나오는건 정상입니다!**

   ![대충 트레이 아이콘](https://user-images.githubusercontent.com/34199905/134860146-450dadb3-09fc-4c01-9c7c-c0cda50d2c65.png)

   다음과 같은 아이콘이 시스템 트레이에 생겼을겁니다!

   ![대충 트레이 아이콘 우클릭 하면 나오는 창](https://user-images.githubusercontent.com/34199905/134860803-85f0a9bc-0cd4-4fcf-bc07-f32c4bc142f3.png)

   우클릭을 해보면, 다음과 같은 창이 나옵니다.

4. 업스케일링 할 이미지를 선택하세요!

   선택한 이미지는 클립보드에 복사 하거나, 파일의 경우 파일을 선택해서 복사를 누르세요!

   ![대충 엣지 이미지 우클릭 메뉴에서 이미지 복사 선택](https://user-images.githubusercontent.com/34199905/134861467-b0ea186d-d117-4cd2-a0c0-1d47da41d92d.png)

   또는

   ![대충 윈도우 익스플로러에서 파일 선택하고 복사](https://user-images.githubusercontent.com/34199905/134861652-755413a3-6f82-4f06-9477-3c70947a492e.png)

   파일은 Png, Jpg타입을 지원하며, 여러 이미지 선택이 가능합니다!

5. 트레이 아이콘에서 업스케일링 (O)를 누르거나, Win + Shift + E 키를 눌러 작업을 시작합니다.

   **업스케일링이 불가능한 개체가 선택되면 아무런 창도 뜨지 않습니다.**

6. 업스케일링할 파일을 선택합니다. **2개 이상 파일을 선택해을때만 나옵니다.**

   ![업스케일 할 파일 선택](https://user-images.githubusercontent.com/34199905/134862833-e1e37db1-0e41-4090-b9ac-8104cc90db05.png)

   

7. 업스케일링 옵션을 선택합니다.

   ![렌더링 옵션](https://user-images.githubusercontent.com/34199905/134862515-88fa53b6-63b8-4750-8e90-8c34e2b49dd1.png)
   설정을 건드리지 않아도 기본값으로 업스케일링 할 수 있습니다.

8. 업스케일링을 시작합니다.

   ![업스케일링 창](https://user-images.githubusercontent.com/34199905/134863526-ab198049-3827-45c6-9177-41e5ab54014f.png)

   단일 파일인 경우 자동으로 클립보드로 복사되며, 다중 파일인 경우에는 내보내기를 통해 저장해야 합니다.

   ![알림](https://user-images.githubusercontent.com/34199905/134863810-1fc77acb-e806-40b8-8493-7f260529d3bd.png)

   인코딩이 모두 완료되면 알림이 옵니다.

## Settings

![설정 창](https://user-images.githubusercontent.com/34199905/134864218-83f3ab24-6ab4-4f86-a978-1b6e6f909336.png)

Win + Shift + R 단축키로 열거나, 트레이 아이콘 메뉴에서 열수 있습니다. 

Waifu2x 파라미터 기본 값: Waifu2x 기본 렌더링 옵션을 설정할 수 있습니다.

여러개 파일 처리 묻지 않음: 거짓의 경우 업스케일링할 파일을 무조건 물어봅니다.

시작 프로그램: 윈도우 시작프로그램에 등록합니다.
