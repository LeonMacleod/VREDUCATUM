using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineAxisManager : MonoBehaviour {

    public GameObject equationManager;


    public int height;
    public LineRenderer xAxis;
    public LineRenderer yAxis;
    public LineRenderer zAxis;
    public LineRenderer Function;

    private Vector3[] xPositions;
    private Vector3[] yPositions;
    private Vector3[] zPositions;

    public TextMeshPro defaultLabel;
    public TextMeshPro wholeIncrementLabel;
    public TextMeshPro smallIncrementLabel;
    public TextMeshPro liveEquation;


    //for y=ACos(Bx)+c+0
    public float amplitude;
    public float yShift;
    //public float xShift;
    public float B = 1;

    public static EquationManager eM;

    public float increment;

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



    private void RenderAxisLabel(int positionCount, float increment, Vector3 axisVector, float posXChange, float posYChange, float posZChange)
    {
        for (float i = -(positionCount / 2); i < (positionCount / 2); i += increment)
        {
            TextMeshPro AxisLabel;

            float roundedi = (float)Mathf.Round(i * 100f) / 100f;

            //returns true if i is a whole float or an exact half, these are the values I want to have a larger font size.
            if ((roundedi % 1 == 0))
            {
                AxisLabel = wholeIncrementLabel;
                AxisLabel.text = i.ToString("n0");

            }
            else
            {
                AxisLabel = smallIncrementLabel;
                AxisLabel.text = i.ToString("n1");
            }

            Vector3 pos = axisVector * roundedi;
            AxisLabel.transform.position = pos;

            Vector3 newPos = AxisLabel.transform.position;
            newPos.x += posXChange;
            newPos.y += posYChange;
            newPos.z += posZChange;


            AxisLabel.transform.position = newPos;

            Instantiate(AxisLabel, AxisLabel.transform.position, AxisLabel.transform.rotation);

        }
    }


    private void RenderLabels()
    {

        int xPosCount = xAxis.positionCount;
        int yPosCount = yAxis.positionCount;
        int zPosCount = zAxis.positionCount;

        RenderAxisLabel(xPosCount, increment, Vector3.right, 0f, -0.15f, 0f);
        RenderAxisLabel(yPosCount, increment, Vector3.up, -0.15f, 0f, 0f);
        RenderAxisLabel(zPosCount, increment, Vector3.forward, 0f, -0.15f, 0f);
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
            //equation definited in EquationManager y=ACos(Bx)+c
            
            toRender.y = amplitude * Mathf.Cos(B * i ) + yShift;


            Function.SetPosition(incrementCounter, toRender);

        }

    }

	// Use this for initialization
	void Start () {
        RenderAxis();
		

        RenderLabels();


        eM = new EquationManager();



    }
	
	// Update is called once per frame
	void Update () {

        float rightThumbStickAxisVertical = Input.GetAxis("VerticalThumbStickRight");
        /*
        float leftThumbStickAxisHorizontal = Input.GetAxis("HorizontalThumbStickLeft");
        float rightThumbStickAxisHorizontal = Input.GetAxis("HorizontalThumbStickRight");
        float leftThumbStickAxisVertical = Input.GetAxis("VerticalThumbStickLeft");
        */





        RenderFunction();
        /*

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            eM.Right();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            eM.Left();
        }

        */






        //amplitude controller


        /*
        if (Input.GetKey(KeyCode.Alpha1))
        {
            amplitude += 0.1f;
            RenderFunction();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            amplitude -= 0.1f;
            RenderFunction();
        }*/

        //yShift controller

        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            yShift += 0.1f;
            RenderFunction();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            yShift -= 0.1f;
            RenderFunction();
        }*/

        //xShift controller

        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            xShift += 0.25f;
            RenderFunction();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            xShift -= 0.25f;
            RenderFunction();
        }*/

        //cycles per 2pi controller

        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            B += 0.25f;
            RenderFunction();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            B -= 0.25f;
            RenderFunction();
        }*/

        //equation rendering

        //liveEquation.text = "y = " + amplitude.ToString("n2") + "Cos(" + B.ToString("n2") + "x + " + xShift.ToString("n2") + ") + " + yShift.ToString("n2");













    }
}
