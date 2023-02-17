using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackCell : MonoBehaviour {

    private GameObject gameManager;

    //position
    public float maximumDirectionSpeed = 5f;
    private float directionSpeedX;
    private float directionSpeedY;

    //rotation
    public float maximumRotationSpeed = 5f;
    private float rotationSpeed;

    //scale
    public float maximumScaleSpeed = 0.001f;
    private float scaleSpeed;
    private const float maximumScale = 2.0f;
    private const float minimumScale = 0.0f;

    //color           
    public float maximumColorSpeed = 0.001f;
    private float colorSpeed;
    private const float maximumColor = 1.0f;
    private const float minimumColor = 0.0f;

    //alpha
    public float maximumAlphaSpeed = 0.001f;
    private float alphaSpeed;
    private const float maximumAlpha = 0.5f;
    private const float minimumAlpha = 0.0f;

    // Use this for initialization
    void Start() {
        gameManager = GameObject.Find("GameManager");
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(
            Random.Range(-gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform>().rect.width / 2, gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform>().rect.width / 2),
            Random.Range(-gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform>().rect.height / 2, gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform>().rect.height / 2),
            0);
            //this.gameObject.GetComponent<RectTransform>().localPosition.z);
        
        SetAll();
    }

    // Update is called once per frame
    void Update() {
        //position
        if ((Mathf.Abs(this.gameObject.GetComponent<RectTransform>().localPosition.x + directionSpeedX) > gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform>().rect.width / 2)||
            (Mathf.Abs(this.gameObject.GetComponent<RectTransform>().localPosition.y + directionSpeedY) > gameManager.GetComponent<GameManager>().canvasManager.GetComponent<RectTransform>().rect.height / 2))
        {
            SetAll();
        }
        this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(this.gameObject.GetComponent<RectTransform>().localPosition.x + directionSpeedX,
                                                                                  this.gameObject.GetComponent<RectTransform>().localPosition.y + directionSpeedY,
                                                                                  this.gameObject.GetComponent<RectTransform>().localPosition.z);
        //rotation
        this.gameObject.GetComponent<RectTransform>().Rotate(0, 0, rotationSpeed);

        //scale
        if ((this.gameObject.GetComponent<RectTransform>().localScale.x + scaleSpeed) < minimumScale ||
            (this.gameObject.GetComponent<RectTransform>().localScale.x + scaleSpeed) > maximumScale)
        {
            scaleSpeed = -scaleSpeed;
        }
        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(this.gameObject.GetComponent<RectTransform>().localScale.x + scaleSpeed,
                                                                               this.gameObject.GetComponent<RectTransform>().localScale.y + scaleSpeed,
                                                                               this.gameObject.GetComponent<RectTransform>().localScale.z);
        //color
        if ((this.gameObject.GetComponent<Image>().color.r + colorSpeed) < minimumColor ||
            (this.gameObject.GetComponent<Image>().color.r + colorSpeed) > maximumColor)
        {
            colorSpeed = -colorSpeed;
        }
        if ((this.gameObject.GetComponent<Image>().color.a + alphaSpeed) < minimumAlpha ||
            (this.gameObject.GetComponent<Image>().color.a + alphaSpeed) > maximumAlpha)
        {
            alphaSpeed = -alphaSpeed;
        }
        this.gameObject.GetComponent<Image>().color = new Color(this.gameObject.GetComponent<Image>().color.r + colorSpeed,
                                                                this.gameObject.GetComponent<Image>().color.g + colorSpeed,
                                                                this.gameObject.GetComponent<Image>().color.b + colorSpeed,
                                                                this.gameObject.GetComponent<Image>().color.a + alphaSpeed);

    }

    public void SetAll ()
    {
        directionSpeedX = Random.Range(-maximumDirectionSpeed, maximumDirectionSpeed);
        if (directionSpeedX == 0.0f) directionSpeedX = maximumDirectionSpeed;
        directionSpeedY = Random.Range(-maximumDirectionSpeed, maximumDirectionSpeed);
        if (directionSpeedY == 0.0f) directionSpeedY = maximumDirectionSpeed;
        rotationSpeed = Random.Range(-maximumRotationSpeed, maximumRotationSpeed);
        if (rotationSpeed == 0.0f) rotationSpeed = maximumRotationSpeed;
        scaleSpeed = Random.Range(-maximumScaleSpeed, maximumScaleSpeed);
        if (scaleSpeed == 0.0f) scaleSpeed = maximumScaleSpeed;
        colorSpeed = Random.Range(-maximumColorSpeed, maximumColorSpeed);
        if (colorSpeed == 0.0f) colorSpeed = maximumColorSpeed;
        alphaSpeed = Random.Range(-maximumAlphaSpeed, maximumAlphaSpeed);
        if (alphaSpeed == 0.0f) alphaSpeed = maximumAlphaSpeed;
        this.gameObject.GetComponent<Image>().color = new Color(Random.Range(minimumColor, maximumColor), Random.Range(minimumColor, maximumColor), Random.Range(minimumColor, maximumColor), Random.Range(minimumAlpha, maximumAlpha));
    }
}