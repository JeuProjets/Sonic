using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Gestion de déplacement et du saut du personnage à l'aide des touches : a, d et w      
* Gestion des détections de collision entre le personnage et les objets du jeu  
* Par: Vahik Toroussian
* Modifié: 5/12/2018
*/
public class ControlerPersonnage : MonoBehaviour
{
    float vitesseX;      //vitesse horizontale actuelle
    public float vitesseXMax;   //vitesse horizontale Maximale désirée
    float vitesseY;      //vitesse verticale 
    public float vitesseSaut;   //vitesse de saut désirée
    bool estMort;

    /* Détection des touches et modification de la vitesse de déplacement;
       "a" et "d" pour avancer et reculer, "w" pour sauter
    */
    void Update ()
    {
        // déplacement vers la gauche
        if (Input.GetKey("a"))
        {
            vitesseX = -vitesseXMax;
            GetComponent<SpriteRenderer>().flipX = true;

        }
        else if (Input.GetKey("d"))   //déplacement vers la droite
        {
            vitesseX = vitesseXMax;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            vitesseX = GetComponent<Rigidbody2D>().velocity.x;  //mémorise vitesse actuelle en X
        }

        // saut
        Physics2D.OverlapCircle(transform.position, 0.2f);


        if (Input.GetKeyDown("w") && Physics2D.OverlapCircle(transform.position, 0.2f))
        {
            vitesseY = vitesseSaut;
        }
        else
        {
            vitesseY = GetComponent<Rigidbody2D>().velocity.y;  //vitesse actuelle verticale
        }
        
        //Applique les vitesses en X et Y
        GetComponent<Rigidbody2D>().velocity = new Vector2(vitesseX, vitesseY);


        //**************************Gestion des animaitons de course et de repos********************************
        //Active l'animation de course si la vitesse de déplacement n'est pas 0, sinon le repos sera jouer par Animator
        if(vitesseX > 0.01f || vitesseX < -0.1f)
        {
            GetComponent<Animator>().SetBool("marche", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("marche", false);
        }
        
       
       


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.OverlapCircle(transform.position, 0.2f))
        {
            GetComponent<Animator>().SetBool("saut", false);
        }
        
        if(infosCollision.gameObject.name == "Bombe")
        {
            GetComponent<Animator>().SetTrigger("mort");
            estMort = true;

            if(transform.position.x > infosCollision.transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(20, 30);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-20, 30);
            }
            
            Invoke("RelanceDuJeu", 2f);
        }
    }


    private void RelaceDuJeu()
    {
        SceneManager.LoadScene("SonicDebut");
    }
}

