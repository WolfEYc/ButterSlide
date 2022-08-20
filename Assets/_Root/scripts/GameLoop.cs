using UnityEngine;
using UnityEngine.Events;

public class GameLoop : MonoBehaviour
{
    public static GameLoop Inst { get; private set; }

    public UnityEvent death;
    public UnityEvent spawn;
    
    void Awake()
    {
        if (Inst != null)
        {
            Destroy(this);
            return;
        }

        Inst= this;
        DontDestroyOnLoad(Inst);
    }


    public void InvokeDeath()
    {
        death.Invoke();
    }

    public void InvokeSpawn()
    {
        spawn.Invoke();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
