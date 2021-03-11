from tkinter import *   #  GUI 필수
from tkinter import messagebox
from tkinter.filedialog import  * # 파일 대화상자
from tkinter.simpledialog import *

import cv2
import numpy as np    # numpy를 np로 축약해서 쓰겠다. ex) numpy.abc --> np.abc

## 함수 선언부

def malloc(row, col, init=0):  # value 값을 안적고 보내면 0 있으면 value 값 ex. mall2(3,4)
    # 배열 생성
    retAry = [
        [
            [init for _ in range(col)] for _ in range(row)
        ] for _ in range(RGB)
    ]
    return retAry

## 마우스 이벤트
def myLeftClick(event) :
    x = event.x
    y = event.y
    label.configure(text = str(x) + ',' + str(y))

def clickme1() :    #마우스 이벤트 시작
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB

    window.bind("<Button-1>", myLeftClick)   #마우스 왼쪽 버튼 클릭 시(1:왼쪽, 2:가운데, 3:오른쪽)
    # 윈도우 기준 왼쪽 위부터 0,0

def clickme2() :    #마우스 이벤트 종료
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB

    window.unbind("<Button-1>")             #마우스 왼쪽 버튼 클릭 시(1:왼쪽, 2:가운데, 3:오른쪽)

#키보드 이벤트
def myKeyEvent(event) :
    code = event.keycode
    print(chr(code))

def controlPress(event) :
    if chr(event.keycode) == 's' or chr(event.keycode) == 'S' :
        saveImg()
    elif chr(event.keycode) == 'o' or chr(event.keycode) == 'O' :
        openImg()



def openImg() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB

    fileName = askopenfilename(parent=window,
                               filetypes=(('Color File', '*.png;*.jpg;*,bmp;*.tif'), ('All File', '*.*')))
    # File --> CV 객체
    inCvImg = cv2.imread(fileName)
    # 입력 영상 크기 알아내기 (중요!)
    inH, inW = inCvImg.shape[:2]
    # 메모리 확보
    inImg = malloc(inH, inW)
    # OpenCV --> 메모리
    for i in range(inH):
        for k in range(inW):
            inImg[BB][i][k] = inCvImg.item(i, k, RR)
            inImg[GG][i][k] = inCvImg.item(i, k, GG)
            inImg[RR][i][k] = inCvImg.item(i, k, BB)

    # print(inImage[RR][100][100], inImage[GG][100][100], inImage[BB][100][100])

    equal()


def saveImg() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB

    if fileName == None :
        return

    saveCvImg = np.zeros((outH, outW, 3), np.uint8)     # unsigned int 8bit 형태로 3차원 배열을 만들겠다.
    for i in range(outH) :
        for k in range(outW) :
            tup = tuple(( [outImg[BB][i][k]], [outImg[GG][i][k]], [outImg[RR][i][k]] ))

            saveCvImg[i, k] = tup


    saveFp = asksaveasfile(parent = window, mode = 'wb', defaultextension = '.png',
                           filetype = (("Image Type", "*.png; *.jpg; *.bmp; *.tif"),("All File", "*.*")))

    if(saveFp == "" or saveFp == None) :
        return
    cv2.imwrite(saveFp.name, saveCvImg)
    print('save')

    # 2021-03 MES 스마트팩토리 구축전문가과정
    # C# 부분 추가 강의 Python
    # 심준보 作



def displayImg() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB

    ## 기존에 그림을 붙인적이 있으면 게시판(canvas) 뜯어내기
    if (canvas != None) :
        canvas.destroy()

    window.geometry(str(outW) + "x" + str(outH + 70))        # 벽 크기 조절
    canvas = Canvas(window, height=outH + 70, width=outW)    # 게시판 크기 조절
    paper = PhotoImage(height=outH + 70, width=outW)
    canvas.create_image(
        (outW/2, outH/2),   # 중심점 찾기
        image=paper,
        state='normal'
    )

    #Python은 느리기 떄문에 하나씩 찍기는 힘들어서 메모리를 사용하여 찍기
    rgbString = ""  # 전체 펜을 저장함
    for i in range (outH) :
        tmpString = ""  # 각 한 줄의 펜
        for k in range(outW) :
            rr = outImg[RR][i][k]
            gg = outImg[GG][i][k]
            bb = outImg[BB][i][k]
            tmpString += "#%02x%02x%02x " % (rr, gg, bb)  # 제일 뒤 공백 한칸
        rgbString += '{' + tmpString + '} ' # 제일 뒤 공백 한칸

    paper.put(rgbString)
    canvas.pack()
    status.configure(text = str(outW) + 'x' + str(outH) + ' ' + fileName)


