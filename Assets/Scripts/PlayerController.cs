using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CharacterController))]
public class PlayerController : MonoBehaviour
{
    //Handling
    public float RotationSpeed = 450;
    public float WalkSpeed = 5;
    public float RunSpeed = 8;

    //Components
    private CharacterController _controller;
    private Camera _cam;
    public Gun Gun;

    //System
    private Vector3 _input;
    private Quaternion _targetRotation;
    private Vector3 _motion;
    private Vector3 _mousePos;

	// Use this for initialization
	void Start ()
	{
	    _controller = GetComponent<CharacterController>();
        _cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	    //ControlWASD();
        ControlMouse();

	    if (Input.GetButtonDown("Shoot"))
	    {
	        Gun.Shoot();
	    }
	}

    void ControlMouse()
    {
        _mousePos = Input.mousePosition;
        _mousePos =
            _cam.ScreenToWorldPoint(new Vector3(_mousePos.x, _mousePos.y,
                _cam.transform.position.y - transform.position.y));
        _targetRotation = Quaternion.LookRotation(_mousePos - new Vector3(transform.position.x,0,transform.position.z));
        transform.eulerAngles = Vector3.up *
                                    Mathf.MoveTowardsAngle(transform.eulerAngles.y, _targetRotation.eulerAngles.y,
                                        RotationSpeed * Time.deltaTime);

        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _motion = _input;
        _motion *= (Mathf.Abs(_input.x) == 1 && Mathf.Abs(_input.z) == 1) ? .7f : 1;
        _motion *= (Input.GetButton("Run")) ? RunSpeed : WalkSpeed;
        _motion += Vector3.up * -8;
        _controller.Move(_motion * Time.deltaTime);
    }

    void ControlWASD()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (_input != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(_input);
            transform.eulerAngles = Vector3.up *
                                    Mathf.MoveTowardsAngle(transform.eulerAngles.y, _targetRotation.eulerAngles.y,
                                        RotationSpeed * Time.deltaTime);
        }
        _motion = _input;
        _motion *= (Mathf.Abs(_input.x) == 1 && Mathf.Abs(_input.z) == 1) ? .7f : 1;
        _motion *= (Input.GetButton("Run")) ? RunSpeed : WalkSpeed;
        _motion += Vector3.up * -8;
        _controller.Move(_motion * Time.deltaTime);
    }
}
