using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWolf : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Transform transformer;
    private GameObject P1;
    private GameObject explosion;
    private GameObject damageBox;
    private GameObject rightAttack;
    private GameObject leftAttack;
    private GameObject rightAttack2;
    private GameObject leftAttack2;
    private GameObject rightProjectile;
    private GameObject leftProjectile;
    public GameObject projectile;
    private bool projectileReset;
    public LayerMask attack;
    public LayerMask super;
    private bool hurtReset;
    private float death;
    private bool hit;
    private bool hitsuper;
    private bool superRst;
    private bool blink;
    private float blinkVal;
    private int blinkStop;
    private float pattern;
    private bool patternOFF;
    private float standTime;
    private bool atkRst;
    private bool atkRst2;
    private bool atkRst3;
    private float random;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    private float deathVal = 30;

    void whiteSprite()
    {
        sprite.material.shader = shaderGUItext;
        sprite.color = Color.white;
    }

    void normalSprite()
    {
        sprite.material.shader = shaderSpritesDefault;
        sprite.color = Color.white;
    }

    private void FixedUpdate()
    {
        hit = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), attack);
        hitsuper = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), super);
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        transformer = this.GetComponent<Transform>();
        P1 = GameObject.Find("P1 position");
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
        this.transform.GetChild(7).GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        rightAttack = GameObject.Find("attack1R");
        leftAttack = GameObject.Find("attack1L");
        rightAttack2 = GameObject.Find("attack2R");
        leftAttack2 = GameObject.Find("attack2L");
        explosion = GameObject.Find("explosionwolf");
        damageBox = GameObject.Find("dBox");
        rightProjectile = GameObject.Find("projectiler");
        leftProjectile = GameObject.Find("projectilel");
        random = Random.value;
        if (explosion.GetComponent<SpriteRenderer>().sprite.name == "blank")
        {
            Destroy(this.gameObject);
        }
        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0, 0);
            body.gravityScale = 0;
            sprite.color = new Color(255, 255, 255, 255);
            animator.StartPlayback();
        }
        else
        {
            body.gravityScale = 5;
            animator.StopPlayback();
            if ((hit || hitsuper) && blink == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                this.transform.GetChild(0).GetComponent<AudioSource>().Play();
                death += 1f;
            }
            if (sprite.sprite.name == "wolf boss_13" && atkRst2 == false)
            {
                this.transform.GetChild(6).GetComponent<AudioSource>().Play();
                atkRst2 = true;
            }
            else if (sprite.sprite.name != "wolf boss_13")
            {
                atkRst2 = false;
            }
            if (patternOFF == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("start"))
            {
                pattern += 0.1f;
            }
            if (pattern > 0 && pattern <= 0.3f)
            {
                animator.SetBool("walk", true);
            }
            if (pattern >= 4.9f && pattern <= 5.1f && patternOFF == false)
            {
                animator.SetBool("walk", false);
                if (random <= 0.4f)
                {
                    animator.SetBool("attack", true);
                }
                else if (random > 0.4f && random <= 0.8f)
                {
                    animator.SetBool("attack2", true);
                }
                else if (random > 0.8f)
                {
                    animator.SetBool("super", true);
                }
                patternOFF = true;
            }
            if (standTime >= 5 || (animator.GetCurrentAnimatorStateInfo(0).IsName("stand") && hit == true))
            {
                pattern = 0;
                patternOFF = false;
            }
            if (hitsuper == true && superRst == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                animator.SetBool("hurt2", true);
                death += 3;
                blink = true;
                superRst = true;
            }
            else if (hitsuper == false)
            {
                superRst = false;
            }
            if (sprite.sprite.name == "wolf boss_13")
            {
                if (sprite.flipX == true)
                {
                    leftAttack.layer = 10;
                }
                else if (sprite.flipX == false)
                {
                    rightAttack.layer = 10;
                }
            }
            else if (sprite.sprite.name == "wolf boss_19")
            {
                if (sprite.flipX == true)
                {
                    leftAttack2.layer = 10;
                }
                else if (sprite.flipX == false)
                {
                    rightAttack2.layer = 10;
                }
            }
            else
            {
                leftAttack.layer = 13;
                rightAttack.layer = 13;
                leftAttack2.layer = 13;
                rightAttack2.layer = 13;
            }
            if (sprite.sprite.name == "wolf boss_13" && projectileReset == false && animator.GetCurrentAnimatorStateInfo(0).IsName("super"))
            {
                if (sprite.flipX == true)
                {
                    Instantiate(projectile, leftProjectile.transform.position, leftProjectile.transform.rotation);
                }
                else if (sprite.flipX == false)
                {
                    Instantiate(projectile, rightProjectile.transform.position, rightProjectile.transform.rotation);
                }
                projectileReset = true;
            }
            else if (sprite.sprite.name != "wolf boss_13")
            {
                projectileReset = false;
            }
            if (hit == true && !(animator.GetCurrentAnimatorStateInfo(0).IsName("stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("walk")) || blink == true)
            {
                blink = true;
            }
            if (blink == true)
            {
                hit = false;
                blinkVal += 0.2f;
                if (blinkVal >= 0.9f && blinkVal <= 1.1f)
                {
                    whiteSprite();
                }
                else if (blinkVal >= 1.9f && blinkVal <= 2.1f)
                {
                    normalSprite();
                    blinkStop += 1;
                    blinkVal = 0;
                }
            }
            if (blinkStop == 3)
            {
                blink = false;
                blinkVal = 0;
                blinkStop = 0;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                if (sprite.flipX == true)
                {
                    body.velocity = new Vector2(-5, body.velocity.y);
                }
                else
                {
                    body.velocity = new Vector2(5, body.velocity.y);
                }
                damageBox.gameObject.layer = 13;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                animator.SetBool("hurt2", false);
                animator.SetBool("hurt", false);
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    body.velocity = new Vector2(2, 0);
                }
                else
                {
                    body.velocity = new Vector2(-2, 0);
                }
                damageBox.gameObject.layer = 13;
            }
            else if (death != (deathVal + 1))
            {
                damageBox.gameObject.layer = 10;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && atkRst == false)
            {
                this.transform.GetChild(2).GetComponent<AudioSource>().Play();
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
                atkRst = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") && atkRst == false)
            {
                this.GetComponent<AudioSource>().Play();
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
                atkRst = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("super") && atkRst == false)
            {
                this.transform.GetChild(3).GetComponent<AudioSource>().Play();
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
                atkRst = true;
            }
            else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("super") && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
            {
                atkRst = false;
                atkRst3 = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            {
                animator.SetBool("attack", false);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack2"))
            {
                animator.SetBool("attack2", false);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("super"))
            {
                animator.SetBool("super", false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") && sprite.sprite.name == "wolf boss_18" && atkRst3 == false)
            {
                this.transform.GetChild(8).GetComponent<AudioSource>().Play();
                atkRst3 = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stand"))
            {
                standTime += 0.1f;
                if (hit == true)
                {
                    animator.SetBool("hurt", true);
                }
                body.velocity = new Vector2(0, 0);
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
            else if (hit == true || blink == true && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                standTime = 0;
                blink = true;
            }
            else
            {
                standTime = 0;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
            {
                this.transform.GetChild(4).GetComponent<AudioSource>().Play();
                hurtReset = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                hurtReset = false;
            }
            if (death >= deathVal && sprite.sprite.name == "wolf boss_24")
            {
                this.transform.GetChild(5).GetComponent<AudioSource>().Play();
                if (sprite.flipX == true)
                {
                    body.velocity = new Vector2(8, 16);
                }
                else if (sprite.flipX == false)
                {
                    body.velocity = new Vector2(-8, 16);
                }
                damageBox.gameObject.layer = 13;
                animator.SetBool("death", true);
                death = deathVal + 1;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                normalSprite();
                damageBox.gameObject.layer = 13;
                if (sprite.sprite.name == "wolf boss_26")
                {
                    body.velocity = new Vector2(0, body.velocity.y);
                }
            }
            if (sprite.sprite.name == "blank")
            {
                sprite.color = new Color(0, 0, 0, 0);
                if (explosion.GetComponent<Animator>().GetBool("boom") == false)
                {
                    this.transform.GetChild(1).GetComponent<AudioSource>().Play();
                }
                explosion.GetComponent<Animator>().SetBool("boom", true);
            }
        }
    }
}
