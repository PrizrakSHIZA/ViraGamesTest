using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    [Header("Settings")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField, Range(0, 1f)] float starChance;
    [Header("References")]
    [SerializeField] Basket[] basketList;
    [SerializeField] GameObject basketPrefab;
    [SerializeField] Transform gameField;

    int globalBasket = 0;
    int score = 0;
    int starCount = 0;

    int baseScore = 1;

    int lastBasketId = 3;
    int currentBasket { get { if (lastBasketId - 1 < 0) return 3; else return lastBasketId - 1; } }

    private void Start()
    {
        if (Singleton == null)
            Singleton = this;

        for (int i = 0; i < 4; i++)
        {
            globalBasket++;
            basketList[i].GetComponent<Basket>().id = globalBasket;
        }
    }

    private void Update()
    {
        if (basketList[currentBasket].transform.position.y > -3f)
        {
            gameField.position -= new Vector3(0, moveSpeed, 0f) * Time.deltaTime;
        }
    }

    public void Catch(int id)
    {
        // check if its next bucket adn not the same
        if (id == globalBasket) 
        {
            AddScore();
            SpawnBasket();
        }
    }

    public void GameOver()
    {
        if (score == 0)
        { 
            BallController.Singleton.transform.position = basketList[currentBasket].transform.position;
            basketList[currentBasket].transform.rotation = Quaternion.identity;
            BallController.Singleton.ClearHitData();
        }
        else
        { 
            
        }
    }

    public void AddStar()
    {
        starCount++;
    }

    void AddScore()
    {
        int toAdd = baseScore;
        if (BallController.Singleton.wallBounce) toAdd *= 2;
        if (BallController.Singleton.hitCount == 0) toAdd *= 2;

        score += toAdd;

        //clear data from ball
        BallController.Singleton.ClearHitData();
    }

    void SpawnBasket()
    {
        globalBasket++;

        int nextBasketId = lastBasketId + 1;
        if(nextBasketId > 3)
            nextBasketId = 0;

        Basket lastBasket = basketList[lastBasketId];
        Basket nextBasket = basketList[nextBasketId];

        if (++lastBasketId > 3)
            lastBasketId = 0;

        //change spawn depend on even or odd basket is
        float x;
        if (nextBasketId % 2 == 0)
        {
            x = Random.Range(-2f, 0f);
            nextBasket.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-50f, 0f));
        }
        else
        { 
            x = Random.Range(0f, 2f);
            nextBasket.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 50f));
        }
        float y = lastBasket.transform.position.y + Random.Range(3f, 4f);

        nextBasket.transform.position = new Vector3(x, y, 0);
        nextBasket.id = globalBasket;

        //star spawn
        if(Random.Range(0f, 1f) <= starChance)
            nextBasket.star.SetActive(true);

        nextBasket.gameObject.SetActive(true);
        //spawn animation
    }
}
