using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Player;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    [SerializeField]
    private GameObject bulletReference;

    public float movementX;

    [SerializeField]
    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;

    private bool enoughAmmo = false;

    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";
    private string COIN_TAG = "Coin";
    private string PISTOL_TAG = "Pistol";

    private bool isGrounded = true;
    private GameObject bullet;

    // Audio related fields
    public AudioClip jumpSound;
    public AudioClip landSound;
    private AudioSource audioSource;


    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

    }

    //below delegate to send infos about a specific events
    public delegate void mainDelegate();

    public static event mainDelegate gameOver;
    public static event mainDelegate getCoin;
    public static event mainDelegate getAmmo;
    public static event mainDelegate schoot;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool(WALK_ANIMATION, false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    private void FixedUpdate()
    {
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (movementX > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            anim.SetBool(WALK_ANIMATION, true);
        }
        else if (movementX < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            anim.SetBool(WALK_ANIMATION, true);
        }
        else
            anim.SetBool(WALK_ANIMATION, false);

    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
            audioSource.PlayOneShot(landSound);
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            PlayerDied();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag(ENEMY_TAG))
        {
            PlayerDied();
            Destroy(gameObject);

        }
        if (collision.CompareTag(COIN_TAG))
        {
            gotCoin();
            Destroy(collision.gameObject);

        }
        if (collision.CompareTag(PISTOL_TAG))
        {
            gotAmmo();
            Destroy(collision.gameObject);


        }
    }

        void GetUp() // used no more
    {

        if ((Mathf.Abs(transform.rotation.eulerAngles.z) > 89f && Mathf.Abs(transform.rotation.eulerAngles.z) < 279f) && isGrounded)
        {
            isGrounded = false;
            // myBody.AddForce(new Vector2(0f, 3), ForceMode2D.Impulse);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            transform.position += new Vector3(0f, 1f, 0f) * Time.deltaTime * moveForce;
        }

    }

    void ableToAttackListener()
    {
        enoughAmmo = true;
    }

    void PlayerDied()
    {
        if (gameOver != null)
        {
            gameOver();
        }
    }

    void gotCoin()
    {
        if (getCoin != null)
        {
            getCoin();
        }
    }

    void gotAmmo()
    {
        if (getAmmo != null)
        {
            getAmmo();
        }
    }

    void takeShot()
    {
        if (schoot != null)
        {
            schoot();
        }
    }
}


