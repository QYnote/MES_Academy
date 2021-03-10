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

    equal_img()


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



def displayImg() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB

    ## 기존에 그림을 붙인적이 있으면 게시판(canvas) 뜯어내기
    if (canvas != None) :
        canvas.destroy()

    window.geometry(str(outW) + "x" + str(outH))        # 벽 크기 조절
    canvas = Canvas(window, height=outH, width=outW)    # 게시판 크기 조절
    paper = PhotoImage(height=outH, width=outW)
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


def equal_img() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    outH = inH; outW = inW
    outImg = malloc(outH, outW)

    for rgb in range (RGB) :
        for i in range(inH) :
            for k in range (inW) :
                outImg[rgb][i][k] = inImg[rgb][i][k]

    displayImg()

def gray_img() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return

    outH = inH;    outW = inW
    outImg = malloc(outH, outW)

    for i in range(inH) :
        for k in range (inW) :
            hap = inImg[RR][i][k] + inImg[GG][i][k] + inImg[BB][i][k]
            outImg[RR][i][k] = outImg[GG][i][k] = outImg[BB][i][k] = hap//3

    displayImg()

def bright_img() :
    global window, canvas, paper, inImg, outImg, inH, inW, outH, outW
    global fileName, inCvImg, outCvImg, RGB, RR, GG, BB
    if (inImg == None) :
        return;

    outImg = malloc(outH, outW)

    value = askinteger("밝게/어둡게", "값 입력", minvalue=-255, maxvalue=255)

    for rgb in range(RGB):
        for i in range(inH):
            for k in range(inW):
                if ((inImg[rgb][i][k] + value) > 255) :
                    outImg[rgb][i][k] = 255
                elif ((inImg[rgb][i][k] + value) < 0) :
                    outImg[rgb][i][k] = 0
                else :
                    outImg[rgb][i][k] = inImg[rgb][i][k] + value

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
functionMenu.add_command(label = '동일이미지', command = equal_img)
#
# # 화소점 처리
dotFunctionMenu = Menu(functionMenu)
functionMenu.add_cascade(label = '화소점처리', menu = dotFunctionMenu)
dotFunctionMenu.add_command(label = '밝게/어둡게', command = bright_img)
# dotFunctionMenu.add_command(label = '감마', command = gamma)
# dotFunctionMenu.add_command(label = '반전', command = reverse_img)
dotFunctionMenu.add_command(label = '그레이', command = gray_img)
# dotFunctionMenu.add_command(label = '강조', command = focus)
# dotFunctionMenu.add_command(label = '채도', command = changeColor_img)
# dotFunctionMenu.add_command(label = '포스터라이징', command = posterising)
#
# # 화소영역 처리
# rangeFunctionMenu = Menu(functionMenu)
# functionMenu.add_cascade(label = '화소영역처리', menu = rangeFunctionMenu)
# rangeFunctionMenu.add_command(label = '샤프닝', command = sharp_image)
# rangeFunctionMenu.add_command(label = '고주파 필터링', command = hFillter_image)
# rangeFunctionMenu.add_command(label = '가우시안', command = gaussian_image)
# rangeFunctionMenu.add_command(label = '수직엣지', command = vecticalEdege_image)
# rangeFunctionMenu.add_command(label = '수평엣지', command = horizonEdge_image)
# rangeFunctionMenu.add_command(label = '블러링', command = blurr_image)
# rangeFunctionMenu.add_command(label = '엠보싱', command = emboss_image)
# rangeFunctionMenu.add_command(label = '유사 연산자', command = homogen_image)
#
# # 기하학 처리
# moveFunctionMenu = Menu(functionMenu)
# functionMenu.add_cascade(label = '기하학처리', menu = moveFunctionMenu)
# moveFunctionMenu.add_command(label = '확대', command = zoom_in_Img)
# moveFunctionMenu.add_command(label = '축소', command = zoom_out_Img)
# moveFunctionMenu.add_command(label = '이동', command = move_Img)
# moveFunctionMenu.add_command(label = '가로반전', command = Row_Mirror_Img)
# moveFunctionMenu.add_command(label = '세로반전', command = High_Mirror_Img)
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