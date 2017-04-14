using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if on, allow each of the neuron's to calibrate their weights
public class CalibratingSwitch : MonoBehaviour
{
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

    public bool lit;

    public Material off;
    public Material on;

    void OnMouseDown()
    {
        if (lit)
        {
            lit = false;
            GetComponent<Renderer>().material = off;
            neuron0.calibrating = false;
            neuron1.calibrating = false;
            neuron2.calibrating = false;
            neuron3.calibrating = false;
            neuron4.calibrating = false;
            neuron5.calibrating = false;
            neuron6.calibrating = false;
            neuron7.calibrating = false;
            neuron8.calibrating = false;
            neuron9.calibrating = false;
        }
        else
        {
            lit = true;
            GetComponent<Renderer>().material = on;
            neuron0.calibrating = true;
            neuron1.calibrating = true;
            neuron2.calibrating = true;
            neuron3.calibrating = true;
            neuron4.calibrating = true;
            neuron5.calibrating = true;
            neuron6.calibrating = true;
            neuron7.calibrating = true;
            neuron8.calibrating = true;
            neuron9.calibrating = true;
        }        
    }
}