def equal() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    outH = inH; outW = inW
    outImg = malloc(outH, outW)

    for rgb in range (RGB) :
        for i in range(outH):
            for k in range(outW):
                outImg[rgb][i][k] = inImg[rgb][i][k]

    displayImg()

# OpenCv용 영상처리
def CvtoRGB() :     #OpenCvOut --> outImg
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB

    # 출력 영상 크기
    outH, outW = outCvImg.shape[:2]
    # 출력 메모리 할당
    outImg = malloc(outH,outW)
    #Cv2 결과  --->  출력 메모리
    for i in range(outH) :
        for k in range(outW) :
            # OpenCV 사용시 사진이 그레이상태면 OpenCV 스스로 차원이 줄임
            # Cv2가 흑백 or 칼라
            if(outCvImg.ndim == 2) :
                outImg[BB][i][k] = outCvImg.item(i, k)
                outImg[GG][i][k] = outCvImg.item(i, k)
                outImg[RR][i][k] = outCvImg.item(i, k)
            else :
                outImg[BB][i][k] = outCvImg.item(i, k, RR)
                outImg[GG][i][k] = outCvImg.item(i, k, GG)
                outImg[RR][i][k] = outCvImg.item(i, k, BB)

def Cv_Equal() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 진짜 OpenCV용 영상처리
    outCvImg = inCvImg[:]   # ':' : 포인터
    CvtoRGB()

    # 실행
    displayImg()

## OpenCV 화소점처리
def Cv_bright() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    value = askinteger("밝기", "값 입력", minvalue=-255, maxvalue=255)
    ary = np.full(inCvImg.shape, (value, value, value), dtype=np.uint8)

    ## 진짜 OpenCV용 영상처리
    if (value > 0) :
        outCvImg = cv2.add(inCvImg, ary)
    elif (value < 0) :
        outCvImg = cv2.subtract(inCvImg, ary)

    CvtoRGB()
    # 실행
    displayImg()

def Cv_Gray() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 진짜 OpenCV용 영상처리
    outCvImg = cv2.cvtColor(inCvImg, cv2.COLOR_BGR2GRAY)
    CvtoRGB()

    # 실행
    displayImg()

def Cv_reverse() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 진짜 OpenCV용 영상처리
    outCvImg = cv2.bitwise_not(inCvImg)

    CvtoRGB()
    # 실행
    displayImg()

# OpenCV 화소 영역 처리
def Cv_Emboss() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 진짜 OpenCV용 영상처리
    # 마스트 지정
    mask = np.zeros((3,3), np.float32)
    mask[0][0] = -1.0
    mask[2][2] =  1.0
    # 출력에 씌우기
    outCvImg = cv2.filter2D(inCvImg, -1, mask)
    outCvImg += 127
    CvtoRGB()

    # 실행
    displayImg()


# OpenCV 기하학 처리
def Cv_zoom() :     # 확대 1.5배같은 소수점 들어가면 확대 안됨
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    height, width, channel = inCvImg.shape
    value = askfloat("배율", "값 입력", minvalue = 0, maxvalue = 3)

    ## 진짜 OpenCV용 영상처리
    if (value > 1) :
        outCvImg = cv2.pyrUp(inCvImg, None, (int(width * value), int(height * value)), borderType=cv2.BORDER_DEFAULT)
    elif (value < 1) :
        outCvImg = cv2.pyrDown(inCvImg, None, (int(width * value), int(height * value)), borderType=cv2.BORDER_DEFAULT)



    CvtoRGB()
    # 실행
    displayImg()

def Cv_Cartoon() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 진짜 OpenCV용 영상처리
    outCvImg = cv2.cvtColor(inCvImg, cv2.COLOR_BGR2GRAY)
    outCvImg = cv2.medianBlur(outCvImg, 7)  #수치는 변경 가능
    edges = cv2.Laplacian(outCvImg, cv2.CV_8U, ksize=5)
    ret, mask = cv2.threshold(edges, 100, 255, cv2.THRESH_BINARY_INV)
    outCvImg = cv2.cvtColor(mask, cv2.COLOR_GRAY2RGB)

    CvtoRGB()
    # 실행
    displayImg()

def Cv_FaceDetect() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None):
        return

    ## 진짜 OpenCV용 영상처리
    face_cascade = cv2.CascadeClassifier("haarcascade_frontalface_alt.xml")
    gray = cv2.cvtColor(inCvImg, cv2.COLOR_RGBA2GRAY)
    
    #얼굴 찾기
    face_rects = face_cascade.detectMultiScale(gray, 1.1, 5)    # 못찾으면 여기 값 조절
    
    outCvImg = inCvImg[:]  # ':' : 포인터
    for (x,y,w,h) in face_rects :
        cv2.rectangle(outCvImg, (x,y), (x + w, y + h), (0, 255, 0), 3)  ##찾은 영역 빨간 사각형 두께3으로 표시
    
    CvtoRGB()

    # 실행
    displayImg()


