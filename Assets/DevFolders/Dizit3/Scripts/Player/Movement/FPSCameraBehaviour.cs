using UnityEditor.SearchService;
using UnityEngine;

public class FPSCameraBehaviour : MonoBehaviour
{
    private Camera _mainCamera;

    [SerializeField] private Vector3 _offset = Vector3.zero;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = transform.position + _offset;

        _mainCamera.transform.position = desiredPosition;
    }

}
