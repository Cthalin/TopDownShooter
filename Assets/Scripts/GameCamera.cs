using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{

    private Vector3 _cameraTarget;
    private Transform _target;

	// Use this for initialization
	void Start ()
	{
	    _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        _cameraTarget = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.position = Vector3.Lerp(transform.position, _cameraTarget, Time.deltaTime * 8);
    }
}
