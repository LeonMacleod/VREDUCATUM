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





    // Start is called before the first frame update
    void Start()
    {

        lastHit = new RaycastHit();
        //All selectable scene objects
        sceneSelectObjects = GameObject.FindGameObjectsWithTag("sceneselectobject");


        // this is required to ensure the forest menu item doesn't reduce to nothing.
        normalScale = forest.transform.localScale;

        levelSelectThreshold = 2.5f;
    }


    private void ScaleUp(GameObject toScale, Vector3 localScaleNormal)
    {

        Vector3 thisScale = toScale.transform.localScale;
        thisScale += localScaleNormal * 0.0025f;
        toScale.transform.localScale = thisScale;

    }

    private void ScaleDown(GameObject toScale, Vector3 localScaleNormal)
    {
        Vector3 thisScale = toScale.transform.localScale;
        if (thisScale.magnitude <= localScaleNormal.magnitude)
        {
            forest.transform.localScale = localScaleNormal;
        }
        else
        {
            thisScale -= 0.01f * localScaleNormal ;
            forest.transform.localScale = thisScale;
        }
    }



    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 10;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {

            //Scaling the menu item up as the raycast hits
            /*
            Vector3 lScale = forest.transform.localScale;
            lScale += forestNormalScale * 0.0025f;
            forest.transform.localScale = lScale;
            */

            ScaleUp(hit.collider.gameObject, normalScale);

            // if the object gets big enough the user will transition to the scene they are sleecting
            /*
            if(forest.transform.localScale.magnitude >= levelSelectThreshold)
            {
                Debug.Log(forest.transform.localScale.magnitude.ToString() + " is of the threshold");
                SceneManager.LoadScene(hit.collider.gameObject.name);
            }
            */

            lastHit = hit;
        }
        else
        {


            // If the user looks away from the menu item it will get smaller and smaller before returning to its original state.
            foreach (GameObject item in sceneSelectObjects)
            {
                ScaleDown(item, normalScale);
                Debug.Log("scaled down " + item.name);

            }
                


            /*
            Vector3 lScale = forest.transform.localScale;
            if(lScale.magnitude <= forestNormalScale.magnitude)
            {
                forest.transform.localScale = forestNormalScale;
            }
            else
            {
                lScale -= 0.01f * forestNormalScale;
                forest.transform.localScale = lScale;
            }
            */
            
        }

    }
}
