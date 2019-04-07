using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    Animator anim;
    public static int bossHealth = 100, playerHealth = 50;
    public Material blueFire, orangeFire;
    public GameObject orange, blue, red;
    private int randomNumber;
    private bool attackGo = false;
    public Transform raycastCenter;
    public WandController controller;
    public Transform player;
    public GameObject shield;

    public GameObject pointer, button;
    public AudioSource win, loss, shieldSound, damageDealt, playerDamage;


	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        InvokeRepeating("attack", 3f, 14f);
        InvokeRepeating("changeColor", 3f, 6f);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(shield.transform.up);

        randomNumber = Random.Range(2, 5);
        if (bossHealth <= 0)
        {
            anim.SetBool("die", true);
            Destroy(gameObject);
            pointer.SetActive(true);
            button.SetActive(true);
            win.Play();
            return;
        }
        if (playerHealth <= 0)
        {
            loss.Play();
            pointer.SetActive(true);
            button.SetActive(true);
            return;
        }
        //Debug.Log("Boss health: " + bossHealth);
        //Debug.Log("Player health" + playerHealth);
        //Debug.Log(randomNumber);
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.forward, new Color(0f, 0f, 1.0f));
    }

    //Randomly change the outline of the enemy
    private void changeColor()
    {
        //Debug.Log("Changing Color");
        if (red.activeSelf == false)
        {
            if (Random.Range(0, 2) == 0)
                orange.SetActive(true);
            else
                blue.SetActive(true);
        }

    }

    private void attack()
    {
        //Debug.Log("Boss attacking");
        if (bossHealth > 0)
        {
            //turn red
            orange.SetActive(false);
            blue.SetActive(false);
            red.SetActive(true);
            attackGo = true;
            //attack
            StartCoroutine("delayedAttack");

        }
    }  

    IEnumerator delayedAttack ()
    {
        yield return new WaitForSeconds(2f);
        if (attackGo == true)
        {
            anim.SetTrigger("attack");

            //No matter what the raycast points in a different direction than intended

            //Raycast from center to see if player holding up shield
            /*RaycastHit hit;
            Vector3 direction = raycastCenter.transform.position - player.transform.position;
            Ray ray = new Ray(raycastCenter.transform.position, raycastCenter.transform.right);
            bool hitted = Physics.Raycast(ray, out hit, 5f);
            //Debug.Log("Hitting Object: " +hit.collider.gameObject);
            if (hitted != false && hit.collider.gameObject.tag != "Shield")
            {
                Debug.Log("HITTING PLAYER");
                //Damage the player with a random value
                playerHealth -= Random.Range(0, 10);
                //Play effect here
                
            }*/

            //Check if shield is up
            if (shield.transform.up.x < 0.5)
            {
                Debug.Log("HITTING PLAYER");
                //Damage the player with a random value
                playerHealth -= Random.Range(0, 10);
                playerDamage.Play();
                //Play effect here
            }
            else
            {
                Debug.Log("MISSED PLAYER");
                shieldSound.Play();
            }

        }
        attackGo = false;
        yield return new WaitForSeconds(0.5f);
        red.SetActive(false);
        if (Random.Range(0,2) == 0)
           orange.SetActive(true);
        else
           blue.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hitting");

        if ((orange.activeSelf == true && controller.orange == true && blue.activeSelf == false && controller.blue == false) 
            || (blue.activeSelf == true && controller.blue == true && orange.activeSelf == false && controller.orange == false))
        {
            anim.SetTrigger("damage");
            bossHealth -= Random.Range(0, 4);
            damageDealt.Play();
        }

        //Stop the enemy from attacking and give back damage ONLY IF PROJECTILE
        if (attackGo == true && collision.collider.tag != "Weapon")
        {
            anim.SetTrigger("damage");
            bossHealth -= Random.Range(0, 4);
            attackGo = false;
        }
        else if (collision.collider.tag != "Weapon")
        {
            if (orange.activeSelf == true && controller.orange == true || (blue.activeSelf == true && controller.blue == true)) {
                Destroy(collision.collider.gameObject);
                anim.SetTrigger("damage");
                bossHealth -= Random.Range(0, 4);
                damageDealt.Play();
            }
        }

        /*if (collision.collider.gameObject.transform.GetChild(0).GetComponent<Renderer>().material == blueFire)
            Debug.Log("blue fire");
        else if (collision.collider.gameObject.transform.GetChild(0).GetComponent<Renderer>().material == orangeFire)
            Debug.Log("orange fire");*/
    }
}
