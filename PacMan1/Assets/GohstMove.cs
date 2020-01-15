using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GohstMove : MonoBehaviour
{
    // Start is called before the first frame update

    int cur = 0;
    public Transform[] waypoints;
    public float speed = 0.3f;
    // Update is called once per frame
    //Debug.Log(hitleft.transform.gameObject);


    void FixedUpdate()
    {
        RaycastHit2D hitup = Physics2D.Raycast(transform.position, Vector2.up, 2);
        RaycastHit2D hitdown = Physics2D.Raycast(transform.position, Vector2.down, 2);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position, Vector2.right, 2);
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, 2);



        // Waypoint not reached yet? then move closer
        if (transform.position != waypoints[cur].position)
        {
           
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            waypoints[cur].position,
                                            speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else
        {
          
            cur = (cur + 1)% waypoints.Length;
        }

    }
 
}
