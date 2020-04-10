# Beat tracking example
from __future__ import print_function
import librosa
import os

# 1. Get the file path to the included audio example
music_file = "D:\\crypt_music\\1-1.wav"
beat_file = "D:\\crypt_music\\beat.txt"
# 2. Load the audio as a waveform `y`
#    Store the sampling rate as `sr`
y, sr = librosa.load(music_file)

# 3. Run the default beat tracker
tempo, beat_frames = librosa.beat.beat_track(y=y, sr=sr)

print('Estimated tempo: {:.2f} beats per minute'.format(tempo))

# 4. Convert the frame indices of beat events into timestamps
beat_times = librosa.frames_to_time(beat_frames, sr=sr)

print(beat_times[:20])

if os.path.exists(beat_file) :
	os.remove(beat_file)

f = open(beat_file, "a")
for item in beat_times:
	f.write(str(item)+'\n')
f.close()