using System.Collections.Generic;
using UnityEngine;

public abstract class ABaseState
{
    public delegate bool TransitionDelegate();

    public bool IsFinished { get; protected set; }

    public Dictionary<TransitionDelegate, ABaseState> Transitions
    {
        get
        {
            return new Dictionary<TransitionDelegate, ABaseState>(m_transitions);
        }
    }

    protected AEnemyController m_controller;

    private Dictionary<TransitionDelegate, ABaseState> m_transitions;

    public virtual void Init(AEnemyController _controller,params KeyValuePair<TransitionDelegate, ABaseState>[] _transitions)
    {
        m_transitions = new Dictionary<TransitionDelegate, ABaseState>();
        foreach (var keyValuePair in _transitions)
        {
            m_transitions.Add(keyValuePair.Key, keyValuePair.Value);
        }

        m_controller = _controller;
    }

    public virtual bool Enter()
    {
        // Debug.Log($"Entered {this}!");
        IsFinished = false;
        return true;
    }

    public abstract void Update();

    public virtual void Exit()
    {
        // Debug.Log($"Exit {this}!");
    }
}