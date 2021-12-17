using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;


public class PlayerMovement : MonoBehaviour
{
    public CanvasGroup UI;
    public Rigidbody player;
    private float speed = 25.0f;
    private bool grounded;
    public float score = 0;


    [DllImport("SpeedUp")]
    private static extern int SpeedUp(int min, int max);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = new Vector3(0, 10, 0);//Resets the position of the player
        }
        if (UI.blocksRaycasts == false)//Checks if in edit or play mode
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));//Moving left, right, up, and down
            player.MovePosition(transform.position + movement * Time.fixedDeltaTime * speed);//Adding all the different movement together
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")//collision check for ground
        {
            grounded = true;
        }

        if (collision.gameObject.tag == "Respawn")//collision check for win
        {
            transform.position = new Vector3(-60, 1, 30);
            grounded = true;
        }

        if (collision.gameObject.tag == "Enemy")//collision check for enemy
        {
            score++;
            Debug.Log("+1");
            Destroy(collision.gameObject);
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
