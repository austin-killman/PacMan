using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PacmanMove : MonoBehaviour
{
    // public List<GameObject> pellets = new List<GameObject>();
    // public List<float[]> pelletPos = new List<float[]>();
    int inputLength = 168;
    int neurons = 8;
        
    float[] pelletX;
    float[] pelletY;
    float[] pelletZ;
    public Vector3 pos;
    public float speed = 1.0f;
    private float[] inputLayer;
    NeuralNetwork pacController;

    void Start()
    {
        inputLayer = new float[inputLength];
        pelletX = new float[inputLength];
        pelletY = new float[inputLength];
        pelletZ = new float[inputLength];
        pos = transform.position; // Take the current position
       // pelletIn = new Inputs();
        float[] tempArray = new float[3];
        string tempStr = "spr_pill_0";
        // pellets.Add(GameObject.Find("spr_pill_0"));
        for (int i = 0; i < inputLength; i++)
        {
           // Debug.Log(GameObject.Find(tempStr + " " + "(" + i.ToString() + ")").transform.position.x);
            pelletX[i] = GameObject.Find(tempStr + " " + "(" + i.ToString() + ")").transform.position.x;
            pelletY[i]= GameObject.Find(tempStr + " " + "(" + i.ToString() + ")").transform.position.x;
            pelletZ[i] = 1;
            //pelletPos.Add(tempArray);
           
        }

     
        for (int i = 0; i < inputLayer.Length; i++)
        {
            inputLayer[i] = 0;
        }
        this.pacController = new NeuralNetwork(neurons, inputLength, new Matrix(inputLayer));
        Matrix a = new Matrix(new float[2][] { new float[] { 13, 1 }, new float[] { 1, 2 } });
        Matrix b = new Matrix(new float[2][] { new float[] { 1 }, new float[] { 2 } });
        Matrix c =MatrixMath.Multiplication(a, b);
        c.Print();
        Matrix d= MatrixMath.Transpose(c);
        Debug.Log(d.getColumns());
        Debug.Log(d.getRows());
    }
    void FixedUpdate()
    {
       // Debug.Log(pelletPos[0]);

        RaycastHit2D hitup = Physics2D.Raycast(transform.position, Vector2.up, 2);
        RaycastHit2D hitdown = Physics2D.Raycast(transform.position, Vector2.down, 2);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position , Vector2.right,2);
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, 2);
       
        //==Inputs==//
        if (hitleft.rigidbody != null)
        {
            //Debug.Log(hitleft.transform.gameObject.name);
            if(hitleft.rigidbody.gameObject.name == "Gohst")
            {
                Destroy(this.gameObject);
            }
            if (hitleft.rigidbody.gameObject.name != "Gohst" && hitleft.transform.gameObject.name != "pac_man_0") 
            {             
                if(hitleft.rigidbody.gameObject.name.Length == 14)
                {
                     pelletZ[int.Parse(hitleft.rigidbody.gameObject.name[12].ToString())]=150;
                
                }
                else if(hitleft.rigidbody.gameObject.name.Length == 15)
                {
                    pelletZ[int.Parse(hitleft.rigidbody.gameObject.name[12].ToString()+ hitleft.rigidbody.gameObject.name[13].ToString())]= 150;
              
                }
                else if (hitleft.rigidbody.gameObject.name.Length == 16)
                {
                    pelletZ[int.Parse(hitleft.rigidbody.gameObject.name[12].ToString() + hitleft.rigidbody.gameObject.name[13].ToString()
                        + hitleft.rigidbody.gameObject.name[14].ToString())] = 150;
                }
                    // pellets.Remove(hitleft.rigidbody.gameObject);
                    GameObject.Destroy(hitleft.transform.gameObject);        
            }          
        }

        else if (hitright.rigidbody != null)
        {
            if (hitright.rigidbody.gameObject.name != "Gohst" && hitright.rigidbody.gameObject.name != "pac_man_0")
            {
                 GameObject.Destroy(hitright.transform.gameObject);            
            }       
        }
        else if (hitup.rigidbody != null)
        {
            if (hitup.rigidbody.gameObject.name != "Gohst" && hitup.rigidbody.gameObject.name != "pac_man_0")
            {
                GameObject.Destroy(hitup.transform.gameObject);
            }
        }
        else if (hitdown.rigidbody != null)
        {
            if (hitdown.rigidbody.gameObject.name != "Gohst" && hitdown.rigidbody.gameObject.name != "pac_man_0")
            {
                GameObject.Destroy(hitdown.transform.gameObject);
            }

        }
        dist();
        Array.Sort(inputLayer);
        int movement = movementDecider(this.pacController.feedforward(new Matrix(inputLayer)));
        this.pacController.learn(new Matrix(new float[4][] { new float[] { 1f }, new float[] { 0f }, new float[] { 0f }, new float[] { 0f } }));
       // Debug.Log(movement);
        if (movement==0 && transform.position == pos && hitleft.transform == null)// && hitleft.collider.name != "map")
        {
            MoveLeft();
        }
        if (movement == 1 && transform.position == pos && hitright.transform == null)// && hitright.collider.name !="map" )
        {           //(1,0)
            MoveRight();
        }
        if (movement == 2 && transform.position == pos && hitup.transform == null)//&& hitup.collider.name != "map")
        {           
            MoveUp(); 
        }
        if (movement==3 && transform.position == pos && hitdown.transform == null)//&& hitdown.collider.name != "map")
        {           
            MoveDown();
        }
        //Input.GetKey(KeyCode.S)


        //The Current Position = Move To (the current position to the new position by the speed * Time.DeltaTime)
        transform.position = Vector3.MoveTowards(transform.position, pos, speed);    // Move there
        //pelletIn.Update();

       // int movement = this.pacController.feedforward(austin);
      //  austin.Print();

    }


    private int movementDecider(Matrix outputOut)
    {
        float temp = -1f;
        int tempOut = 0;
        for (int i = 0; i < outputOut.getRows(); i++)
        {
            for (int j = 0; j < outputOut.getColumns(); j++)
            {
                if (outputOut.matrix[i][j] > temp)
                {
                    temp = outputOut.matrix[i][j];
                    tempOut = i;
                }
            }
        }
        return tempOut;
    }



    public void dist()
    {
        float sum;
        for (int i = 0; i < inputLength; i++)
        {
        
            sum = Mathf.Abs(pos.x - pelletX[i] + Mathf.Abs(pos.y - pelletY[i]));
            //Debug.Log(sum);
            if (sum > 0)
            {
                inputLayer[i] = sum * pelletZ[i];
                
                //inputLayer[i] = (1 /(Mathf.Pow((float)1.01,sum))) * pelletZ[i];
            }
        }   
    }


    private void MoveRight()
    {
       
        pos += Vector3.right * 2;
    }
    private void MoveLeft()
    {
        pos += Vector3.left * 2;
    }
    private void MoveUp()
    {
        pos += Vector3.up * 2;
    }
    private void MoveDown()
    {
        pos += Vector3.down * 2;
    }

    


}