using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour 
{
  const string MASTER_VOLUME_KEY = "master_volume";
  const string DIFFICULTY_KEY = "difficulty";
  const string LEVEL_KEY = "level_unlocked_";

  public static void SetMasterVolume(float volume)
  {
    if (volume >= 0f && volume <= 1f)
      PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    else
      Debug.LogError("Master volume out of range: " + volume);
  }

  public static float GetMasterVolume()
  {
    return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
  }

  public static void UnlockLevel(int level)
  {
    if (level <= SceneManager.sceneCountInBuildSettings - 1)
      PlayerPrefs.SetInt(LEVEL_KEY + level, 1);
    else
      Debug.LogError("Attempted to unlock invalid level: " + level);
  }

  public static bool IsLevelUnlocked(int level)
  {
    if (level >= SceneManager.sceneCountInBuildSettings)
    {
      Debug.LogError("Attempted to check level unlocked on invalid level: " + level);
      return false;
    }

    return PlayerPrefs.GetInt(LEVEL_KEY + level) == 1;
  }

  public static void SetDifficulty(float diff)
  {
    if (diff >= 1f && diff <= 3f)
      PlayerPrefs.SetFloat(DIFFICULTY_KEY, diff);
    else
      Debug.LogError("Attempted to set invalid difficulty: " + diff);
  }

  public static float GetDifficulty()
  {
    return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
  }
}
