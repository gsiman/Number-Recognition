using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hidden layers of the neural network
public class NeuronHidden : MonoBehaviour {
    public bool[,] image = new bool[8, 10];             //pixel matrix

    private int[] topLeft = new int[2];                 //top left corner of image drawn
    private int[] bottomRight = new int[2];             //bottom right corner of image drawn
    private int[] resolution = new int[2];              //resolution of image drawn
    
    public float[,] weights;                            //weights applied to image values
                                                            //this matrix is created from weight gradient
    public float output;                                //output of neuron
    public int number;                                  //number neuron is representing
    public float threshold;                             //required number to be above for output to be positive
    public bool signal;                                 //is output positive
    public bool answer;                                 //was this the answer? (only used for calibration)
    public bool calibrating;                            //calibration/training mode toggle

    private float adjustment = 0.001f;                  //adjustment for weights during calibration
    private float thresholdAdj = 0.01f;                 //adjustment for threshold during calibration

    private float xRatio;                               //1/(x dir image size)
    private float yRatio;                               //1/(y dir image size)

    //the other neurons
    public NeuronHidden[] otherNeurons = new NeuronHidden[9];

    public Material off;
    public Material on;

    void Start()
    {
        //set initial values of corners to the opposite 
        topLeft[0] = 8;         //x val
        topLeft[1] = 0;         //y val
        bottomRight[0] = 0;     //x val
        bottomRight[1] = 10;    //y val

        GetComponent<Renderer>().material = off;
    }

    //if you were selected as the answer run first and then the other neurons
    //user selection is only important for training
    public void OnMouseDown()
    {
        //check to see that at least one pixel is turned on
        bool imageCheck = false;
        for (int x=0; x<8; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                imageCheck = imageCheck || image[x, y]; 
            }
        }

