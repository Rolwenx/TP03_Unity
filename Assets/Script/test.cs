using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player; // Référence au joueur

    [Header("Camera Settings")]
    public float distance = 5f; // Distance initiale de la caméra au joueur
    public float zoomSpeed = 2f; // Vitesse de zoom/dézoom
    public float minDistance = 2f; // Distance minimale de la caméra
    public float maxDistance = 10f; // Distance maximale de la caméra
    public float rotationSpeed = 5f; // Vitesse de rotation de la caméra
    public float verticalRotationSpeed = 2f; // Vitesse de rotation verticale
    public float minVerticalAngle = -20f; // Angle vertical minimum
    public float maxVerticalAngle = 80f; // Angle vertical maximum

    private float currentDistance;
    private float currentVerticalAngle = 0f;
    private float currentHorizontalAngle = 0f;
    
    void Start()
    {
        currentDistance = distance;
    }

    void LateUpdate()
    {
        // Gérer le zoom/dézoom
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scrollInput * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Gérer la rotation horizontale
        if (Input.GetMouseButton(1)) // Bouton droit de la souris
        {
            currentHorizontalAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            currentVerticalAngle -= Input.GetAxis("Mouse Y") * verticalRotationSpeed;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);
            
            // Tourner le joueur avec la caméra
            player.rotation = Quaternion.Euler(0f, currentHorizontalAngle, 0f);
        }
        else if (Input.GetMouseButton(0)) // Bouton gauche de la souris
        {
            currentHorizontalAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            currentVerticalAngle -= Input.GetAxis("Mouse Y") * verticalRotationSpeed;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);
        }

        // Calculer la position de la caméra
        Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0f);
        Vector3 direction = rotation * Vector3.back * currentDistance;
        transform.position = player.position + direction;
        transform.LookAt(player.position);
    }
}
