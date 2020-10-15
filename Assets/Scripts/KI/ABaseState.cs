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

    protected SheepController m_sheepController;
    protected BeeController m_beeController;
    protected MushroomController m_mushroomController;

    private Dictionary<TransitionDelegate, ABaseState> m_transitions;

    public virtual void SheepInit(SheepController _controller,params KeyValuePair<TransitionDelegate, ABaseState>[] _transitions)
    {
        m_transitions = new Dictionary<TransitionDelegate, ABaseState>();
        foreach (var keyValuePair in _transitions)
        {
            m_transitions.Add(keyValuePair.Key, keyValuePair.Value);
        }

        m_sheepController = _controller;
    }

    public virtual void BeeInit(BeeController _controller,
        params KeyValuePair<TransitionDelegate, ABaseState>[] _transitions)
    {
        m_transitions = new Dictionary<TransitionDelegate, ABaseState>();
        foreach (var keyValuePair in _transitions)
        {
            m_transitions.Add(keyValuePair.Key, keyValuePair.Value);
        }
    
        m_beeController = _controller;
    }
    
    public virtual void MushroomInit(MushroomController _controller,
        params KeyValuePair<TransitionDelegate, ABaseState>[] _transitions)
    {
        m_transitions = new Dictionary<TransitionDelegate, ABaseState>();
        foreach (var keyValuePair in _transitions)
        {
            m_transitions.Add(keyValuePair.Key, keyValuePair.Value);
        }
    
        m_mushroomController = _controller;
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