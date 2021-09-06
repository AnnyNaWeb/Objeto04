using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandColorCS : MonoBehaviour
{
    struct Cube
    {
        public Vector3 position;
        public Color color;
    }


    public ComputeShader computeShader;
    GameObject[] gameObjects;
    Cube[] data;
    public GameObject modelPref;
    public int iteractions = 50;

    [Range(1, 50)]
    public int count = 2;

    [Range(1, 10)]
    public float velocidadeMin = 1;

    [Range(1, 10)]
    public float velocidadeMax = 3;

    [Range(1, 50)]
    public int massa = 1;

    public bool cpu = false;
    public bool gpu = false;
    public static bool playtime = false;

    public static float totalTime;
    public Text tempo;
    private float minutes;
    private static float seconds = 60;
    private static float miliseconds = 1000;

    private void createCube()
    {
        data = new Cube[count * count];
        gameObjects = new GameObject[count * count];

        for (int i = 0; i < count; i++)
        {
            float offsetX = (-count / 2 + i);

            for (int j = 0; j < count; j++)
            {
                float offsetY = (-count / 2 + j);

                //Color _color = Random.ColorHSV();

                GameObject go = GameObject.Instantiate(modelPref, new Vector3(offsetX * 0.7f, 3, offsetY * 0.7f), Quaternion.identity);
               // go.GetComponent<MeshRenderer>().material.SetColor("_Color", _color);
                go.GetComponent<Rigidbody>().drag = Random.Range(velocidadeMin, velocidadeMax);
                go.GetComponent<Rigidbody>().mass = Random.Range(1, massa);

                gameObjects[i * count + j] = go;

                data[i * count + j] = new Cube();
                data[i * count + j].position = go.transform.position;
                //data[i * count + j].color = _color;
            }
        }
    }
}

    
