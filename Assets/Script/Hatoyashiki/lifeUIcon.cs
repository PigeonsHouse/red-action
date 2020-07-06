using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifeUIcon : MonoBehaviour
{
    public Sprite fulllife;
    public Sprite emptylife;
    private GameObject heart1;
    private GameObject heart2;
    private GameObject heart3;
    private GameObject heart4;
    private GameObject heart5;
    private SpriteRenderer sprRen1;
    private SpriteRenderer sprRen2;
    private SpriteRenderer sprRen3;
    private SpriteRenderer sprRen4;
    private SpriteRenderer sprRen5;

    void Start(){
        heart1 = GameObject.Find( "heart1" );
        heart2 = GameObject.Find( "heart2" );
        heart3 = GameObject.Find( "heart3" );
        heart4 = GameObject.Find( "heart4" );
        heart5 = GameObject.Find( "heart5" );
        sprRen1 = heart1.GetComponent<SpriteRenderer>();
        sprRen2 = heart2.GetComponent<SpriteRenderer>();
        sprRen3 = heart3.GetComponent<SpriteRenderer>();
        sprRen4 = heart4.GetComponent<SpriteRenderer>();
        sprRen5 = heart5.GetComponent<SpriteRenderer>();
    }

    public void SetPlayerLifeUI(int health){
        if (health == 5){
            sprRen1.sprite = fulllife;
            sprRen2.sprite = fulllife;
            sprRen3.sprite = fulllife;
            sprRen4.sprite = fulllife;
            sprRen5.sprite = fulllife;
        }
        if (health == 4){
            sprRen1.sprite = fulllife;
            sprRen2.sprite = fulllife;
            sprRen3.sprite = fulllife;
            sprRen4.sprite = fulllife;
            sprRen5.sprite = emptylife;
        }
        if (health == 3){
            sprRen1.sprite = fulllife;
            sprRen2.sprite = fulllife;
            sprRen3.sprite = fulllife;
            sprRen4.sprite = emptylife;
            sprRen5.sprite = emptylife;
        }
        if (health == 2){
            sprRen1.sprite = fulllife;
            sprRen2.sprite = fulllife;
            sprRen3.sprite = emptylife;
            sprRen4.sprite = emptylife;
            sprRen5.sprite = emptylife;
        }
        if (health == 1){
            sprRen1.sprite = fulllife;
            sprRen2.sprite = emptylife;
            sprRen3.sprite = emptylife;
            sprRen4.sprite = emptylife;
            sprRen5.sprite = emptylife;
        }
        if (health == 0){
            sprRen1.sprite = emptylife;
            sprRen2.sprite = emptylife;
            sprRen3.sprite = emptylife;
            sprRen4.sprite = emptylife;
            sprRen5.sprite = emptylife;
        }
    }
}
