using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JointManager : MonoBehaviour
{

    public FixedJoint joint;
    public GameObject rigidCube;
    public TextMeshPro canCatchText;
    public bool canCatch;
    public bool onCollisionWay;


    // Start is called before the first frame update
    void Start()
    {
        canCatch = true;


    }


    private void OnCollisionEnter(Collision collision)
    {

        // When a collision between a hand and a rigidcube exists while 

        if (collision.gameObject == rigidCube && canCatch == true && onCollisionWay == true)
        {
            //getting all joints on the rigidcube
            FixedJoint[] joints = rigidCube.GetComponents<FixedJoint>();

            //ensuring the rigidcube is only being held by one hand at a time (frame problems can cause this to happen with multiple collisions occuring at the exact same time etc)
            if (joints.Length > 1)
            {
                foreach (FixedJoint fx in joints)
                {
                    Destroy(fx);
                }
            }
            else
            {
                // destroy it
                Destroy(rigidCube.GetComponent<FixedJoint>());
            }

            // create abd add tge fuxed hiubt
            FixedJoint fixedJointToCreate = rigidCube.AddComponent<FixedJoint>();
            // connecting the foxiedjoint to the hand on collision ('this' refers to the hand')
            fixedJointToCreate.connectedBody = this.gameObject.GetComponent<Rigidbody>();
        }
       
    }



    // Update is called once per frame
    void Update()
    {


        // On the right trigger press (used for now to control whether or not the catching of objects is enabled
        // this will definitely be subject to change.

        if (Input.GetButtonDown("RightTriggerPress"))
        {
            if (canCatch == false)
            {
                canCatch = true;
  

            }
            else if (canCatch == true)
            {
                //Destroy all active joints as catching is not disabled.

                FixedJoint[] joints = rigidCube.GetComponents<FixedJoint>();
                foreach (FixedJoint joint in joints)
                {
                    Destroy(joint);

                }


                canCatch = false;



            }
        }

        //Changing the informative text based on the information at hand:

        canCatchText.text = "Catch mode :" + canCatch.ToString();


    }
}
