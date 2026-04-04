using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

enum States {Resting, Rising, Waiting, Falling}
public class PlatformRising : MonoBehaviour
{
    [SerializeField] private GameObject PlatformObjectRef;
    [SerializeField] private float RideHeight;
    [SerializeField] private float RideSpeed;
    [SerializeField] private float WaitTime;
    private States curState = States.Resting;
    private float startingY;
    private float endingY;
    private float timer;

  
    private void Awake()
    {
        //set starting y to current position of platform
        //calculate ending y by start + height of ride
        startingY = PlatformObjectRef.transform.position.y;
        endingY = startingY + RideHeight;
    }

    //check whether the player has entered the trigger object
    //if so move the elevator up at a constant rate/ curve rate
    // wait some time then fall back down to start pos

    private void Update()
    {
        switch (curState)
        {
            case States.Rising:
                //if platform triggered move to the endpoint and move into waiting phase
                PlatformObjectRef.transform.position = Vector3.MoveTowards(PlatformObjectRef.transform.position,new Vector3(PlatformObjectRef.transform.position.x, endingY, PlatformObjectRef.transform.position.z), RideSpeed * Time.deltaTime);
                if (PlatformObjectRef.transform.position.y >= endingY)
                {
                    timer = WaitTime;
                    curState = States.Waiting;
                }
                break;

            case States.Waiting:
                //wait for the timer and then move to falling phase
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    curState = States.Falling;
                }
                break;

            case States.Falling:
                //fall to the starting position and move into resting phase
                PlatformObjectRef.transform.position = Vector3.MoveTowards(PlatformObjectRef.transform.position,new Vector3(PlatformObjectRef.transform.position.x, startingY, PlatformObjectRef.transform.position.z),RideSpeed * Time.deltaTime);
                if (PlatformObjectRef.transform.position.y <= startingY)
                {
                    curState = States.Resting;
                }
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //if player and the platform is currently resting move into rising phase
        if (other.CompareTag("Player") && curState == States.Resting)
        {
            curState = States.Rising;
        }
    }

}

