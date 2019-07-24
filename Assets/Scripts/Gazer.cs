using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gazer : MonoBehaviour
{

    public GameObject forest;
    private Vector3 normalScale;
    public float levelSelectThreshold;

    public GameObject[] sceneSelectObjects;
    public Vector3[] localScales;

    private RaycastHit lastHit;

    public GameObject currentSelection;





    // Start is called before the first frame update
    void Start()
    {

        lastHit = new RaycastHit();
        //All selectable scene objects 
        sceneSelectObjects = GameObject.FindGameObjectsWithTag("sceneselectobject");


        // this is required to ensure the forest menu item doesn't reduce to nothing.
        normalScale = new Vector3(1.5f, 1.5f, 1.5f);

        // If the scale of a scene selection object (magnitude of local scale vector) becomes greater than this the player will transition to the scene.
        levelSelectThreshold = 3.25f;
    }


    private void ScaleUp(GameObject toScale, Vector3 localScaleNormal)
    {
        //Scaling up the object

        Vector3 thisScale = toScale.transform.localScale;
        thisScale += localScaleNormal * 0.0025f;
        toScale.transform.localScale = thisScale;

    }

    private void ScaleDown(GameObject toScale, Vector3 localScaleNormal)
    {
        //Scaling down the object but logic is included to ensure the object doesn't become smaller
        //than the localScalNormal (smallest it can be)
        Vector3 thisScale = toScale.transform.localScale;
        if (thisScale.magnitude <= localScaleNormal.magnitude)
        {
         toScale.transform.localScale = localScaleNormal;
        }
        else
        {
            thisScale -= 0.01f * localScaleNormal ;
            toScale.transform.localScale = thisScale;
        }
    }



    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 10;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {


            //Scaling up the object while the raycast is hitting the scene selector
            ScaleUp(hit.collider.gameObject, normalScale);

            // if the object gets big enough the user will transition to the scene they are sleecting
            if(hit.collider.gameObject.transform.localScale.magnitude >= levelSelectThreshold)
            {

                //The name of the object the ray is hitting is used to load the scene therefore when setting up new objects to navigate
                // to new scenes the name of these objects must be the name of the scene it is transitioning to. (along with appropriate tags)
                SceneManager.LoadScene(hit.collider.gameObject.name);
            }
            

            lastHit = hit;
        }
        else
        {


            // If the user looks away from the menu item it will get smaller and smaller before returning to its original state.

            foreach (GameObject item in sceneSelectObjects)
            {
                ScaleDown(item, normalScale);
            }

        }

        

    }
}
