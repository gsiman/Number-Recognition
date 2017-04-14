using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//resets the weights to default values before training upon being clicked
public class Reset : MonoBehaviour {
    public NeuronHidden neuron0;
    public NeuronHidden neuron1;
    public NeuronHidden neuron2;
    public NeuronHidden neuron3;
    public NeuronHidden neuron4;
    public NeuronHidden neuron5;
    public NeuronHidden neuron6;
    public NeuronHidden neuron7;
    public NeuronHidden neuron8;
    public NeuronHidden neuron9;

    void OnMouseDown()
    {
        GameControl.control.weight0 = new float[3, 5] { {1, 1, 1, 1, 1},
                                    {1, -1, -1, -1, 1},
                                    {1, 1, 1, 1, 1}};
        GameControl.control.weight1 = new float[3, 5] { {1, 1, 1, 1, 1},
                                    {1, 1, 1, 1, 1},
                                    {1, 1, 1, 1, 1}};
        GameControl.control.weight2 = new float[3, 5] { {1, 1, 1, -1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, -1, 1, 1, 1}};
        GameControl.control.weight3 = new float[3, 5] { {1, -1, 1, -1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, 1, 1}};
        GameControl.control.weight4 = new float[3, 5] { {-1, -1, 1, 1, 1},
                                    {-1, -1, 1, -1, -1},
                                    {1, 1, 1, 1, 1}};
        GameControl.control.weight5 = new float[3, 5] { {1, -1, 1, 1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, -1, 1}};
        GameControl.control.weight6 = new float[3, 5] { {1, 1, 1, 1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, -1, 1}};
        GameControl.control.weight7 = new float[3, 5] { {-1, -1, -1, -1, 1},
                                    {1, 1, 1, -1, 1},
                                    {-1, -1, -1, 1, 1}};
        GameControl.control.weight8 = new float[3, 5] { {1, 1, 1, 1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, 1, 1}};
        GameControl.control.weight9 = new float[3, 5] { {1, -1, 1, 1, 1},
                                    {1, -1, 1, -1, 1},
                                    {1, 1, 1, 1, 1}};

        neuron0.weightGradient = GameControl.control.weight0;
        neuron1.weightGradient = GameControl.control.weight1;
        neuron2.weightGradient = GameControl.control.weight2;
        neuron3.weightGradient = GameControl.control.weight3;
        neuron4.weightGradient = GameControl.control.weight4;
        neuron5.weightGradient = GameControl.control.weight5;
        neuron6.weightGradient = GameControl.control.weight6;
        neuron7.weightGradient = GameControl.control.weight7;
        neuron8.weightGradient = GameControl.control.weight8;
        neuron9.weightGradient = GameControl.control.weight9;
                
        GameControl.control.threshold0 = neuron0.threshold = 0;
        GameControl.control.threshold1 = neuron1.threshold = 0;
        GameControl.control.threshold2 = neuron2.threshold = 0;
        GameControl.control.threshold3 = neuron3.threshold = 0;
        GameControl.control.threshold4 = neuron4.threshold = 0;
        GameControl.control.threshold5 = neuron5.threshold = 0;
        GameControl.control.threshold6 = neuron6.threshold = 0;
        GameControl.control.threshold7 = neuron7.threshold = 0;
        GameControl.control.threshold8 = neuron8.threshold = 0;
        GameControl.control.threshold9 = neuron9.threshold = 0;
    }
}
