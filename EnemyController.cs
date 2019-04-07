using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private float seconds = 10f;
    private GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("PlayerLoc");
        
        //StartCoroutine("DestroyAfterSeconds");
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(seconds);
        if (gameObject)
        {
            Destroy(gameObject);
            EnemySpawner.currentEnemies--;
        }
    }

    // Update is called once per frame
    void Update () {
        transform.LookAt(player.transform);
        transform.Translate(transform.forward * Time.deltaTime);

        Vector3 ghostPosition = Random.insideUnitSphere;
        ghostPosition = gameObject.transform.position - ghostPosition;
        float speed = Random.Range(0, 1f);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ghostPosition, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //Game Over
            EnemySpawner.currentEnemies--;
        }
    }
}
