using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//resets the weights to default values before training upon being clicked
public class Reset : MonoBehaviour {
    public NeuronHidden[] neurons = new NeuronHidden[10];

    void OnMouseDown()
    {        
        GameControl.control.weights = new float[10, 3, 5] { { {1, 1, 1, 1, 1},
                                    {1, -1, -1, -1, 1},
                                    {1, 1, 1, 1, 1}},

                                    { {1, 1, 1, 1, 1},
                                    {1, 1, 1, 1, 1},
                                    {1, 1, 1, 1, 1}},

                                    { {1, 1, 1, -1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, -1, 1, 1, 1}},

                                    { {1, -1, 1, -1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, 1, 1}},

                                    { {-1, -1, 1, 1, 1},
                                    {-1, -1, 1, -1, -1},
                                    {1, 1, 1, 1, 1}},

                                    { {1, -1, 1, 1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, -1, 1}},

                                    { {1, 1, 1, 1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, -1, 1}},

                                    { {-1, -1, -1, -1, 1},
                                    {1, 1, 1, -1, 1},
                                    {-1, -1, -1, 1, 1}},

                                    { {1, 1, 1, 1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, 1, 1}},

                                    { {1, -1, 1, 1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, 1, 1}}};

        for (int i=0; i<10; i++) {
            GameControl.control.thresholds[i] = neurons[i].threshold = 0;
        }
        GameControl.control.status = "Weights set to default values";
        print(GameControl.control.status);
    }
}