def Cv_CatFaceDetect():
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None):
        return

    ## 진짜 OpenCV용 영상처리
    face_cascade = cv2.CascadeClassifier("haarcascade_frontalcatface.xml")
    gray = cv2.cvtColor(inCvImg, cv2.COLOR_RGBA2GRAY)

    # 얼굴 찾기
    face_rects = face_cascade.detectMultiScale(gray, 1.2, 5)  # 못찾으면 여기 값 조절

    outCvImg = inCvImg[:]  # ':' : 포인터
    for (x, y, w, h) in face_rects:
        cv2.rectangle(outCvImg, (x, y), (x + w, y + h), (0, 255, 0), 3)  ##찾은 영역 빨간 사각형 두께3으로 표시

    CvtoRGB()

    # 실행
    displayImg()



#화소점처리
def bright() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    outH = inH;    outW = inW
    outImg = malloc(outH, outW)

    value = askinteger("밝게/어둡게", "값 입력", minvalue=-255, maxvalue=255)

    for rgb in range(RGB):
        for i in range(outH):
            for k in range(outW):
                if ((inImg[rgb][i][k] + value) > 255) :
                    outImg[rgb][i][k] = 255
                elif ((inImg[rgb][i][k] + value) < 0) :
                    outImg[rgb][i][k] = 0
                else :
                    outImg[rgb][i][k] = inImg[rgb][i][k] + value

    displayImg()

def gamma() :   # 작동 이상
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    outH = inH; outW = inW
    outImg = malloc(outH, outW, 0.0)

    value = askfloat("밝게/어둡게", "값 입력", minvalue=0.0, maxvalue=5.0)

    for rgb in range (RGB) :
        for i in range(outH):
            for k in range(outW):
                outImg[rgb][i][k] = int(255.0 * ((inImg[rgb][i][k] / 255.0) ** value))

    displayImg()

def reverse() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, cvInImg, cvOutImg, RGB, RR, GG, BB
    global boxLine
    global sx, sy, ex, ey

    sx, sy, ex, ey = [-1] * 4
    boxLine = None  # 처음인지 체크하기.

    if (inImg == None):
        return

    def reverse_image_click(event):
        global sx, sy, ex, ey
        sx = event.x
        sy = event.y

    def reverse_image_release(event):
        global sx, sy, ex, ey
        ex = event.x
        ey = event.y
        if sx > ex:
            sx, ex = ex, sx
        if sy > ey:
            sy, ey = ey, sy
        __reverse_image()
        canvas.unbind("<Button-1>")
        canvas.unbind("<B1-Motion>")
        canvas.unbind("<Button-3>")
        canvas.unbind("<ButtonRelease-1>")

    def reverse_image_Rclick(event):
        global sx, sy, ex, ey
        global inH, inW
        sx = 0
        sy = 0
        ex = inW - 1
        ey = inH - 1
        __reverse_image()
        canvas.unbind("<Button-1>")
        canvas.unbind("<B1-Motion>")
        canvas.unbind("<Button-3>")
        canvas.unbind("<ButtonRelease-1>")

    def mouseMove(event):
        global sx, sy, ex, ey, boxLine
        if sx < 0:
            return
        ex = event.x
        ey = event.y
        if not boxLine:  # 기존 박스선 지우기
            pass
        else:
            canvas.delete(boxLine)

        boxLine = canvas.create_rectangle(sx, sy, ex, ey, fill=None)

    def __reverse_image():
        global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
        global fileName, cvInImg, cvOutImg, RGB, RR, GG, BB
        global sx, sy, ex, ey
        if (inImg == None):
            return
        ## 중요! 출력이미지의 높이, 폭을 결정  --> 알고리즘에 영향
        outH = inH;
        outW = inW;
        outImg = malloc(outH, outW)
        ## *** 진짜 영상처리 알고리즘을 구현 ***
        for rgb in range(RGB):
            for i in range(outH):
                for k in range(outW):
                    if (sx <= k <= ex) and (sy <= i <= ey):
                        outImg[rgb][i][k] = 255 - inImg[rgb][i][k]
                    else:
                        outImg[rgb][i][k] = inImg[rgb][i][k]
        ##/////////////////////////////////////////////
        displayImg()

    canvas.bind("<Button-1>", reverse_image_click)
    canvas.bind("<B1-Motion>", mouseMove)
    canvas.bind("<Button-3>", reverse_image_Rclick)
    canvas.bind("<ButtonRelease-1>", reverse_image_release)


