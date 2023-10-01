using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ButtonTimer : MonoBehaviour {
    [SerializeField] private int seconds;
    [SerializeField] private Button trapButton;
    [SerializeField] private TextMeshProUGUI textCountdown;
    private int initialSeconds;
    void Start() {
        initialSeconds = seconds;
    }
    void Update() {
        
    }
    public void TaskOnClick() {
        StartCoroutine(ButtonCooldown());
    }

    IEnumerator ButtonCooldown () {
        trapButton.interactable = false;
        InvokeRepeating ("Countdown", 1, 1);
        yield return new WaitForSeconds (seconds);
        trapButton.interactable = true;
        textCountdown.text = "";
        seconds = initialSeconds;
        StopCoroutine(ButtonCooldown());
    }

    public void Countdown() {
        if (--seconds == 0) CancelInvoke ("Countdown");
        textCountdown.text = seconds.ToString();
    }
}