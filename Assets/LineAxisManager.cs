using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineAxisManager : MonoBehaviour {


    public TextMeshPro coefficientPrefab;
    public GameObject emptyEquation;
    public GameObject equationIdentifier;
    public float marginIncrement = 2.5f;
    public List<List<int>> equationCoefficients;

    public GameObject child;


    // Outsourced to LineAxisManager.cs

    public List<GameObject> coefficientsToManipulate;
    public int selectedCoefficientIndex;
    public GameObject selectedCoefficientGameobject;

    public GameObject currentSelection;

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

    public TextMeshPro amplitudeGO;
    public TextMeshPro yShiftGO;
    public TextMeshPro BGO;



    public List<TextMeshPro> coefficients;




    //for y=ACos(Bx)+c+0
    public float amplitude;
    public float yShift;
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
        for (int i = -(height / 2); i < (height / 2) + 1; i++)
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



    public void RenderAxisLabel(int positionCount, float increment, Vector3 axisVector, float posXChange, float posYChange, float posZChange)
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


    public void RenderLabels()
    {

        int xPosCount = xAxis.positionCount;
        int yPosCount = yAxis.positionCount;
        int zPosCount = zAxis.positionCount;

        RenderAxisLabel(xPosCount, increment, Vector3.right, 0f, -0.15f, 0f);
        RenderAxisLabel(yPosCount, increment, Vector3.up, -0.15f, 0f, 0f);
        RenderAxisLabel(zPosCount, increment, Vector3.forward, 0f, -0.15f, 0f);
    }


    public void RenderFunction()
    {
        int halfHeight = (height / 2);

        float increment = 0.1f;
        float incrementHeight = height / increment;
        //Debug.Log(incrementHeight);
        //Debug.Log(-(incrementHeight / 2));
        //Debug.Log(incrementHeight / 2);

        Function.positionCount = (int)incrementHeight;

        int incrementCounter = -1;


        Vector3[] functionPositions = new Vector3[height];

        for (float i = -(height / 2); i < (height / 2) + 1; i += increment)
        {
            incrementCounter += 1;

            //Debug.Log(" for position:" + i + " incrementcounter position: " + incrementCounter);


            Vector3 toRender = new Vector3(i, 0, 0);
            //equation definited in EquationManager y=ACos(Bx)+c

            toRender.y = amplitude * Mathf.Cos(B * i) + yShift;


            Function.SetPosition(incrementCounter, toRender);

        }

    }

    public string ListToString(List<int> list)
    {
        string start = "";
        foreach (int item in list)
        {
            start += item.ToString() + ",";
        }

        return start;

    }

    public void CoefficientMove(int uniqueIdentifier)
    {

    }


    public void EquationRender(string equation, List<int> coefficients)
    {
        // adding this equations coefficients to the equationCoefficients public list of lists (coefficients),
        // this will be used to uniquely identify the equations coefficients by its index within this public list of lists.
        equationCoefficients.Add(coefficients);

        // this is the equation identifier, it is a gameobject within each coefficient used to identify coefficients. (essentially acting as a second tag).
        // this is stored under each textmeshpro element with a name of 'equationidentifier' and a tag of the equation index e.g. 0 or 1. 
        GameObject thisIdentifier = equationIdentifier;
        thisIdentifier.tag = "equationidentifier";

        //The margin to increment off (when splitting up the equation)
        float initialMargin = 0f;
        // The empty game object that will hold any equation created using this function.
        GameObject equationHolder = emptyEquation;
        // Instantiating the equation holder under the equationmanager as a child and naming it.
        GameObject instantiatedEquationHolder = (GameObject)Instantiate(equationHolder, this.transform);
        // .count returns the length of elements E.g. [1,2,3] has a count of 2 but if I were to index 2 I would get '3' this '-1' ensures the count returns exactly what it index's.
        instantiatedEquationHolder.name = equation;


        // Each character is split up into its own TextMeshPro element (thisEquatinPart) and eventually instantiated under the equationHolder (specified above).
        foreach (char character in equation.ToCharArray())
        {
            TextMeshPro thisEquationPart = coefficientPrefab;
            thisEquationPart.text = character.ToString();


            //Margin manipulation (marginIncrement) is public and is essentially the default margin between characters in the rendered equations.
            float margin = initialMargin + marginIncrement;
            thisEquationPart.margin = new Vector4(initialMargin, 0, 0, 0);
            initialMargin = margin;

            //string name = (equationCoefficients.Count - 1).ToString();
            string name = "equationpart";


            //default color, white
            thisEquationPart.color = Color.white;
            thisEquationPart.tag = "equationpart";



            //Whether or not a character is a coefficient

            foreach (int coefficient in coefficients)
            {
                //is coefficient
                if (character == equation.ToCharArray()[coefficient])
                {
                    // THE MARGIN Can be specifically adjusted here to give coefficients extra spacing if for example they will be larger than one character.

                    //thisEquationPart.color = Color.red;
                    name = coefficient.ToString();
                    thisEquationPart.tag = "coefficient";


                }

            }
            //Instantiating the equation parts under the equationHolder
            TextMeshPro thisCoefficientGameobject = (TextMeshPro)Instantiate(thisEquationPart, instantiatedEquationHolder.transform);
            //specifying the name, this changes depending on whether or not the character under this iteration is a coefficient.
            thisCoefficientGameobject.name = name;

            GameObject thisIdentifierInstantiated = (GameObject)Instantiate(thisIdentifier, thisCoefficientGameobject.transform);
            thisIdentifierInstantiated.name = (equationCoefficients.Count - 1).ToString();


        }

    }

    public void clearColors(List<GameObject> coefficients)
    {

        foreach (GameObject coefficient in coefficients)
        {
            coefficient.transform.parent.gameObject.GetComponent<TextMeshPro>().color = Color.white;
        }

    }




    public List<GameObject> YieldCoefficients(int uniqueIdentifier)
    {
        // finding all objects within the scene with the 'equationidentifier' tag
        GameObject[] equationIdentifiers = GameObject.FindGameObjectsWithTag("equationidentifier");
        // the list of gameobjects we will add these found objects to.
        List<GameObject> equationIdentifiersAsList = new List<GameObject>();

        // for every object with the equation identifier.
        foreach (GameObject equationid in equationIdentifiers)
        {

            // if we are dealing with the equation we wish to yield coefficients from (unique indentifier parameter)

            if (equationid.gameObject.name == uniqueIdentifier.ToString())
            {
                // the resulting integer from our tryParse.
                int resultingInteger;
                // trying to parse the equationidentifier name to an integer, if its an integer its a coefficient, if not it is not a coefficient (we are trying to yield coefficients)
                bool isInt = int.TryParse(equationid.transform.parent.gameObject.name, out resultingInteger);
                if (isInt == true)
                {
                    //its a coefficient so we will add it to our list of equationidentifiers which are coefficients
                    equationIdentifiersAsList.Add(equationid);
                }
            }
        }
        //return
        return equationIdentifiersAsList;

    }

    // Use this for initialization
    void Start() {
        RenderAxis();


        RenderLabels();




        //for equation of coefficients 2, 7, 11


        equationCoefficients = new List<List<int>>();
        //EquationRender("y=Ax", new List<int> { 2 });
        //EquationRender("y=ACos(Bx)+c+0", new List<int> { 2, 7, 11 });
        //EquationRender("y=3x^2", new List<int> { 3 });
        //EquationRender("y=Bx+C", new List<int> { 2, 5 });
        EquationRender("y=ACos(Bx)+c", new List<int> { 2, 7, 11 });

        coefficientsToManipulate = YieldCoefficients(0);
        selectedCoefficientIndex = -1;

        //eM = new EquationManager();






        // StartCoroutine(Wait());


    }






    public void Right()
    {
        if (selectedCoefficientIndex < coefficientsToManipulate.Count - 1)
        {
            selectedCoefficientIndex += 1;

            Debug.Log((coefficientsToManipulate.Count - 1).ToString() + "count and current selection " + selectedCoefficientIndex.ToString());
            Debug.Log(coefficientsToManipulate[selectedCoefficientIndex].transform.parent.name);
            clearColors(coefficientsToManipulate);
            coefficientsToManipulate[selectedCoefficientIndex].transform.parent.gameObject.GetComponent<TextMeshPro>().color = Color.blue;
        }
    }

    public void Left()
    {
        if (selectedCoefficientIndex > 0)
        {
            selectedCoefficientIndex -= 1;

            Debug.Log((coefficientsToManipulate.Count - 1).ToString() + "count and current selection " + selectedCoefficientIndex.ToString());
            Debug.Log(coefficientsToManipulate[selectedCoefficientIndex].transform.parent.name);
            clearColors(coefficientsToManipulate);
            coefficientsToManipulate[selectedCoefficientIndex].transform.parent.gameObject.GetComponent<TextMeshPro>().color = Color.blue;
        }
    }

    public void Up()
    {
        if(selectedCoefficientIndex == 0)
        {
            amplitude += 1f;
            coefficientsToManipulate[selectedCoefficientIndex].transform.parent.GetComponent<TextMeshPro>().text = amplitude.ToString();
        }

        if(selectedCoefficientIndex == 1)
        {
            B += 1f;
            coefficientsToManipulate[selectedCoefficientIndex].transform.parent.GetComponent<TextMeshPro>().text = B.ToString();
        }

        if (selectedCoefficientIndex == 2)
        {
            yShift += 1f;
            coefficientsToManipulate[selectedCoefficientIndex].transform.parent.GetComponent<TextMeshPro>().text = yShift.ToString();
        }

    }

    public void Down()
    {
        if (selectedCoefficientIndex == 0)
        {
            amplitude -= 1f;
            coefficientsToManipulate[selectedCoefficientIndex].transform.parent.GetComponent<TextMeshPro>().text = amplitude.ToString();
        }

        if (selectedCoefficientIndex == 1)
        {
            B -= 1f;
            coefficientsToManipulate[selectedCoefficientIndex].transform.parent.GetComponent<TextMeshPro>().text = B.ToString();
        }

        if (selectedCoefficientIndex == 2)
        {
            yShift -= 1f;
            coefficientsToManipulate[selectedCoefficientIndex].transform.parent.GetComponent<TextMeshPro>().text = yShift.ToString();
        }

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

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right();



        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left();
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Up();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Down();
        }















    }
}
