## 랜덤하게 1~100까지의 숫자 10개를 배열(=list)에 저장하고, 그 합계를 출력
import random       # using ,#include

## 함수 선언

## 전역 변수 선언
numlist = []        # 빈 배열
hap = 0

## 메인 코드
if __name__ == '__main__' : ## void main

    for i in range (10) :
        num = random.randint(1, 100)
        numlist.append(num)

    print(numlist)

    hap = sum(numlist)

    print(hap)