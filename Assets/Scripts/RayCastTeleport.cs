using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastTeleport : MonoBehaviour
{
    public GameObject player;
    public GameObject CastedPlayer;
    private LineRenderer thisLR;
    private Quaternion fixedRotation;
    // Start is called before the first frame update
    void Start()
    {


        thisLR = GetComponent<LineRenderer>();
        fixedRotation = CastedPlayer.transform.rotation;


    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        // Layermasks can be configured so that players can only teleport to certain objects.
        int layerMask = 1 << 9;

        // On ray hit
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)){

            // A line renderer is projecting a line (Cannot view debug rays in gameplay)

            // Setting the first position as the hands transform (raycast is attatched to hand cube.
            thisLR.SetPosition(0, this.transform.position);
            // Setting the second position (of the line renderer) to the point at which the raycast hits.
            thisLR.SetPosition(1, hit.point);

            CastedPlayer.SetActive(true);

            Vector3 thisHit = hit.point;
            thisHit.y += 0.25f;
            CastedPlayer.transform.position = thisHit;



            // On right trigger press the player teleports to the raycast hit location.
            if (Input.GetButtonDown("RightTriggerPress"))
            {
                player.transform.position = hit.point;
            }


    

        }
        else
        {
            // If no teleportation is available a teleport casted player shouldn't be viewable.
            CastedPlayer.SetActive(false);
            // If the raycast doesn't hit the linerenderer is reset.
            thisLR.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
        }

        

        
    }


    private void LateUpdate()
    {
        CastedPlayer.transform.rotation = fixedRotation;
    }
}
