using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particles;

    public void spawnParticles(Transform transform, Renderer renderer)
    {
        //instantiates the particles
        GameObject part = Instantiate(particles, transform.position, transform.rotation);
        //makes the particles color the same as the brick color
        part.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material.color = renderer.material.color;
        //rotate them to the right for better visibility
        part.transform.Rotate(Vector3.right * 90);
        //destroys the particles after a bit
        Destroy(part, 0.2f);
    }
}
