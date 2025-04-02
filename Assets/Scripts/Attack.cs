using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public bool isRotation;
    public bool isEnemys;

    private void Update()
    {
        if (isRotation)
        {
            transform.Rotate(Vector3.forward * 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnemys && (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall"))
        {
            Destroy(gameObject);
        }

        else if (isEnemys && (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Wall"))
        {
            Destroy(gameObject);
        }

        else if (isEnemys && collision.gameObject.tag == "Player")
        {
            HpOxygenManager.instance.OnHit(damage);
            Destroy(gameObject);
        }
    }
}
