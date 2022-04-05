using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour,IBulletObject
{
    [SerializeField] private float _bulletSpeed = 0.0f;
    public void OnObjectSpawn()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * _bulletSpeed, ForceMode.Force);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        if (Vector3.Distance(transform.position,GameManager.gameManager.transform.position) > 40f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.tag = "Untagged";
            gameObject.SetActive(false);
        }
    }
}
