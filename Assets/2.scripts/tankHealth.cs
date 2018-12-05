using UnityEngine;
using System.Collections;

public class tankHealth : MonoBehaviour
{

    public float fullHealth = 100f;
    public GameObject effect;//特效s
    private float currentHealth;
    private bool isAlive;

    private void OnEnable()
    {
        currentHealth = fullHealth;
        isAlive = true;
    }

    public void TakeDamage(float number) {
        currentHealth -= number;
        if (currentHealth <= 0f && isAlive == true) {
            OnDeath();
        }
    }
    private void OnDeath() {
        Instantiate (effect, transform.position, transform.rotation);
        isAlive = false;
        if (gameObject.tag!="Player") {
            gameObject.SetActive(false);
        }
    }
    public float GetHealthPercent() {
        return currentHealth / fullHealth;
    }
    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void Update()
    {

    }
}
