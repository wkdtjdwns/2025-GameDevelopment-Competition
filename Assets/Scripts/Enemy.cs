using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Info")]
    public bool isNormal;
    public bool isArrive;
    public bool isSeeRight;
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;
    public float hp;
    public float damage;
    public string enemyName;

    [Header("Shot Enemy")]
    public bool isShot;
    public float curDelay;
    public float reloadDelay;
    public GameObject bulletObj;

    [Header("Follow Enemy")]
    public bool isFollow;
    public GameObject player;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        startPos = transform.position;
        player = GameObject.Find("Player").gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Reload();
        Shot();
    }

    private void Move()
    {
        if (isNormal || isShot)
        {
            NormalMove();
        }

        else if (isFollow)
        {
            FollowMove();
        }
    }

    private void Shot()
    {
        if (isShot && curDelay >= reloadDelay)
        {
            GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            int dir = (spriteRenderer.flipX) ? -1 : 1;
            rigid.AddForce(Vector2.right * dir * 10, ForceMode2D.Impulse);

            curDelay = 0;
        }
    }

    private void Reload()
    {
        if (isShot)
        {
            curDelay += Time.deltaTime;
        }
    }

    private void NormalMove()
    {
        if (!isArrive && transform.position.Equals(endPos)) { isArrive = true; }
        else if (isArrive && transform.position.Equals(startPos)) { isArrive = false; }

        if (isSeeRight)
        {
            spriteRenderer.flipX = isArrive;
        }
        else
        {
            spriteRenderer.flipX = !isArrive;
        }

        if (isArrive)
        {

            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        }
    }

    private void FollowMove()
    {
        spriteRenderer.flipX = transform.position.x < player.transform.position.x;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HpOxygenManager.instance.OnHit(damage);
            Destroy(gameObject);
        }
    }
}
