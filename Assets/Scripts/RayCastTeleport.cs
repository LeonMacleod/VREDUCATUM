using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTeleport : MonoBehaviour
{
    public GameObject player;
    private LineRenderer thisLR;
    // Start is called before the first frame update
    void Start()
    {


        thisLR = GetComponent<LineRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        int layerMask = 1 << 9;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)){

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("RayHit");

            thisLR.SetPosition(0, this.transform.position);
            thisLR.SetPosition(1, hit.point);
            Debug.Log(hit.point.ToString());



            if (Input.GetButtonDown("RightTriggerPress"))
            {
                Debug.Log("GOT HERE");
                player.transform.position = hit.point;
            }


            




        }
        else
        {
            thisLR.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        }

        

        
    }
}
