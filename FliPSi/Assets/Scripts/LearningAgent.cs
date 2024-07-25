using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LearningAgent : MonoBehaviour
{
    //
    public class State {
        public static int id = 0;
        public List<Action> available_actions;

        public State() {
            id = id++;
        }
    }

    //
    public class Action {
        public delegate void exec();
    }

    public class Reward {
        public int r = 0;
    }

    //

    public List<State> current_states;

    public void register_state() {
        current_states.Add(new State());
    }

}
