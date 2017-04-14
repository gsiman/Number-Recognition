using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//click on a pixel to turn it on and change the neurons' image matrix
public class Pixel : MonoBehaviour {
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
    public int x;
    public int y;

    void Start()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
    }

	void OnMouseDown()
    {
        if (lit)
        {
            lit = false;            
            GetComponent<Renderer>().material = off;            
        } else
        {
            lit = true;
            GetComponent<Renderer>().material = on;
        }
        neuron0.image[x, y] = lit;
        neuron1.image[x, y] = lit;
        neuron2.image[x, y] = lit;
        neuron3.image[x, y] = lit;
        neuron4.image[x, y] = lit;
        neuron5.image[x, y] = lit;
        neuron6.image[x, y] = lit;
        neuron7.image[x, y] = lit;
        neuron8.image[x, y] = lit;
        neuron9.image[x, y] = lit;
    }
}
