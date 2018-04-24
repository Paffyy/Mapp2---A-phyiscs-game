using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Machine : MonoBehaviour {
    public GameObject settingsPanel;
    public GameObject machine;
    public Canvas canvas;
    public Button ButtonTemplate;
    public string[] actionNames;
    public Button.ButtonClickedEvent[] actions;
    public Slider sliderTemplate;
    public string sliderNames;
    public Slider.SliderEvent sliderEvents;
    public int minValue;
    public int maxValue;
    private float fanSpeed;
    private int flip;
    private GameObject panel;
    private RaycastHit2D tapped;
    private Collider2D colliderClick;
    private Vector3 offset;
    private Vector3 screenPoint;
    private List<GameObject> panels;
    private float heldDownTime = 0;
    public GameController gameController;

    void Start()
    {
        colliderClick = GetComponent<Collider2D>();
        tapped = new RaycastHit2D();
    }
    private void Update()
    {
        foreach (var item in Input.touches)
        {
            if (item.phase == TouchPhase.Began && gameController.CanEdit())
            {
                ShowSettingsPanel();
            }
        }
    }
    public void ShowSettingsPanel()
    {
        panel = Instantiate(settingsPanel);
        int i;
        for (i = 0; i < actions.Length || i < actionNames.Length; i++)
        {
            AddButtonToPanel(actionNames[i], actions[i], i);
        }
        if (sliderEvents.GetPersistentEventCount() > 0 && !sliderNames.Equals(""))
        {
            AddSliderToPanel(sliderNames, sliderEvents, i, minValue, maxValue);
        }
        panel.transform.SetParent(canvas.transform, false);
        panel.transform.position = GetLocation();
        panel.SetActive(true);
        panel.GetComponentInChildren<Slider>().value = (int)fanSpeed;
        panel.GetComponentInChildren<Button>().onClick.AddListener(() => 
        {
            HideSettingsPanel(panel);
        });
    }
    public void HideSettingsPanel(GameObject panel)
    {
        if (panel != null)
        {
            Destroy(panel);
        }
    }
    // Returns location of the top right corner of the sprite converted to UI coordinates
    public Vector3 GetLocation()
    {
        return Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2, transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2, transform.position.z));
    }
    public void AddButtonToPanel(string buttonText, Button.ButtonClickedEvent actions, int i)
    {
        Button button = Instantiate(ButtonTemplate, new Vector3(0,110- 60*i), new Quaternion(0f,0f,0f,0f));
        var text = button.GetComponentInChildren<Text>();
        text.text = buttonText;
        button.onClick = actions;
        button.transform.SetParent(panel.transform, false);
    }

    public void AddSliderToPanel(string sliderName, Slider.SliderEvent sliderEvents, int i, int minValue, int maxValue)
    {
        Slider slider = Instantiate(sliderTemplate, new Vector3(78, 110 - 40 * (i+1)), new Quaternion(0f, 0f, 0f, 0f));
        slider.GetComponentInChildren<Text>().text = sliderNames;
        slider.transform.SetParent(panel.transform, false);
        slider.wholeNumbers = true;
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.onValueChanged = sliderEvents;

    }
    void OnMouseDown()
    {
        if (panel == null)
        {
            ShowSettingsPanel();
        }
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    }
    void OnMouseDrag()
    {
        heldDownTime += Time.deltaTime;
        if (heldDownTime > 0.6f)
        {
            panel.SetActive(false);
        }
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
        Vector3 curPostition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPostition;
    }
    void OnMouseUp()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            panel.transform.position = GetLocation();
        }
    }
    public void Flip()
    {
        flip = flip == 1 ? -1 : 1;
        transform.localScale = new Vector3(transform.transform.localScale.x * flip, transform.transform.localScale.y, transform.transform.localScale.z);
    }
    public void FlipLeft()
    {
        transform.localScale = new Vector3(System.Math.Abs(transform.transform.localScale.x) * (-1), transform.transform.localScale.y, transform.transform.localScale.z);
    }
    public void FlipRight()
    {
        transform.localScale = new Vector3(System.Math.Abs(transform.transform.localScale.x), transform.transform.localScale.y, transform.transform.localScale.z);
    }
    public void FanSpeed()
    {
        fanSpeed = panel.GetComponentInChildren<Slider>().value;
        Debug.Log(fanSpeed);
    }
}
