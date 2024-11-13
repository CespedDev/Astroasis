using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimController : MonoBehaviour
{
    //private float speed = 5.0f;          // Velocidad de movimiento de la cápsula
    private float rotationSpeed = 25.0f; // Velocidad de rotación en grados por segundo
    [SerializeField] private float fovAngle = 90.0f;      // Ángulo del campo de visión simulado en grados
    [SerializeField] private Transform cameraTransform;   // Referencia al Transform de la cámara

    private float targetAngle = 0.0f;
    private float smoothTime = 0.1f;
    private float currentVelocity = 0.0f;

    // Variables adicionales para la inclinación vertical
    private float targetVerticalAngle = 0.0f;
    private float currentVerticalAngle = 0.0f;
    private float verticalVelocity = 0.0f;
    [SerializeField] private float verticalAmplitude = 0.5f; // Amplitud del cabeceo

    private float currentAngle = 0.0f;
    private float direction = 1.0f;

    void Start()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("No se ha asignado el Transform de la cámara.");
        }
    }

    void FixedUpdate()
    {
        /*/ Mover la cápsula hacia adelante en la dirección especificada
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Calcular el ángulo de rotación en el eje horizontal
        //currentAngle += rotationSpeed * direction * Time.deltaTime;

        // Limitar el ángulo de rotación al campo de visión especificado
        //if (currentAngle > fovAngle / 2.0f)
        //{
        //    currentAngle = fovAngle / 2.0f;
        //    direction = -1.0f;
        //}
        //else if (currentAngle < -fovAngle / 2.0f)
        //{
        //    currentAngle = -fovAngle / 2.0f;
        //    direction = 1.0f;
        //}

        //// Aplicar la rotación en el eje horizontal a la cámara
        //if (cameraTransform != null)
        //{
        //    cameraTransform.localRotation = Quaternion.Euler(0.0f, currentAngle, 0.0f);
        //}*/

        // Calcular el ángulo de rotación en el eje horizontal
        targetAngle += rotationSpeed * direction * Time.deltaTime;

        // Limitar el ángulo de rotación al campo de visión especificado
        if (targetAngle > fovAngle / 2.0f)
        {
            targetAngle = fovAngle / 2.0f;
            direction = -1.0f;
        }
        else if (targetAngle < -fovAngle / 2.0f)
        {
            targetAngle = -fovAngle / 2.0f;
            direction = 1.0f;
        }

        // Suavizar la rotación usando interpolación
        currentAngle = Mathf.SmoothDamp(currentAngle, targetAngle, ref currentVelocity, smoothTime);

        // Calcular el ángulo de inclinación vertical (cabeceo)
        targetVerticalAngle = Mathf.Sin(Time.time * rotationSpeed) * verticalAmplitude;
        currentVerticalAngle = Mathf.SmoothDamp(currentVerticalAngle, targetVerticalAngle, ref verticalVelocity, smoothTime);

        // Aplicar la rotación en el eje horizontal y la inclinación vertical a la cámara
        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(currentVerticalAngle, currentAngle, 0.0f);
        }
    }
}
