using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtoise : MonoBehaviour
{
    [SerializeField] Transform[] movePoints;
    [SerializeField] float speed;

    int currentPointIndex = 1;

    private void Start()
    {
        transform.position = movePoints[0].position;
    }

    private void Update()
    {
       gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, movePoints[currentPointIndex].position, speed * Time.deltaTime);

        Debug.Log(transform.position);

        if(movePoints[currentPointIndex].position == gameObject.transform.position)
        {
            Debug.Log("Update next points");    
            if (currentPointIndex + 1 < movePoints.Length)
            {
                currentPointIndex++;
            }
            else { currentPointIndex = 0; }
        }
    }

}
