using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RColorCS : MonoBehaviour
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

     void OnGUI(){
        if(data == null){//so usa o create cube se o data tiver vazio
            if(GUI.Button(new Rect (0,0,100,50), "Create")){
              createCube();
            }
        }
       if(data != null){ //troca cor em CPU
           if(GUI.Button(new Rect(110, 0, 100, 50), "Random CPU")){
               for (int k = 0; k < iteractions; k++){
                    for(int i = 0; i< gameObjects.Length; i++){
                        gameObjects[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV()); 
                    }
               }
               
           }
       }
       if(data != null){
           if(GUI.Button(new Rect(220, 0, 100, 50), "Random GPU")){
               int totalSize = 4*sizeof(float) + 3*sizeof(float);
               ComputeBuffer computeBuffer = new ComputeBuffer(data.Length, totalSize);//alocamos memoria
               computeBuffer.SetData(data);//jogando dados da cpu pra gpu
//seta parametros
                computeShader.SetBuffer(0, "cubes", computeBuffer);
                computeShader.SetInt("iteractions", iteractions);
                //executa kernel
                computeShader.Dispatch(0, data.Length/10, 1, 1);
                //copiar dados de volta pra CPU
                computeBuffer.GetData(data);
                //pegar e setar cores no objeto
                for(int i = 0; i< gameObjects.Length; i++){
                    gameObjects[i].GetComponent<MeshRenderer>().material.SetColor("_Color",data[i].color); 
                }

                computeBuffer.Dispose();

           }
       }
    }

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
                
                GameObject go = GameObject.Instantiate(modelPref, new Vector3(offsetX *0.7f, 0, offsetY*0.7f), Quaternion.identity);
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
