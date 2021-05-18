using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CameraFollow cameraFollow;
    int currentBirdIndex;
    public SlingShot slingshot;
    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Bricks;
    private List<GameObject> Birds;
    private List<GameObject> Pigs;
    public int currentLevel = 1;
    public int highestLevel = 5;
    public int score = 0;
    public int highScore = 0;

    public static int score1 = 0;
    public static int score2 = 0;
    public static int score3 = 0;
    public static int score4 = 0;
    public static int score5 = 0;

    public GameObject WinPanel;
    public GameObject LosePanel;
    public Text WinHscore;
    public Text WinScore;
    public Text LoseHscore;
    public Text LoseScore;
    public int Flag = 0;

    // Use this for initialization
    void Start()
    {
        CurrentGameState = GameState.Start;
        slingshot.enabled = false;
        //find all relevant game objects
        Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
        Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));
        //unsubscribe and resubscribe from the event
        //this ensures that we subscribe only once
        slingshot.BirdThrown -= Slingshot_BirdThrown; slingshot.BirdThrown += Slingshot_BirdThrown;
    }


    // Update is called once per frame
    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                //if player taps, begin animating the bird 
                //to the slingshot
                if (Input.GetMouseButtonUp(0))
                {
                    AnimateBirdToSlingshot();
                }
                break;
            case GameState.BirdMovingToSlingshot:
                //do nothing
                break;
            case GameState.Playing:
                //if we have thrown a bird
                //and either everything has stopped moving
                //or there has been 5 seconds since we threw the bird
                //animate the camera to the start position
                if (slingshot.slingshotState == SlingshotState.BirdFlying &&
                    (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
                {
                    slingshot.enabled = false;
                    AnimateCameraToStartPosition();
                    CurrentGameState = GameState.BirdMovingToSlingshot;
                }
                break;
            case GameState.Won:
                if (Flag == 0)
                {
                    IncreaseScore((3 - (currentBirdIndex + 1)) * 100);
                    if (currentLevel == 1 && score > score1)
                    {
                        score1 = score;
                        StaticVar.score1 = score;
                    }
                    if (currentLevel == 2 && score > score2)
                    {
                        score2 = score;
                        StaticVar.score2 = score;
                    }
                    if (currentLevel == 3 && score > score3)
                    {
                        score3 = score;
                        StaticVar.score3 = score;
                    }
                    if (currentLevel == 4 && score > score4)
                    {
                        score4 = score;
                        StaticVar.score4 = score;
                    }
                    if (currentLevel == 5 && score > score5)
                    {
                        score5 = score;
                        StaticVar.score5 = score;
                    }
                    Flag = 1;
                }
                WinPanel.SetActive(true);
                if (currentLevel == 1) {
                    WinHscore.text = "High Score: " + score1;
                }
                else if (currentLevel == 2)
                {
                    WinHscore.text = "High Score: " + score2;
                }
                else if (currentLevel == 3)
                {
                    WinHscore.text = "High Score: " + score3;
                }
                else if (currentLevel == 4)
                {
                    WinHscore.text = "High Score: " + score4;
                }
                else if (currentLevel == 5)
                {
                    WinHscore.text = "High Score: " + score5;
                }
                WinScore.text = "Score: " + score;
                break;
            case GameState.Lost:
                LosePanel.SetActive(true);
                if (currentLevel == 1)
                {
                    LoseHscore.text = "High Score: " + score1;
                }
                else if (currentLevel == 2)
                {
                    LoseHscore.text = "High Score: " + score2;
                }
                else if (currentLevel == 3)
                {
                    LoseHscore.text = "High Score: " + score3;
                }
                else if (currentLevel == 4)
                {
                    LoseHscore.text = "High Score: " + score4;
                }
                else if (currentLevel == 5)
                {
                    LoseHscore.text = "High Score: " + score5;
                }
                LoseScore.text = "Score: " + score;
                break;
            default:
                break;
        }
    }

    public void ResetLevel()
    {
         SceneManager.LoadScene("lvl" + currentLevel);
    }



    public void IncreaseLevel()
    {
        if (currentLevel < highestLevel)
        {          
            currentLevel++;
            SceneManager.LoadScene("lvl" + currentLevel);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void IncreaseScore(int amount)
    {
        // Increase the score by the given amount
        score += amount;

        // Show the new score in the console
        print("New Score: " + score.ToString());

        if (score > highScore)
        {
            highScore = score;
            print("New high score: " + highScore);
        }
    }
    /// <summary>
    /// A check whether all Pigs are null
    /// i.e. they have been destroyed
    /// </summary>
    /// <returns></returns>
    private bool AllPigsDestroyed()
    {
        return Pigs.All(x => x == null);
    }

    /// <summary>
    /// Animates the camera to the original location
    /// When it finishes, it checks if we have lost, won or we have other birds
    /// available to throw
    /// </summary>
    private void AnimateCameraToStartPosition()
    {
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
        if (duration == 0.0f) duration = 0.1f;
        //animate the camera to start
        Camera.main.transform.positionTo
            (duration,
            cameraFollow.StartingPosition). //end position
            setOnCompleteHandler((x) =>
                        {
                            cameraFollow.IsFollowing = false;
                            if (AllPigsDestroyed())
                            {
                                CurrentGameState = GameState.Won;
                            }
                            //animate the next bird, if available
                            else if (currentBirdIndex == Birds.Count - 1)
                            {
                                //no more birds, go to finished
                                CurrentGameState = GameState.Lost;
                            }
                            else
                            {
                                slingshot.slingshotState = SlingshotState.Idle;
                                //bird to throw is the next on the list
                                currentBirdIndex++;
                                AnimateBirdToSlingshot();
                            }
                        });
    }
    
    void AnimateBirdToSlingshot()
    {
        CurrentGameState = GameState.BirdMovingToSlingshot;
        Birds[currentBirdIndex].transform.positionTo
            (Vector2.Distance(Birds[currentBirdIndex].transform.position / 10,
            slingshot.BirdWaitPosition.transform.position) / 10, //duration
            slingshot.BirdWaitPosition.transform.position). //final position
                setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy(); //destroy the animation
                            CurrentGameState = GameState.Playing;
                            slingshot.enabled = true; //enable slingshot
                            //current bird is the current in the list
                            slingshot.BirdToThrow = Birds[currentBirdIndex];
                        });
    }

    private void Slingshot_BirdThrown(object sender, System.EventArgs e)
    {
        cameraFollow.BirdToFollow = Birds[currentBirdIndex].transform;
        cameraFollow.IsFollowing = true;
    }

    bool BricksBirdsPigsStoppedMoving()
    {
        foreach (var item in Bricks.Union(Birds).Union(Pigs))
        {
            if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Constants.MinVelocity)
            {
                return false;
            }
        }

        return true;
    }

    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }
}
