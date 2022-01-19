using Assets.Scripts.Player;
using UnityEngine;

public class ReverseSectionEngager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject firstLavaWall;
    public GameObject secondLavaWall;

    private Vector3 firstLavaDefaultPos = new Vector3(-1000, 16, -1150);
    private float firstLavaSpeed = 0.3f;
    private int firstLavaEndPos = -330;
    public bool firstLavaWallReachedEnd = false;

    private Vector3 secondLavaDefaultPos = new Vector3(-1094, -10, -247);
    private float secondLavaSpeed = 0.03f;
    private int secondLavaEndPos = 15;

    private void Start()
    {
        firstLavaWall.transform.position = firstLavaDefaultPos;
        secondLavaWall.transform.position = secondLavaDefaultPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            //Make sure the track doesnt keep restarting when the player hits this hidden wall
            if (gameManager.isReversing != true)
            {
                AudioManager.GetInstance().StopMusic();
                AudioManager.GetInstance().PlayMusic(AudioManager.MusicType.reversalMusic);
            }
            
            gameManager.isReversing = true;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.isPaused)
        {
            return;
        }

        if (gameManager.isReversing && firstLavaWall.transform.position.z < firstLavaEndPos)
        {
            firstLavaWall.transform.position += new Vector3(0, 0, firstLavaSpeed);

            if (firstLavaWall.transform.position.z >= firstLavaEndPos)
            {
                firstLavaWallReachedEnd = true;
            }
        }

        if (firstLavaWallReachedEnd)
        {
            if (secondLavaWall.transform.position.y < secondLavaEndPos)
            {
                secondLavaWall.transform.position += new Vector3(0, secondLavaSpeed, 0);
            }
        }
    }

    public void ResetReversal()
    {
        firstLavaWallReachedEnd = false;
        firstLavaWall.transform.position = firstLavaDefaultPos;
        secondLavaWall.transform.position = secondLavaDefaultPos;
    }
}