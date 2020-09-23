using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, exit);

        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            Debug.Log("OK");
            enter[i] = p;
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            Debug.Log("OK2");
            exit[i] = p;
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, exit);
    }
}