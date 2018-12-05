using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
	public GameObject effect;//特效
    public AudioClip[] hitsound;
    public AudioSource tankSource;
    private float volLowRange = 0.5f;
    private float volHighRange = 0.9f;
	void Start () {
        Destroy(gameObject, 3);
        tankSource = GameObject.FindWithTag("hit").GetComponent<AudioSource>();
    }

	void Update () {
    }

	void OnCollisionEnter (Collision collision) {//碰撞發生時呼叫
		//碰撞後產生爆炸

        Instantiate (effect, transform.position, transform.rotation);
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Player") {
            tankHealth targetHealth = collision.gameObject.GetComponent<tankHealth>();
            if (!targetHealth) {
                return;
            }
            float vol = Random.Range(volLowRange, volHighRange);
            tankSource.PlayOneShot(hitsound[Random.Range(0,hitsound.Length)], vol);
            float damage = collision.gameObject.tag == "Player"? 10f: 5f;
            damage += (gameObject.tag == "bullet"? 0f: 5f);
            targetHealth.TakeDamage(damage);
        }
        Destroy(gameObject);//刪除砲彈
		// }
	}
}
