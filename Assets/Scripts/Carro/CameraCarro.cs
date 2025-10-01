using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCarro : MonoBehaviour
{
    public Transform carro;
    public float sensibilidadeMouse = 300f;
    public float distancia;
    public float altura;

    float rotacaoY = 0f;
    float rotacaoX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse * Time.deltaTime;

        rotacaoY += mouseX;
        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -30f, 60f);

        Quaternion rotacao = Quaternion.Euler(rotacaoX, rotacaoY, 0);
        Vector3 posicaoAlvo = carro.position - rotacao * Vector3.forward * distancia + Vector3.up * altura;

        transform.position = posicaoAlvo;
        transform.LookAt(carro.position + Vector3.up * altura * 0.5f);
    }
}
