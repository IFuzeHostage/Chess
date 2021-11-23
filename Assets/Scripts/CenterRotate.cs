using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRotate : MonoBehaviour
{
    [SerializeField]
    public GameObject Focus;
    // Start is called before the first frame update
    private Vector3 _cameraOffset;
    public bool Rotatearound;
    public float SmoothFactor = 0.5f;
    public float RotationSpeed = 5.0f;
    public bool MouseDown = false;
    void Start()
    {
        _cameraOffset = transform.position - Focus.transform.position;
        transform.LookAt(Focus.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            MouseDown = true;
        }   
        if(Input.GetMouseButtonUp(1)){
            MouseDown = false;
        }
        if(MouseDown){
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
            _cameraOffset = camTurnAngle * _cameraOffset;
        }
        Vector3 newPos = Focus.transform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        transform.LookAt(Focus.transform);
    }
}
