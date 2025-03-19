using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Button _shootingButton;
    [SerializeField] private Button _bombButton;
    [SerializeField] private Button _flyButton;
    void Awake()
    {
        _shootingButton.onClick.AddListener(GameEvent.Shoot);
        _bombButton.onClick.AddListener(GameEvent.DropBomb);
        _flyButton.onClick.AddListener(GameEvent.Fly);
    }

    void OnDestroy()
    {
        _shootingButton.onClick.RemoveListener(GameEvent.Shoot);
        _bombButton.onClick.RemoveListener(GameEvent.DropBomb);
        _flyButton.onClick.RemoveListener(GameEvent.Fly);
    }
}
