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

    public Transform Spawn;

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
            //Debug.DrawRay(_ray.origin, _ray.direction * _shotDistance, Color.red, 1, true);
            print("Pew!");

            _nextPossibleShootTime = Time.time + _secondsBetweenShots;
            _shotSound.Play();
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
}
