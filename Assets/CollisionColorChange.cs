using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionColorChange : MonoBehaviour
{
    struct Cube
    {
        public Vector3 position;
        public Color color;
    }

    public ComputeShader computeShader;
    int iteractions = 50;
    Cube[] data;
    int count;
    bool cpu = false;
    bool gpu = false;
    public int countobj = 0;

    void Update()
    {
        count = GameObject.FindWithTag("construct").GetComponent<RandColorCS>().count;
        cpu = GameObject.FindWithTag("construct").GetComponent<RandColorCS>().cpu;
        gpu = GameObject.FindWithTag("construct").GetComponent<RandColorCS>().gpu;
    }

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (cpu == true)
        {
            Debug.Log(RandColorCS.totalTime);

            for (int k = 0; k < iteractions; k++)
            {
                for (int i = 0; i < count; i++)
                {
                    Color _color = Random.ColorHSV();
                    this.GetComponent<MeshRenderer>().material.SetColor("_Color", _color);
                    countobj++;
                    //if (countobj == count * iteractions)
                    // RandColorCS.playtime = false;
                }
            }
        }