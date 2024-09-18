using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 1f;

    public void LoadNextScene()
    {
        // When Save function comes out this should change
        StartCoroutine(LoadScene(1));
    }

    IEnumerator LoadScene(int index)
    {
        animator.SetTrigger("FishLoadStart");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
