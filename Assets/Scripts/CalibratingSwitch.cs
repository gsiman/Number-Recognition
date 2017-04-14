using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if on, allow each of the neuron's to calibrate their weights
public class CalibratingSwitch : MonoBehaviour
{
    public NeuronHidden[] neurons = new NeuronHidden[10];

    public bool lit;

    public Material off;
    public Material on;

    void OnMouseDown()
    {
        if (lit)
        {
            lit = false;
            GetComponent<Renderer>().material = off;
            for (int i = 0; i < 10; i++)
            {
                neurons[i].calibrating = false;
            }                
        }
        else
        {
            lit = true;
            GetComponent<Renderer>().material = on;
            for (int i = 0; i < 10; i++)
            {
                neurons[i].calibrating = true;
            }
        }        
    }
}
