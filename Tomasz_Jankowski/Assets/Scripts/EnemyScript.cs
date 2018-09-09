using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    AudioSource audioS;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRend;

    public Sprite[] statesSprites;

    Vector2 orginalCollSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();
        orginalCollSize = boxCollider.size;
    }

    private void Update()
    {
        if (rb != null && boxCollider.enabled == true)
        {
            if (Mathf.Abs(rb.velocity.x) > 0)
            {
                spriteRend.sprite = statesSprites[1];
                Vector2 s = spriteRend.bounds.size;
                boxCollider.size = s - new Vector2(0.40f,0f);
            }
            else if (rb.IsSleeping())
            {
                spriteRend.sprite = statesSprites[0];
                boxCollider.size = orginalCollSize;
                punched = false;
            }

        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (punched)
        {
            if (col.gameObject.tag == "Wall")
            {
                rb.AddForce(-rb.velocity);
                spriteRend.sprite = statesSprites[2];
                boxCollider.enabled = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        OnCollisionStay2D(col);
    }

    bool punched = false;

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Punch" && !audioS.isPlaying)
        {
            punched = true;
            audioS.Play();
        }
    }
}
