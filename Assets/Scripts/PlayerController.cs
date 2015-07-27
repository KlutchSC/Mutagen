using UnityEngine;
using System.Collections;

public class PlayerController : Unit {

    Rigidbody2D myBody;
    string curMutagen;

    bool jumpCheck;
    bool facingRight = true;

    int mutantPoints;

    public LayerMask whatIsGround;
    public Transform groundCheck;

    public GameObject attackArea;

    public GameObject humanSprite;
    public GameObject flyingSprite;
    public GameObject piggySprite;
    public GameObject foxSprite;

    void Start()
    {
        maxHealth = 100.0f;
        healthPoints = maxHealth;

        mutantPoints = 20;

        myBody = this.GetComponent<Rigidbody2D>();

        curMutagen = "human";
    }

    void Update()
    {
        jumpCheck = Physics2D.OverlapCircle(groundCheck.position, 0.5f, (whatIsGround));
        CheckPlayerInput();
    }

    public override void Move(string dir)
    {
        if (dir == "Left") {
            if (facingRight)
            {
                SwapPlayer();
            }
            if (curMutagen == "cheetah")
            {
                myBody.velocity = new Vector2(((moveSpeed*2.0f)*-1.0f), myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2((moveSpeed*-1.0f), myBody.velocity.y);
            }
        }
        if (dir == "Right")
        {
            if (!facingRight)
            {
                SwapPlayer();
            }
            if (curMutagen == "cheetah")
            {
                myBody.velocity = new Vector2((moveSpeed*2.0f), myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }
        if (dir == "Up")
        {
            if (curMutagen == "eagle")
            {
                myBody.velocity = new Vector2(myBody.velocity.x, 5.0f);
            }
            else
            {
                if (jumpCheck == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, 5.0f);
                }
            }
        }
        if (dir == "Zero")
        {
            myBody.velocity = new Vector2(0.0f, myBody.velocity.y);
        }
    }

    public override void Action()
    {
        if (curMutagen == "gorilla")
        {
            if (facingRight)
            {
                Instantiate(attackArea, (this.gameObject.transform.position + (new Vector3(1.8f, 0.0f, 0.0f))), this.gameObject.transform.rotation);
            }
            else
            {
                Instantiate(attackArea, (this.gameObject.transform.position + (new Vector3(-1.8f, 0.0f, 0.0f))), this.gameObject.transform.rotation);
            }
        }
        if (facingRight)
        {
            Instantiate(attackArea, (this.gameObject.transform.position + (new Vector3(0.8f, 0.0f, 0.0f))), this.gameObject.transform.rotation);
        }
        else
        {
            Instantiate(attackArea, (this.gameObject.transform.position + (new Vector3(-0.8f, 0.0f, 0.0f))), this.gameObject.transform.rotation);
        }
    }

    public void Swap(string swapValue)
    {
        if (swapValue != "human")
        {
            CancelInvoke();
            InvokeRepeating("CountDownTimer", 1.0f, 1.0f);
            if (swapValue == "eagle")
            {
                humanSprite.SetActive(false);
                flyingSprite.SetActive(true);
                piggySprite.SetActive(false);
                foxSprite.SetActive(false);
            }
            else if (swapValue == "gorilla")
            {
                humanSprite.SetActive(false);
                flyingSprite.SetActive(false);
                piggySprite.SetActive(true);
                foxSprite.SetActive(false);
            }
            else if (swapValue == "cheetah")
            {
                humanSprite.SetActive(false);
                flyingSprite.SetActive(false);
                piggySprite.SetActive(false);
                foxSprite.SetActive(true);
            }
        }
        else
        {
            CancelInvoke();
            InvokeRepeating("AddMutantPoints", 1.0f, 1.0f);
            humanSprite.SetActive(true);
            flyingSprite.SetActive(false);
            piggySprite.SetActive(false);
            foxSprite.SetActive(false);
        }
        curMutagen = swapValue;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {
            healthPoints = healthPoints - 10.0f;
            //Debug.Log(healthPoints);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {
            healthPoints = healthPoints - 10.0f;
            //Debug.Log("hitin trigger");
        }
        if (other.gameObject.tag == "timePickup")
        {
            GameController.control.AddGameTime(10.0f);
            Destroy(other.gameObject);
        }
    }

    void SwapPlayer()
    {
        facingRight = !facingRight;
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    void CountDownTimer()
    {
        mutantPoints = mutantPoints - 3;
        if (mutantPoints <= 0)
        {
            mutantPoints = 0;
            Swap("human");
        }
        //Debug.Log("Losing..." + mutantPoints);
    }

    void AddMutantPoints()
    {
        mutantPoints++;
        if (mutantPoints >= 100)
        {
            mutantPoints = 100;
        }
        //Debug.Log("Gaining..." + mutantPoints);
    }

    void CheckPlayerInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Move("Right");
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move("Left");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move("Up");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Action();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Move("Zero");
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            Swap("cheetah");
            Debug.Log("Cheetah mode engage!");
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            Swap("gorilla");
            Debug.Log("Swapped to gorilla mode!");
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            Swap("eagle");
            Debug.Log("Time to fly in eagle mode!");
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            Swap("human");
            Debug.Log("Back to human mode");
        }
    }
}