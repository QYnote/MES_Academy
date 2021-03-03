using System;

using OpenCvSharp;  //openCV사용

namespace Mission02_OpenCV_기능
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat inCvImg, outCvImg;  //배열 2개 준비
            inCvImg = Cv2.ImRead("c:\\img\\Etc_JPG(rectangle)\\airplane.jpg");  //이미지 불러오기
            outCvImg = new Mat();

            Random rnd = new Random();



            /*
             * 화소점
                감마, 포스터라이징, 채도

               화소영역
                엠보싱, 블러링, 가우신, 샤프닝, 고주파 필터링, 수직, 수평엣지, 유사연산자

               기하학 처리
                이동

               히스토그램
                스트레치, 엔드인, 평활화
             */
            //화소점 처리
            //1. 그레이 스케일
            Cv2.CvtColor(inCvImg, outCvImg, ColorConversionCodes.BGR2GRAY);
            //원본,  출력      효과

            //밝기
            int br_value = rnd.Next(-255, 255);
            Cv2.Add(inCvImg, br_value , outCvImg);

            //반전
            Cv2.BitwiseNot(inCvImg, outCvImg);

            //감마


            //2. 2진화
            Cv2.CvtColor(inCvImg, outCvImg, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(outCvImg, outCvImg, 127, 255, ThresholdTypes.Otsu);
            Cv2.Threshold(입력, 출력, 기준값, 최대값, ThresholdTypes.Otsu);

            //3. 적응형 2진화
            Cv2.CvtColor(inCvImg, outCvImg, ColorConversionCodes.BGR2GRAY);
            Cv2.AdaptiveThreshold(outCvImg, outCvImg, 255, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 25, 5);


            //기하학처리
            //확대
            Cv2.PyrUp(inCvImg, outCvImg, new OpenCvSharp.Size(inCvImg.Width * 2, inCvImg.Height * 2));
            //축소
            Cv2.PyrDown(inCvImg, outCvImg, new OpenCvSharp.Size(inCvImg.Width / 2, inCvImg.Height / 2));
            //상하 반전
            Cv2.Flip(inCvImg, outCvImg, FlipMode.X);
            //좌우 반전
            Cv2.Flip(inCvImg, outCvImg, FlipMode.Y);
            //상하+좌우 반전
            Cv2.Flip(inCvImg, outCvImg, FlipMode.XY);

            //화소 영역처리
            //5.블러링
            Cv2.Blur(inCvImg, outCvImg, new OpenCvSharp.Size(15, 15));


            // (6) 주황색 추출
            inCvImg = Cv2.ImRead("c:\\img\\\\opencv\\tomato.jpg");
            Mat hsv = new Mat(new OpenCvSharp.Size(inCvImg.Width, inCvImg.Height), MatType.CV_8UC3);
                //사진 박스 8bit, unsigned, 3채널(칼라(?)로 생성
            Cv2.CvtColor(inCvImg, hsv, ColorConversionCodes.BGR2HSV);
                //사진박스 hsv에 inCvImg의 RGB색을 HSV로 변환하여 보관
            Mat[] HSV = Cv2.Split(hsv);
                //일렬로 나열한 hsv를 HSV 배열로 변환
            Mat H = new Mat(inCvImg.Size(), MatType.CV_8UC1);
                //inCvImg와 같은 크기의 사진 박스 생성
            Cv2.InRange(HSV[0], new Scalar(8), new Scalar(20), H);
                //배열 요소가 다른 두 배열의 요소 사이에 있는지 확인합니다.(Scalar의 의미를 모르곘음)
            Cv2.BitwiseAnd(hsv, hsv, outCvImg, H);
                //두 배열의 비트 결합을 계산합니다.
            Cv2.CvtColor(outCvImg, outCvImg, ColorConversionCodes.HSV2BGR);
                //HSV색을 RGB 색으로 변환

            /*
            //히스토그램 --이해 못하는중
            Mat histB = new Mat();
            Mat histG = new Mat();
            Mat histR = new Mat();
            Mat resultB = Mat.Ones(new Size(256, inCvImg.Height), MatType.CV_8UC3);
            Mat resultG = Mat.Ones(new Size(256, inCvImg.Height), MatType.CV_8UC3);
            Mat resultR = Mat.Ones(new Size(256, inCvImg.Height), MatType.CV_8UC3);

            Cv2.CvtColor(inCvImg, outCvImg, ColorConversionCodes.BGR2BGRA);

            Cv2.CalcHist(new Mat[] { outCvImg }, new int[] { 0 }, null, histB, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(histB, histB, 0, 255, NormTypes.MinMax);

            Cv2.CalcHist(new Mat[] { outCvImg }, new int[] { 1 }, null, histG, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(histG, histG, 0, 255, NormTypes.MinMax);

            Cv2.CalcHist(new Mat[] { outCvImg }, new int[] { 2 }, null, histR, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
            Cv2.Normalize(histR, histR, 0, 255, NormTypes.MinMax);

            for (int i = 0; i < histB.Rows; i++)
            {
                Cv2.Line(resultB, new Point(i, inCvImg.Height), new Point(i, inCvImg.Height - histB.Get<float>(i)), Scalar.Blue);
            }
            for (int i = 0; i < histG.Rows; i++)
            {
                Cv2.Line(resultG, new Point(i, inCvImg.Height), new Point(i, inCvImg.Height - histG.Get<float>(i)), Scalar.Green);
            }
            for (int i = 0; i < histR.Rows; i++)
            {
                Cv2.Line(resultR, new Point(i, inCvImg.Height), new Point(i, inCvImg.Height - histR.Get<float>(i)), Scalar.Red);
            }

            Cv2.ImShow("Blue", resultB);
            Cv2.ImShow("Green", resultG);
            Cv2.ImShow("Red", resultR);
            */

            //화면 출력
            Cv2.ImShow("InputImg", inCvImg);
            Cv2.ImShow("outputImg", outCvImg);

            Cv2.WaitKey(0); //키보드 입력시까지 기다리기
        }
    }
}
