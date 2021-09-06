using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandColorCS : MonoBehaviour
{
    struct Cube{
        public Vector3 position;
        public Color color;
    }

    public ComputeShader computeShader;
    public int iteractions = 50;
    public int count= 100;
    GameObject[] gameObjects;
    Cube[] data; 
    public GameObject modelPref;
 
    //botão pra criar o cubo pra executar, pra configurar as cores aleatorias em CPU e GPU a medida que formos clicando
   //tudo em cpu
    void OnGUI(){
        if(data == null){//so usa o create cube se o data tiver vazio
            if(GUI.Button(new Rect (0,0,100,50), "Create")){
              createCube();
            }
        }
       
    }
//tudo em CPU
    private void createCube(){
        //alocar memoria pra esse cubo
        data = new Cube[count *count]; //100 x 100 cubos
        gameObjects = new GameObject[count * count];

        for (int i=0; i<count; i++){
            float offsetX = (-count/2+i);
            //dimensao y
            for(int j =0; j < count; j++){
                //50 lado negativo e 50 lado positivo do x e mesma coisa pro y, vamos centralizar sabendo que o centro é o 0, dar offset pra começar considerando cubo unitario com 10 cubos centralizados a gnt tem que começar no -5 e vai até o 5 pra centralizar tudo na tela
                float offsetY = (-count/2+j);
                Color _color = Random.ColorHSV(); //cor randomica
                
                GameObject go = GameObject.Instantiate(modelPref, new Vector3(offsetX* 2.2f, 0, offsetY* 2.2f), Quaternion.identity);
                go.GetComponent<MeshRenderer>().material.SetColor("_Color", _color); //shader tem o _ na frente

                //conversão de indice duas ou mais dimensoes para uma dimensao inferior ou superior no caso bidimensional pra unidimensional
                gameObjects[i*count + j] = go;
                data[i*count + j] = new Cube();
                data[i*count+j].position = go.transform.position;
                data[i*count+j].color = _color;
            }
        }


    }
}
