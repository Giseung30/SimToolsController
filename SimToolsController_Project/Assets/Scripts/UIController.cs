using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text newworkInfo;
    public Slider roll;
    public Slider pitch;
    public Slider yaw; //Yaw <-> Heave
    public Slider heave; //Yaw <-> Heave
    public Slider sway;
    public Slider surge;
    public Slider extra1;
    public Slider extra2;
    public Slider extra3;

    private void Update()
    {
        newworkInfo.text = $"ip : {ControlSimTools.instance.ip}\nport : {ControlSimTools.instance.port}";

        ControlSimTools.instance.simData.Roll = Mathf.RoundToInt(roll.value);
        ControlSimTools.instance.simData.Pitch = Mathf.RoundToInt(pitch.value);
        ControlSimTools.instance.simData.Yaw = Mathf.RoundToInt(yaw.value); //Yaw <-> Heave
        ControlSimTools.instance.simData.Heave = Mathf.RoundToInt(heave.value); //Yaw <-> Heave
        ControlSimTools.instance.simData.Sway = Mathf.RoundToInt(sway.value);
        ControlSimTools.instance.simData.Surge = Mathf.RoundToInt(surge.value);
        ControlSimTools.instance.simData.Extra1 = Mathf.RoundToInt(extra1.value);
        ControlSimTools.instance.simData.Extra2 = Mathf.RoundToInt(extra2.value);
        ControlSimTools.instance.simData.Extra3 = Mathf.RoundToInt(extra3.value);
    }
}