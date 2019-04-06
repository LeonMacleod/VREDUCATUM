using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineAxisManager : MonoBehaviour {

    public int height;
    public LineRenderer xAxis;
    public LineRenderer yAxis;
    public LineRenderer zAxis;
    public LineRenderer Function;

    private Vector3[] xPositions;
    private Vector3[] yPositions;
    private Vector3[] zPositions;

    public TextMeshPro defaultLabel;


    public float amplitude;
    public float yShift;
    public float xShift;
    public float B = 1;

    private void RenderAxis()
    {

      
        xAxis.positionCount = height;
        yAxis.positionCount = height;
        zAxis.positionCount = height;

        xPositions = new Vector3[height];
        yPositions = new Vector3[height];
        zPositions = new Vector3[height];


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

    }

    private void RenderLabels()
    {

        int xPosCount = xAxis.positionCount;
        int yPosCount = yAxis.positionCount;
        int zPosCount = zAxis.positionCount;

        //x axis
        for (int i = 0; i < xPosCount; i++)
        {
            TextMeshPro labelx = defaultLabel;
            labelx.transform.position = xPositions[i];

            string positionTextx = xPositions[i].x.ToString();
            labelx.text = positionTextx;

            Vector3 newPos = labelx.transform.position;
            newPos.y -= 0.25f;
            labelx.transform.position = newPos;

            Instantiate(labelx, labelx.transform.position, labelx.transform.rotation);
        }

        //y axis
        for (int i = 0; i < yPosCount; i++)
        {

            TextMeshPro labely = defaultLabel;
            labely.transform.position = yPositions[i];

            string positionTexty = yPositions[i].y.ToString();
            labely.text = positionTexty;

            Vector3 newPos = labely.transform.position;
            newPos.x -= 0.25f;
            labely.transform.position = newPos;

            Instantiate(labely, labely.transform.position, labely.transform.rotation);
        }

        //z axis
        for (int i = 0; i < zPosCount; i++)
        {

            TextMeshPro labelz = defaultLabel;
            labelz.transform.position = zPositions[i];

            string positionTextz = zPositions[i].z.ToString();
            labelz.text = positionTextz;

            Vector3 newPos = labelz.transform.position;
            newPos.y -= 0.25f;
            labelz.transform.position = newPos;

            Instantiate(labelz, labelz.transform.position, labelz.transform.rotation);
        }


    }


    private void RenderFunction()
    {
		int halfHeight = (height / 2);

        float increment = 0.1f;
        float incrementHeight = height / increment;
        //Debug.Log(incrementHeight);
        //Debug.Log(-(incrementHeight / 2));
        //Debug.Log(incrementHeight / 2);

        Function.positionCount = (int) incrementHeight;

        int incrementCounter = -1;


		Vector3[] functionPositions = new Vector3[height];

        for (float i = -(height/2); i < (height / 2) + 1; i += increment)
        {
            incrementCounter += 1;

            //Debug.Log(" for position:" + i + " incrementcounter position: " + incrementCounter);
  

            Vector3 toRender = new Vector3(i, 0, 0);
            toRender.y = amplitude * Mathf.Cos(B * i + xShift) + yShift;


            Function.SetPosition(incrementCounter, toRender);

        }

    }

	// Use this for initialization
	void Start () {
        RenderAxis();
		

        RenderLabels();
	}
	
	// Update is called once per frame
	void Update () {

        //amplitude controller

        if (Input.GetKey(KeyCode.Alpha1))
        {
            amplitude += 0.1f;
            RenderFunction();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            amplitude -= 0.1f;
            RenderFunction();
        }

        //yShift controller

        if (Input.GetKeyDown(KeyCode.Q))
        {
            yShift += 0.1f;
            RenderFunction();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            yShift -= 0.1f;
            RenderFunction();
        }

        //xShift controller

        if (Input.GetKeyDown(KeyCode.E))
        {
            xShift += 0.25f;
            RenderFunction();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            xShift -= 0.25f;
            RenderFunction();
        }

        //cycles per 2pi controller

        if (Input.GetKeyDown(KeyCode.A))
        {
            B += 0.25f;
            RenderFunction();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            B -= 0.25f;
            RenderFunction();
        }













    }
}
