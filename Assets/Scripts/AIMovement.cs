using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {

    public Transform goal;
    public Transform[] hidingSpots;
    int currentSpotIndex = 0;
    NavMeshAgent agent;

    GameObject player;

    float timeBetweenSpotSwaps = 5;
    float timeOfNextSpotSwap = 0;

    // Use this for initialization
    void Start () {
        
        //player = FindObjectOfType<Player>().gameObject;
        
	}
	
	// Update is called once per frame
	void Update () {
        //agent.destination = goal.position;
        if(CanSeePlayer(transform.position))
        {
            FindNewHidingSpot();
        }
    }

    bool CanSeePlayer(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, (player.transform.position - position), out hit, 10))
        {
            if (hit.collider.GetComponent<Player>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public void Setup(Transform[] spots, GameObject _player)
    {
        player = _player;
        hidingSpots = spots;
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
        FindNewHidingSpot();
    }

    void FindNewHidingSpot()
    {
        // has path is only true when pathing
        // once the AI gets to the destination, it no longer has a path
        if (Time.time > timeOfNextSpotSwap || !agent.hasPath)
        {
            timeOfNextSpotSwap = Time.time + timeBetweenSpotSwaps;


            /////// RANDOM SPOT
            // don't go to the same hiding spot twice in a row
            // this is the same as not moving
            int r = 0;
            do
            {
                r = Random.Range(0, hidingSpots.Length);
            } while (r == currentSpotIndex);
            currentSpotIndex = r;
            goal = hidingSpots[currentSpotIndex];


            /////// NEAREST SPOT

            /////// SHORTEST PATH SPOT

            /////// OUT OF VIEW SPOT

            /////// FAVORITE SPOT

            agent.destination = goal.position;
        }
    }
}
