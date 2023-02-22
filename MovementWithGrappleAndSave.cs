using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    float moveInput;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    private Vector3 direction;
    private Rigidbody2D rb;

    bool jumpOn;


    public GameObject Coin;
    public LineRenderer line;
    public SpringJoint2D joint;
    public LayerMask grapple;
    public GameObject groundRayObject;
    public float pull = .5f;


    public GameObject Rayline;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpOn = false;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {

        //Movement
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        RaycastHit2D hitGround = Physics2D.Raycast(groundRayObject.transform.position, Vector2.down);
        Debug.DrawRay(groundRayObject.transform.position, Vector2.down * hitGround.distance, Color.red);

        if (hitGround.collider != null)
        {
            if(hitGround.distance<= 0.2)
            {
                jumpOn = true;
                Rayline.GetComponent<SpriteRenderer>().color = Color.blue;
            }

            else
            {
                jumpOn = false;
                Rayline.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }


    }

    private void Update()
    {
        line.SetPosition(0, transform.position);
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 10, grapple);

            if (hit)
            {
                line.enabled = true;
                joint.enabled = true;
                joint.connectedAnchor = hit.point;
                line.SetPosition(1, hit.point);

            }

            

        }

        
        else if (Input.GetMouseButtonUp(0))
        {
            line.enabled = false;
            joint.enabled = false;
        }

      
        if(joint.enabled)
        {
            joint.distance -= pull;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpOn == true)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else return;



        }


        if (Input.GetKeyDown(KeyCode.F5))
        {
            PlayerPrefs.SetInt("CoinAmmount", GameObject.FindGameObjectsWithTag("Coin").Length);
            PlayerPrefs.SetFloat("PlayerX", transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", transform.position.y);
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Coin").Length; i++)
            {
                PlayerPrefs.SetFloat("CoinX" + i, GameObject.FindGameObjectsWithTag("Coin") [i].transform.position.x);
                PlayerPrefs.SetFloat("CoinY" + i, GameObject.FindGameObjectsWithTag("Coin") [i].transform.position.y);

            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Coin").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Coin") [i]);
            }

            transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
            for (int i = 0; i < PlayerPrefs.GetInt("CoinAmmount"); i++)
            {
                Instantiate(Coin, new Vector3(PlayerPrefs.GetFloat("CoinX" +i), PlayerPrefs.GetFloat("CoinY" +i), 0), Quaternion.identity);
            }

        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
        }
    }






}
