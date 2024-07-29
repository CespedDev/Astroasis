using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;          // Velocidad de movimiento de la c�psula
    [SerializeField] private float rotationSpeed = 25.0f; // Velocidad de rotaci�n en grados por segundo
    [SerializeField] private float fovAngle = 90.0f;      // �ngulo del campo de visi�n simulado en grados
    [SerializeField] private Transform cameraTransform;   // Referencia al Transform de la c�mara

    private float currentAngle = 0.0f;
    private float direction = 1.0f;

    void Start()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("No se ha asignado el Transform de la c�mara.");
        }
    }

    void FixedUpdate()
    {
        // Mover la c�psula hacia adelante en la direcci�n especificada
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Calcular el �ngulo de rotaci�n en el eje horizontal
        currentAngle += rotationSpeed * direction * Time.deltaTime;

        // Limitar el �ngulo de rotaci�n al campo de visi�n especificado
        if (currentAngle > fovAngle / 2.0f)
        {
            currentAngle = fovAngle / 2.0f;
            direction = -1.0f;
        }
        else if (currentAngle < -fovAngle / 2.0f)
        {
            currentAngle = -fovAngle / 2.0f;
            direction = 1.0f;
        }

        // Aplicar la rotaci�n en el eje horizontal a la c�mara
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(0.0f, currentAngle, 0.0f);
        }
    }
}
