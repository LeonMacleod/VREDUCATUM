using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInteractionRange : MonoBehaviour
{
    public Transform eventPosition;
    public UnityEvent InteractionRange;



    // Start is called before the first frame update
    void Start()
    {

        if(InteractionRange == null)
        {
            InteractionRange = new UnityEvent();
           
        }
        
    }

    // Update is called once per frame
    void Update()
    { 

        Vector3 playerPosition = this.transform.position;
        float distance = Vector3.Distance(playerPosition, eventPosition.position);
        if (distance < 5.0f)
        {


            InteractionRange.Invoke();
            Debug.Log("Invoked Reaction Range");
        }

        Debug.Log("distance:" + distance);
        

    }
}
