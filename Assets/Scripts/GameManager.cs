using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public delegate void GameDelegate();
	public static event GameDelegate OnGameStarted;
	public static event GameDelegate OnGameOverConfirmed;

	public static GameManager Instance;

	public GameObject startPage;
	public GameObject gameOverPage;
	public GameObject countdownPage;
	public GameObject creditsPage;
	public Text scoreText;

	enum PageState {
		None,
		Start,
		Countdown,
		GameOver,
		Credits
	}

	int score = 0;
	bool gameOver = true;			/////Remember this

	public bool GameOver { get { return gameOver; } }
	public int Score {get { return score; } }

	
	void Awake() {
		// if (Instance != null) {
		// 	Destroy(gameObject);
		// }
		//else {
	 		Instance = this;
	// PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
 //        PlayGamesPlatform.InitializeInstance(config);
 //        PlayGamesPlatform.Activate();
 //        Social.localUser.Authenticate((bool success) =>
 //        {
 //            if (success == true)
 //            {
 //                Debug.Log("Logged in to Google Play Games Services");

 //                SceneManager.LoadScene("LeaderboardUI");

 //                PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_quarantine_kings);
 //            }
 //            else
 //            {
 //                Debug.LogError("Unable to sign in to Google Play Games Services");
 //            }
 //        });
        
 
		//	DontDestroyOnLoad(gameObject);
		//}
	}

	 void OnEnable() {
	 	TapController.OnPlayerDied += OnPlayerDied;
	 	TapController.OnPlayerScored += OnPlayerScored;
	 	CountdownText.OnCountdownFinished += OnCountdownFinished;
	 }

	 void OnDisable() {
	 	TapController.OnPlayerDied -= OnPlayerDied;
	 	TapController.OnPlayerScored -= OnPlayerScored;
	 	CountdownText.OnCountdownFinished -= OnCountdownFinished;
	 }

	 void OnCountdownFinished() {
	 	SetPageState(PageState.None);
	 	OnGameStarted();
	 	score = 0;
	 	gameOver = false;
	 }

	 void OnPlayerScored() {
	 	score++;
	 	scoreText.text = score.ToString();
	 }

	 void OnPlayerDied() {
	 	gameOver = true;
	 	int savedScore = PlayerPrefs.GetInt("HighScore");
	 	if (score > savedScore) {
			PlayerPrefs.SetInt("HighScore", score);
	 	}
	 	SetPageState(PageState.GameOver);
	 }

	void SetPageState(PageState state) {
		switch (state) {
			case PageState.None:
				startPage.SetActive(false);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
				creditsPage.SetActive(false);
				break;
			case PageState.Start:
				startPage.SetActive(true);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
				creditsPage.SetActive(false);
				break;
			case PageState.Countdown:
				startPage.SetActive(false);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(true);
				creditsPage.SetActive(false);
				break;
			case PageState.GameOver:
				startPage.SetActive(false);
				gameOverPage.SetActive(true);
				countdownPage.SetActive(false);
				creditsPage.SetActive(false);
				break;
			case PageState.Credits:
				startPage.SetActive(false);
				gameOverPage.SetActive(false);
				countdownPage.SetActive(false);
				creditsPage.SetActive(true);
				break;
		}
	}
	
	public void ConfirmGameOver() {
		 SetPageState(PageState.Start);
		 scoreText.text = "0";
		 OnGameOverConfirmed();
	}

	public void StartGame() {
		SetPageState(PageState.Countdown);
	}

	public void Creditsflaticon(){

	}

	public void CreditsPageOpen(){
		SetPageState(PageState.Credits);
	}

	public void BackfromCredits(){
		SetPageState(PageState.Start);
	}

	public void Githublink(){
		Application.OpenURL("https://github.com/qdotdash/Dribbley.git");
	}

	public void Soundeffectslink(){
		Application.OpenURL("https://www.zapsplat.com");
	}

	public void Vectorcitylink(){
		Application.OpenURL("https://www.vecteezy.com/free-vector/city");
	}

	public void flaticonlink(){
		Application.OpenURL("https://www.flaticon.com/authors/mavadee");
	}






}
