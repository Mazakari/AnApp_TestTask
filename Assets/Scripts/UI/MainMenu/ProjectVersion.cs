using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class ProjectVersion : MonoBehaviour
{
    private TMP_Text _projectVersion = null;

    // Start is called before the first frame update
    void Start()
    {
        _projectVersion = GetComponent<TMP_Text>();

        _projectVersion.text = $"Maza Games 2023. v{Application.version}";
    }
}