def gray():
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None):
        return

    outH = inH;
    outW = inW
    outImg = malloc(outH, outW)

    for i in range(outH):
        for k in range(outW):
            hap = inImg[RR][i][k] + inImg[GG][i][k] + inImg[BB][i][k]
            outImg[RR][i][k] = outImg[GG][i][k] = outImg[BB][i][k] = hap // 3

    displayImg()


def focus():
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None):
        return

    focusMin = askinteger("최솟값", "값 입력", minvalue = 0, maxvalue = 254)
    focusMax = askinteger("최댓값", "값 입력", minvalue = focusMin, maxvalue = 255)

    outH = inH;    outW = inW
    outImg = malloc(outH, outW)

    for rgb in range(RGB) :
        for i in range(outH) :
            for k in range(outW) :
                if (focusMin < inImg[rgb][i][k] < focusMax) :
                    outImg[rgb][i][k] = 255;
                else :
                    outImg[rgb][i][k] = inImg[rgb][i][k];

    displayImg()

def posterising() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    outH = inH; outW = inW
    outImg = malloc(outH, outW)

    # 구간값 : 계단 높이
    cutLevel = askinteger("구간값", "값 입력", minvalue=2, maxvalue=254)  # 계단 높이
    cut = int(255 / cutLevel)                                           # 계단 수

    ## ** *진짜 영상처리 알고리즘을 구현 ** *
    # 칼라 -> 흑백 -> 칼라
    for i in range(inH) :
        for k in range (inW) :
            hap = inImg[RR][i][k] + inImg[GG][i][k] + inImg[BB][i][k]
            avg = int(hap / RGB) # 흑백 값

            for m in range (cut) :
        # 흑백값에 따른 포스터라이징
                if (cutLevel * m <= avg < cutLevel * (m + 1)) :
                    if (avg < cutLevel) :
                        avg = 0
                        break
                    else :
                        avg = int(cutLevel * m)
                        break

            # RGB(255)기준 R,G,B의 각각 비율 구하기
            if hap == 0 :
                RRate = GRate = BRate = 0
            else :
                RRate = inImg[RR][i][k] / hap # 칼라에서 R비율
                GRate = inImg[GG][i][k] / hap # 칼라에서 G비율
                BRate = inImg[BB][i][k] / hap # 칼라에서 B비율

            # 포스터 라이징 된 값에 RGB 비율만큼 R,G,B 각각 대입
            outImg[RR][i][k] = int(avg * RGB * RRate)
            outImg[GG][i][k] = int(avg * RGB * GRate)
            outImg[BB][i][k] = int(avg * RGB * BRate)

    displayImg()

# 화소영역처리
def sharp() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 중요! 출력이미지의 높이, 폭을 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)

    # 화소 영역처리
    # !중요 마스크 결정
    mSize = 3
    mask = ((-1.0, 0.0, 0.0),
            ( 0.0, 0.0, 0.0),
            ( 0.0, 0.0, 1.0))

    # 임시 입출력 메모리 할당
    tmpInput = malloc(inH+2, inW+2)
    tmpOutput = malloc(outH, outW)

    # 임시 입력 중간값인 평균값(127)으로 초기화
    for rgb in range(RGB) :
        for i in range(inH + 2) :
            for k in range (inW + 2) :
                tmpInput[rgb][i][k] = 127

    # 입력 -> 임시 입력
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpInput[rgb][i + 1][k + 1] = inImg[rgb][i][k];

    # ***진짜 영상처리 알고리즘을구현 ***
    S = 0;

    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :

                for m in range(mSize) :
                    for n in range(mSize) :
                        S += tmpInput[rgb][i + m][k + n] * mask[m][n]
                tmpOutput[rgb][i][k] = S
                S = 0 ## S초기화

    # 후처리: mask의 합이 0이면 127 더하기
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpOutput[rgb][i][k] += 127;

    # 임시 출력 -> 원래 출력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                d = tmpOutput[rgb][i][k]
                if (d > 255) :
                    d = 255;
                elif (d < 0) :
                    d = 0;

                outImg[rgb][i][k] = int(d)

    displayImg()

