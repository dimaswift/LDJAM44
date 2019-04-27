using UnityEngine;

[CreateAssetMenu(menuName = "Effect")]
public class Effect : ScriptableObject
{
    public void Init()
    {
        instance = Instantiate(ParticleSystem);
    }
    public AudioClip[] Sounds;
    public ParticleSystem ParticleSystem;

    ParticleSystem instance;

    float lastPlayTime;

    public void Play(Vector3 point, float rotation, float multiplier)
    {
        if (instance == null)
            Init();
        instance.transform.eulerAngles = new Vector3(0, 0, rotation);
        instance.transform.position = point;
        instance.Play(true);
        if(Time.time - lastPlayTime > .1f)
        {
            AudioSource.PlayClipAtPoint(Sounds.Random(), point, Mathf.Min(multiplier, 1));
            lastPlayTime = Time.time;
        }
       
    }
}