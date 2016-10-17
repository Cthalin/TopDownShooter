using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public Transform Spawn;

    public Ray _ray;
    public RaycastHit _hit;
    //public float _shotDistance = 20;

    public void Shoot()
    {
        _ray = new Ray(Spawn.position, Spawn.forward);
        float _shotDistance = 20;
        if (Physics.Raycast(_ray, out _hit, _shotDistance))
        {
            _shotDistance = _hit.distance;
            print("Möp");
        }

        Debug.DrawRay(_ray.origin, _ray.direction * _shotDistance,Color.red,1,true);
    }
}
