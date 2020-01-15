using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class Matrix
{
    public float[][] matrix;
    public Matrix(float[][] matrix)
    {
        this.matrix = matrix;
    }
    public Matrix(float[] matrix)
    {
        
        this.matrix = new float[matrix.Length][];
        for(int i = 0; i < matrix.Length; i++)
        {
            float[] temp = new float[1];
            temp[0] = matrix[i];
           // Debug.Log(temp[0]);
            this.matrix[i] = temp;

        }
    }
    public Matrix(int a)
    {
        matrix = new float[a][];
    }

   public Matrix(int a, int b)
    {
        matrix = new float[a][];
        for(int i=0; i < a; i++)
        {
            matrix[i] = new float[b];
        }
        for (int i = 0; i < a; i++)
        {
            for(int j = 0; j < b; j++)
            {
                matrix[i][j] = 0f;
            }
            
        }
    }
    public int getRows()
    {
        return matrix.Length;
    }
    public int getColumns()
    {
        return matrix[0].Length;
    }
    
    public void Print()
    {
        StringBuilder temp;
        for(int i = 0; i < matrix.Length; i++)
        {
            temp = new StringBuilder();
            for (int j = 0; j < matrix[0].Length; j++)
            {
                temp.Append(matrix[i][j].ToString()+" ");
                
                
            }
            Debug.Log(temp.ToString());
     
        }
        
    }

}