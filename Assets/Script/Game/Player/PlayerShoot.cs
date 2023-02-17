using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {
	public GameObject gameManager;
	public GameObject canvasManager;
	public GameObject soundManager;
	public GameObject effectFail;
	public GameObject effectInvincible;

	public GameObject hero;
	public GameObject moveTarget;
	public GameObject attackTarget;
	public GameObject eye;
	public GameObject gunLeft;
	public GameObject gunRight;
	public GameObject skillBomb;
	public GameObject skillWave;

	public float minimumDistance;
	public float maximumDistance;
	public Vector3 recordMousePosition;
	public Vector3 currentMousePosition;
	public bool isDrag;
	public bool isWin;

	private long health;
	private float speed;
	private long attack;
	private long skillBombLevel;
	//private long skillBombAttack = 30;

	private int state;
	private const int STATE_ALIVE = 0;
	private const int STATE_DEAD = 1;
	private const int STATE_WIN = 2;

	private const float GRAVITY_VALUE = -0.098f;
	private const float InvincibleTime = 3.0f;
	private float currentInvincibleTime = -1.0f;

	//about upgrade
	private long currentGradeMain;
	private long currentGradeHealth;
	private long currentGradeAttack;
	private long currentGradeSkillBombAttack;
	private long currentGradeSkillWaveTime;
	private long money;
	private const long INITIAL_MONEY = 1000;
	public GradeData[] gradeData;
	[System.Serializable]
	public class GradeData {
		public long health;
		public float speed;
		public long attack;
		public long defend;
		public float eyeDistance;
		public float skillBombTime;
		public long skillBombAttack;
		public float skillBombDistance;
		public float skillWaveTime;
		public long skillWavePower;
		public float skillWaveDistance;
		public long money;
	}

	//about result
	public long levelTime;
	public long levelDamage;
	public long levelKill;
	public long levelMoney;

	// Use this for initialization
	void Start () {
		InitialData ();
		PreparePlayer ();
		//record the joystick position, from canvas world to the input world.
		recordMousePosition = canvasManager.GetComponent<Canvas> ().worldCamera.ViewportToScreenPoint(new Vector3 (
			((canvasManager.GetComponent<CanvasManager> ().imageJoystick.GetComponent<RectTransform> ().transform.localPosition.x / canvasManager.GetComponent<RectTransform> ().rect.width) + 0.5f),
			((canvasManager.GetComponent<CanvasManager> ().imageJoystick.GetComponent<RectTransform> ().transform.localPosition.y / canvasManager.GetComponent<RectTransform> ().rect.height) + 0.5f),
			0)
		);
	}
	
	// Update is called once per frame
	void Update () {
		//check gameManager state
		if (gameManager.GetComponent<GameManager>().IsStateLevel() == false)
			return;
		//keep gravity
		this.GetComponent<CharacterController>().Move(new Vector3(0,GRAVITY_VALUE,0));
		//state
		switch (state) {
		case STATE_ALIVE:
			//Use Mouse	and the joystick is not fixed on screen
			if (Input.GetMouseButtonDown (0)) {
				recordMousePosition = Input.mousePosition;
				canvasManager.GetComponent<CanvasManager> ().imageJoystick.GetComponent<RectTransform> ().transform.localPosition = new Vector3 (
					canvasManager.GetComponent<RectTransform> ().rect.width * (canvasManager.GetComponent<Canvas> ().worldCamera.ScreenToViewportPoint(Input.mousePosition).x - 0.5f),
					canvasManager.GetComponent<RectTransform> ().rect.height * (canvasManager.GetComponent<Canvas> ().worldCamera.ScreenToViewportPoint(Input.mousePosition).y - 0.5f),
					0);
				canvasManager.GetComponent<CanvasManager> ().imageJoystick.SetActive (true);
				isDrag = true;
			}
			if (isDrag) {
				currentMousePosition = Input.mousePosition;
				if (Vector3.Distance (currentMousePosition, recordMousePosition) > minimumDistance) {
					//this.transform.Translate (new Vector3((currentMousePosition-recordMousePosition).normalized.x,0,(currentMousePosition-recordMousePosition).normalized.y).normalized * speed * Time.deltaTime);
					this.GetComponent<CharacterController>().Move(new Vector3((currentMousePosition-recordMousePosition).normalized.x,0,(currentMousePosition-recordMousePosition).normalized.y).normalized * Time.deltaTime * speed);
					moveTarget.transform.position = new Vector3 (this.transform.localPosition.x + (currentMousePosition-recordMousePosition).x,0,this.transform.localPosition.z + (currentMousePosition-recordMousePosition).y);
				}
			}
			if (Input.GetMouseButtonUp (0)) {
				isDrag = false;
				canvasManager.GetComponent<CanvasManager> ().imageJoystick.SetActive (false);
			}
//			//Use Mouse	and the joystick is fixed on screen
//			if (Input.GetMouseButton (0)) {
//				currentMousePosition = Input.mousePosition;
//				if (Vector3.Distance (currentMousePosition, recordMousePosition) > maximumDistance) {
//					isDrag = false;
//				} else {
//					isDrag = true;
//				}
//			}
//			if (isDrag) {
//				currentMousePosition = Input.mousePosition;
//				if (Vector3.Distance (currentMousePosition, recordMousePosition) > minimumDistance) {
//					this.GetComponent<CharacterController>().Move(new Vector3((currentMousePosition-recordMousePosition).normalized.x,0,(currentMousePosition-recordMousePosition).normalized.y).normalized * Time.deltaTime * speed);
//					moveTarget.transform.localPosition = new Vector3 (this.transform.localPosition.x + (currentMousePosition-recordMousePosition).x,0,this.transform.localPosition.z + (currentMousePosition-recordMousePosition).y);
//					//				hero.transform.LookAt(moveTarget.transform);
//				}
//				//print (currentMousePosition);
//			}
//			if (Input.GetMouseButtonUp (0)) {
//				isDrag = false;
//			}
			//keep look at enemy
			//attackTarget = eye.GetComponent<Eye> ().GetNearestEnemy ();
			if (eye.GetComponent<Eye> ().GetNearestEnemy () != null) {
				hero.transform.LookAt (eye.GetComponent<Eye> ().GetNearestEnemy ().transform);
				gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayFire ();
				gunLeft.GetComponent<Gun> ().Fire ();
				gunRight.GetComponent<Gun> ().Fire ();
			} else {
				hero.transform.LookAt(moveTarget.transform);
				gunLeft.GetComponent<Gun> ().Wait ();
				gunRight.GetComponent<Gun> ().Wait ();
			}
			//Invincible
			if (currentInvincibleTime > 0.0f) {
				effectInvincible.SetActive (true);
				currentInvincibleTime -= Time.deltaTime;
			} else {
				effectInvincible.SetActive (false);
			}
			break;
		case STATE_DEAD:
			break;
		case STATE_WIN:
			break;
		}
	}

	public void Reset () {
		state = STATE_ALIVE;
		InitialData ();
		PreparePlayer ();
		hero.SetActive (true);
		this.transform.position = Vector3.zero;
	}

	public void Revive () {
		state = STATE_ALIVE;
		InitialData ();
		PreparePlayer ();
		hero.SetActive (true);
		this.transform.position = Vector3.zero;
		currentInvincibleTime = InvincibleTime;
	}

	public void Win () {
		state = STATE_WIN;
		hero.SetActive (false);
		this.transform.position = Vector3.zero;
	}

//	private void OnControllerColliderHit(ControllerColliderHit setHit){
//		print ("here");
//		if (setHit.gameObject.name == "Target") {
//			if (gameManager.GetComponent<GameManager> ().IsStateLevel ()) {
//				isWin = true;
//				effectWin.GetComponent<ParticleSystem> ().Play ();
//				soundManager.GetComponent<SoundManager> ().PlayWin ();
//				//canvasManager.GetComponent<CanvasManager> ().canvasGame.SetActive (false);
//			}
//		}
//	}

	public void PreparePlayer () {
		isDrag = false;
		isWin = false;
		effectFail.GetComponent<ParticleSystem> ().Stop ();
		skillBomb.GetComponent<SkillBomb>().Reset();
		skillWave.GetComponent<SkillWave>().Reset();
	}

	public void InitialData () {
		if (PlayerPrefs.HasKey ("GameManager.Player.CurrentGradeMain")) {
			currentGradeMain = (long)PlayerPrefs.GetFloat ("GameManager.Player.CurrentGradeMain");
		} else {
			currentGradeMain = 0;
			PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeMain", currentGradeMain);
		}
		if (PlayerPrefs.HasKey ("GameManager.Player.CurrentGradeHealth")) {
			currentGradeHealth = (long)PlayerPrefs.GetFloat ("GameManager.Player.CurrentGradeHealth");
		} else {
			currentGradeHealth = 0;
			PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeHealth", currentGradeHealth);
		}
		if (PlayerPrefs.HasKey ("GameManager.Player.CurrentGradeAttack")) {
			currentGradeAttack = (long)PlayerPrefs.GetFloat ("GameManager.Player.CurrentGradeAttack");
		} else {
			currentGradeAttack = 0;
			PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeAttack", currentGradeAttack);
		}
		if (PlayerPrefs.HasKey ("GameManager.Player.CurrentGradeSkillBombAttack")) {
			currentGradeSkillBombAttack = (long)PlayerPrefs.GetFloat ("GameManager.Player.CurrentGradeSkillBombAttack");
		} else {
			currentGradeSkillBombAttack = 0;
			PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeSkillBombAttack", currentGradeSkillBombAttack);
		}
		if (PlayerPrefs.HasKey ("GameManager.Player.CurrentGradeSkillWaveTime")) {
			currentGradeSkillWaveTime = (long)PlayerPrefs.GetFloat ("GameManager.Player.CurrentGradeSkillWaveTime");
		} else {
			currentGradeSkillWaveTime = 0;
			PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeSkillWaveTime", currentGradeSkillWaveTime);
		}
		if (PlayerPrefs.HasKey ("GameManager.Player.Money")) {
			money = (long)PlayerPrefs.GetFloat ("GameManager.Player.Money");
		} else {
			money = INITIAL_MONEY;
			PlayerPrefs.SetFloat ("GameManager.Player.Money", money);
		}
		health = gradeData [currentGradeHealth].health;
		attack = gradeData [currentGradeAttack].attack;
		speed = gradeData [currentGradeMain].speed;
		//about result
		levelTime = 0;
		levelDamage = 0;
		levelKill = 0;
		levelMoney = 0;
	}

	public void SaveData () {
		PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeMain", currentGradeMain);
		PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeHealth", currentGradeHealth);
		PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeAttack", currentGradeAttack);
		PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeSkillBombAttack", currentGradeSkillBombAttack);
		PlayerPrefs.SetFloat ("GameManager.Player.CurrentGradeSkillWaveTime", currentGradeSkillWaveTime);
		PlayerPrefs.SetFloat ("GameManager.Player.Money", money);
	}

	public enum ChangeType {
		IncreaseHealth = 0,
		ReduceHealth = 1,
		IncreaseMoney = 2,
		ReduceMoney = 3,
		IncreaseCurrentGradeHealth = 4,
		IncreaseCurrentGradeAttack = 5,
		IncreaseCurrentGradeSkillBombAttack = 6,
		IncreaseCurrentGradeSkillWaveTime = 7,
	}

	public void Change (ChangeType setChangeType, long setChangeValue) {
		if (state == STATE_DEAD)	//if dead, no change.
			return;
		switch (setChangeType) {
		case ChangeType.IncreaseHealth:
			health += setChangeValue;
			if (health > gradeData [currentGradeHealth].health)
				health = gradeData [currentGradeHealth].health;
			break;
		case ChangeType.ReduceHealth:
			if (currentInvincibleTime > 0.0f) {
				currentInvincibleTime -= Time.deltaTime;
				return;
			}
			health -= setChangeValue;
			soundManager.GetComponent<SoundManager> ().PlayHurt ();
			gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<CanvasManager> ().canvasLevel.GetComponent<CanvasLevel> ().ShowHurt ();
			if (health <= 0) {
				health = 0;			//for show player health is 0.
				hero.SetActive (false);
				effectFail.SetActive (true);
				levelTime = (long)gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().currentTime;
				gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().selectLevel.GetComponent<Level> ().Pause();
				gameManager.GetComponent<GameManager> ().levelManager.GetComponent<LevelManager> ().SetSelectLevelWin (false);
				gameManager.GetComponent<GameManager> ().canvasManager.GetComponent<CanvasManager> ().canvasLevel.GetComponent<CanvasLevel> ().ShowFail ();
				gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayGameover ();
				state = STATE_DEAD;
			}
			break;
		case ChangeType.IncreaseMoney:
			money += setChangeValue;
			levelMoney += setChangeValue;
			break;
		case ChangeType.ReduceMoney:
			money -= setChangeValue;
			if (money < 0)
				money = 0;
			break;
		case ChangeType.IncreaseCurrentGradeHealth:
			gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayUpgrade ();
			currentGradeHealth += setChangeValue;
			if (currentGradeHealth >= gradeData.Length - 1)
				currentGradeHealth = gradeData.Length - 1;
			break;
		case ChangeType.IncreaseCurrentGradeAttack:
			gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayUpgrade ();
			currentGradeAttack += setChangeValue;
			if (currentGradeAttack >= gradeData.Length - 1)
				currentGradeAttack = gradeData.Length - 1;
			break;
		case ChangeType.IncreaseCurrentGradeSkillBombAttack:
			gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayUpgrade ();
			currentGradeSkillBombAttack += setChangeValue;
			if (currentGradeSkillBombAttack >= gradeData.Length - 1)
				currentGradeSkillBombAttack = gradeData.Length - 1;
			break;
		case ChangeType.IncreaseCurrentGradeSkillWaveTime:
			gameManager.GetComponent<GameManager> ().soundManager.GetComponent<SoundManager> ().PlayUpgrade ();
			currentGradeSkillWaveTime += setChangeValue;
			if (currentGradeSkillWaveTime >= gradeData.Length - 1)
				currentGradeSkillWaveTime = gradeData.Length - 1;
			break;
		}
	}

	public enum DataType {		//if get every data from GetData(), there will be too many Types, so we have to get data from the public param "gradeData".
		CurrentGradeMain = 0,
		CurrentGradeHealth = 1,
		CurrentGradeAttack = 2,
		CurrentGradeSkillBombAttack = 3,
		CurrentGradeSkillWaveTime = 4,
		Health = 5,
		Attack = 6,
		Money = 7,
		SkillBombAttack = 8,
		SkillWaveTime = 9
	}

	public long GetData (DataType setDataType) {
		switch (setDataType) {
		case DataType.CurrentGradeMain:
			return currentGradeMain;
		case DataType.CurrentGradeHealth:
			return currentGradeHealth;
		case DataType.CurrentGradeAttack:
			return currentGradeAttack;
		case DataType.CurrentGradeSkillBombAttack:
			return currentGradeSkillBombAttack;
		case DataType.CurrentGradeSkillWaveTime:
			return currentGradeSkillWaveTime;
		case DataType.Health:
			return health;
		case DataType.Attack:
			return attack;
		case DataType.Money:
			return money;
		case DataType.SkillBombAttack:
			return gradeData[currentGradeSkillBombAttack].skillBombAttack;
		case DataType.SkillWaveTime:
			return (long)gradeData[currentGradeSkillWaveTime].skillWaveTime;
//get upgrade money
		}
		return -1;
	}

	public bool IsDead () {
		if (state == STATE_DEAD)
			return true;
		return false;
	}
}