def hFillter() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 중요! 출력이미지의 높이, 폭을 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)

    # 화소 영역처리
    # !중요 마스크 결정
    mSize = 3
    mask = ((-1.0/9, -1.0/9, -1.0/9),
            (-1.0/9,  8.0/9, -1.0/9),
            (-1.0/9, -1.0/9, -1.0/9))

    # 임시 입출력 메모리 할당
    tmpInput = malloc(inH+2, inW+2)
    tmpOutput = malloc(outH, outW)

    # 임시 입력 중간값인 평균값(127)으로 초기화
    for rgb in range(RGB) :
        for i in range(inH + 2) :
            for k in range (inW + 2) :
                tmpInput[rgb][i][k] = 127

    # 입력 -> 임시 입력
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpInput[rgb][i + 1][k + 1] = inImg[rgb][i][k];

    # ***진짜 영상처리 알고리즘을구현 ***
    S = 0;

    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :

                for m in range(mSize) :
                    for n in range(mSize) :
                        S += tmpInput[rgb][i + m][k + n] * mask[m][n]
                tmpOutput[rgb][i][k] = S
                S = 0 ## S초기화

    # 후처리: mask의 합이 0이면 127 더하기
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpOutput[rgb][i][k] += 127;

    # 임시 출력 -> 원래 출력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                d = tmpOutput[rgb][i][k]
                if (d > 255) :
                    d = 255;
                elif (d < 0) :
                    d = 0;

                outImg[rgb][i][k] = int(d)

    displayImg();


def gaussian() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 중요! 출력이미지의 높이, 폭을 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)

    # 화소 영역처리
    # !중요 마스크 결정
    mSize = 3
    mask = ((1.0/16, 1.0/8, 1.0/16),
            (1.0/ 8, 1.0/4, 1.0/ 8),
            (1.0/16, 1.0/8, 1.0/16))

    # 임시 입출력 메모리 할당
    tmpInput = malloc(inH+2, inW+2)
    tmpOutput = malloc(outH, outW)

    # 임시 입력 중간값인 평균값(127)으로 초기화
    for rgb in range(RGB) :
        for i in range(inH + 2) :
            for k in range (inW + 2) :
                tmpInput[rgb][i][k] = 127

    # 입력 -> 임시 입력
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpInput[rgb][i + 1][k + 1] = inImg[rgb][i][k];

    # ***진짜 영상처리 알고리즘을구현 ***
    S = 0;

    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :

                for m in range(mSize) :
                    for n in range(mSize) :
                        S += tmpInput[rgb][i + m][k + n] * mask[m][n]
                tmpOutput[rgb][i][k] = S
                S = 0 ## S초기화

    # 임시 출력 -> 원래 출력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                d = tmpOutput[rgb][i][k]
                if (d > 255) :
                    d = 255;
                elif (d < 0) :
                    d = 0;

                outImg[rgb][i][k] = int(d)

    displayImg()

def vecticalEdege() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 중요! 출력이미지의 높이, 폭을 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)

    # 화소 영역처리
    # !중요 마스크 결정
    mSize = 3
    mask = (( 0, 0, 0),
            (-1, 1, 0),
            ( 0, 0, 0))

    # 임시 입출력 메모리 할당
    tmpInput = malloc(inH+2, inW+2)
    tmpOutput = malloc(outH, outW)

    # 임시 입력 중간값인 평균값(127)으로 초기화
    for rgb in range(RGB) :
        for i in range(inH + 2) :
            for k in range (inW + 2) :
                tmpInput[rgb][i][k] = 127

    # 입력 -> 임시 입력
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpInput[rgb][i + 1][k + 1] = inImg[rgb][i][k];

    # ***진짜 영상처리 알고리즘을구현 ***
    S = 0;

    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :

                for m in range(mSize) :
                    for n in range(mSize) :
                        S += tmpInput[rgb][i + m][k + n] * mask[m][n]
                tmpOutput[rgb][i][k] = S
                S = 0 ## S초기화

    # 후처리: mask의 합이 0이면 127 더하기
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpOutput[rgb][i][k] += 127;

    # 임시 출력 -> 원래 출력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                d = tmpOutput[rgb][i][k]
                if (d > 255) :
                    d = 255;
                elif (d < 0) :
                    d = 0;

                outImg[rgb][i][k] = int(d)

    displayImg()

def horizonEdge() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 중요! 출력이미지의 높이, 폭을 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)

    # 화소 영역처리
    # !중요 마스크 결정
    mSize = 3
    mask = (( 0, -1, 0),
            ( 0,  1, 0),
            ( 0,  0, 0))

    # 임시 입출력 메모리 할당
    tmpInput = malloc(inH+2, inW+2)
    tmpOutput = malloc(outH, outW)

    # 임시 입력 중간값인 평균값(127)으로 초기화
    for rgb in range(RGB) :
        for i in range(inH + 2) :
            for k in range (inW + 2) :
                tmpInput[rgb][i][k] = 127

    # 입력 -> 임시 입력
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpInput[rgb][i + 1][k + 1] = inImg[rgb][i][k];

    # ***진짜 영상처리 알고리즘을구현 ***
    S = 0;

    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :

                for m in range(mSize) :
                    for n in range(mSize) :
                        S += tmpInput[rgb][i + m][k + n] * mask[m][n]
                tmpOutput[rgb][i][k] = S
                S = 0 ## S초기화

    # 후처리: mask의 합이 0이면 127 더하기
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpOutput[rgb][i][k] += 127;

    # 임시 출력 -> 원래 출력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                d = tmpOutput[rgb][i][k]
                if (d > 255) :
                    d = 255;
                elif (d < 0) :
                    d = 0;

                outImg[rgb][i][k] = int(d)

    displayImg()

