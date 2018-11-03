using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK
}

public class FSMManager : MonoBehaviour {

    public PlayerState currentState;
    public PlayerState startState;
    public Transform marker;

    Dictionary<PlayerState, PlayerFSMState> states
        = new Dictionary<PlayerState, PlayerFSMState>();

    private void Awake() {
        marker = GameObject.FindGameObjectWithTag("Marker").transform;

        states.Add(PlayerState.IDLE, GetComponent<PlayerIDEL>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add(PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add(PlayerState.ATTACK, GetComponent<PlayerATTACK>());
    }

    public void SetState(PlayerState newState) {
        foreach (PlayerFSMState fsm in states.Values) {
            fsm.enabled = false;
        }
        
        /*
        states[PlayerState.IDLE].enabled = false;
        states[PlayerState.RUN].enabled = false;
        states[PlayerState.CHASE].enabled = false;
        states[PlayerState.ATTACK].enabled = false;
        */

        states[newState].enabled = true;
    }

    // Use this for initialization
    void Start () {
        SetState(startState);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            //Debug.Log("Click" + Input.mousePosition);
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 1000)) {
                marker.position = hit.point;

                SetState(PlayerState.RUN);
            }
        }
	}

}
