using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//manage the weights of each neuron
public class GameControl : MonoBehaviour
{
    public static GameControl control;
    //neurons
    public NeuronHidden[] neurons = new NeuronHidden[10];
    //weights and corresponding thresholds
    public float[,,] weights = new float[10,3,5];
    public float[] thresholds = new float[10];
    //margin above threshold
    public float[] margins = new float[10];
    //individual neuron status
    public bool[] updates = new bool[10];

    public string answer;

    //called first, load the trained file
    void Awake()
    {
        print(Application.dataPath);
        control = this;
        Load();
    }

    //runtime loop
    void Update()
    {
        //if all the neurons are done processing        
        bool updateMain = true;
        for (int i=0; i<10; i++)
        {            
            updateMain = updates[i] && updateMain;
        }
        if (updateMain)
        {
            //checks for the number that is most over its threshold
            int max = Array.IndexOf(margins, Mathf.Max(margins));            
            answer = "it's a " + max;
            neurons[max].Feedback();            
            print(answer);

            //reset the margins and neuron status
            for (int i = 0; i < 10; i++)
            {
                margins[i] = 0;
                updates[i] = false;
            }
        }        
    }

    //if clicked on save the training file
    public void OnMouseDown()
    {
        Save();
    }

    //save function
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        FileStream file = File.Create(Application.dataPath + "/playerInfo.dat");        
        //list of units and equipment info
        PlayerData data = new PlayerData();
        data.weights = weights;
        data.thresholds = thresholds;
        bf.Serialize(file, data);
        file.Close();
        print("Save Successful");
    }

    //load function
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
            weights = data.weights;
            thresholds = data.thresholds;
            print("Load Successful");
        }
        else
        {
            print("No file to load");
        }
    }

    //print to screen
    void OnGUI()
    {
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
    //weights and corresponding thresholds
    public float[,,] weights = new float[3, 5, 10];
    public float[] thresholds = new float[10];
}
