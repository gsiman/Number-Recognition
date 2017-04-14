using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//save the weights of each neuron
public class GameControl : MonoBehaviour
{
    //PlayerData data = new PlayerData ();

    public static GameControl control;
    //list of units and equipment info
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

    public float[,] weight0 = new float[3,5];
    public float threshold0;
    public float[,] weight1 = new float[3, 5];
    public float threshold1;
    public float[,] weight2 = new float[3, 5];
    public float threshold2;
    public float[,] weight3 = new float[3, 5];
    public float threshold3;
    public float[,] weight4 = new float[3, 5];
    public float threshold4;
    public float[,] weight5 = new float[3, 5];
    public float threshold5;
    public float[,] weight6 = new float[3, 5];
    public float threshold6;
    public float[,] weight7 = new float[3, 5];
    public float threshold7;
    public float[,] weight8 = new float[3, 5];
    public float threshold8;
    public float[,] weight9 = new float[3, 5];
    public float threshold9;

    public float margin0;
    public float margin1;
    public float margin2;
    public float margin3;
    public float margin4;
    public float margin5;
    public float margin6;
    public float margin7;
    public float margin8;
    public float margin9;

    public bool update0;
    public bool update1;
    public bool update2;
    public bool update3;
    public bool update4;
    public bool update5;
    public bool update6;
    public bool update7;
    public bool update8;
    public bool update9;

    public string answer;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
        //print(Application.dataPath);
        //print(Application.persistentDataPath);
        Load();      
    }

    void Update()
    {
        //if all the neurons are done processing
        if (update0 && update1 && update2 && update3 && update4 && update5 && update6 && update7 && update8 && update9)
        {
            //checks for the number that is most over its threshold
            string max = Mathf.Max(margin0, margin1, margin2, margin3, margin4, margin5, margin6, margin7, margin8, margin9).ToString();
            if (max == margin0.ToString())
            {
                print("it's a 0");
                answer = "it's a 0";
                neuron0.Feedback();
            } else if (max == margin1.ToString())
            {
                print("it's a 1");
                answer = "it's a 1";
                neuron1.Feedback();
            } else if (max == margin2.ToString())
            {
                print("it's a 2");
                answer = "it's a 2";
                neuron2.Feedback();
            } else if (max == margin3.ToString())
            {
                print("it's a 3");
                answer = "it's a 3";
                neuron3.Feedback();
            } else if (max == margin4.ToString())
            {
                print("it's a 4");
                answer = "it's a 4";
                neuron4.Feedback();
            } else if (max == margin5.ToString())
            {
                print("it's a 5");
                answer = "it's a 5";
                neuron5.Feedback();
            } else if (max == margin6.ToString())
            {
                print("it's a 6");
                answer = "it's a 6";
                neuron6.Feedback();
            } else if (max == margin7.ToString())
            {
                print("it's a 7");
                answer = "it's a 7";
                neuron7.Feedback();
            } else if (max == margin8.ToString())
            {
                print("it's a 8");
                answer = "it's a 8";
                neuron8.Feedback();
            } else if (max == margin9.ToString())
            {
                print("it's a 9");
                answer = "it's a 9";
                neuron9.Feedback();
            }

            margin0 = 0;
            margin1 = 0;
            margin2 = 0;
            margin3 = 0;
            margin4 = 0;
            margin5 = 0;
            margin6 = 0;
            margin7 = 0;
            margin8 = 0;
            margin9 = 0;

            update0 = false;
            update1 = false;
            update2 = false;
            update3 = false;
            update4 = false;
            update5 = false;
            update6 = false;
            update7 = false;
            update8 = false;
            update9 = false;
        }        
    }

    public void OnMouseDown()
    {
        Save();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        FileStream file = File.Create(Application.dataPath + "/playerInfo.dat");        
        //list of units and equipment info
        PlayerData data = new PlayerData();
        data.weight0 = weight0;
        data.weight1 = weight1;
        data.weight2 = weight2;
        data.weight3 = weight3;
        data.weight4 = weight4;
        data.weight5 = weight5;
        data.weight6 = weight6;
        data.weight7 = weight7;
        data.weight8 = weight8;
        data.weight9 = weight9;
        data.threshold0 = threshold0;
        data.threshold1 = threshold1;
        data.threshold2 = threshold2;
        data.threshold3 = threshold3;
        data.threshold4 = threshold4;
        data.threshold5 = threshold5;
        data.threshold6 = threshold6;
        data.threshold7 = threshold7;
        data.threshold8 = threshold8;
        data.threshold9 = threshold9;
        bf.Serialize(file, data);
        file.Close();
        print("Save Successful");
    }

    public void Load()
    {
        //if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        if (File.Exists(Application.dataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            FileStream file = File.Open(Application.dataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            //list of units and equipment info
            weight0 = data.weight0;
            weight1 = data.weight1;
            weight2 = data.weight2;
            weight3 = data.weight3;
            weight4 = data.weight4;
            weight5 = data.weight5;
            weight6 = data.weight6;
            weight7 = data.weight7;
            weight8 = data.weight8;
            weight9 = data.weight9;
            threshold0 = data.threshold0;
            threshold1 = data.threshold1;
            threshold2 = data.threshold2;
            threshold3 = data.threshold3;
            threshold4 = data.threshold4;
            threshold5 = data.threshold5;
            threshold6 = data.threshold6;
            threshold7 = data.threshold7;
            threshold8 = data.threshold8;
            threshold9 = data.threshold9;
            print("Load Successful");
        }
        else
        {
            print("No file to load");
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 200), "Only works with a 5x3(y x) matrix");
        GUI.Label(new Rect(0, 15, 200, 200), "Click on 'pixels' to turn on");
        GUI.Label(new Rect(0, 30, 300, 200), "Click on any of the red blocks below");
        GUI.Label(new Rect(0, 45, 200, 200), "It will not effect results");
        GUI.Label(new Rect(0, 60, 200, 200), "Answer will appear below:");
        GUI.Label(new Rect(0, 75, 200, 200), answer);
    }
}

[Serializable]
class PlayerData
{
    //list of units and equipment info
    public float[,] weight0 = new float[3, 5];
    public float threshold0;
    public float[,] weight1 = new float[3, 5];
    public float threshold1;
    public float[,] weight2 = new float[3, 5];
    public float threshold2;
    public float[,] weight3 = new float[3, 5];
    public float threshold3;
    public float[,] weight4 = new float[3, 5];
    public float threshold4;
    public float[,] weight5 = new float[3, 5];
    public float threshold5;
    public float[,] weight6 = new float[3, 5];
    public float threshold6;
    public float[,] weight7 = new float[3, 5];
    public float threshold7;
    public float[,] weight8 = new float[3, 5];
    public float threshold8;
    public float[,] weight9 = new float[3, 5];
    public float threshold9;
}
