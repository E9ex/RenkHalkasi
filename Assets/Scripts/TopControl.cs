using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TopControl : MonoBehaviour
{
    private Rigidbody2D rb;//zıplama fiziksel işlemi için
    public float ziplamaKuvveti = 3f;// ne kadar zıplayacak onun kontrolü için
    public bool basildiMi = false;// kullanıcı input yaptı mı onu anlamak için
    public string mevcutrenk;
    public Color TopunRengi;
    public Color turkuaz, sari, mor, pembe;
    [SerializeField] private Text scoretext,Bestscoretext;
    public static int score = 0;
    public static int bestscore=0;
    public GameObject halka, renkTekeri;
    public GameObject taptoplay, helpbutton,helpPanel;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        Bestscoretext.text = "BestScore: " + PlayerPrefs.GetInt("bestscore");
        Time.timeScale = 0;
        helpPanel.SetActive(false);
        scoretext.text = "Score: " + score;
        rastgeleRenkBelirle();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            basildiMi = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            basildiMi = false;
        }
    }

    private void FixedUpdate()
    {
        if (basildiMi)
        {
            rb.velocity = Vector2.up * ziplamaKuvveti;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="RenkTekeri")
        {
            rastgeleRenkBelirle();
            Destroy(col.gameObject);
            return;
        }
        if (col.tag!=mevcutrenk&&col.tag!="puanArtirici"&& col.tag!="RenkTekeri")
        {
            score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (col.tag=="puanArtirici")
        {
            score += 5;
            scoretext.text = "score: "+score;
            if (score>bestscore)
            {
                bestscore = score;
                PlayerPrefs.SetInt("bestscore", bestscore);
            }
            
            Destroy(col.gameObject);
            Instantiate(halka, new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z),Quaternion.identity);
            Instantiate(renkTekeri, new Vector3(transform.position.x, transform.position.y + 11.7f, transform.position.z),Quaternion.identity);
            
        }
        if (col.CompareTag("death"))
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void rastgeleRenkBelirle()
    {
        int rastgelesayi = Random.Range(0, 4);//0-1-2-3
        switch (rastgelesayi)
        {
            case 0:
                mevcutrenk = "turkuaz";
                TopunRengi = turkuaz;
                break;
            case 1:
                mevcutrenk = "sarı";
                TopunRengi = sari;
                break; 
            case 2:
                mevcutrenk = "pembe";
                TopunRengi = pembe;
                break;
            case 3:
                mevcutrenk = "mor";
                TopunRengi = mor;
                break;
           
        }
        GetComponent<SpriteRenderer>().color = TopunRengi;
    }

   public void basOyna()
    {
        taptoplay.SetActive(false);
        helpbutton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
   public  void infoVer()
   {
       helpPanel.SetActive(!helpPanel.activeSelf);
    } 
}
