using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour
{
    public HudManager hud;
    public float Health = 150f;
    public Sprite SpriteShownWhenHurt;
    private float ChangeSpriteHealth;
    public GameManager lvl;
    // Use this for initialization
    void Start()
    {
        ChangeSpriteHealth = Health - 30f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        //if we are hit by a bird
        if (col.gameObject.tag == "Bird")
        {
            GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            lvl.IncreaseScore(50);
            hud.Refresh();
        }
        else //we're hit by something else
        {
            //calculate the damage via the hit object velocity
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            Health -= damage;
            //don't play sound for small damage
            if (damage >= 10)
                GetComponent<AudioSource>().Play();

            if (Health < ChangeSpriteHealth)
            {//change the shown sprite
                GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
            }
            if (Health <= 0)
            {
                Destroy(this.gameObject);
                lvl.IncreaseScore(50);
                hud.Refresh();
            }
        }
    }

    //sound found in
    //https://www.freesound.org/people/yottasounds/sounds/176731/
}
