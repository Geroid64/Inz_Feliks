using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTimer : MonoBehaviour
{
    public MainPlayerHUD player_hud;
    public float time_in_seconds = 60;
    public bool timer_activated = true;
    public PlayerLostScript timer_stop;
    public float minutes, seconds = 0;

    void Start()
    {
        minutes = time_in_seconds / 60;
        seconds = time_in_seconds % 60;
        player_hud.UIUpdateTimeLabel("MissionTimeLabel", minutes, seconds);
        Coroutine routine = StartCoroutine(MissionTimerCountdown());
    }
    private void TimerStopped()
    {
        timer_stop.LoserManager();
    }
    IEnumerator MissionTimerCountdown()
    {
        while (timer_activated)
        {
            time_in_seconds--;
            minutes = Mathf.Floor(time_in_seconds / 60);
            seconds = Mathf.Floor(time_in_seconds % 60);
            player_hud.UIUpdateTimeLabel("MissionTimeLabel", minutes, seconds);
            if (time_in_seconds >= 0)
            {

                yield return new WaitForSeconds(1);
            }
            else
            {
                timer_activated = false;
                TimerStopped();
            }
        }
    }
}
