using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMan : MonoBehaviour {

    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Transform transformer;
    private GameObject P1;
    public GameObject projectile;
    public GameObject log;
    public GameObject venusaur;
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
    private float random;
    public Shader shaderGUItext;
    public Shader shaderSpritesDefault;
    private int logCounter;
    private bool movesRst;
    private float deathVal = 40;
    private bool posRst;
    private bool axeRst;
    private bool expRst = true;

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

    void Start () {
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        transformer = this.GetComponent<Transform>();
        P1 = GameObject.Find("P1 position");
    }

    void Update()
    {
        random = Random.value;
        if (P1.transform.localScale.x == 1)
        {
            body.velocity = new Vector2(0, 0);
            sprite.color = new Color(255, 255, 255, 255);
            animator.StartPlayback();
        }
        else
        {
            animator.StopPlayback();
            if ((hit || hitsuper) && blink == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                this.transform.GetChild(0).GetComponent<AudioSource>().Play();
                death += 1f;
            }
            if (sprite.sprite.name == "start" && expRst == true)
            {
                this.transform.GetChild(11).GetComponent<AudioSource>().Play();
                expRst = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("axe"))
            {
                if (sprite.sprite.name == "trans6" && posRst == false)
                {
                    if (P1.transform.position.x >= -10 && P1.transform.position.x <= 10)
                    {
                        if (random <= 0.5f)
                        {
                            this.transform.position = new Vector3(P1.transform.position.x + 5, this.transform.position.y, -0.5f);
                        }
                        else
                        {
                            this.transform.position = new Vector3(P1.transform.position.x - 5, this.transform.position.y, -0.5f);
                        }                       
                    }
                    else
                    {
                        this.transform.position = new Vector3(-10f + (20 * Random.value), this.transform.position.y, -0.5f);
                    }                   
                    posRst = true;
                }
                else if (sprite.sprite.name != "trans6" && posRst == true)
                {
                    if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.5f);
                    posRst = false;
                }
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("cannon"))
            {
                if (sprite.sprite.name == "trans6" && posRst == false)
                {
                    if (P1.transform.position.x <= 0)
                    {
                        this.transform.position = new Vector3(-15f, this.transform.position.y, -0.5f);
                    }
                    else
                    {
                        this.transform.position = new Vector3(15f, this.transform.position.y, -0.5f);
                    }
                    
                    posRst = true;
                }
                else if (sprite.sprite.name != "trans6" && posRst == true)
                {
                    if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.5f);
                    posRst = false;
                }
            }
            if (patternOFF == false)
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
                    if (random <= 0.25f)
                    {
                        animator.SetBool("summVen", true);
                    }
                    else if (random > 0.25f && random <= 0.5f)
                    {
                        animator.SetBool("summLog", true);
                    }
                    else if (random > 0.5f && random <= 0.75f)
                    {
                        animator.SetBool("cannon", true);
                    }
                    else if (random > 0.75f)
                    {
                        animator.SetBool("axe", true);
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
                superRst = true;
            }
            else if (hitsuper == false)
            {
                superRst = false;
            }
            if (death >= deathVal && hit == true && !animator.GetCurrentAnimatorStateInfo(0).IsName("death") && superRst == false)
            {
                animator.SetBool("hurt2", true);
                blink = true;
                superRst = true;
            }
            if (sprite.sprite.name == "plant man_94" || sprite.sprite.name == "plant man_95" || sprite.sprite.name == "plant man_96" || sprite.sprite.name == "plant man_97" || sprite.sprite.name == "plant man_98")
            {
                if (sprite.flipX == true)
                {
                    this.transform.GetChild(3).gameObject.layer = 10;
                }
                else if (sprite.flipX == false)
                {
                    this.transform.GetChild(2).gameObject.layer = 10;
                }
            }
            else if (sprite.sprite.name == "plant man_316")
            {
                if (sprite.flipX == true)
                {
                    this.transform.GetChild(5).gameObject.layer = 10;
                }
                else if (sprite.flipX == false)
                {
                    this.transform.GetChild(4).gameObject.layer = 10;
                }
            }
            else if (sprite.sprite.name == "plant man_317")
            {
                if (sprite.flipX == true)
                {
                    this.transform.GetChild(7).gameObject.layer = 10;
                }
                else if (sprite.flipX == false)
                {
                    this.transform.GetChild(6).gameObject.layer = 10;
                }
            }
            else if (sprite.sprite.name == "plant man_446" || sprite.sprite.name == "plant man_452")
            {
                this.transform.GetChild(10).gameObject.layer = 10;
            }
            else if (sprite.sprite.name == "plant man_447")
            {
                if (sprite.flipX == true)
                {
                    this.transform.GetChild(8).gameObject.layer = 10;
                }
                else if (sprite.flipX == false)
                {
                    this.transform.GetChild(9).gameObject.layer = 10;
                }
            }
            else if (sprite.sprite.name == "plant man_453")
            {
                if (sprite.flipX == true)
                {
                    this.transform.GetChild(9).gameObject.layer = 10;
                }
                else if (sprite.flipX == false)
                {
                    this.transform.GetChild(8).gameObject.layer = 10;
                }
            }
            else
            {
                this.transform.GetChild(2).gameObject.layer = 13;
                this.transform.GetChild(3).gameObject.layer = 13;
                this.transform.GetChild(4).gameObject.layer = 13;
                this.transform.GetChild(5).gameObject.layer = 13;
                this.transform.GetChild(6).gameObject.layer = 13;
                this.transform.GetChild(7).gameObject.layer = 13;
                this.transform.GetChild(8).gameObject.layer = 13;
                this.transform.GetChild(9).gameObject.layer = 13;
                this.transform.GetChild(10).gameObject.layer = 13;
            }
            if (sprite.sprite.name == "plant man_234" && projectileReset == false && animator.GetCurrentAnimatorStateInfo(0).IsName("cannon"))
            {
                this.transform.GetChild(7).GetComponent<AudioSource>().Play();
                if (sprite.flipX == true)
                {
                    transform.GetChild(16).GetComponent<Animator>().SetBool("shot", true);
                    Instantiate(projectile, this.transform.GetChild(12).transform.position, this.transform.GetChild(12).transform.rotation);
                }
                else if (sprite.flipX == false)
                {
                    transform.GetChild(17).GetComponent<Animator>().SetBool("shot", true);
                    Instantiate(projectile, this.transform.GetChild(11).transform.position, this.transform.GetChild(11).transform.rotation);
                }
                projectileReset = true;
            }
            else if (sprite.sprite.name == "plant man_119" && projectileReset == false && animator.GetCurrentAnimatorStateInfo(0).IsName("groundSummon"))
            {
                this.transform.GetChild(4).GetComponent<AudioSource>().Play();
                logCounter += 1;
                if (logCounter == 4)
                {
                    logCounter = 1;
                }
                if (logCounter == 1)
                {
                    Instantiate(log, this.transform.GetChild(14).transform.position, this.transform.GetChild(14).transform.rotation);
                    Instantiate(log, this.transform.GetChild(13).transform.position, this.transform.GetChild(13).transform.rotation);
                }
                if (logCounter == 2)
                {
                    Instantiate(log, new Vector3(this.transform.GetChild(14).transform.position.x - 4, this.transform.GetChild(14).transform.position.y, -2), this.transform.rotation);
                    Instantiate(log, new Vector3(this.transform.GetChild(13).transform.position.x + 4, this.transform.GetChild(13).transform.position.y, -2), this.transform.rotation);
                }
                if (logCounter == 3)
                {
                    Instantiate(log, new Vector3(this.transform.GetChild(14).transform.position.x - 8, this.transform.GetChild(14).transform.position.y, -2), this.transform.rotation);
                    Instantiate(log, new Vector3(this.transform.GetChild(13).transform.position.x + 8, this.transform.GetChild(13).transform.position.y, -2), this.transform.rotation);
                }
                projectileReset = true;
            }
            else if (sprite.sprite.name == "plant man_119" && projectileReset == false && animator.GetCurrentAnimatorStateInfo(0).IsName("summonVenusaur"))
            {
                if (sprite.flipX == true)
                {
                    Instantiate(venusaur, this.transform.GetChild(14).transform.position, this.transform.rotation);
                }
                else if (sprite.flipX == false)
                {
                    Instantiate(venusaur, this.transform.GetChild(13).transform.position, this.transform.rotation);
                }
                projectileReset = true;
            }
            else if (sprite.sprite.name != "plant man_119" && sprite.sprite.name != "plant man_234")
            {
                transform.GetChild(16).GetComponent<Animator>().SetBool("shot", false);
                transform.GetChild(17).GetComponent<Animator>().SetBool("shot", false);
                projectileReset = false;
            }
            if (hitsuper == true && superRst == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                animator.SetBool("hurt2", true);
                death += 4;
                blink = true;
                superRst = true;
            }
            else if (hitsuper == false)
            {
                superRst = false;
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
            if (hit == true || blink == true || hitsuper == true)
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
                    body.velocity = new Vector2(-10, 0);
                }
                else
                {
                    body.velocity = new Vector2(10, 0);
                }
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
                this.gameObject.layer = 13;
            }
            else if (death != (deathVal + 1))
            {
                this.gameObject.layer = 10;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
            {
                this.transform.GetChild(1).GetComponent<AudioSource>().Stop();
                this.transform.GetChild(5).GetComponent<AudioSource>().Stop();
                this.transform.GetChild(6).GetComponent<AudioSource>().Stop();
                this.transform.GetChild(8).GetComponent<AudioSource>().Stop();
                this.GetComponent<AudioSource>().Play();
                logCounter = 0;
                hurtReset = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                hurtReset = false;
            }
            if (death >= deathVal && sprite.sprite.name == "plant man_504")
            {
                this.transform.GetChild(9).GetComponent<AudioSource>().Play();
                if (P1.GetComponent<Transform>().position.x < transformer.position.x)
                {
                    sprite.flipX = true;
                    body.velocity = new Vector2(12, 8);
                }
                else
                {
                    sprite.flipX = false;
                    body.velocity = new Vector2(-12, 8);
                }
                this.gameObject.layer = 13;
                animator.SetBool("death", true);
                death = deathVal + 1;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                normalSprite();
                this.gameObject.layer = 13;
                if (sprite.sprite.name == "plantDeath8")
                {
                    body.velocity = new Vector2(0, body.velocity.y);
                }
            }
            if (sprite.sprite.name == "trans6" || sprite.sprite.name == "trans7" || sprite.sprite.name == "trans8" || sprite.sprite.name == "trans9")
            {
                this.gameObject.layer = 13;
            }
            if (sprite.sprite.name == "plant man_313" || sprite.sprite.name == "trans7" || sprite.sprite.name == "trans8" || sprite.sprite.name == "trans9")
            {
                this.gameObject.layer = 13;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("axe") && atkRst2 == false)
            {
                this.transform.GetChild(8).GetComponent<AudioSource>().Play();
                atkRst2 = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("cannon") && atkRst2 == false)
            {
                this.transform.GetChild(6).GetComponent<AudioSource>().Play();
                atkRst2 = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("groundSummon") && atkRst2 == false)
            {
                this.transform.GetChild(1).GetComponent<AudioSource>().Play();
                atkRst2 = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("summonVenusaur") && atkRst2 == false)
            {
                this.transform.GetChild(5).GetComponent<AudioSource>().Play();
                atkRst2 = true;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk") || animator.GetCurrentAnimatorStateInfo(0).IsName("stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
            {
                atkRst2 = false;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("axe"))
            {
                animator.SetBool("axe", false);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("cannon"))
            {
                animator.SetBool("cannon", false);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("groundSummon"))
            {
                animator.SetBool("summLog", false);
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("summonVenusaur"))
            {
                animator.SetBool("summVen", false);
            }
            if (sprite.sprite.name == "plant man_313" && axeRst == false)
            {
                this.transform.GetChild(2).GetComponent<AudioSource>().Play();
                axeRst = true;
            }
            else if (sprite.sprite.name == "plant man_325" && axeRst == true)
            {
                if (sprite.flipX == true)
                {
                    this.transform.GetChild(15).transform.position = new Vector3(this.transform.position.x - 5f, this.transform.GetChild(15).transform.position.y, this.transform.GetChild(15).transform.position.z);
                }
                else
                {
                    this.transform.GetChild(15).transform.position = new Vector3(this.transform.position.x + 5f, this.transform.GetChild(15).transform.position.y, this.transform.GetChild(15).transform.position.z);
                }
                this.transform.GetChild(15).GetComponent<Animator>().SetBool("rocks", true);
                this.transform.GetChild(4).GetComponent<AudioSource>().Play();
                axeRst = false;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("axe"))
            {
                axeRst = false;
            }
            if (this.transform.GetChild(15).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("rocks"))
            {
                this.transform.GetChild(15).GetComponent<Animator>().SetBool("rocks", false);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                animator.SetBool("walk", false);
            }
            if (sprite.sprite.name == "explosions_0" && expRst == false)
            {
                this.transform.GetChild(10).GetComponent<AudioSource>().Play();
                expRst = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("death") && sprite.sprite.name == "blank")
            {
                Destroy(this.gameObject);
            }
            print(death);
        }
    }
}
