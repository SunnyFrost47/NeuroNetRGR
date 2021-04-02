using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuroNetRGR
{
    public partial class Form1 : Form
    {
        List<Bitmap> imgsTrain0 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain1 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain2 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain3 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain4 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain5 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain6 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain7 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain8 = new List<Bitmap>(10000);
        List<Bitmap> imgsTrain9 = new List<Bitmap>(10000);

        List<Bitmap> imgsTest0 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest1 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest2 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest3 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest4 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest5 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest6 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest7 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest8 = new List<Bitmap>(1600);
        List<Bitmap> imgsTest9 = new List<Bitmap>(1600);

        NeuralNetwork NN = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CutImg("../../train/mnist_train0.jpg", imgsTrain0);
            CutImg("../../train/mnist_train1.jpg", imgsTrain1);
            CutImg("../../train/mnist_train2.jpg", imgsTrain2);
            CutImg("../../train/mnist_train3.jpg", imgsTrain3);
            CutImg("../../train/mnist_train4.jpg", imgsTrain4);
            CutImg("../../train/mnist_train5.jpg", imgsTrain5);
            CutImg("../../train/mnist_train6.jpg", imgsTrain6);
            CutImg("../../train/mnist_train7.jpg", imgsTrain7);
            CutImg("../../train/mnist_train8.jpg", imgsTrain8);
            CutImg("../../train/mnist_train9.jpg", imgsTrain9);

            CutImg("../../test/mnist_test0.jpg", imgsTest0);
            CutImg("../../test/mnist_test1.jpg", imgsTest1);
            CutImg("../../test/mnist_test2.jpg", imgsTest2);
            CutImg("../../test/mnist_test3.jpg", imgsTest3);
            CutImg("../../test/mnist_test4.jpg", imgsTest4);
            CutImg("../../test/mnist_test5.jpg", imgsTest5);
            CutImg("../../test/mnist_test6.jpg", imgsTest6);
            CutImg("../../test/mnist_test7.jpg", imgsTest7);
            CutImg("../../test/mnist_test8.jpg", imgsTest8);
            CutImg("../../test/mnist_test9.jpg", imgsTest9);
            int[] numbers = { imgsTrain0.Count, imgsTrain1.Count, imgsTrain2.Count, imgsTrain3.Count, imgsTrain4.Count, imgsTrain5.Count, imgsTrain6.Count, imgsTrain7.Count, imgsTrain8.Count, imgsTrain9.Count };
            int maxSize = numbers.Min();
            numericUpDown1.Maximum = maxSize*10;
            numericUpDown1.Value = maxSize*10;
        }

        void CutImg(string url, List<Bitmap> imgs)
        {
            int x1 = 0, x2, y1, y2;
            Bitmap procImg = new Bitmap(Image.FromFile(url));
            //Bitmap resImg = new Bitmap(28, 28);
            while (x1 < procImg.Width)
            {
                y1 = 0;
                bool isNum = false;
                while (y1 < procImg.Height)
                {
                    Bitmap resImg = new Bitmap(28, 28);
                    isNum = false;
                    for (x2 = 0; x2 < 28; x2++)
                    {
                        for (y2 = 0; y2 < 28; y2++)
                        {
                            Color procPix = procImg.GetPixel(x1, y1);
                            if ((!isNum) && (procPix.R > 200) && (procPix.G > 200) && (procPix.B > 200)) isNum = true;
                            resImg.SetPixel(x2, y2, procPix);
                            y1++;
                        }
                        x1++;
                        y1 -= 28;
                    }
                    if (isNum) imgs.Add(resImg);
                    else break;
                    y1 += 28;
                    x1 -= 28;
                }
                if (!isNum) break;
                x1 += 28;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] structure = { 34, 10 };
            NN = new NeuralNetwork(112, structure);
            //Bitmap procImg;
            double[][] trainInputs0 = new double[imgsTrain0.Count][];
            double[][] trainOutputs0 = new double[imgsTrain0.Count][];
            double[][] trainInputs1 = new double[imgsTrain1.Count][];
            double[][] trainOutputs1 = new double[imgsTrain1.Count][];
            double[][] trainInputs2 = new double[imgsTrain2.Count][];
            double[][] trainOutputs2 = new double[imgsTrain2.Count][];
            double[][] trainInputs3 = new double[imgsTrain3.Count][];
            double[][] trainOutputs3 = new double[imgsTrain3.Count][];
            double[][] trainInputs4 = new double[imgsTrain4.Count][];
            double[][] trainOutputs4 = new double[imgsTrain4.Count][];
            double[][] trainInputs5 = new double[imgsTrain5.Count][];
            double[][] trainOutputs5 = new double[imgsTrain5.Count][];
            double[][] trainInputs6 = new double[imgsTrain6.Count][];
            double[][] trainOutputs6 = new double[imgsTrain6.Count][];
            double[][] trainInputs7 = new double[imgsTrain7.Count][];
            double[][] trainOutputs7 = new double[imgsTrain7.Count][];
            double[][] trainInputs8 = new double[imgsTrain8.Count][];
            double[][] trainOutputs8 = new double[imgsTrain8.Count][];
            double[][] trainInputs9 = new double[imgsTrain9.Count][];
            double[][] trainOutputs9 = new double[imgsTrain9.Count][];

            int size = (int)numericUpDown1.Value / 10;
            GetVectSigns(trainInputs0, trainOutputs0, imgsTrain0, 0, size);
            GetVectSigns(trainInputs1, trainOutputs1, imgsTrain1, 1, size);
            GetVectSigns(trainInputs2, trainOutputs2, imgsTrain2, 2, size);
            GetVectSigns(trainInputs3, trainOutputs3, imgsTrain3, 3, size);
            GetVectSigns(trainInputs4, trainOutputs4, imgsTrain4, 4, size);
            GetVectSigns(trainInputs5, trainOutputs5, imgsTrain5, 5, size);
            GetVectSigns(trainInputs6, trainOutputs6, imgsTrain6, 6, size);
            GetVectSigns(trainInputs7, trainOutputs7, imgsTrain7, 7, size);
            GetVectSigns(trainInputs8, trainOutputs8, imgsTrain8, 8, size);
            GetVectSigns(trainInputs9, trainOutputs9, imgsTrain9, 9, size);
            
            double[][] trainInputs = new double[10 * size][];
            double[][] trainOutputs = new double[10 * size][];
            for (int i = 0; i < size; i++)
            {
                trainInputs[10 * i] = trainInputs0[i];
                trainOutputs[10 * i] = trainOutputs0[i];
                trainInputs[10 * i + 1] = trainInputs1[i];
                trainOutputs[10 * i + 1] = trainOutputs1[i];
                trainInputs[10 * i + 2] = trainInputs2[i];
                trainOutputs[10 * i + 2] = trainOutputs2[i];
                trainInputs[10 * i + 3] = trainInputs3[i];
                trainOutputs[10 * i + 3] = trainOutputs3[i];
                trainInputs[10 * i + 4] = trainInputs4[i];
                trainOutputs[10 * i + 4] = trainOutputs4[i];
                trainInputs[10 * i + 5] = trainInputs5[i];
                trainOutputs[10 * i + 5] = trainOutputs5[i];
                trainInputs[10 * i + 6] = trainInputs6[i];
                trainOutputs[10 * i + 6] = trainOutputs6[i];
                trainInputs[10 * i + 7] = trainInputs7[i];
                trainOutputs[10 * i + 7] = trainOutputs7[i];
                trainInputs[10 * i + 8] = trainInputs8[i];
                trainOutputs[10 * i + 8] = trainOutputs8[i];
                trainInputs[10 * i + 9] = trainInputs9[i];
                trainOutputs[10 * i + 9] = trainOutputs9[i];
           
            }
            NN.Learn(trainInputs, trainOutputs);
            
        }

        void GetVectSigns(double[][] inputs, double[][] outputs, List<Bitmap> imgs, int n, int size)
        {
            if (size == -1) size = imgs.Count;
            Bitmap procImg;
            for (int i = 0; i < size; i++)
            {
                inputs[i] = new double[112];
                outputs[i] = new double[10];
                procImg = imgs[i];
                for (int x = 0; x < 28; x++)
                {
                    for (int y = 0; y < 28; y++)
                    {
                        int j = x / 7 * 28 + y;
                        if (x == 0 || x == 7 || x == 14 || x == 21) inputs[i][j] = 0.0;
                        Color procPix = procImg.GetPixel(x, y);
                        if ((procPix.R > 200) && (procPix.G > 200) && (procPix.B > 200))
                        {
                            inputs[i][j] = 1.0;
                        }
                    }
                }
                for (int j = 0; j < 10; j++)
                {
                    if (j == n) outputs[i][j] = 1.0;
                    else outputs[i][j] = 0.0;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            double[][] testInputs0 = new double[imgsTest0.Count][];
            double[][] testOutputs0 = new double[imgsTest0.Count][];
            double[][] testInputs1 = new double[imgsTest1.Count][];
            double[][] testOutputs1 = new double[imgsTest1.Count][];
            double[][] testInputs2 = new double[imgsTest2.Count][];
            double[][] testOutputs2 = new double[imgsTest2.Count][];
            double[][] testInputs3 = new double[imgsTest3.Count][];
            double[][] testOutputs3 = new double[imgsTest3.Count][];
            double[][] testInputs4 = new double[imgsTest4.Count][];
            double[][] testOutputs4 = new double[imgsTest4.Count][];
            double[][] testInputs5 = new double[imgsTest5.Count][];
            double[][] testOutputs5 = new double[imgsTest5.Count][];
            double[][] testInputs6 = new double[imgsTest6.Count][];
            double[][] testOutputs6 = new double[imgsTest6.Count][];
            double[][] testInputs7 = new double[imgsTest7.Count][];
            double[][] testOutputs7 = new double[imgsTest7.Count][];
            double[][] testInputs8 = new double[imgsTest8.Count][];
            double[][] testOutputs8 = new double[imgsTest8.Count][];
            double[][] testInputs9 = new double[imgsTest9.Count][];
            double[][] testOutputs9 = new double[imgsTest9.Count][];

            GetVectSigns(testInputs0, testOutputs0, imgsTest0, 0, -1);
            GetVectSigns(testInputs1, testOutputs1, imgsTest1, 1, -1);
            GetVectSigns(testInputs2, testOutputs2, imgsTest2, 2, -1);
            GetVectSigns(testInputs3, testOutputs3, imgsTest3, 3, -1);
            GetVectSigns(testInputs4, testOutputs4, imgsTest4, 4, -1);
            GetVectSigns(testInputs5, testOutputs5, imgsTest5, 5, -1);
            GetVectSigns(testInputs6, testOutputs6, imgsTest6, 6, -1);
            GetVectSigns(testInputs7, testOutputs7, imgsTest7, 7, -1);
            GetVectSigns(testInputs8, testOutputs8, imgsTest8, 8, -1);
            GetVectSigns(testInputs9, testOutputs9, imgsTest9, 9, -1);

            int[] numbers = { imgsTest0.Count, imgsTest1.Count, imgsTest2.Count, imgsTest3.Count, imgsTest4.Count, imgsTest5.Count, imgsTest6.Count, imgsTest7.Count, imgsTest8.Count, imgsTest9.Count };
            int maxSize = numbers.Max();
            label3.Text = (maxSize * 10).ToString();
            int fails = 0;
            for (int i = 0; i < maxSize; i++)
            {
                if (i < imgsTest0.Count)
                {
                    var predict0 = NN.Compute(testInputs0[i]);
                    if (predict0.Max() != predict0[0]) fails++;
                }
                if (i < imgsTest1.Count)
                {
                    var predict1 = NN.Compute(testInputs1[i]);
                    if (predict1.Max() != predict1[1]) fails++;
                }
                if (i < imgsTest2.Count)
                {
                    var predict2 = NN.Compute(testInputs2[i]);
                    if (predict2.Max() != predict2[2]) fails++;
                }
                if (i < imgsTest3.Count)
                {
                    var predict3 = NN.Compute(testInputs3[i]);
                    if (predict3.Max() != predict3[3]) fails++;
                }
                if (i < imgsTest4.Count)
                {
                    var predict4 = NN.Compute(testInputs4[i]);
                    if (predict4.Max() != predict4[4]) fails++;
                }
                if (i < imgsTest5.Count)
                {
                    var predict5 = NN.Compute(testInputs5[i]);
                    if (predict5.Max() != predict5[5]) fails++;
                }
                if (i < imgsTest6.Count)
                {
                    var predict6 = NN.Compute(testInputs6[i]);
                    if (predict6.Max() != predict6[6]) fails++;
                }
                if (i < imgsTest7.Count)
                {
                    var predict7 = NN.Compute(testInputs7[i]);
                    if (predict7.Max() != predict7[7]) fails++;
                }
                if (i < imgsTest8.Count)
                {
                    var predict8 = NN.Compute(testInputs8[i]);
                    if (predict8.Max() != predict8[8]) fails++;
                }
                if (i < imgsTest9.Count)
                {
                    var predict9 = NN.Compute(testInputs9[i]);
                    if (predict9.Max() != predict9[9]) fails++;
                }
            }
            label5.Text = ((double)fails / (double)maxSize / 10).ToString();
        }
    }
}
