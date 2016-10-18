using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private Rigidbody _enemyRB;
    public float MoveSpeed;

    public PlayerController ThePlayer;

	// Use this for initialization
	void Start () {
        _enemyRB = GetComponent<Rigidbody>();
        ThePlayer = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(ThePlayer.transform.position);
        _enemyRB.velocity = transform.forward * MoveSpeed;
    }
}
