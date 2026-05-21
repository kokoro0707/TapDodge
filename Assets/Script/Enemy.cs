using UnityEngine;



public class Enemy : MonoBehaviour
{

    public float speed = 500f;

    public float destroyY = -500f;

    public static bool isStopped = false;


    private RectTransform rect;

    private bool scored = false;



    void Awake()
    {

        rect = GetComponent<RectTransform>();

    }



    void Update()
    {
        if(isStopped) return;

        rect.anchoredPosition += Vector2.down * speed * Time.deltaTime;



        if (!scored && rect.anchoredPosition.y < destroyY)

        {

            scored = true;

            ScoreManager.Instance.AddScore(10);

            Destroy(gameObject);

        }

    }

}