from tkinter import *   #  GUI 필수
from tkinter import messagebox
from tkinter.filedialog import  * # 파일 대화상자
from tkinter.simpledialog import *

# 함수 선언부
def clickme() :
    global inH, inW, window
    messagebox.showinfo('제목', '내용')

def openImg() :
    global inH, inW, window
    fileName = askopenfilename(parent=window,
                               filetypes = (('칼라파일', '*.png; *.jpg'),('All File', '*.*'))
                               )
    label1.configure(text=fileName)

def brightImg() :
    global inH, inW, window
    value = askinteger('제목', '내용', minvalue = 0, maxvalue = 255)
    label2.configure(text = str(value))


# 전역 변수
window = None
inH, inW = 0,0

# 메인 코드

window = Tk();
window.title("창이름");
window.geometry('400x300')
window.resizable(width=False, height=False)

mainMenu = Menu(window) # 메인 메뉴
window.config(menu = mainMenu)

fileMenu = Menu(mainMenu)
mainMenu.add_cascade(label = '파일', menu = fileMenu)
fileMenu.add_command(label = '열기', command = openImg)
fileMenu.add_command(label = '저장', command = None)
fileMenu.add_separator()        #빈줄
fileMenu.add_command(label = '종료', command = None)

photoMenu = Menu(mainMenu)
mainMenu.add_cascade(label = '영상처리', menu = photoMenu)
photoMenu.add_command(label = '동일이미지', command = None)
photoMenu.add_command(label = '밝게 하기', command = brightImg)
photoMenu.add_command(label = '그레이 스케일', command = None)


label1 = Label(window, text = "글자")
label2 = Label(window, text = "글자", font = ('궁서체',30), fg = 'red')
button1 = Button(window, text = "버튼", bg = 'blue', fg = 'green', command = clickme)

label1.pack();
label2.pack();
button1.pack();
window.mainloop()