def blurr() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 중요! 출력이미지의 높이, 폭을 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)

    # 화소 영역처리
    # !중요 마스크 결정
    mSize = 3
    mask = ((1.0/9, 1.0/9, 1.0/9),
            (1.0/9, 1.0/9, 1.0/9),
            (1.0/9, 1.0/9, 1.0/9))

    # 임시 입출력 메모리 할당
    tmpInput = malloc(inH+2, inW+2)
    tmpOutput = malloc(outH, outW)

    # 임시 입력 중간값인 평균값(127)으로 초기화
    for rgb in range(RGB) :
        for i in range(inH + 2) :
            for k in range (inW + 2) :
                tmpInput[rgb][i][k] = 127

    # 입력 -> 임시 입력
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpInput[rgb][i + 1][k + 1] = inImg[rgb][i][k];

    # ***진짜 영상처리 알고리즘을구현 ***
    S = 0;

    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :

                for m in range(mSize) :
                    for n in range(mSize) :
                        S += tmpInput[rgb][i + m][k + n] * mask[m][n]
                tmpOutput[rgb][i][k] = S
                S = 0 ## S초기화

    # 임시 출력 -> 원래 출력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                d = tmpOutput[rgb][i][k]
                if (d > 255) :
                    d = 255;
                elif (d < 0) :
                    d = 0;

                outImg[rgb][i][k] = int(d)

    displayImg()

def emboss() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 중요! 출력이미지의 높이, 폭을 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)

    # 화소 영역처리
    # !중요 마스크 결정
    mSize = 3
    mask = (( -1, 0, 0),
            (  0, 0, 0),
            (  0, 0, 1))

    # 임시 입출력 메모리 할당
    tmpInput = malloc(inH+2, inW+2)
    tmpOutput = malloc(outH, outW)

    # 임시 입력 중간값인 평균값(127)으로 초기화
    for rgb in range(RGB) :
        for i in range(inH + 2) :
            for k in range (inW + 2) :
                tmpInput[rgb][i][k] = 127

    # 입력 -> 임시 입력
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpInput[rgb][i + 1][k + 1] = inImg[rgb][i][k];

    # ***진짜 영상처리 알고리즘을구현 ***
    S = 0;

    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :

                for m in range(mSize) :
                    for n in range(mSize) :
                        S += tmpInput[rgb][i + m][k + n] * mask[m][n]
                tmpOutput[rgb][i][k] = S
                S = 0 ## S초기화

    # 후처리: mask의 합이 0이면 127 더하기
    for rgb in range(RGB) :
        for i in range(inH) :
            for k in range (inW) :
                tmpOutput[rgb][i][k] += 127;

    # 임시 출력 -> 원래 출력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                d = tmpOutput[rgb][i][k]
                if (d > 255) :
                    d = 255;
                elif (d < 0) :
                    d = 0;

                outImg[rgb][i][k] = int(d)

    displayImg()

def homogen() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    ## 중요! 출력이미지의 높이, 폭을 결정 --> 알고리즘에 영향
    outH = inH;     outW = inW
    outImg = malloc(outH, outW)

    # 화소영역처리
    # !중요 마스크 결정
    mSize = 5;

    # 임시 입출력 메모리 할당
    tmpInput = malloc(inH + 4, inW +4) # 위 아래 1칸씩 추가공간 마련
    tmpOutput = malloc(outH, outW)

    # 임시 입력 중간값인 평균값으로 초기화
    imsiSum = [0,0,0]
    imsiAvg = [0,0,0]

    for rgb in range(RGB) :
        for i in range (inH) :
            for k in range (inW) :
                imsiSum[rgb] += inImg[rgb][i][k]

    for rgb in range(RGB) :
        imsiAvg[rgb] = int(imsiSum[rgb] / (inW * inH))

    for rgb in range(RGB) :
        for i in range (inH + 4) :
            for k in range (inW + 4) :
                tmpInput[rgb][i][k] = imsiAvg[rgb]

    # 입력 -> 임시 입력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                tmpInput[rgb][i + 2][k + 2] = inImg[rgb][i][k]

    # ** *진짜 영상처리 알고리즘을 구현 ** *
    S = 0.0

    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                Max = 0

                for m in range (mSize) :
                    for n in range (mSize) :
                        S = abs(tmpInput[rgb][i + 2][k + 2] - tmpInput[rgb][i + m][k + n])

                        if (Max < S) :
                            Max = S


                tmpOutput[rgb][i][k] = Max
                S = 0.0; # S초기화

    # 후처리: mask의 합이 0이면 127 더하기
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                tmpOutput[rgb][i][k] += 127.0

    # 임시 출력 -> 원래 출력
    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                if (tmpOutput[rgb][i][k] > 255) :
                    tmpOutput[rgb][i][k] = 255
                elif (tmpOutput[rgb][i][k] < 0) :
                    tmpOutput[rgb][i][k] = 0.0;
                else :
                    outImg[rgb][i][k] = int(tmpOutput[rgb][i][k])

    displayImg();

