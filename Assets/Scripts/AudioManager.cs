using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*
    Script AudioManager này được dùng để quản lý âm thanh trong game
    -Nhạc nền (background music).
    -Hiệu ứng âm thanh (SFX – Sound Effects).
    -Điều chỉnh âm lượng và quản lý trạng thái phát nhạc.
    */
    [Header("-------Audio source-------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("--------Audio clip--------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip buttonSelection;
    [Header("--------Audio clip--------")]
    public float AudioVolume = 1.0f;
    public float SFXVolume = 1.0f;

    //  hàm này sẽ tự động gọi PlayBackgroundMusic() để phát nhạc nền khi start game
    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (background)
        {
            /*
            Kiểm tra nếu có file nhạc nền (background), thì gán vào musicSource rồi phát nhạc.
            musicSource.volume = AudioVolume; giúp điều chỉnh âm lượng. 
            */
            musicSource.clip = background;
            musicSource.volume = AudioVolume;
            musicSource.Play();
        }
    }
    private void Update()
    {
        /* Luôn cập nhật âm lượng cho cả nhạc nền và hiệu ứng âm thanh dựa vào AudioVolume và SFXVolume */
        if (background)
            musicSource.volume = AudioVolume;
        if (death && buttonSelection)
            SFXSource.volume = SFXVolume;
    }
    public void PlaySFX(AudioClip clip, float volumn)
    {
        if (clip != null)
        {
            SFXVolume = volumn;
            SFXSource.PlayOneShot(clip, SFXVolume);
        }
    }

    /* thì gọi Stop() để dừng. */
    public void StopBackgroundMusic()
    {
        if (background && musicSource.isPlaying) 
        {
            musicSource.Stop();
        }
    }

    public void PauseBackgroundMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void UnpauseBackgroundMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }
}


























/*
 🛠 Cách sử dụng script này trong Unity
Bước 1: Thêm AudioManager vào game
Tạo một GameObject, đặt tên là AudioManager.

Gán script AudioManager vào GameObject đó.

Bước 2: Thêm AudioSource vào AudioManager
Chọn AudioManager trong Hierarchy.

Bấm Add Component → Chọn AudioSource (thêm 2 cái, đặt tên MusicSource và SFXSource).

Tắt Play On Awake của SFXSource (để tránh nó tự phát).

Bước 3: Kéo thả Audio vào Inspector
MusicSource → musicSource

SFXSource → SFXSource

Background music → background

Death sound → death

Button sound → buttonSelection

Bước 4: Gọi hàm từ UI
Chọn nút trong UI → Thêm OnClick().

Kéo AudioManager vào OnClick().

Chọn hàm AudioManager → PlaySFX() và truyền file âm thanh vào.
 */