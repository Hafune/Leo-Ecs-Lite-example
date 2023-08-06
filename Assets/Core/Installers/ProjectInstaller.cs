using Core;
using Lib;
using Reflex;
using Reflex.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectInstaller : Installer
{
    [SerializeField] private Transform _projectDependencies;
    [SerializeField] private SceneField _nextScene;
    
    public override void InstallBindings(Container container)
    {
        var projectDependencies = container.Instantiate(_projectDependencies);
        
        var ecs = projectDependencies.GetComponentInChildren<EcsEngine>(true);
        var joystick = projectDependencies.GetComponentInChildren<Joystick>(true);
        
        container.BindInstanceAs(ecs);
        container.BindInstanceAs(joystick);

        EnableProjectDependencies(projectDependencies);
        
        SceneManager.LoadScene(_nextScene);
    }
    
    private void EnableProjectDependencies(Transform projectDependencies)
    {
        projectDependencies.gameObject.SetActive(true);
        var children = new Transform[projectDependencies.childCount];

        for (int i = 0; i < projectDependencies.childCount; i++)
            children[i] = projectDependencies.GetChild(i);

        projectDependencies.DetachChildren();

        foreach (var child in children)
            DontDestroyOnLoad(child);

        Destroy(projectDependencies.gameObject);
    }
}