# 기하학 처리
def zoom() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    scale = askfloat("배율", "값 입력", minvalue = 0, maxvalue = 4)

    # 결정 --> 알고리즘에 영향
    outH = int(inH * scale)
    outW = int(inW * scale)
    outImg = malloc(outW, outH)
    # ** *진짜영상처리 알고리즘을 구현 ***
    for rgb in range(RGB):
        for i in range(outH):
            for k in range(outW):
                    outImg[rgb][i][k] = inImg[rgb][(int)(i / scale)][(int)(k / scale)]

    displayImg()

def move() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    high = askinteger("세로 이동", "값 입력") # 세로
    row = askinteger("가로 이동", "값 입력") # 가로

    # 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)
    # ** *진짜영상처리 알고리즘을 구현 ***
    for rgb in range(RGB):
        for i in range(outH):
            for k in range(outW):
                if (i < high or k < row) :
                    outImg[rgb][i][k] = 0
                else :
                    outImg[rgb][i][k] = inImg[rgb][i - high][k - row]

    displayImg()

def High_Mirror() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    # 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)
    # ** *진짜영상처리 알고리즘을 구현 ***
    for rgb in range(RGB):
        for i in range(outH):
            for k in range(outW):
                outImg[rgb][i][k] = inImg[rgb][outH - i - 1][k]

    displayImg()

def Row_Mirror() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    # 결정 --> 알고리즘에 영향
    outH = inH;   outW = inW
    outImg = malloc(outH, outW)
    # ** *진짜영상처리 알고리즘을 구현 ***
    for rgb in range(RGB):
        for i in range(outH):
            for k in range(outW):
                outImg[rgb][i][k] = inImg[rgb][i][outW - k - 1]

    displayImg()

# 히스토그램
def draw_Histogram() :  # 작동하지 않음
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    pass
    # gry = cv2.cvtColor(inCvImg, cv2.COLOR_BGR2GRAY)
    # result = np.zeros((inCvImg.shape[0], 256), dtype=np.uint8)
    #
    # hist = cv2.calcHist([gry], [0], None, [256], [0, 256])
    # cv2.normalize(hist, hist, 0, result.shape[0], cv2.NORM_MINMAX)
    #
    # for x, y in enumerate(hist):
    #     cv2.line(result, (x, result.shape[0]), (x, result.shape[0] - y), 255)
    #
    # dst = np.hstack([gry, result])
    #
    # cv2.imshow("dst", dst)
    # cv2.waitKey(0)
    # cv2.destroyAllWindows()


## 전역 변수부
window, canvas, paper = None, None, None    # 벽, 게시판, 종이
inImg, outImg = None, None                   # 3차원 배열
inH, inW, outH, outW = [0] * 4
fileName = None
inCvImg, outCvImg = None, None  # OpenCV용
RGB, RR, GG, BB = 3, 0, 1, 2

## 메인 코드부
window = Tk()
window.title("영상처리(Python) (Beta 1)")
window.geometry('700x600')
window.resizable(width=False, height=False)

## 키보드 이벤트
window.bind("<Key>", myKeyEvent)
window.bind("<Control_L>", controlPress)

## 아래 상태창
status = Label(window, text = "이미지정보:", bd=1, relief=SUNKEN, anchor=W)
status.pack(side=BOTTOM, fill=X)

## 위 메뉴바
mainMenu = Menu(window) # 메인 메뉴
window.config(menu = mainMenu)

# 파일 메뉴
fileMenu = Menu(mainMenu)
mainMenu.add_cascade(label = '파일', menu = fileMenu)
fileMenu.add_command(label = '열기', command = openImg)
fileMenu.add_command(label = '저장', command = saveImg)
fileMenu.add_separator()    # 빈줄
# fileMenu.add_command(label = '종료', command = None)

