using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int kingHealth = 100;
    public int bookcollected = 0;
    public int booksneeded = 6;
    public TextMeshProUGUI books;

    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        bookui();
    }

    public void UpdateKingHealth(int amt = -1)
    {
        kingHealth-=amt;
        print(kingHealth);
    }
    public void collectbook()
    {
        bookcollected++;
        Soundmanager.playSound(soundtype.bookpickup);
        bookui();
    }
    void bookui()
    {
        books.text = bookcollected + "/" + booksneeded;
    }
    public void deadking()
    {
        if(bookcollected >= booksneeded)
        {
            SceneManager.LoadScene("MONKEY KING");
        }
        else
        {
            SceneManager.LoadScene("FALSE ENDING");
        }
    }
 }
