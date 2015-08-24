using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageInfo : MonoBehaviour {
	public int numofGoal=0;
	public int getGoal=0;  

	public bool retry_flag = false;
	public bool complete_flag = false;
	private bool ShowUI_flag = false;
	Quaternion target = Quaternion.Euler(0, 0, 90);
	private float rotation_speed = 1.0f;
	public GameObject camera_main;
	public GameObject main_UI;
	public GameObject complete_UI;
	public GameObject retry_UI;
	public GameObject start_UI;
	public GameObject ballSpeed_UI;
	public GameObject clear_UI;
	public GameObject Timer;
	public GameObject StageNum;

	public int speedMode =0;
	public bool camera_main_intro_on = false; 
	BoxBehavior_2[] bb ;
	private float currentSpeed;

	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	private Vector3 temp_camera_vector = new Vector3 (0, -30, -10);
	private Vector3 target_camera_vector = new Vector3 (0, 0, -10);

	public MoneyManager money_manager;

	private int easy_speed = 8;
	private int normal_speed = 12;
	private int hard_speed = 16;
	private int nextLevel_timer =3;
	bool[] stage_num;
	bool[] stage_mode;
	int thisStage;

	void Awake()
	{
		//camera_main.GetComponent<camera_main>().orthographicSize =0;
		//camera_main_intro_on = true;
		stage_num = PlayerPrefsX.GetBoolArray ("Cleared_Stage");
		stage_mode = PlayerPrefsX.GetBoolArray ("Cleared_Stage_Mode");
		thisStage =Application.loadedLevel-1;

		numofGoal = GameObject.Find ("Goal").transform.childCount;
		money_manager = this.gameObject.GetComponent<MoneyManager> ();
		start_UI.SetActive (true);

		if (stage_mode [thisStage * 3] == false)
			speedMode = 0;
		else if (stage_mode [thisStage * 3 + 1] == false)
			speedMode = 1;
		else if (stage_mode [thisStage * 3 + 2] == false)
			speedMode = 2;
		else
			speedMode = 2;

		SetInitialBallSpeed ();
		bb = GameObject.Find ("Player").GetComponentsInChildren<BoxBehavior_2> ();

		foreach (BoxBehavior_2 player in bb) {
			player.boxSpeed =0;
		}

		StageNum.GetComponent<Text> ().text = Application.loadedLevel.ToString();
		main_UI.SetActive(false);
		camera_main.transform.position = temp_camera_vector;
		camera_main_intro_on = true;
		//camera_main.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>().enabled = true;
		//camera_main.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized> ().blurType = UnityStandardAssets.ImageEffects.BlurOptimized.BlurType.StandardGauss;
	}

	// Update is called once per frame
	void Update () {
		if (camera_main_intro_on) {
			camera_main.transform.position = Vector3.SmoothDamp(camera_main.transform.position,target_camera_vector,ref velocity,smoothTime);
			//camera_main_intro_on = false;
		}


		if (complete_flag) {
			camera_main.transform.Rotate(0,0,50.0f*Time.deltaTime);
			//camera_main.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>().enabled = true;
			
			if(camera_main.GetComponent<Camera>().orthographicSize>10)
			{
				clear_UI.SetActive(true);
				camera_main.GetComponent<Camera>().orthographicSize -= Time.deltaTime*20.0f;
				
			}
			else{
				//camera_main.transform.rotation = Quaternion.Slerp(camera_main.transform.rotation, target, Mathf.PingPong(0,360));
				if(thisStage%10 !=9)
				{
					if(!ShowUI_flag){
						clear_UI.SetActive(false);
						main_UI.SetActive(false);
						complete_UI.SetActive(true);
						//complete_UI.transform.Find("Complete_Panel").localScale = new Vector3(5,5,5);
						
						GetMoney();
						
						stage_num[thisStage] = true;
						stage_mode[thisStage*3 + speedMode] = true;
						PlayerPrefsX.SetBoolArray ("Cleared_Stage",stage_num);
						PlayerPrefsX.SetBoolArray ("Cleared_Stage_Mode",stage_mode);
						ShowUI_flag = true;
						nextLevel_timer =3;
						InvokeRepeating ("DecreaseTimeRemaining", 1.0f, 1.0f);
					}	
				}else{ //stage 10
					if(!ShowUI_flag){
						clear_UI.SetActive(false);
						main_UI.SetActive(false);
						
						GetMoney();
						
						stage_num[thisStage] = true;
						stage_mode[thisStage*3 + speedMode] = true;
						PlayerPrefsX.SetBoolArray ("Cleared_Stage",stage_num);
						PlayerPrefsX.SetBoolArray ("Cleared_Stage_Mode",stage_mode);
						ShowUI_flag = true;
						Application.LoadLevel("1_Main_stage");
					}
				}

			}

		}
		if (retry_flag) {
			//print ("retry");

			Retry();
		}
	}

	void DecreaseTimeRemaining()
	{
		nextLevel_timer --;
		Timer.GetComponent<Text> ().text = nextLevel_timer.ToString ();
		if (nextLevel_timer == 0)
			GoNextLevel ();
	}
	void GetMoney()
	{
		switch (speedMode) {
		case 0:
			money_manager.IncreaseCurrentMoney (10);
			break;
		case 1:
			money_manager.IncreaseCurrentMoney (30);
			break;
		case 2:
			money_manager.IncreaseCurrentMoney (100);
			break;

		}
		money_manager.saveMoney ();

	}
	public void ClickStart()
	{
		start_UI.SetActive (false);
		foreach (BoxBehavior_2 player in bb) 
		{
			player.boxSpeed = currentSpeed;
		}
		this.gameObject.GetComponent<Controller>().start_flag = true;
		//camera_main.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>().enabled = false;
		main_UI.SetActive(true);
	}

	void SetInitialBallSpeed()
	{
		if (speedMode == 0) {
			ballSpeedSetting(new Color32 (103,217,255,200),"Easy",easy_speed);
		}else if (speedMode == 1) {
			ballSpeedSetting(new Color32 (255,232,103,200),"Norm",normal_speed);
		}else if (speedMode == 2) {
			ballSpeedSetting(new Color32 (255,103,103,200),"Hard",hard_speed);
		}
	}

	public void SetBallSpeed()
	{
		speedMode = (++speedMode) % 3;
		if (speedMode == 0) {
			ballSpeedSetting(new Color32 (103,217,255,200),"Easy",easy_speed);
		}else if (speedMode == 1) {
			ballSpeedSetting(new Color32 (255,232,103,200),"Norm",normal_speed);
		}else if (speedMode == 2) {
			ballSpeedSetting(new Color32 (255,103,103,200),"Hard",hard_speed);
		}

	}

	void ballSpeedSetting(Color32 gui_color,string str,int ballSpeed)
	{
		ballSpeed_UI.GetComponent<Image>().color = gui_color;
		ballSpeed_UI.GetComponentInChildren<Text> ().text = str;
		currentSpeed = ballSpeed;
	}

	public void Retry()
	{

		GameObject.Find ("Retry").transform.Find ("retry").GetComponent<Image> ().enabled = true;
		StartCoroutine (Waiting());
	}

	IEnumerator Waiting()
	{
		yield return new WaitForSeconds (.3f);
		Time.timeScale =1;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void RetryForButton()
	{
		Time.timeScale =1;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void GoNextLevel () 
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void GotoMainMenu()
	{
		Application.LoadLevel ("1_Main_stage");
	}

}