        if (imageCheck)
        {
            //set answer to true because this was chosen
            answer = true;
            Output();
            answer = false;

            for (int i = 0; i < 9; i++)
            {
                otherNeurons[i].Output();
            }
        } else
        {
            GameControl.control.status = "You must turn on at least 1 pixel";
        }                 
    }

    public void Output()
    {
        //set initial values of corners to the opposite 
        topLeft[0] = 8;         //x val
        topLeft[1] = 0;         //y val
        bottomRight[0] = 0;     //x val
        bottomRight[1] = 10;    //y val

        GetComponent<Renderer>().material = off;
        output = 0;
        //scan for top left and bottom right corners of image
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (image[x, y])
                {
                    if (x < topLeft[0])
                    {
                        topLeft[0] = x;
                    }
                    if (y > topLeft[1])
                    {
                        topLeft[1] = y;
                    }
                    if (x > bottomRight[0])
                    {
                        bottomRight[0] = x;
                    }
                    if (y < bottomRight[1])
                    {
                        bottomRight[1] = y;
                    }
                }
            }
        }
        //print(topLeft[0] + " " + topLeft[1]);
        //print(bottomRight[0] + " " + bottomRight[1]);
        //set resolution of the image, account for 0 by adding 1
        resolution[0] = bottomRight[0] - topLeft[0] + 1;
        resolution[1] = topLeft[1] - bottomRight[1] + 1;
        //print(resolution[0] + " " + resolution[1]);

        //weights used to calculate the output
        weights = new float[resolution[0], resolution[1]];
        //the percentage size of one pixel to the rest of the axis size
        xRatio = 1f / (resolution[0] + 1f) * 100f;
        yRatio = 1f / (resolution[1] + 1f) * 100f;
        //print(xRatio);
        //print(yRatio);
        //loop through x
        for (float i = 0; i < resolution[0]; i++)
        {
            //loop through y
            for (float j = 0; j < resolution[1]; j++)
            {
                //scale the weights from (3,5) to the size of the image drawn
                int q = (int)Mathf.Round((i + 1) * xRatio / 100 * 4) - 1;   //decide what original weight gradient value to use for x 
                int r = (int)Mathf.Round((j + 1) * yRatio / 100 * 6) - 1;   //decide what original weight gradient value to use for y
                float ySpot = yRatio * (j + 1);     //location of the pixel in the y dir on 100 point scale
                float xSpot = xRatio * (i + 1);     //location of the pixel in the y dir on 100 point scale
                //print("i,j = " + i + " " + j);
                //print("q,r = " + q + " " + r);
                //interpolation along y axis
                if (yRatio * (j + 1) < 16.7)
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, q, 0] + weights[(int)i, (int)j];
                }
                else if (ySpot < 33.4)
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, q, 0] * (33.3f - ySpot) / 16.7f + GameControl.control.weights[number, q, 1] * (ySpot - 16.7f) / 16.7f + weights[(int)i, (int)j];
                }
                else if (ySpot < 50.1)
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, q, 1] * (50f - ySpot) / 16.7f + GameControl.control.weights[number, q, 2] * (ySpot - 33.3f) / 16.7f + weights[(int)i, (int)j];
                }
                else if (ySpot < 66.7)
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, q, 2] * (66.7f - ySpot) / 16.7f + GameControl.control.weights[number, q, 3] * (ySpot - 50f) / 16.7f + weights[(int)i, (int)j];
                }
                else if (ySpot < 83.4)
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, q, 3] * (83.3f - ySpot) / 16.7f + GameControl.control.weights[number, q, 4] * (ySpot - 66.7f) / 16.7f + weights[(int)i, (int)j];
                }
                else
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, q, 4] + weights[(int)i, (int)j];
                }
                //interpolation along x axis
                if (xRatio * (i + 1) < 25)
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, 0, r] + weights[(int)i, (int)j];
                }
                else if (xSpot < 50)
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, 0, r] * (50f - xSpot) / 25f + GameControl.control.weights[number, 1, r] * (xSpot - 25f) / 25f + weights[(int)i, (int)j];
                }
                else if (xSpot < 75)
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, 1, r] * (75f - xSpot) / 25f + GameControl.control.weights[number, 2, r] * (xSpot - 50f) / 25f + weights[(int)i, (int)j];
                }
                else
                {
                    weights[(int)i, (int)j] = GameControl.control.weights[number, 2, r] + weights[(int)i, (int)j];
                }                
            }
        }

        //output calculation, the sume of the weights with the pixel turned on
        //output = SUM(w_xy * input_xy)
        //input_xy either equals 0,off or 1,on
        for (int i = topLeft[0]; i < bottomRight[0] + 1; i++)
        {
            for (int j = bottomRight[1]; j < topLeft[1] + 1; j++)
            {
                if (image[i, j])
                {
                    output += weights[i - topLeft[0], j - bottomRight[1]];
                }
            }
        }
        //check to see that the output is greater than the threshold
        output -= threshold;
        if (output >= 0)
        {
            signal = true;
            //record the amount that the output is greater by the threshold by
            GameControl.control.margins[number] = output;
        }
        //the threshold was not reached
        else
        {
            signal = false;
        }
        //let main controller know that this neuron is done with calculations
        GameControl.control.updates[number] = true;

        //if calibrating
        if (calibrating)
        {
            //if this is the correct answer
            if (answer)
            {
                //increase weights saying this was the number
                // or decrease weights saying this was not the number
                for (float i = 0; i < resolution[0]; i++)
                {
                    for (float j = 0; j < resolution[1]; j++)
                    {
                        int q = (int)Mathf.Round((i + 1) * xRatio / 100 * 4) - 1;
                        int r = (int)Mathf.Round((j + 1) * yRatio / 100 * 6) - 1;
                        float ySpot = yRatio * (j + 1);
                        float xSpot = xRatio * (i + 1);
                        //interpolation along y axis
                        if (ySpot < 16.7)
                        {
                            PositiveAdjustment(q, 0);
                        }
                        else if (ySpot < 33.4)
                        {
                            PositiveAdjustment(q, 0);
                            PositiveAdjustment(q, 1);
                        }
                        else if (ySpot < 50.1)
                        {
                            PositiveAdjustment(q, 1);
                            PositiveAdjustment(q, 2);
                        }
                        else if (ySpot < 66.7)
                        {
                            PositiveAdjustment(q, 2);
                            PositiveAdjustment(q, 3);
                        }
                        else if (ySpot < 83.4)
                        {
                            PositiveAdjustment(q, 3);
                            PositiveAdjustment(q, 4);
                        }
                        else
                        {
                            PositiveAdjustment(q, 4);
                        }
                        //interpolation along x axis
                        if (xSpot < 25)
                        {
                            PositiveAdjustment(0, r);
                        }
                        else if (xSpot < 50)
                        {
                            PositiveAdjustment(0, r);
                            PositiveAdjustment(1, r);
                        }
                        else if (xSpot < 75)
                        {
                            PositiveAdjustment(1, r);
                            PositiveAdjustment(2, r);
                        }
                        else
                        {
                            PositiveAdjustment(2, r);
                        }
                    }
                }
                if (signal)
                {
                    //good decision
                    //threshold += adjustment;
                }
                else
                {
                    //if this was the answer but the threshold was not passed
                    //make it easier to say it is this number by decreasing the threshold
                    threshold -= thresholdAdj;
                }
            }            
        }
    }

    //called if this was the best answer, this feedback will always be negative
    //if the AI was correct in guessing the number this is called assuming that the answer is wrong
        //this cancels all the adjustments made at the end of Output() for the same neuron
        //by cancelling the previous adjustments when the AI is already outputting the correct number,
        //it is preventing the neural network from creating a bias towards a single output
    public void Feedback()
    {
        GetComponent<Renderer>().material = on;
        if (calibrating)
        {
            for (float i = 0; i < resolution[0]; i++)
            {
                for (float j = 0; j < resolution[1]; j++)
                {
                    int q = (int)Mathf.Round((i + 1) * xRatio / 100 * 4) - 1;
                    int r = (int)Mathf.Round((j + 1) * yRatio / 100 * 6) - 1;
                    float ySpot = yRatio * (j + 1);
                    float xSpot = xRatio * (i + 1);
                    //interpolation along y axis
                    if (ySpot < 16.7)
                    {
                        NegativeAdjustment(q, 0);
                    }
                    else if (ySpot < 33.4)
                    {
                        NegativeAdjustment(q, 0);
                        NegativeAdjustment(q, 1);
                    }
                    else if (ySpot < 50.1)
                    {
                        NegativeAdjustment(q, 1);
                        NegativeAdjustment(q, 2);
                    }
                    else if (ySpot < 66.7)
                    {
                        NegativeAdjustment(q, 2);
                        NegativeAdjustment(q, 3);
                    }
                    else if (ySpot < 83.4)
                    {
                        NegativeAdjustment(q, 3);
                        NegativeAdjustment(q, 4);
                    }
                    else
                    {
                        NegativeAdjustment(q, 4);
                    }
                    //interpolation along x axis
                    if (xRatio * (i + 1) < 25)
                    {
                        NegativeAdjustment(0, r);
                    }
                    else if (xSpot < 50)
                    {
                        NegativeAdjustment(0, r);
                        NegativeAdjustment(1, r);
                    }
                    else if (xSpot < 75)
                    {
                        NegativeAdjustment(1, r);
                        NegativeAdjustment(2, r);
                    }
                    else
                    {
                        NegativeAdjustment(2, r);
                    }
                }
            }
            if (signal)
            {
                //make it more difficult to say it is this number to avoid false positives
                threshold += thresholdAdj;
            }
            else
            {
                //good decision, no action is needed
                //threshold -= adjustment;
            }            
        }
    }

    //if the orginial weightGradient value was use to turn the neuron on while it was the correct answer
    void PositiveAdjustment(int x, int y)
    {
        //print("x,y = " + x + "," + y);

        if (GameControl.control.weights[number, x, y] > 0)
        {
            GameControl.control.weights[number, x, y] += adjustment;
        }
        if (GameControl.control.weights[number, x, y] < 0)
        {
            GameControl.control.weights[number, x, y] -= adjustment;
        }
    }
    //if the orginial weightGradient value was use to turn the neuron on while it was the wrong answer
    void NegativeAdjustment(int x, int y)
    {
        if (GameControl.control.weights[number, x, y] > 0)
        {
            GameControl.control.weights[number, x, y] -= adjustment;
        }
        if (GameControl.control.weights[number, x, y] < 0)
        {
            GameControl.control.weights[number, x, y] += adjustment;
        }
    }
}
