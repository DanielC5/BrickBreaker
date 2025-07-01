using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particles;

    public void spawnParticles(Transform transform, Renderer renderer)
    {
        GameObject part = Instantiate(particles, transform.position, transform.rotation);
        part.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material.color = renderer.material.color;
        part.transform.Rotate(Vector3.right * 90);
        Destroy(part, 0.2f);
    }
}
