using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catwoman : MonoBehaviour {

    private Animator animator;
    public Transform feet;
    private bool onGround;
    private bool onPlatform;
    private bool hit;
    public LayerMask enemy;
    private bool hpItem;
    public LayerMask HP;
    public LayerMask scene;
    private bool sceneChange;
    public LayerMask ground;
    public LayerMask deathLayer;
    private bool death;
    //private GameObject status;
    private GameObject cameraHUD;
    private GameObject life;
    private GameObject mana;
    public GameObject projectile;
    bool projectileRst;
    public GameObject projectileX;
    private GameObject projectileXR;
    private GameObject projectileXL;
    private GameObject projectile1;
    private GameObject superFace;
    private GameObject ultimate;
    private GameObject ultimate2;
    private GameObject ultimate3;
    private GameObject ultimate4;
    private GameObject superCut;
    private GameObject superCut1;
    private GameObject superCut2;
    private GameObject superCut3;
    private GameObject superCut4;
    private GameObject superCut5;
    private GameObject superCut6;
    private GameObject feetObj;
    private GameObject myPosition; 
    private GameObject rightAttack;
    private GameObject leftAttack;
    private GameObject rightCrounchAttack;
    private GameObject leftCrounchAttack;
    private GameObject rightAirAttack;
    private GameObject leftAirAttack;
    private GameObject attack2;
    private GameObject airattack2;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private KeyCode up = KeyCode.W;
    private KeyCode right = KeyCode.D;
    private KeyCode left = KeyCode.A;
    private KeyCode down = KeyCode.S;
    private KeyCode attack = KeyCode.J;
    private KeyCode ult = KeyCode.L;
    private KeyCode jump = KeyCode.K;
    private bool hurtReset;
    private bool blink;
    private float blinkVal;
    private int blinkStop;
    private float jumpVal = 25;
    private float walkVal = 10;
    private bool crounchRst;
    private float randomVal;
    private bool manaRst;
    private bool ultimateRst;
    private bool move = true;
    private bool attack1Rst;
    private bool attack2Rst;
    private bool deathRst;
    private float lifeVal = 10;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            onPlatform = true;
            body.velocity = new Vector2(collision.rigidbody.velocity.x, body.velocity.y + collision.rigidbody.velocity.y);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            onPlatform = false;
        }
    }

    private void DontMove()
    {
        move = false;
        //body.velocity = new Vector2(0, 0);
        up = 0;
        right = 0;
        left = 0;
        down = 0;
        attack = 0;
        ult = 0;
        jump = 0;
    }

    private void Move()
    {
         move = true;
         up = KeyCode.W;
         right = KeyCode.D;
         left = KeyCode.A;
         down = KeyCode.S;
         attack = KeyCode.J;
         ult = KeyCode.L;
         jump = KeyCode.K;
    }

    private void FixedUpdate()
    {
        if (sprite.flipX == true)
        {
            onGround = Physics2D.OverlapBox(new Vector2(feet.transform.position.x - 0.3f, feet.transform.position.y), new Vector2(1.3f, 0.06f), 0, ground);
        }
        else if (sprite.flipX == false)
        {
            onGround = Physics2D.OverlapBox(new Vector2(feet.transform.position.x + 0.3f, feet.transform.position.y), new Vector2(1.3f, 0.06f), 0, ground);
        }
        hit = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), enemy);
        hpItem = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), HP);
        sceneChange = Physics2D.IsTouchingLayers(this.GetComponent<BoxCollider2D>(), scene); 
        myPosition = GameObject.Find("P1 position");
        myPosition.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y + 2, myPosition.transform.position.z);
        death = Physics2D.OverlapBox(new Vector2(feet.transform.position.x + 0.3f, feet.transform.position.y), new Vector2(1.3f, 0.06f), 0, deathLayer);
    }

    void Start () {
        life = GameObject.Find("life");
        mana = GameObject.Find("mana");
        //status = GameObject.Find("lifeManaMemory");
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        //life.transform.localScale = new Vector3(status.transform.localScale.x, life.transform.localScale.y, life.transform.localScale.z);
        //mana.transform.localScale = new Vector3(status.transform.localScale.y, mana.transform.localScale.y, mana.transform.localScale.z);
    }


    void Update() {
        feetObj = GameObject.Find("Feet");
        cameraHUD = GameObject.Find("hud y camera");
        superFace = GameObject.Find("katsuperface");
        projectileXR = GameObject.Find("projectileR");
        projectileXL = GameObject.Find("projectileL");
        projectile1 = GameObject.Find("projectile1");
        ultimate = GameObject.Find("ultimate");
        ultimate2 = GameObject.Find("ultimate2");
        ultimate3 = GameObject.Find("ultimate3");
        ultimate4 = GameObject.Find("ultimate4");
        superCut = GameObject.Find("katsuper");
        superCut1 = GameObject.Find("cut1");
        superCut2 = GameObject.Find("cut2");
        superCut3 = GameObject.Find("cut3");
        superCut4 = GameObject.Find("cut4");
        superCut5 = GameObject.Find("cut5");
        superCut6 = GameObject.Find("cut6");
        rightAttack = GameObject.Find("AttackRight");
        leftAttack = GameObject.Find("AttackLeft");
        rightCrounchAttack = GameObject.Find("CrounchAttackRight");
        leftCrounchAttack = GameObject.Find("CrounchAttackLeft");
        rightAirAttack = GameObject.Find("AirAttackRight");
        leftAirAttack = GameObject.Find("AirAttackLeft");
        attack2 = GameObject.Find("attack2");
        airattack2 = GameObject.Find("airattack2");
        Physics2D.IgnoreLayerCollision(11, 10, true);
        Physics2D.IgnoreLayerCollision(11, 13, true);
        randomVal = Random.value;
        if (life.transform.localScale.x <= 0)
        {
            animator.SetBool("death", true);
        }
        if (hpItem == true)
        {
            projectileXR.GetComponent<AudioSource>().Play();
            life.transform.localScale = new Vector2(life.transform.localScale.x + 0.4f, 1);
        }
        if (sprite.flipX == true)
        {
            feet.GetComponent<BoxCollider2D>().offset = new Vector2(-0.05317497f, 0.1144819f);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("crounch") || animator.GetCurrentAnimatorStateInfo(0).IsName("crounchAttack"))
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(-0.01584501f, 0.2864861f);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.2405268f, 0.5215065f);
            }
            else
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(-0.02882385f, 0.4214224f);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.2145691f, 0.791379f);
            }
        }
        else if (sprite.flipX == false)
        {
            feet.GetComponent<BoxCollider2D>().offset = new Vector2(0.056633f, 0.1144819f);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("crounch") || animator.GetCurrentAnimatorStateInfo(0).IsName("crounchAttack"))
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0.01528263f, 0.2864861f);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.2405273f, 0.5215065f);
            }
            else
            {
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0.03198592f, 0.4214224f);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.2145691f, 0.791379f);
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") && (sprite.sprite.name == "attack1 3" || sprite.sprite.name == "attack1 4"))
        {
            if (sprite.flipX == true)
            {
                rightAttack.layer = 12;
                leftAttack.layer = 0;
            }
            else if (sprite.flipX == false)
            {
                rightAttack.layer = 0;
                leftAttack.layer = 12;
            }
        }
        else
        {
            rightAttack.layer = 0;
            leftAttack.layer = 0;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("crounchAttack") && (sprite.sprite.name == "crounch attack1 3" || sprite.sprite.name == "crounch attack1 4"))
        {
            if (sprite.flipX == true)
            {
                rightCrounchAttack.layer = 12;
                leftCrounchAttack.layer = 0;
            }
            else if (sprite.flipX == false)
            {
                rightCrounchAttack.layer = 0;
                leftCrounchAttack.layer = 12;
            }
        }
        else
        {
            rightCrounchAttack.layer = 0;
            leftCrounchAttack.layer = 0;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("airAttack2") && sprite.sprite.name == "jump attack2 3")
        {
            if (sprite.flipX == true)
            {
                rightAirAttack.layer = 12;
                leftAirAttack.layer = 0;
            }
            else if (sprite.flipX == false)
            {
                rightAirAttack.layer = 0;
                leftAirAttack.layer = 12;
            }
        }
        else
        {
            rightAirAttack.layer = 0;
            leftAirAttack.layer = 0;
        }
        if (onGround == true)
        {
            animator.SetBool("ground", true);
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack3") || animator.GetCurrentAnimatorStateInfo(0).IsName("crounchAttack") || animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate") || animator.GetCurrentAnimatorStateInfo(0).IsName("death")) && onPlatform == false)
            {
                body.velocity = new Vector2(0, 0);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") && onPlatform == false)
            {
                body.velocity = new Vector2(0, body.velocity.y);
            }
            if (Input.GetKey(up) && !(Input.GetKey(right) || Input.GetKey(left)) && mana.transform.localScale.x >= 0.25f)
            {
                if (Input.GetKeyDown(attack))
                {
                    animator.SetBool("walk", false);
                    animator.SetBool("attack2", true);
                }
                else
                {
                    animator.SetBool("attack2", false);
                }
            }
            else if (Input.GetKey(up) && !(Input.GetKey(right) || Input.GetKey(left)) && mana.transform.localScale.x < 0.25f)
            {
                if (Input.GetKeyDown(attack))
                {
                    animator.SetBool("attack", true);
                }
            }
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("death") && !animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate") && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack3") && !animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && !animator.GetCurrentAnimatorStateInfo(0).IsName("airAttack2") && !animator.GetCurrentAnimatorStateInfo(0).IsName("crounchAttack"))
            {
                if (Input.GetKeyDown(jump) && (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") || animator.GetCurrentAnimatorStateInfo(0).IsName("walk")))
                {
                    this.GetComponent<AudioSource>().Play();
                    body.velocity = new Vector2(body.velocity.x, jumpVal);
                    animator.SetBool("jump", true);
                }
                if (Input.GetKeyDown(ult) && mana.transform.localScale.x == 1)
                {
                    myPosition.transform.localScale = new Vector3(1, myPosition.transform.localScale.y, myPosition.transform.localScale.z);
                    if (randomVal <= 0.5)
                    {
                        ultimate.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                        ultimate3.GetComponent<AudioSource>().Play();
                    }
                    animator.SetBool("super", true);
                    this.transform.GetChild(17).GetComponent<Animator>().SetBool("background", true);
                    transform.GetChild(18).GetComponent<Animator>().SetBool("spark", true);
                    transform.GetChild(18).GetComponent<AudioSource>().Play();
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -9);
                }
                else if (Input.GetKey(attack) && Input.GetKey(jump))
                {
                    this.GetComponent<AudioSource>().Play();
                    body.velocity = new Vector2(body.velocity.x, jumpVal);
                    animator.SetBool("jump", true);
                }
                else if (Input.GetKey(right) && !Input.GetKeyDown(attack))
                {
                    animator.SetBool("walk", true);
                    sprite.flipX = true;
                }
                else if (Input.GetKey(left) && !Input.GetKeyDown(attack))
                {
                    animator.SetBool("walk", true);
                    sprite.flipX = false;
                }
                else if ((Input.GetKey(right) || Input.GetKey(left)) && Input.GetKeyDown(attack) && !Input.GetKey(up))
                {
                    animator.SetBool("walk", true);
                    animator.SetBool("attack", true);
                }
                else if ((Input.GetKey(right) || Input.GetKey(left)) && Input.GetKeyDown(attack) && Input.GetKey(up) && mana.transform.localScale.x >= 0.25f)
                {
                    animator.SetBool("walk", true);
                    animator.SetBool("attack2", true);
                }
                else if ((Input.GetKey(right) || Input.GetKey(left)) && Input.GetKeyDown(attack) && Input.GetKey(up) && mana.transform.localScale.x < 0.25f)
                {
                    animator.SetBool("walk", true);
                    animator.SetBool("attack", true);
                }
                else if (!(Input.GetKey(right) || Input.GetKey(left)) && Input.GetKeyDown(attack) && !Input.GetKey(up))
                {
                    animator.SetBool("walk", false);
                    animator.SetBool("attack", true);
                }
                else
                {
                    animator.SetBool("attack", false);
                    animator.SetBool("walk", false);
                }
                if (Input.GetKey(down))
                {
                    animator.SetBool("crounch", true);
                    animator.SetBool("walk", false);
                    if (onPlatform == false)
                    {
                        body.velocity = new Vector2(0, 0);
                    }            
                }
                else
                {
                    animator.SetBool("crounch", false);
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                {
                    if (sprite.flipX == true)
                    {
                        body.velocity = new Vector2(walkVal, body.velocity.y);
                    }
                    else if (sprite.flipX == false)
                    {
                        body.velocity = new Vector2(-walkVal, body.velocity.y);
                    }
                }
            }
        }
        else
        {
            animator.SetBool("ground", false);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && !animator.GetCurrentAnimatorStateInfo(0).IsName("airAttack2") && (animator.GetCurrentAnimatorStateInfo(0).IsName("jump") || animator.GetCurrentAnimatorStateInfo(0).IsName("falling")))
            {
                if (Input.GetKey(right))
                {
                    sprite.flipX = true;
                    body.velocity = new Vector2(walkVal, body.velocity.y);
                }
                else if (Input.GetKey(left))
                {
                    sprite.flipX = false;
                    body.velocity = new Vector2(-walkVal, body.velocity.y);
                }
                else
                {
                    body.velocity = new Vector2(0, body.velocity.y);
                }
                if (Input.GetKeyDown(attack))
                {
                    animator.SetBool("airAttack", true);
                }
                else
                {
                    animator.SetBool("airAttack", false);
                }
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("airAttack2"))
            {
                animator.SetBool("airAttack", false);
                if (sprite.flipX == true)
                {
                    if (Input.GetKey(right))
                    {
                        body.velocity = new Vector2(walkVal, body.velocity.y);
                    }
                    else if (Input.GetKey(left))
                    {
                        body.velocity = new Vector2(0, body.velocity.y);
                    }
                    else
                    {
                        body.velocity = new Vector2(0, body.velocity.y);
                    }
                }
                else if (sprite.flipX == false)
                {
                    if (Input.GetKey(right))
                    {
                        body.velocity = new Vector2(0, body.velocity.y);
                    }
                    else if (Input.GetKey(left))
                    {
                        body.velocity = new Vector2(-walkVal, body.velocity.y);
                    }
                    else
                    {
                        body.velocity = new Vector2(0, body.velocity.y);
                    }
                }
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("falling"))
        {
            animator.SetBool("jump", false);
        }
        if (body.velocity.y > 0)
        {
            animator.SetBool("airAtk", true);
            Physics2D.IgnoreLayerCollision(8, 11, true);
        }
        else
        {
            animator.SetBool("airAtk", false);
            Physics2D.IgnoreLayerCollision(8, 11, false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            hit = false;
            animator.SetBool("hurt", false);
        }
        else if (move == true && !animator.GetCurrentAnimatorStateInfo(0).IsName("death") && !animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate"))
        {
            if (hit == true && blink == false)
            {
                animator.SetBool("hurt", true);
            }
            else
            {
                animator.SetBool("hurt", false);
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && hurtReset == false)
        {
            this.GetComponent<AudioSource>().Stop();
            this.transform.GetChild(1).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(2).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(5).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(12).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(13).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(14).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(15).GetComponent<AudioSource>().Stop();
            if (!(life.transform.localScale.x <= 0))
            {
                life.transform.localScale = new Vector2(life.transform.localScale.x - ((1/lifeVal) + 0.01f), 1);
            }
            if (randomVal <= 0.25f)
            {
                feetObj.GetComponent<AudioSource>().Play();
            }
            else if (randomVal > 0.25f && randomVal <= 0.5f)
            {
                leftAirAttack.GetComponent<AudioSource>().Play();
            }
            else if (randomVal > 0.5f && randomVal <= 0.75f)
            {
                rightCrounchAttack.GetComponent<AudioSource>().Play();
            }
            else if (randomVal > 0.75f)
            {
                leftCrounchAttack.GetComponent<AudioSource>().Play();
            }
            if (sprite.flipX == true)
            {
                body.velocity = new Vector2(-5, 9);
            }
            else if (sprite.flipX == false)
            {
                body.velocity = new Vector2(5, 9);
            }
            hurtReset = true;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            hurtReset = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt") && sprite.sprite.name == "hurt 1" || blink == true)
        {
            blink = true;
        }
        if (blink == true)
        {
            hit = false;
            blinkVal += 0.2f;
            if (blinkVal >= 0.9f && blinkVal <= 1.1f)
            {
                sprite.color = new Color(0, 0, 0, 0);
            }
            else if (blinkVal >= 1.9f && blinkVal <= 2.1f)
            {
                sprite.color = new Color(255, 255, 255, 255);
                blinkStop += 1;
                blinkVal = 0;
            }
        }
        if (blinkStop == 7)
        {
            blink = false;
            blinkVal = 0;
            blinkStop = 0;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack3") && manaRst == false)
        {
            if (randomVal < 0.5)
            {
                projectile1.GetComponent<AudioSource>().Play();
            }
            else
            {
                projectileXL.GetComponent<AudioSource>().Play();
            }
            //mana.transform.localScale = new Vector2(mana.transform.localScale.x - 0.25f, 1);
            if (sprite.flipX == true)
            {
                Instantiate(projectileX, projectileXL.transform.position, projectileXL.transform.rotation);
            }
            else if (sprite.flipX == false)
            {
                Instantiate(projectileX, projectileXR.transform.position, projectileXR.transform.rotation);
            }
            animator.SetBool("attack2", false);
            manaRst = true;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            manaRst = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") && attack1Rst == false)
        {
            animator.SetBool("attack", false);
            if (randomVal <= 0.5)
            {
                attack2.GetComponent<AudioSource>().Play();
            }
            else
            {
                leftAttack.GetComponent<AudioSource>().Play();
            }
            rightAttack.GetComponent<AudioSource>().Play();

            attack1Rst = true;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            attack1Rst = false;
            projectileRst = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1") && sprite.sprite.name == "attack1 3" && projectileRst == false)
        {
            if (sprite.flipX == true)
            {
                Instantiate(projectile, transform.GetChild(19).transform.position, transform.GetChild(19).transform.rotation);
            }
            else if (sprite.flipX == false)
            {
                Instantiate(projectile, transform.GetChild(20).transform.position, transform.GetChild(20).transform.rotation);
            }
            projectileRst = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("airAttack2") && attack2Rst == false)
        {
            rightAttack.GetComponent<AudioSource>().Play();
            if (randomVal <= 0.5)
            {
                airattack2.GetComponent<AudioSource>().Play();
            }
            else
            {
                rightAirAttack.GetComponent<AudioSource>().Play();
            }
            attack2Rst = true;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("airAttack2"))
        {
            attack2Rst = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate"))
        {
            hit = false;
            sprite.color = new Color(255, 255, 255, 255);
            blinkStop = 5;
            animator.SetBool("super", false);
            this.transform.GetChild(17).GetComponent<Animator>().SetBool("background", false);
            transform.GetChild(18).GetComponent<Animator>().SetBool("spark", false);
        }
        if (superFace.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("katSuperFace"))
        {
            superFace.GetComponent<Animator>().SetBool("super", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate") && sprite.sprite.name == "pose1 1")
        {
            ultimate2.GetComponent<AudioSource>().Play();
            ultimate4.GetComponent<AudioSource>().Play();
            superCut.GetComponent<Animator>().SetBool("cut", true);
            superCut1.GetComponent<Animator>().SetBool("cut", true);
            superCut2.GetComponent<Animator>().SetBool("cut", true);
            superCut3.GetComponent<Animator>().SetBool("cut", true);
            superCut4.GetComponent<Animator>().SetBool("cut", true);
            superCut5.GetComponent<Animator>().SetBool("cut", true);
            superCut6.GetComponent<Animator>().SetBool("cut", true);
            //mana.transform.localScale = new Vector2(4, 1);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1f);
            myPosition.transform.localScale = new Vector3(0, myPosition.transform.localScale.y, myPosition.transform.localScale.z);
        }
        else
        {
            superCut.GetComponent<Animator>().SetBool("cut", false);
            superCut1.GetComponent<Animator>().SetBool("cut", false);
            superCut2.GetComponent<Animator>().SetBool("cut", false);
            superCut3.GetComponent<Animator>().SetBool("cut", false);
            superCut4.GetComponent<Animator>().SetBool("cut", false);
            superCut5.GetComponent<Animator>().SetBool("cut", false);
            superCut6.GetComponent<Animator>().SetBool("cut", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate") && (sprite.sprite.name == "pose1 1" || sprite.sprite.name == "pose1 2"))
        {
            cameraHUD.gameObject.layer = 14;
        }
        else
        {
            cameraHUD.gameObject.layer = 0;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate") && ultimateRst == false)
        {
            body.velocity = new Vector2(0, 0);
            //mana.transform.localScale = new Vector2(mana.transform.localScale.x - 1f, 1);
            ultimateRst = true;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate"))
        {
            ultimateRst = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            body.velocity = new Vector2(0, 0);
            life.transform.localScale = new Vector2(0, 1);
            mana.transform.localScale = new Vector2(0, 1);
            this.GetComponent<AudioSource>().Stop();
            this.transform.GetChild(1).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(2).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(5).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(12).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(13).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(14).GetComponent<AudioSource>().Stop();
            this.transform.GetChild(15).GetComponent<AudioSource>().Stop();
            hit = false;
            sprite.color = new Color(255, 255, 255, 255);
            blinkStop = 5;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("crounchAttack") && crounchRst == false)
        {
            rightAttack.GetComponent<AudioSource>().Play();
            if (randomVal <= 0.5)
            {
                attack2.GetComponent<AudioSource>().Play();
            }
            else
            {
                leftAttack.GetComponent<AudioSource>().Play();
            }
            animator.SetBool("attack", false);
            crounchRst = true;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("crounchAttack"))
        {
            crounchRst = false;
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ultimate") && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            mana.transform.localScale = new Vector2(mana.transform.localScale.x + 0.0005f, 1);
        }
        if (life.transform.localScale.x >= 1)
        {
            life.transform.localScale = new Vector2(1, 1);
        }
        if (mana.transform.localScale.x >= 1)
        {
            mana.transform.localScale = new Vector2(1, 1);
        }
        if (sceneChange == true || myPosition.transform.localScale.y == 1 || animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            DontMove();
        }
        else if (myPosition.transform.localScale.y == 0)
        {
            Move();
        }
        if (death)
        {
            animator.SetBool("death2", true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("death") && sprite.sprite.name == "hurt 5" && deathRst == false)
        {
            this.transform.GetChild(16).GetComponent<AudioSource>().Play();
            deathRst = true;
        }
        if (sprite.sprite.name == "explosions_0" && deathRst == true)
        {
            this.transform.GetChild(17).GetComponent<AudioSource>().Play();
            deathRst = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            animator.SetBool("attack", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
        {
            animator.SetBool("attack2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("crounchAttack"))
        {
            animator.SetBool("attack", false);
        }

        //status.transform.localScale = new Vector3(life.transform.localScale.x, mana.transform.localScale.x, status.transform.localScale.z);
    }
}
