using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
        StartCoroutine("ShellFade");
	}

    IEnumerator ShellFade()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Ground")
        {
            GetComponent<Rigidbody>().Sleep();
        }
    }
}
