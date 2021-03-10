# (중요!) 리스트(=배열) 처리법

# 함수 선언
def mall(size) :    # 빈 함수
    retAry = []     # 확인 작업을 위한 코드, 지워도 상관 없음
    retAry = [0 for _ in range(size)]
    return retAry

def mall2(row, col, value = 0) : # value 값을 안적고 보내면 0 있으면 value 값 ex. mall2(3,4)
    retAry = [[value for _ in range (col)] for _ in range(row)]
    return  retAry


# 전역 변수

# 메인 코드
if __name__ == '__main__' : ## void main

    ### 1차원 배열###
    # 빈 배열 100칸 준비   =   int[] numlist = new int [100];
    # numlist = []
    # for _ in range (100) :
    #    numlist.append(0)

    # numlist = [0 for _ in range(100)]   # numlist = (int*) malloc(100 + sizeof(int));
    # numlist = mall(100)

    ###2차원 배열###
    # 4*5 2차원 리스트
    image = []
    tmp = []    # 임시 1차원 배열

    # for _ in range(4) :     # 3행
    #     tmp = []            # tmp 초기화
    #     for _ in range(5)   # 5행
    #         tmp.append(0)
    #     image.append(tmp)

    # image = [ [0 for _ in range(4)] for _ in range(3)]
    mall2(3,4)



