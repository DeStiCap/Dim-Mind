using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Default timer is
/// start_timer = 2
/// mode = count_down
/// active = false
/// loop = false
/// </summary>
public class Timer : MonoBehaviour
{
    //[SerializeField] private Text txt;
    [SerializeField] private float start_timer = 0;
    [SerializeField] private float timer;

    private enum Mode { count_down, count_up }
    [SerializeField]
    private Mode mode = Mode.count_down;

    [SerializeField] private bool active = false;
    //[SerializeField] private bool loop = false;
    //[SerializeField] private bool ActoinPoint = true;


    public UnityEvent onFinish;

    private void Update()
    {
        if (active == true)
        {
            if (mode == Mode.count_down) { CountdownPerFrame(); }
            else { CountUpPerFrame(); }

            if (mode == Mode.count_down && timer <= 0)
            {
                active = false;
                onFinish.Invoke();
            }

        }
        //txt.text = timer.ToString("f1");
    }

    public void SetTime(float value) { timer = value; }
    /// <summary>
    /// set start value of timer
    /// </summary>
    /// <param name="start_time_float"></param>
    public void SetStartTime(float start_time_float) { start_timer = start_time_float; }
    public float GetStartTime() { return start_timer; }

    /// <summary>
    /// Use to play, resume, play again.
    /// </summary>
    public void Play()
    {
        if (mode == Mode.count_up) { active = true; }
        else if (timer > 0 && mode == Mode.count_down) { active = true; }
        else if (timer <= 0 && mode == Mode.count_down)
        {
            timer = start_timer;
            active = true;
        }
    }
    public void Resume()
    {
        if (mode == Mode.count_up) { active = true; }
        else if (timer > 0 && mode == Mode.count_down && timer != start_timer) { active = true; }
    }
    public void PlayAt(float duration)
    {
        timer = duration / 10f;
        if (mode == Mode.count_up) { active = true; }
        else if (timer > 0 && mode == Mode.count_down) { active = true; }
        else if (timer <= 0 && mode == Mode.count_down)
        {
            active = true;
        }
    }

    /// <summary>
    /// Reset is not play.
    /// You need to use Play(); if you want to start Timer. <para/>
    /// Reset only reset value of (float) timer. <para/>
    /// </summary>
    public void Reset() { Pause(); timer = start_timer; }

    /// <summary>
    /// Set active to false. Make an update class unable to count up or down anymore
    /// </summary>
    public void Pause() { active = false; }

    /// <summary>
    /// Return current time of Timer.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentTime() { return timer; }

    /// <summary>
    /// Set is Timer gonna loop
    /// default is false <para/>
    /// </summary>
    /// <param name="isLooping"></param>
    //public void Loop(bool isLooping) { loop = isLooping; }

    /// <summary>
    /// count_mode must be "up" or "down"
    /// Default is in "down" mode <para/>
    /// </summary>
    /// <param name="count_mode">  </param>
    public void ChangeMode(string count_mode)
    {
        switch (count_mode)
        {
            case "up": mode = Mode.count_up; break;
            case "down": mode = Mode.count_down; break;
            default: break;
        }
    }

    private void CountdownPerFrame()
    {
        timer -= Time.deltaTime;
        /*
        if (loop == true && timer <= 0)
        {
            timer = start_timer;
        }
        */
    }
    private void CountUpPerFrame() { timer += Time.deltaTime; }
    //public void SetActionEnable(bool true_or_false) { ActoinPoint = true_or_false; }
    //public bool IsAction() { return ActoinPoint; }

    public void ModTime(int value)
    {
        timer += value;
    }
}
