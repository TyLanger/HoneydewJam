using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour {

    // controls when the player wins or loses the game
    // wins when they find all the hiding players
    // no lose state yet.
    // Maybe a kick the can mechanic?

    public Player player;

    int numHidersTagged = 0;
    public int totalNumHiders = 5;
    public AIMovement hider;

    public Transform[] hidingSpots;

	// Use this for initialization
	void Start () {
        player.HiderTagged += HiderTagged;
        for (int i = 0; i < totalNumHiders; i++)
        {
            AIMovement copy = Instantiate(hider, transform.position, Quaternion.identity) as AIMovement;
            copy.Setup(hidingSpots, player.gameObject);
        }
	}
	

    void HiderTagged()
    {
        Debug.Log("Hider tagged");

    }
}
