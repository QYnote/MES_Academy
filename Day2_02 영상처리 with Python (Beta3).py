from tkinter import *   #  GUI 필수
from tkinter import messagebox
from tkinter.filedialog import  * # 파일 대화상자
from tkinter.simpledialog import *

import  cv2
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

#화소점처리
def bright() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return;

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
    tmpInput = malloc(inW+2, inH+2)
    tmpOutput = malloc(outW, outH)

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

# 기하학 처리
def zoom_in() :
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
#
# # OpenCV 메뉴
# openCvMenu = Menu(mainMenu)
# mainMenu.add_cascade(label = 'OpenCV', menu = openCvMenu)
# openCvMenu.add_command(label = '열기', command = None)
# openCvMenu.add_command(label = '저장', command = None)
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
# rangeFunctionMenu.add_command(label = '고주파 필터링', command = hFillter)
# rangeFunctionMenu.add_command(label = '가우시안', command = gaussian)
# rangeFunctionMenu.add_command(label = '수직엣지', command = vecticalEdege)
# rangeFunctionMenu.add_command(label = '수평엣지', command = horizonEdge)
# rangeFunctionMenu.add_command(label = '블러링', command = blurr)
# rangeFunctionMenu.add_command(label = '엠보싱', command = emboss)
# rangeFunctionMenu.add_command(label = '유사 연산자', command = homogen)
#
# # 기하학 처리
moveFunctionMenu = Menu(functionMenu)
functionMenu.add_cascade(label = '기하학처리', menu = moveFunctionMenu)
moveFunctionMenu.add_command(label = '확대', command = zoom_in)
# moveFunctionMenu.add_command(label = '축소', command = zoom_out)
# moveFunctionMenu.add_command(label = '이동', command = move)
# moveFunctionMenu.add_command(label = '가로반전', command = Row_Mirror)
# moveFunctionMenu.add_command(label = '세로반전', command = High_Mirror)
#
# # 히스토그램
# histoFunctionMenu = Menu(functionMenu)
# functionMenu.add_cascade(label = '히스토그램', menu = histoFunctionMenu)
# histoFunctionMenu.add_command(label = '표 작성하기', command = draw_Histogram)
# histoFunctionMenu.add_command(label = '앤드인', command = histo_Endin_img)
# histoFunctionMenu.add_command(label = '스트레치', command = histo_Strech_img)
# histoFunctionMenu.add_command(label = '평활화', command = equalized_img)


## 마우스 이벤트
label = Label(window, text = "좌표")
button1 = Button(window, text = "마우스 이벤트 시작", fg = 'green', command = clickme1)
button2 = Button(window, text = "마우스 이벤트 시작", fg = 'red', command = clickme2)

label.pack()
button1.pack()
button2.pack()

window.mainloop()
