using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        semi,
        burst,
        auto
    };

    public GunType gunType;
    public float rpm;

    //Components
    public Transform Spawn;
    public Transform ShellEjectionPoint;
    public Rigidbody Shell;
    private LineRenderer _tracer;

    private Ray _ray;
    private RaycastHit _hit;
    private AudioSource _shotSound;

    //System
    private float _shotDistance = 20;
    private float _secondsBetweenShots;
    private float _nextPossibleShootTime;
    private bool _canShoot;

    void Start()
    {
        _secondsBetweenShots = 60/rpm;
        _shotSound = GetComponent<AudioSource>();
        if (GetComponent<LineRenderer>())
        {
            _tracer = GetComponent<LineRenderer>();
        }
    }

    public void Shoot()
    {

        if (CanShoot())
        {
            _ray = new Ray(Spawn.position, Spawn.forward);
            if (Physics.Raycast(_ray, out _hit, _shotDistance))
            {
                _shotDistance = _hit.distance;
            }

            _nextPossibleShootTime = Time.time + _secondsBetweenShots;
            _shotSound.Play();

            if (_tracer)
            {
                StartCoroutine("RenderTracer", _ray.direction * _shotDistance);
            }

            Rigidbody newShell = Instantiate(Shell, ShellEjectionPoint.position,Quaternion.identity) as Rigidbody;
            newShell.AddForce(ShellEjectionPoint.forward * Random.Range(150f,200f) + Spawn.forward * Random.Range(-10f,10f));
        }
    }

    public void ShootContiniously()
    {
        if (gunType == GunType.auto)
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        _canShoot = true;

        if (Time.time < _nextPossibleShootTime)
        {
            _canShoot = false;
        }
        return _canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        _tracer.enabled = true;
        _tracer.SetPosition(0,Spawn.position);
        _tracer.SetPosition(1, Spawn.position + hitPoint);
        yield return null;
        _tracer.enabled = false;
    }
}
