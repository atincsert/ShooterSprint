using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float bulletSpeed = 12;
    [SerializeField] private float _torque = 120;
    [SerializeField] private float maxTorqueBonus = 150;
    [SerializeField] private float maxAngularVelocity = 10;
    [SerializeField] private float forceAmount = 600;
    [SerializeField] private float maxUpAssist = 30;
    [SerializeField] private float maxY = 10;
    [SerializeField] private float customGravity = 6f;

    private Rigidbody _rb;
    private float _lastFired;
    private bool _fire;

    private void Awake() => _rb = GetComponent<Rigidbody>();

    private void Start()
    {
        Physics.gravity = new Vector3(0, -customGravity, 0);
    }

    void Update()
    {
        // Clamp max velocity
        _rb.angularVelocity = new Vector3(0, 0, Mathf.Clamp(_rb.angularVelocity.z, -maxAngularVelocity, maxAngularVelocity));

        if (Input.GetMouseButtonDown(0))
        {
            // Spawn
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            var hitsTarget = Physics.Raycast(spawnPoint.position, spawnPoint.forward, float.PositiveInfinity, targetLayer);
            bullet.Init(spawnPoint.forward * bulletSpeed, hitsTarget);
            _lastFired = Time.time;

            // Apply force - More up assist depending on y position
            var assistPoint = Mathf.InverseLerp(0, maxY, _rb.position.y);
            var assistAmount = Mathf.Lerp(maxUpAssist, 0, assistPoint);
            var forceDir = -transform.right * forceAmount + Vector3.up * assistAmount;
            if (_rb.position.y > maxY) forceDir.y = Mathf.Min(0, forceDir.y);
            _rb.AddForce(forceDir);

            // Determine the additional torque to apply when swapping direction
            var angularPoint = Mathf.InverseLerp(0, maxAngularVelocity, Mathf.Abs(_rb.angularVelocity.z));
            var amount = Mathf.Lerp(0, maxTorqueBonus, angularPoint);
            var torque = _torque + amount;

            // Apply torque
            var dir = Vector3.Dot(spawnPoint.forward, Vector3.right) < 0 ? Vector3.back : Vector3.forward;
            _rb.AddTorque(dir * torque, ForceMode.Impulse);
        }
    }
}
