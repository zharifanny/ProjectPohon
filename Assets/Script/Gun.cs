using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 0.1f;
    public float fireRate = 0.5f;
    private float nextfire;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) //to hold the left mouse button, use GetButton instead of GetButtonDown
        {
            FireBullet();

        }
    }

    void FireBullet()
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + fireRate;        
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        }

    }
}
