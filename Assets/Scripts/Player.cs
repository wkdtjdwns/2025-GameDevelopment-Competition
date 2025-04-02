using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float reloadDelay;
    [SerializeField] private float curDelay;
    public GameObject attackBullet;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Awake()
    {
        maxSpeed = 10f;
        jumpPower = 7.5f;
        reloadDelay = 3f;

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
        Reload();
        Attack();
    }

    private void Move()
    {
        // Player Move
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        // Stop Jump
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rigid.velocity.y < 0)
        {
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJump", false);
                }
            }
        }

        // Player Flip
        spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Player Walk Anim
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalk", false);
        }

        else
        {
            anim.SetBool("isWalk", true);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
    }

    private void Attack()
    {
        if (!Input.GetButton("Fire1") || curDelay < reloadDelay)
        {
            return;
        }

        GameObject attack = Instantiate(attackBullet, transform.position, transform.rotation);
        attack.transform.position = transform.position;
        Rigidbody2D rigid = attack.GetComponent<Rigidbody2D>();

        int dir = (spriteRenderer.flipX) ? -1 : 1;
        rigid.AddForce(Vector2.right * dir * 10, ForceMode2D.Impulse);

        curDelay = 0;
    }

    private void Reload()
    {
        curDelay += Time.deltaTime;
    }
}
