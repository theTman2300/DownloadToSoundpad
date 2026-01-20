## Download to soundpad

This is a simple console app to download a sound from youtube and import it into soundpad.

I am not very experienced in making things like this so expect a little bit of bugginess.


## Setup

- Download the latest release
- download [yt-dlp](https://github.com/yt-dlp/yt-dlp)
- Find the file "Settings.json"
- Set the folder path where you would like the downloaded files to end up in "DownloadDirectory"
- Set the path of ytdlp in "ytdlpPath"
- Set the path of ffmpeg in "ffmpegPath"
- (optional) set the template of the output file name in "outputFileTemplate" [yt-dlp readme on setting this](https://github.com/yt-dlp/yt-dlp?tab=readme-ov-file#output-template)


## Usage

Have both this and soundpad open at the same time.

To download and import a sound:

- Copy youtube link
- Paste in console
- Press enter
- Success (hopefully)!

To exit enter e or close the window


Made using:

[Soundpad Connector](https://medokin.github.io/soundpad-connector/index.html)

[yt-dlp](https://github.com/yt-dlp/yt-dlp)

[YoutubeDLSharp](https://github.com/Bluegrams/YoutubeDLSharp)
