using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] float mainSpeed = 100.0f; //regular speed
    [SerializeField] float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    [SerializeField] float _sensitivity = 0.25f; //How sensitive it with mouse

    [SerializeField] private int _minFOV = 20;
    [SerializeField] private int _maxFOV = 100;
    private float totalRun = 1.0f;

    private Vector3 _cameraPositionBeforeObjectPlacing;
    private Quaternion _cameraRotationBeforeObjectPlacing;

    private Vector2 _currentCameraRotation;
    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _camera = Camera.main;
        _currentCameraRotation.x = _camera.transform.eulerAngles.x;
        _currentCameraRotation.y = _camera.transform.eulerAngles.y;

        _cameraPositionBeforeObjectPlacing = _camera.transform.position;
        _cameraRotationBeforeObjectPlacing = _camera.transform.rotation;
    }

    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            if (ApplicationManager.Instance.CurrentApplicationMode != Enums.ApplicationMode.ObjectPlacing)
            {
                _currentCameraRotation.x += -Input.GetAxis("Mouse Y") * _sensitivity;
                _currentCameraRotation.x = Mathf.Clamp(_currentCameraRotation.x, -89, 89);
            }
            else
            {
                _currentCameraRotation.x = _camera.transform.eulerAngles.x;
            }
            _currentCameraRotation.y += Input.GetAxis("Mouse X") * _sensitivity;

            

            _camera.transform.eulerAngles = _currentCameraRotation;


        }
        //Mouse  camera angle done.  

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = _camera.transform.position;
        if (true)//(Input.GetKey(KeyCode.Space))
        { //If player wants to move on X and Z axis only
            _camera.transform.Translate(p);
            newPosition.x = _camera.transform.position.x;
            newPosition.z = _camera.transform.position.z;
            _camera.transform.position = newPosition;
        }
        else
        {
            _camera.transform.Translate(p);
        }
    }
    
    public void SetCameraSensitivity(float p_value)
    {
        _sensitivity = p_value;
    }

    public void ModifyCameraFOV(EventMessage p_message)
    {
        float l_addAmount = -((FloatMessage)p_message).FloatValue; //Invert becouse zoom should be when scrolling up!

        if ((_camera.fieldOfView + l_addAmount) > _minFOV && (_camera.fieldOfView + l_addAmount) < _maxFOV)
            _camera.fieldOfView += l_addAmount;
    }

    public void OnApplicationModeChanged()
    {
        if (ApplicationManager.Instance.CurrentApplicationMode == Enums.ApplicationMode.ObjectPlacing)
        {
            _cameraPositionBeforeObjectPlacing = _camera.transform.position;
            _cameraRotationBeforeObjectPlacing = _camera.transform.rotation;

            _camera.transform.DOMoveY(5.5f, 1f);
            _camera.transform.DORotate(new Vector3(70, 0, 0), 1f);
        }
        else
        {
            _camera.transform.DOMoveY(1.7f,1f);
            //_camera.transform.DORotateQuaternion(_cameraRotationBeforeObjectPlacing,1f);
        }
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    p_Velocity += new Vector3(0, -1, 0);
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    p_Velocity += new Vector3(0, 1, 0);
        //}
;        return p_Velocity;
    }
}