# OpenCV 메뉴
openCvMenu = Menu(mainMenu)
mainMenu.add_cascade(label = 'OpenCV', menu = openCvMenu)
openCvMenu.add_command(label = '동일이미지', command = Cv_Equal)
openCvMenu.add_separator()
# OpenCV 화소점처리
dotCvFunctionMenu = Menu(openCvMenu)
openCvMenu.add_cascade(label = '화소점처리', menu = dotCvFunctionMenu)
dotCvFunctionMenu.add_command(label = '밝게/어둡게', command = Cv_bright)
dotCvFunctionMenu.add_command(label = '그레이스케일', command = Cv_Gray)
dotCvFunctionMenu.add_command(label = '반전', command = Cv_reverse)
# OpenCV 화소영역처리
rangeCvFunctionMenu = Menu(openCvMenu)
openCvMenu.add_cascade(label = '화소영역처리', menu = rangeCvFunctionMenu)
rangeCvFunctionMenu.add_command(label = '엠보싱', command = Cv_Emboss)
# OpenCV 기하학처리
moveCvFunctionMenu = Menu(openCvMenu)
openCvMenu.add_cascade(label = '기하학처리', menu = moveCvFunctionMenu)
moveCvFunctionMenu.add_command(label = '확대/축소', command = Cv_zoom)

openCvMenu.add_separator()
# OpenCV 응용 기능
openCvMenu.add_command(label = '카툰', command = Cv_Cartoon)
openCvMenu.add_command(label = '얼굴 인식', command = Cv_FaceDetect)
openCvMenu.add_command(label = '고양이얼굴 인식', command = Cv_CatFaceDetect)
#
# 기능 메뉴
functionMenu = Menu(mainMenu)
mainMenu.add_cascade(label = '기능', menu = functionMenu)
functionMenu.add_command(label = '동일이미지', command = equal)
functionMenu.add_separator()    # 빈줄
#
# # 화소점 처리
dotFunctionMenu = Menu(functionMenu)
functionMenu.add_cascade(label = '화소점처리', menu = dotFunctionMenu)
dotFunctionMenu.add_command(label = '밝게/어둡게', command = bright)
dotFunctionMenu.add_command(label = '감마', command = gamma)
dotFunctionMenu.add_command(label = '반전(마우스)', command = reverse)
dotFunctionMenu.add_command(label = '그레이', command = gray)
dotFunctionMenu.add_command(label = '강조', command = focus)
# dotFunctionMenu.add_command(label = '채도', command = changeColor_img)
dotFunctionMenu.add_command(label = '포스터라이징', command = posterising)
#
# # 화소영역 처리
rangeFunctionMenu = Menu(functionMenu)
functionMenu.add_cascade(label = '화소영역처리', menu = rangeFunctionMenu)
rangeFunctionMenu.add_command(label = '샤프닝', command = sharp)
rangeFunctionMenu.add_command(label = '고주파 필터링', command = hFillter)
rangeFunctionMenu.add_command(label = '가우시안', command = gaussian)
rangeFunctionMenu.add_command(label = '수직엣지', command = vecticalEdege)
rangeFunctionMenu.add_command(label = '수평엣지', command = horizonEdge)
rangeFunctionMenu.add_command(label = '블러링', command = blurr)
rangeFunctionMenu.add_command(label = '엠보싱', command = emboss)
rangeFunctionMenu.add_command(label = '유사 연산자', command = homogen)

# 기하학 처리
moveFunctionMenu = Menu(functionMenu)
functionMenu.add_cascade(label = '기하학처리', menu = moveFunctionMenu)
moveFunctionMenu.add_command(label = '확대/축소', command = zoom)
moveFunctionMenu.add_command(label = '이동', command = move)
moveFunctionMenu.add_command(label = '세로반전', command = High_Mirror)
moveFunctionMenu.add_command(label = '가로반전', command = Row_Mirror)
#
# 히스토그램
histoFunctionMenu = Menu(functionMenu)
functionMenu.add_cascade(label = '히스토그램', menu = histoFunctionMenu)
histoFunctionMenu.add_command(label = '표 작성하기', command = draw_Histogram)
# histoFunctionMenu.add_command(label = '앤드인', command = histo_Endin)
# histoFunctionMenu.add_command(label = '스트레치', command = histo_Strech)
# histoFunctionMenu.add_command(label = '평활화', command = equalized)


## 마우스 이벤트
label = Label(window, text = "좌표")
button1 = Button(window, text = "마우스 이벤트 시작", fg = 'green', command = clickme1)
button2 = Button(window, text = "마우스 이벤트 시작", fg = 'red', command = clickme2)

label.pack()
button1.pack()
button2.pack()

window.mainloop()