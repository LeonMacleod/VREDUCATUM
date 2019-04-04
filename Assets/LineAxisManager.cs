﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAxisManager : MonoBehaviour {

    public int height;
    public LineRenderer xAxis;
    public LineRenderer yAxis;
    public LineRenderer zAxis;
    public LineRenderer Function;
 

    private void RenderAxis()
    {

      
        xAxis.positionCount = height;
        yAxis.positionCount = height;
        zAxis.positionCount = height;

        Vector3[] xPositions = new Vector3[height];
        Vector3[] yPositions = new Vector3[height];
        Vector3[] zPositions = new Vector3[height];


        xAxis.startColor = Color.red;
        yAxis.startColor = Color.blue;
        zAxis.startColor = Color.green;

        //+1 added to ensure whole axis is full.
        for (int i = -(height/2); i < (height/2) + 1; i++)
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
		int halfHeight = (height / 2);
        Function.positionCount = height;

		Vector3[] functionPositions = new Vector3[height];

        for (int o = 0; o < height; o++)
        {
            Vector3 toRender = xAxis.GetPosition(o);
            //toRender.y = 3 * (toRender.x * toRender.x);
            toRender.y = Mathf.Cos(toRender.x);


            Debug.Log(o);
            Function.SetPosition(o, toRender);


        }

    }



	// Use this for initialization
	void Start () {
        RenderAxis();
		RenderFunction();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
