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

    
