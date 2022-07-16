using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private bool _hitsTarget;

    public void Init(Vector3 vel, bool hitsTarget)
    {
        rb.velocity = vel;
        _hitsTarget = hitsTarget;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
