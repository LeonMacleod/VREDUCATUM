using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAxisManager : MonoBehaviour {

    public int height;
    public LineRenderer xAxis;
    public LineRenderer yAxis;
    public LineRenderer zAxis;

    private void RenderAxis()
    {

      
        xAxis.positionCount = 200;
        yAxis.positionCount = 200;
        zAxis.positionCount = 200;

        Vector3[] xPositions = new Vector3[height];
        Vector3[] yPositions = new Vector3[height];
        Vector3[] zPositions = new Vector3[height];


        xAxis.startColor = Color.red;
        yAxis.startColor = Color.blue;
        zAxis.startColor = Color.green;


        for (int i = -(height/2); i < (height/2); i++)
        {
            //adjusting for a negative i value
            int realIndex = i + (height / 2);
            xPositions[realIndex] = Vector3.right * i;
            yPositions[realIndex] = Vector3.up * i;
            zPositions[realIndex] = Vector3.forward * i;

        }


        xAxis.SetPositions(xPositions);
        yAxis.SetPositions(yPositions);
        zAxis.SetPositions(zPositions);


        // debug
        /*foreach (Vector3 pos in positions)
        {
            Debug.Log(pos.ToString());

        }*/

    }


    private void RenderFunction()
    {
        Debug.Log("Render Function Running");

    }



	// Use this for initialization
	void Start () {
        RenderAxis();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
