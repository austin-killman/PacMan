using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class RandomMovement
{
    public RandomMovement()
    {
         
    }
    public int RandomMoveExcludeLeft()
    {
        float a = Random.Range(0f, 1f);
        if (a < .333)
        {
            return 1;
        }
        else if (a < .6666)
        {
            return 2;
        }
        else
        {
            return 3;
        }

    }


    public int RandomMoveExcludeRight()
    {
        float a = Random.Range(0f, 1f);
        if (a < .333)
        {
            return 0;
        }
        else if (a < .6666)
        {
            return 2;
        }
        else
        {
            return 3;
        }

    }

    public int RandomMoveExcludeUp()
    {
        float a = Random.Range(0f, 1f);
        if (a < .333)
        {
            return 0;
        }
        else if (a < .6666)
        {
            return 1;
        }
        else
        {
            return 3;
        }

    }

    public int RandomMoveExcludeDown()
    {
        float a = Random.Range(0f, 1f);
        if (a < .333)
        {
            return 0;
        }
        else if (a < .6666)
        {
            return 1;
        }
        else
        {
            return 2;
        }

    }

    public int RandomMove()
    {
        float a= Random.Range(0f, 1f);
        if (a < .25)
        {
            return 0;
        }else if(a < .5)
        {
            return 1;
        }else if(a < .75)
        {
            return 2;
        }
        else
        {
            return 3;
        }
       
    }
}