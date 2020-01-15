using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class PacmanMove : MonoBehaviour
{
    // public List<GameObject> pellets = new List<GameObject>();
    // public List<float[]> pelletPos = new List<float[]>();
    int inputLength = 289;// this is the number of pellets it will be multiplied by two for inputs for x and y pos
    int neurons = 20;
    private int movement=1;
    private int score = 0;
    private float nonWall = .5f;
    private float wall = .6f;
    List<int> moves = new List<int>();
    float[] pelletX;
    float[] pelletY;
    float[] pelletZ;
    public Vector3 pos;
    public float speed = 1.0f;
    private float[] inputLayer;
    NeuralNetwork pacController;
    private int nonScore = 0;
    
    void Start()
    {

        setGrid();
     
        for (int i = 0; i < inputLayer.Length; i++)
        {
            inputLayer[i] = 0;
        }
        this.pacController = new NeuralNetwork(neurons, inputLength*2+2, new Matrix(inputLayer));
        //Matrix a = new Matrix(new float[2][] { new float[] { 13, 1 }, new float[] { 1, 2 } });
        //Matrix b = new Matrix(new float[2][] { new float[] { 1 }, new float[] { 2 } });
        //Matrix c =MatrixMath.Multiplication(a, b);
        //c.Print();
        //Matrix d= MatrixMath.Transpose(c);
        //Debug.Log(d.getColumns());
        //Debug.Log(d.getRows());
    }

    private void moveRandomly()
    {
        dist();
        RandomMovement move = new RandomMovement();
        int tempMovement = this.movement;

        if (tempMovement == 0)
        {
            this.movement = move.RandomMoveExcludeRight();
        }
        else if (tempMovement == 1)
        {
            this.movement = move.RandomMoveExcludeLeft();
        }
        else if (tempMovement == 2)
        {
            this.movement = move.RandomMoveExcludeDown();
        }
        else
        {
            this.movement = move.RandomMoveExcludeUp();
        }
    }

    private void setGrid()
    {
        inputLayer = new float[inputLength*2+2];
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
            pelletY[i] = GameObject.Find(tempStr + " " + "(" + i.ToString() + ")").transform.position.x;
            pelletZ[i] = 1;
            //pelletPos.Add(tempArray);

        }
    }

    private void gameOver()
    {
        Destroy(this.gameObject);
        new SaveMoves(score, moves);

        pacController.saveWeights();
        SceneManager.LoadScene("Level1");
    }
    private void hitDetection(RaycastHit2D hit,int move)
    {
        nonScore = 0;
        if (hit.rigidbody.gameObject.name == "Gohst")
        {
            Destroy(this.gameObject);
            new SaveMoves(score,moves);
            SceneManager.LoadScene("Level1");
        }
        if (hit.rigidbody.gameObject.name != "Gohst" && hit.transform.gameObject.name != "pac_man_0")
        {
            if(move == 0)
            {
                this.pacController.learn(new Matrix(new float[4][] { new float[] { 1f }, new float[] { 0f }, new float[] { 0f }, new float[] { 0f } }));
            }else if(move == 1)
            {
                this.pacController.learn(new Matrix(new float[4][] { new float[] { 0f }, new float[] { 1f }, new float[] { 0f }, new float[] { 0f } }));
            }else if(move == 2)
            {
                this.pacController.learn(new Matrix(new float[4][] { new float[] { 0f }, new float[] { 0f }, new float[] { 1f }, new float[] { 0f } }));
            }else if (move == 3)
            {
                this.pacController.learn(new Matrix(new float[4][] { new float[] { 0f }, new float[] { 0f }, new float[] { 0f }, new float[] { 1f } }));
            }
            score +=1;
            if (hit.rigidbody.gameObject.name.Length == 14)
            {
                pelletZ[int.Parse(hit.rigidbody.gameObject.name[12].ToString())] = 150;

            }
            else if (hit.rigidbody.gameObject.name.Length == 15)
            {
                pelletZ[int.Parse(hit.rigidbody.gameObject.name[12].ToString() + hit.rigidbody.gameObject.name[13].ToString())] = 150;

            }
            else if (hit.rigidbody.gameObject.name.Length == 16)
            {
                pelletZ[int.Parse(hit.rigidbody.gameObject.name[12].ToString() + hit.rigidbody.gameObject.name[13].ToString()
                    + hit.rigidbody.gameObject.name[14].ToString())] = 150;
            }
            // pellets.Remove(hitleft.rigidbody.gameObject);
            GameObject.Destroy(hit.transform.gameObject);
        }
        
    }
    void FixedUpdate()
    {
       
        // Debug.Log(pelletPos[0]);
        if (nonScore > 500)
        {
            gameOver();
        }
        RaycastHit2D hitup = Physics2D.Raycast(transform.position, Vector2.up, 2);
        RaycastHit2D hitdown = Physics2D.Raycast(transform.position, Vector2.down, 2);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position , Vector2.right,2);
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, 2);
        int movement = movementDecider(this.pacController.feedforward(new Matrix(inputLayer)));

        //==Inputs==//
        if (hitleft.rigidbody != null)
        {
            hitDetection(hitleft,0);  
        }
        else if (hitright.rigidbody != null)
        {
            hitDetection(hitright,1);      
        }
        else if (hitup.rigidbody != null)
        {
            hitDetection(hitup,2);
        }
        else if (hitdown.rigidbody != null)
        {
            hitDetection(hitdown,3);

        }
        else
        {
            nonScore += 1;
        }
       // moveRandomly();



        //moves.Add(movement);


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

        learnWall(hitright,hitleft,hitup,hitdown);
        //The Current Position = Move To (the current position to the new position by the speed * Time.DeltaTime)
        transform.position = Vector3.MoveTowards(transform.position, pos, speed);    // Move there
        //pelletIn.Update();
        
       // int movement = this.pacController.feedforward(austin);
      //  austin.Print();

    }

    private void learnWall(RaycastHit2D hitRight, RaycastHit2D hitLeft, RaycastHit2D hitUp, RaycastHit2D hitDown)
    {
        // this is the dummest way I could have wrote this code
        if (hitRight.transform != null && hitLeft.transform != null && hitUp.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { nonWall }, new float[] { nonWall }, new float[] { nonWall }, new float[] { wall } }));
        }else if(hitRight.transform != null && hitLeft.transform != null && hitDown.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { nonWall }, new float[] { nonWall }, new float[] { wall }, new float[] { nonWall } }));
        }
        else if (hitDown.transform != null && hitLeft.transform != null && hitUp.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { nonWall }, new float[] { wall }, new float[] { nonWall }, new float[] { nonWall } }));
        }else if (hitDown.transform != null && hitRight.transform != null && hitUp.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { wall }, new float[] { wall }, new float[] { nonWall }, new float[] { nonWall } }));
        }
        else if (hitRight.transform !=null && hitLeft.transform!= null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { nonWall }, new float[] { nonWall }, new float[] { wall }, new float[] { wall } }));
        }else if(hitUp.transform != null && hitDown.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { wall }, new float[] { wall }, new float[] { nonWall }, new float[] { nonWall } }));
        }else if(hitRight.transform !=null && hitDown.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { wall }, new float[] { nonWall }, new float[] { wall }, new float[] { nonWall } }));
        }else if(hitLeft.transform != null && hitDown.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { nonWall }, new float[] { wall }, new float[] { wall }, new float[] { nonWall } }));
        }else if (hitLeft.transform != null && hitUp.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { nonWall }, new float[] { wall }, new float[] { nonWall }, new float[] { wall } }));

        }
        else if (hitRight.transform != null && hitUp.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { wall }, new float[] { nonWall }, new float[] { nonWall }, new float[] { wall } }));
        }
        else if(hitLeft.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { nonWall }, new float[] { wall }, new float[] { wall }, new float[] { wall } }));
        }
        else if (hitRight.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { wall }, new float[] { nonWall }, new float[] { wall }, new float[] { wall } }));
        }
        else if (hitUp.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { wall }, new float[] { wall }, new float[] { nonWall }, new float[] { wall } }));
        }
        else if (hitDown.transform != null)
        {
            this.pacController.learn(new Matrix(new float[4][] { new float[] { 1f }, new float[] { 1f }, new float[] { 1f }, new float[] { nonWall } }));
        }
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
        // this function sorts the x and y components of the inputs and the
        float[] xPos = new float[inputLength], yPos = new float[inputLength];
        // want to put together two matricies for x and y and these respective distances
        for (int i = 0; i < inputLength; i++)
        {
            xPos[i] = (pos.x - pelletX[i])*pelletZ[i];
            yPos[i] = (pos.y - pelletY[i])*pelletZ[i];
            
            //sum = Mathf.Abs(pos.x - pelletX[i] + Mathf.Abs(pos.y - pelletY[i]));
            //Debug.Log(sum);
            //if (sum > 0)
            //{
            //    inputLayer[i] = sum * pelletZ[i];
                
            //    //inputLayer[i] = (1 /(Mathf.Pow((float)1.01,sum))) * pelletZ[i];
            //}
        }
        Array.Sort(xPos);
        Array.Sort(yPos);
        var myList = new List<float>();
        myList.AddRange(xPos);
        myList.AddRange(yPos);
        myList.Add(pos.x);
        myList.Add(pos.y);
        inputLayer = myList.ToArray();
        
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