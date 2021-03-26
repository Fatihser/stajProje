using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    private Animator anim;
    private string tag;
    public ParticleSystem effect;
    private bool sagagit = false;
    private bool solagit = false;
    public Material playerRengi;
    private float tranformScale=0;
    public GameObject tac;
    public Image tacImage;
    private bool kos = true;
    public GameObject restartButton;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("kos");
        tag = "kirmizi";
    }

    
    void Update()
    {
        if (kos)
        {
            transform.Translate(3 * Time.deltaTime, 0, 0);
            if (Input.touchCount > 0)
            {
                Touch parmak = Input.GetTouch(0);
                //benim cihazimda max x position 800 oldugu icin boyle yaptim cihazdan cihaza degisip degismedigini tam hatirlayamadigim ve 
                //sure kisitli oldugu icin boyle yaptim.
                if (parmak.position.x > 400)
                {
                    sagagit = true;
                }
                else if (parmak.position.x < 400)
                {
                    solagit = true;
                }
            }

            if (sagagit)
            {
                if (transform.position.x > -1.9)
                {
                    transform.Translate(0, 0, -2 * Time.deltaTime);
                }
            }
            else if (solagit)
            {
                if (transform.position.x < 0.9f)
                {
                    transform.Translate(0, 0, +2 * Time.deltaTime);
                }
            }

            if (Input.touchCount < 1)
            {
                sagagit = false;
                solagit = false;
            }
        }
        else
        {
            if (transform.eulerAngles.z<90)
            {
                transform.Rotate(0, 0, 100 * Time.deltaTime);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="ol")
        {
            kos = false;
            anim.SetTrigger("ol");
            restartButton.SetActive(true);

        }
        else if (other.gameObject.tag==tag)
        {
            Destroy(other.gameObject);
            transform.localScale = new Vector3(transform.localScale.x+0.1f, transform.localScale.y + 0.1f, transform.localScale.z + 0.1f);
            if (tag=="kirmizi")
            {
                effect.startColor = Color.red;
            }
            else if (tag=="yesil")
            {
                effect.startColor = Color.green;
            }
            else if (tag=="sari")
            {
                effect.startColor = Color.yellow;
            }
            Destroy(Instantiate(effect, new Vector3(transform.position.x, transform.position.y + 0.348f, transform.position.z), effect.transform.rotation),0.5f);
            tranformScale += 1;
            tacImage.fillAmount = tranformScale/50;
            if (tranformScale==30)
            {
                tac.SetActive(true);
            }
            

        }
        else if (other.gameObject.tag == "sariGecis")
        {
            playerRengi.color = Color.yellow;
            Destroy(other.gameObject);
            tag = "sari";
        }
        else if (other.gameObject.tag == "kirmiziGecis")
        {
            playerRengi.color = Color.red;
            Destroy(other.gameObject);
            tag = "kirmizi";
        }
        else if (other.gameObject.tag == "yesilGecis")
        {
            playerRengi.color = Color.green;
            Destroy(other.gameObject);
            tag = "yesil";
        }
        else
        {
            Debug.Log("hem buraya");
            Destroy(other.gameObject);
            effect.startColor = Color.black;
            if (transform.localScale.x>0.1f)
            {
                transform.localScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, transform.localScale.z - 0.1f);
            }
            Destroy(Instantiate(effect, new Vector3(transform.position.x, transform.position.y + 0.348f, transform.position.z), effect.transform.rotation), 0.5f);
                tranformScale -= 1;
                tacImage.fillAmount = tranformScale / 50;
                if (tranformScale < 30 && tac.activeSelf)
                {
                    tac.SetActive(false);
                }
        }
    }
}
