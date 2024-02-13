using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticklerBoss : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;
    public GameObject pointE;
    public float speed = 5;
    bool reverseMove = false;
    Transform nextPos;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointA.transform.position;
        nextPos = pointA.transform;
    }


    void moveToPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        moveToPoint();

        if (Vector2.Distance(transform.position, pointA.transform.position) <= 0.1f)
        {
            reverseMove = false;
            nextPos = pointB.transform;
            Debug.Log("go to point B");
        }
        else if (Vector2.Distance(transform.position, pointB.transform.position) <= 0.1f)
        {
            if (reverseMove == false)
            {
                Debug.Log("go to point C");
                nextPos = pointC.transform;
            }
            else
            {
                nextPos = pointA.transform;
            }
        }
        else if (Vector2.Distance(transform.position, pointC.transform.position) <= 0.1f)
        {
            if (reverseMove == false)
            {
                nextPos = pointD.transform;
            }
            else
            {
                nextPos = pointB.transform;
            }
        }
        else if (Vector2.Distance(transform.position, pointD.transform.position) <= 0.1f)
        {
            if (reverseMove == false)
            {
                nextPos = pointE.transform;

            }
            else
            {
                nextPos = pointC.transform;
            }


        }
        else if (Vector2.Distance(transform.position, pointE.transform.position) <= 0.1f)
        {
            reverseMove = true;
            nextPos = pointD.transform;
        }
    }
}
