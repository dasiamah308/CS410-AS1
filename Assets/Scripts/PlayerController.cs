using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private int jumps_left = 2;
    public int jump_power = 200;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Update(){

        if (Input.GetKeyDown("space"))
        {
            if(jumps_left > 0){
            Debug.Log("space key was pressed");
            rb.AddForce(Vector3.up * jump_power);
            jumps_left --;
            }
        }
        
    }
     void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Ground")){
            Debug.Log("hit the ground?");
            jumps_left = 2;
        }

        else if (other.gameObject.CompareTag("Enemy"))
        {
            // Destroy the player
            Destroy(gameObject);
            // Display "You Lose!"
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
     }
     void OnMove (InputValue movementValue)
    {    
        
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }
    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if (count >= 3)
       {
           winTextObject.SetActive(true);
           Destroy(GameObject.FindGameObjectWithTag("Enemy"));
       }
   }

    private void FixedUpdate() 
   {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        /*if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");
            rb.AddForce(Vector3.up * jump_power);
        }*/
        
   }

   void OnTriggerEnter(Collider other) 
   {
    if (other.gameObject.CompareTag("PickUp")) 
       {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
       }
   } 
}
