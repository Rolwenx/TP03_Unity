using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform _player; 

    [Header("Camera Settings")]

    public float minDistance = 2f;
    public float maxDistance = 8f;
    public float zoomSpeed = 1f;
    // when starting the game, this will be the distance per default
    public float initialDistance = 5f; 
    private float _currentDistance;
 
    public float rotationSpeed = 4f; 
    public float verticalRotationSpeed = 2f; 
    public float minVerticalAngle = -20f; 
    public float maxVerticalAngle = 80f; 
    private float _currentVerticalAngle = 0f;
    private float _currentHorizontalAngle = 0f;
    
    void Start()
    {
        _currentDistance = initialDistance;
    }

    void LateUpdate()
    {
        // part to handle the zooming and dezooming
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        _currentDistance -= scrollInput * zoomSpeed;
        _currentDistance = Mathf.Clamp(_currentDistance, minDistance, maxDistance);

        // part to handle the rotation (horizontal)

        // RIGHT MOUSE BUTTON : caméra tourne autour du player et le player tourne aussi
        if (Input.GetMouseButton(1)) 
        {
            _currentHorizontalAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            _currentVerticalAngle -= Input.GetAxis("Mouse Y") * verticalRotationSpeed;
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, minVerticalAngle, maxVerticalAngle);
        
            // this part handle turning the player accorder to the camera current's angle
            _player.rotation = Quaternion.Euler(0f, _currentHorizontalAngle, 0f);
        }
        // LEFT MOUSE BUTTON : caméra tourne autour du player mais le player ne tourne pas
        else if (Input.GetMouseButton(0)) 
        {
            _currentHorizontalAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            _currentVerticalAngle -= Input.GetAxis("Mouse Y") * verticalRotationSpeed;
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle, minVerticalAngle, maxVerticalAngle);
        }

        Quaternion rotation = Quaternion.Euler(_currentVerticalAngle, _currentHorizontalAngle, 0f);
        Vector3 direction = rotation * Vector3.back * _currentDistance;
        transform.position = _player.position + direction;
        transform.LookAt(_player.position);
    }
}