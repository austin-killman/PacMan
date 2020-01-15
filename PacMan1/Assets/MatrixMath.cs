using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MatrixMath
{
    public static Matrix Addition(Matrix a,Matrix b)
    {
        Matrix result = a;
        for(int i=0; i < a.getRows(); i++)
        {
            for(int j = 0; j < a.getColumns(); j++)
            {
                result.matrix[i][j] = a.matrix[i][j] + b.matrix[i][j];
            }
        }
        return result;
    }
  
    public static Matrix Subtraction(Matrix a, Matrix b)
    {
        //return A- B

        Matrix result = a;
        for (int i = 0; i < a.getRows(); i++)
        {
            for (int j = 0; j < a.getColumns(); j++)
            {
                result.matrix[i][j] = a.matrix[i][j] - b.matrix[i][j];
            }
        }

        return result;
    }
    public static Matrix Multiplication(Matrix a, float b)
    {
        for (int i = 0; i < a.getRows(); i++)
        {
            for (int j = 0; j < a.getColumns(); j++)
            {

                a.matrix[i][j] = a.matrix[i][j] * b;
            }
        }
        return a;
    }
    public static Matrix Multiplication(Matrix a, Matrix b)
    {
        // Matrix result = new Matrix();
        Matrix c = new Matrix(a.getRows(), b.getColumns());
        // float[] temp = new float[b.getColumns()];
        //float tempa=0;
        if (a.getColumns() == b.getRows())
        {
            int i, j, k, m = a.getRows(), n = a.getColumns(), q = b.getColumns();

            for (i = 0; i < m; i++)//goes until rows of a 
            {

                for (j = 0; j < q; j++) //goes until columns of b
                {

                    for (k = 0; k < n; k++)// goes until columns/ rows of a and b
                    {
                        // Debug.Log(a.matrix[i][k] + " " + b.matrix[k][j]);
                        c.matrix[i][j] += a.matrix[i][k] * b.matrix[k][j];

                        //  c.matrix[i][j] += a.matrix[i][k] * b.matrix[k][j];
                    }



                }

            }
        }
        else
        {
            Debug.Log("dummy wrong math operations");
        }

        return c;
    }
    public static Matrix HMultiplication(Matrix a, Matrix b)
    {
        // Matrix result = new Matrix();
        Matrix c = new Matrix(a.getRows(), b.getColumns());
        // float[] temp = new float[b.getColumns()];
        //float tempa=0;
        if (a.getRows() == b.getRows() && a.getColumns() == b.getColumns())
        {
         

            for (int i = 0; i < a.getRows(); i++)//goes until rows of a 
            {

                for (int j = 0; j < a.getColumns(); j++) //goes until columns of b
                {

                        c.matrix[i][j] += a.matrix[i][j] * b.matrix[i][j];
                }

            }
        }
        else
        {
            Debug.Log("dummy wrong math operations");
        }

        return c;
    }

    public static Matrix Transpose(Matrix a)
    {
        Matrix result= new Matrix(a.getColumns(),a.getRows());
        for(int i = 0; i < a.matrix.Length; i++)
        {
            for(int j= 0; j < a.matrix[0].Length; j++)
            {
                result.matrix[j][i] = a.matrix[i][j];
            }
        }
         
        return result;
    }

    public static Matrix Randomize(Matrix a)
    {
        
        Random rnd = new Random();
        for(int i = 0; i < a.getRows(); i++)
        {
            float[] temp = new float[a.getColumns()];
            for (int j = 0; j < a.getColumns(); j++)
            {
                
                temp[j] = Random.Range(-1f,1f); // Mainly for weights assigns rando valo from -1 to 1
            }
            a.matrix[i] = temp;
        }

        return a;
    }
    public static Matrix Randomize(int a, int b)
    {
        Matrix result = new Matrix(a);
        
        for (int i = 0; i < a; i++)
        {
            float[] temp = new float[b];
            for (int j = 0; j < b; j++)
            {
                temp[j] = Random.Range(0f, 2f) - 1; // Mainly for weights assigns rando valo from -1 to 1
            }
            result.matrix[i] = temp;
        }

        return result;
    }

}