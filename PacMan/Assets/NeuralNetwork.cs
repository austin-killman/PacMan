using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class NeuralNetwork
{
    private int outputNeruonLength = 4;
   // private Matrix inputs;
    private Matrix weightH1,weightH2,input;
    private Matrix bias1,bias2,learningOut,hiddenLearn;
    private float lr = .01f;
    
   // private float learningRate = .1f;
    public NeuralNetwork()
    {
    }

    public NeuralNetwork(Matrix inputs)
    {
       
    }

    public NeuralNetwork(int neruons, int inLength,Matrix inputs)
    {
        //will have to change and give inputs straight to feedforward part
       // this.inputs = inputs;
        bias1 = MatrixMath.Randomize(neruons,1);
        weightH1 = MatrixMath.Randomize(neruons, inLength);
        weightH2 = MatrixMath.Randomize(outputNeruonLength, neruons);
        bias2 = MatrixMath.Randomize(outputNeruonLength,1);
       // weightH2 = MatrixMath.Randomize(c, d);
        //weightH1.Print();
       // H1Assign();
    }

    public Matrix feedforward(Matrix inputs)
    {
        this.input = inputs;
        Matrix hidden = MatrixMath.Multiplication(weightH1, inputs);
        Matrix hOut = MatrixMath.Addition(hidden, bias1);
        Matrix hiddenOut = activationFunction(hOut);
        Matrix output = MatrixMath.Multiplication(weightH2, hiddenOut);
        Matrix oOut = MatrixMath.Addition(output, bias2);
        Matrix outputOut = activationFunction(oOut);
        learningOut = outputOut;
        hiddenLearn = hidden;
        return outputOut;
    }

    private Matrix dsigmoid(Matrix a)
    {
        Matrix b = a;
        //a.Print();
        //inputLayer[i] = (1 /(Mathf.Pow((float)1.01,sum))) * pelletZ[i];
        for (int i = 0; i < a.getRows(); i++)
        {
            for (int j = 0; j < a.getColumns(); j++)
            {
                if (a.matrix[i][j] != 0)
                {

                    b.matrix[i][j] = (a.matrix[i][j]*(1- a.matrix[i][j]));
                }
                else
                {
                    b.matrix[i][j] = 0;
                }
                //Debug.Log(a.matrix[i][j]);
            }
        }

        return b;
    }

    

    public void learn(Matrix target)
    {
        Matrix outputError = MatrixMath.Subtraction(target, this.learningOut); // this is the error of my output layer will use a fuck ton of math for next steps

        // This is the gradient from output to 
        Matrix dSigmoidOutput = dsigmoid(learningOut);
       
        //dsig rows: 4 Col: 1   outputError Rows: 4 Col: 1
        Matrix temp = MatrixMath.HMultiplication(dSigmoidOutput,outputError);
        
        Matrix gradient = MatrixMath.Multiplication(temp,this.lr);
       // Debug.Log(gradient.getColumns() + " " + gradient.getRows() + " " + temp.getColumns() + " " + temp.getRows());
        Matrix hiddenTranspose = MatrixMath.Transpose(hiddenLearn);
        Matrix weightHODeltas = MatrixMath.Multiplication(gradient, hiddenTranspose);


        //hiddenLearn.Print();
        
        this.weightH2 = MatrixMath.Addition(this.weightH2,weightHODeltas);


        Matrix weightH2Transpose = MatrixMath.Transpose(weightH2);
        Matrix hiddenErrors = MatrixMath.Multiplication(weightH2Transpose, outputError);
        //calcl hidden gradient
        Matrix dSigmoidHidden = dsigmoid(hiddenLearn);
        Matrix hiddenTemp = MatrixMath.HMultiplication(dSigmoidHidden, hiddenErrors);
        Matrix hiddenGradient = MatrixMath.Multiplication(hiddenTemp, this.lr);
        //clac input to hidden deltas
        Matrix inputTranspose = MatrixMath.Transpose(input);
        Matrix weight1Delta = MatrixMath.Multiplication(hiddenGradient, inputTranspose);
        this.weightH1 = MatrixMath.Addition(this.weightH1,weight1Delta);
        this.weightH1.Print();
    }

    private Matrix activationFunction(Matrix a)
    {
        Matrix b = a;
        //a.Print();
        //inputLayer[i] = (1 /(Mathf.Pow((float)1.01,sum))) * pelletZ[i];
        for (int i =0; i < a.getRows(); i++)
        {
            for (int j = 0; j < a.getColumns(); j++)
            {
                if (a.matrix[i][j] != 0)
                {
                   
                    b.matrix[i][j] = 1 / (1+(Mathf.Pow((float)2.7, -a.matrix[i][j])));
                }
                else
                {
                    b.matrix[i][j] = 0;
                }
                //Debug.Log(a.matrix[i][j]);
            }
        }
        
        return b;
     
    }


    private void H1Assign()
    {
        //byte[] b = new byte[3];
        //FileStream file = new FileStream("",FileMode.Open);
        //IEnumerable<string> a = File.ReadLines("C:/Users/Austin/Desktop/Weights.txt");
        //FileStream a = File.OpenRead("C:/Users/Austin/Desktop/Weights.txt");
        //Debug.Log(a.Read(b,0,2).ToString());
        //a.Close();
    }





   
}