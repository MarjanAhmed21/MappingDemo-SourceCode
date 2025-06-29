using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerScript : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;
    public float airControlForce = 10.0f;
    public float airControlMax = 1.5f;
    Vector2 boxExtents;
    Animator animator;
    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    Collider2D mainCollider;
    // Check every collider except Player and Ignore Raycast
    LayerMask layerMask = ~(1 << 2 | 1 << 8);
    Transform t;
    int totalMints;
    int mintsCollected;
    public Text uiText;
    public GameObject HeadBackText;
    public GameObject CompleteScrn;
    public GameObject Player;
    public GameObject Tools;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        gameObject.layer = 8;
        animator = GetComponent<Animator>();
        mintsCollected = 0;
        totalMints = GameObject.FindGameObjectsWithTag("Mints").Length;

    }

    // Update is called once per frame
    void Update()
    {
        


        // Movement controls
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (isGrounded || r2d.velocity.x > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
            float xSpeed = Mathf.Abs(r2d.velocity.x);
            animator.SetFloat("xspeed", xSpeed);
            float ySpeed = r2d.velocity.y;
            animator.SetFloat("yspeed", ySpeed);
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
                float xSpeed = Mathf.Abs(r2d.velocity.x);
                animator.SetFloat("xspeed", xSpeed);
                float ySpeed = r2d.velocity.y;
                animator.SetFloat("yspeed", ySpeed);
            }
            
        }
        string uiString = mintsCollected + "/" + totalMints;
        uiText.text = uiString;

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        Bounds colliderBounds = mainCollider.bounds;
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, 0.1f, 0);
        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, 0.23f, layerMask);

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, 0.23f, 0), isGrounded ? Color.green : Color.red);

        Vector2 bottom = new Vector2(transform.position.x, transform.position.y - boxExtents.y);
        Vector2 hitBoxSize = new Vector2(boxExtents.x * 2.0f, 0.05f);

        RaycastHit2D result = Physics2D.BoxCast(bottom, hitBoxSize, 0.0f, new Vector3(0.0f, -1.0f),
            0.0f, 1 << LayerMask.NameToLayer("Ground"));


        bool grounded = result.collider != null && result.normal.y > 0.9f;
        if (grounded)
        {
            if (Input.GetAxis("Jump") > 0.0f)
                r2d.AddForce(new Vector2(0.0f, jumpHeight), ForceMode2D.Impulse);
            else
                r2d.velocity = new Vector2(maxSpeed * h, r2d.velocity.y);
        }
        else
        {
            // allow a small amount of movement in the air
            float vx = r2d.velocity.x;
            if (h * vx < airControlMax)
                r2d.AddForce(new Vector2(h * airControlForce, 0));
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Mints")
        {
            Destroy(coll.gameObject);
            mintsCollected++;
            if(mintsCollected == totalMints)
            {
                Destroy(GameObject.FindGameObjectWithTag("Cage"));
                HeadBackText.gameObject.SetActive(true);
            }
        }

        if (coll.gameObject.tag == "Goal")
        {
            Destroy(coll.gameObject);
            CompleteScrn.gameObject.SetActive(true);
            Player.gameObject.SetActive(false);
            Tools.gameObject.SetActive(false);

        }
    }
    }
