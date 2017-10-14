using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioUtils {

	//TODO CLEAN THIS WHOLE THING UP
	public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos, AudioMixerGroup mGroup)
	{
		if (clip == null)
		{
			//Debug.Log("No AudioClip was passed in!");
			return null;
		}

		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.spatialBlend = 1;
		aSource.outputAudioMixerGroup = mGroup;
		aSource.Play(); // start the sound
		MonoBehaviour.Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}

	public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos, AudioSource reference)
	{
		if (clip == null)
		{
			//Debug.Log("No AudioClip was passed in!");
			return null;
		}

		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.spatialBlend = reference.spatialBlend;
		aSource.minDistance = reference.minDistance;
		aSource.maxDistance = reference.maxDistance;
		aSource.rolloffMode = reference.rolloffMode;
		aSource.dopplerLevel = reference.dopplerLevel;
		aSource.pitch = reference.pitch;
		aSource.outputAudioMixerGroup = reference.outputAudioMixerGroup;
		aSource.Play(); // start the sound
		MonoBehaviour.Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}

	public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
	{
		if (clip == null)
		{
			//Debug.Log("No AudioClip was passed in!");
			return null;
		}

		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.Play(); // start the sound
		MonoBehaviour.Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}

	public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float volume)
	{
		if (clip == null)
		{
			//Debug.Log("No AudioClip was passed in!");
			return null;
		}

		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.volume = volume;
		aSource.Play(); // start the sound
		MonoBehaviour.Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}

	/** /
	public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos, AudioSource reference, float volume)
	{
		if (clip == null)
		{
			//Debug.Log("No AudioClip was passed in!");
			return null;
		}

		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.spatialBlend = reference.spatialBlend;
		aSource.minDistance = reference.minDistance;
		aSource.maxDistance = reference.maxDistance;
		aSource.rolloffMode = reference.rolloffMode;
		aSource.volume = volume;
		aSource.outputAudioMixerGroup = reference.outputAudioMixerGroup;
		aSource.Play(); // start the sound
		MonoBehaviour.Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}
	/**/

	public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos, AudioSource reference, float volume, float pitch)
	{
		if (clip == null)
		{
			//Debug.Log("No AudioClip was passed in!");
			return null;
		}

		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.spatialBlend = reference.spatialBlend;
		aSource.minDistance = reference.minDistance;
		aSource.maxDistance = reference.maxDistance;
		aSource.rolloffMode = reference.rolloffMode;
		aSource.volume = volume;
		aSource.pitch = pitch;
		aSource.outputAudioMixerGroup = reference.outputAudioMixerGroup;
		aSource.Play(); // start the sound
		MonoBehaviour.Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}
}
