using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public static GameObject player;
    public static GameObject currentPlatform;
    bool canTurn = false;
    Vector3 startPosition;
    Rigidbody rb;
    public GameObject destroyWall;
    public Transform destroyWallPos;
    Rigidbody wRb;

    public static bool isDead = false;


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Fire" || other.gameObject.tag == "Wall")
        {
            anim.SetTrigger("isDead");
            isDead = true;
        }
        else
        {
            currentPlatform = other.gameObject;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        wRb = destroyWall.GetComponent<Rigidbody>();
        player = this.gameObject;
        startPosition = player.transform.position;
        GenerateWorld.RunDummy();
    }
    void DestroyWall()
    {
        destroyWall.transform.position = destroyWallPos.position;
        destroyWall.SetActive(true);
        wRb.AddForce(this.transform.forward * 4000);
        Invoke("StopWall", 2);
    }

    void StopWall()
    {
        destroyWall.SetActive(false);
    }
   

    void OnTriggerEnter(Collider other)
    {
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "TSection")
            GenerateWorld.RunDummy();

        if (other is SphereCollider)
            canTurn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is SphereCollider)
            canTurn = false;
    }


    void StopAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isDead) return;

        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isAttacking") == false)
        {
            anim.SetBool("isAttacking", true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.Translate(0.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) && canTurn)
        {                                                   
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "TSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.D) && canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "TSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);

        }
          
    }
}
