using UnityEngine;
using System.Collections;

public class EnemyController : Unit {

    float moveX;
    float moveY;

    float attackForce;

    bool isGrounded;
    bool attackRange;
    bool facingRight;

    Rigidbody2D enemyBody;

    public LayerMask whatIsEnemy;
    public LayerMask whatIsGround;
    public Transform rangeCheck;
    public Transform groundCheck;

    public GameObject player;
    public GameObject attackArea;

	// Use this for initialization
	void Start () {
        attackForce = 50.0f;
        enemyBody = this.GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        maxHealth = 100.0f;
	}
	
	// Update is called once per frame
	void Update () {

        attackRange = Physics2D.OverlapCircle(rangeCheck.position, 1.1f, (whatIsEnemy));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.6f, (whatIsGround));

        FlipEnemy();
        if (attackRange == false && isGrounded == true)
        {
            Move("yes");
        }
        else
        {
            Action();
        }

        if (this.gameObject.transform.position.x > 40.0f || this.gameObject.transform.position.x < -40.0f)
        {
            GameController.control.AddGameTime(5.0f);
            KillMe();
        }

        if (this.gameObject.transform.position.y < -10.0f)
        {
            GameController.control.AddGameTime(5.0f);
            KillMe();
        }
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerAttack")
        {
            TakeDamage();
        }
    }

    public override void Action()
    {
        //if (attackRange == true)
        //{
        //    InvokeRepeating("Attack", 0.5f, 2.0f);
        //}
        //if (attackRange == false)
        //{
        //    CancelInvoke();
        //}
    }

    public override void Move(string dir)
    {
        if (isGrounded)
        {
            if (dir != "zero")
            {
                if (facingRight)
                {
                    enemyBody.velocity = new Vector2((moveSpeed * 0.5f), enemyBody.velocity.y);
                }
                else
                {
                    enemyBody.velocity = new Vector2(((moveSpeed * 0.5f) * -1.0f), enemyBody.velocity.y);
                }
            }
            else
            {
                enemyBody.velocity = new Vector2(0.0f,0.0f);
            }
            
        }
    }

    public void TakeDamage()
    {
        healthPoints = ((healthPoints +10.0f) * 2.0f);

        float forceToAddX = ((attackForce*2.0f) + healthPoints) * 1.5f;
        float forceToAddY = ((attackForce+10.0f) + healthPoints) * 1.2f;
        if (facingRight)
        {
            forceToAddX = forceToAddX * -1.0f;
        }
        enemyBody.AddForce(new Vector2(forceToAddX, forceToAddY));
    }

    public void FlipEnemy()
    {
        if (facingRight)
        {
            if (player.gameObject.transform.position.x < this.gameObject.transform.position.x)
            {
                Vector3 theScale = this.transform.localScale;
                theScale.x *= -1;
                this.transform.localScale = theScale;
                facingRight = !facingRight;
            }
        }
        else if (!facingRight)
        {
            if (player.gameObject.transform.position.x > this.gameObject.transform.position.x)
            {
                Vector3 theScale = this.transform.localScale;
                theScale.x *= -1;
                this.transform.localScale = theScale;
                facingRight = !facingRight;
            }
        }        
    }

    public void Attack()
    {
        if (facingRight)
        {
            Instantiate(attackArea, (this.gameObject.transform.position + (new Vector3(0.2f, 0.0f, 0.0f))), this.gameObject.transform.rotation);
        }
        else
        {
            Instantiate(attackArea, (this.gameObject.transform.position + (new Vector3(-0.2f, 0.0f, 0.0f))), this.gameObject.transform.rotation);
        }
    }

    public void KillMe()
    {
        Destroy(this.gameObject);
    }
}
