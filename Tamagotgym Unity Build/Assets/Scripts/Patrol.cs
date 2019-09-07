using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public float speed;
    public float wanderTime;

    private bool movingRight = true;
    private bool isWandering = true;
    private float idleTime;

    public Transform groundDetection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isWandering && wanderTime > 0)
        {
            wander();
            wanderTime -= Time.deltaTime;
        }
        else if (isWandering && wanderTime < 0)
        {
            isWandering = false;
            idleTime = Random.Range(3.0f, 5.0f);
            idle();
        }
        else
        {
            idleTime -= Time.deltaTime;
            if (idleTime < 0)
            {
                isWandering = true;
                wanderTime = Random.Range(3.0f, 9.0f);
            }
        }
    }

    void wander()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        GetComponent<Animator>().SetFloat("speed", speed);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    void idle()
    {
        GetComponent<Animator>().SetFloat("speed", 0);
    }
}
