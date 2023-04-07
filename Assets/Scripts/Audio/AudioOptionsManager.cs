/**using UnityEngine;
using TMPro;

public class AudioOptionsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soudEffectsSliderText;

    
  // the expose parameter
  public const string MIXER_MUSIC = "musicVolume";
  public const string MIXER_SFX = "sfxVolume";

  public static float MusicVolume { get; private set; }
  public static float SoundEffectsVolume { get; private set; }



  public void OnMusicSliderValueChange(float value)
  {
      MusicVolume = value;
      musicSliderTex.text = value.ToString();
  }
  

}*/
