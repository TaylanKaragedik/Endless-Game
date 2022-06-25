using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public static Vector3 direction;
    public static float forwardSpeed;
    public float maxSpeed;
    private int soundCounter = 0;

    private int desiredLane = 1;// 0:left - 1:Middle - 2:Right
    public int laneDistance = 4;// the distance between two lanes

    public static float jumpForce = 8;
    public static float gravity = -4;

    public Animator animator;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!PlayerManager.isGameStarted)
            return;

        animator.SetBool("isGameStarted", true);

        direction.z = forwardSpeed;

        //Move The Player
        controller.Move(direction * Time.fixedDeltaTime);

        //take the inputs on which lane we should be
        //Keybord Controls
        //if (Input.GetKeyDown(KeyCode.RightArrow))// Go right
        //{
        //    if(desiredLane != 2)
        //    {
        //        desiredLane++;
        //    }
        //}


        //if (Input.GetKeyDown(KeyCode.LeftArrow))// Go left
        //{
        //    if (desiredLane != 0)
        //    {
        //        desiredLane--;
        //    }
        //}

        //Swipe Controls

        if (SwipeManager.swipeDown)
        {
            StartCoroutine(Down());
        }

        if (SwipeManager.swipeRight)// Go right
        {
            if (desiredLane != 2)
            {
                desiredLane++;
            }
        }


        if (SwipeManager.swipeLeft)// Go left
        {
            if (desiredLane != 0)
            {
                desiredLane--;
            }
        }

        //calculate where we should be in the future

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;


        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * (laneDistance - 1.5f); //Move Left the game object
        }
        
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * (laneDistance - 1.5f);//Move Right the game object
        }

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
        //controller.center = controller.center;


        //Jump


        animator.SetBool("isGrounded", controller.isGrounded);

        if (controller.isGrounded == true && PlayerManager.numberOfCoins >= 10)//eğer yerde ise
        {

            if (SwipeManager.swipeUp)//zıpla
            {
                Jump();
                PlayerManager.numberOfCoins -= 10; 
            }
        }
        else
        {
            direction.y += gravity * Time.fixedDeltaTime; //gravity addition
        }

        ////Hızlanma denemeleri vol: 1
        //if (TileManager.destroyCount == 5)
        //{
        //    forwardSpeed += 1.5f;
        //    TileManager.destroyCount = 0;
        //}

        //Hızlanma denemeleri vol: 2
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }
       
        //Debug.Log("Position:" + transform.position.x +"       " + "Speed:" + forwardSpeed);
   
        //karakteri ortalama algoritması
        if (transform.position == targetPosition)
        {
            return;
        }

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }

    }

    private void Jump ()
    {
        direction.y = jumpForce;
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)// on Collision
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            if(soundCounter == 0)
            {
                FindObjectOfType<SoundManager>().PlaySound("FailSound");
                soundCounter++;
            }
            else
            return;
        }
    }

    private IEnumerator Down()
    {
        animator.SetBool("isDown", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.3f - (maxSpeed/10));
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isDown", false);
    }
}
