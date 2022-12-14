using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    [SerializeField] Basket[] basketList;
    [SerializeField] GameObject basketPrefab;
    [SerializeField] Transform gameField;

    int globalBasket = 0;
    int score = 0;
    int starCount = 0;

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
            gameField.position += new Vector3(0, -3f, 0f) * Time.deltaTime;
        }
    }

    public void Catch(int id)
    {
        // check if its next bucket adn not the same
        if (id == globalBasket) 
        { 
            SpawnBasket();
        }
    }

    void SpawnBasket()
    {
        globalBasket++;

        int nextBasketId = lastBasketId + 1;
        if(nextBasketId > 3)
            nextBasketId = 0;

        GameObject lastBasket = basketList[lastBasketId].gameObject;
        GameObject nextBasket = basketList[nextBasketId].gameObject;

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
        nextBasket.GetComponent<Basket>().id = globalBasket;

        nextBasket.SetActive(true);
        //spawn animation
    }

    void GameOver()
    { 
        
    }
}
