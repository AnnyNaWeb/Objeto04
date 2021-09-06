using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTex : MonoBehaviour
{

    public ComputeShader computeShader; //só processa dados que estao na GPU
    public RenderTexture renderTexture; //area de memoria em GPU pra ser executada pelo kernel que criamos
    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 32); //32 é a profundidade em bits
        renderTexture.enableRandomWrite = true; //para a textura nao ser acessada fora de ordem, no pipeline grafico geralmente segue uma ordem de gravaçao, entao no kernel tem que habilitar pq nao é habilitada por padrao
        renderTexture.Create(); //criou area de memoria em GPU

        //setar parametros do cs
        computeShader.SetTexture(0, "Result", renderTexture);//kernel index 0 só tem um no arquivo cs
        computeShader.SetFloat("resolution", renderTexture.width); //resolucao
        //executar o kernel e divide a resoluçao pela quantidade de blocos
        computeShader.Dispatch(0, renderTexture.width/8,renderTexture.width/8,1);//criam mais blocos para preencher a imagem pra alem da resolucao configurada, multiplica por 8
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
