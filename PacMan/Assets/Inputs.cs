using UnityEngine;
using System.Collections;
using System.Collections.Generic;


 public class Inputs : PacmanMove
{
    public float[] inputLayer;
    public Inputs()
    {
  
    }
    
    //public void Update()
    //{
    //    float sum;
    //    Debug.Log(pelletPos.Count);
    //    for(int i = 1; i < pelletPos.Count; i++)
    //    {
    //        sum = Mathf.Abs(pos.x - pelletPos[i][0]) + Mathf.Abs(pos.y - pelletPos[i][1]);
    //        if (sum > 0)
    //        {
    //            inputLayer[i] = (1 / sum) * pelletPos[i][2];
    //        }

    //        Debug.Log(inputLayer[i]);
    //    }

    //}
}