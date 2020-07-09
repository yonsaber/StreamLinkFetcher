# StreamLinkFetcher

Yes this sucks.

It runs on WinForms (I couldn't be arsed at the time with WPF) and requires [StreamLink](https://streamlink.github.io/) to run.

## Basic instructions

Enter the streamers username e.g. rileyinthevoid or the full URL https://www.twitch.tv/rileyinthevoid either will work. After that, press "Get Stream URL" (you typically don't need to modify the RingBuffer Size but if you do... well you can). After a few seconds, the Stream Video Link field will be populated, if it isn't, there is a log field below which will give you some info.

Once the link is there, hit "Copy To Clipboard" and boom, you are ready to use the link in VLC or OBS.

When you use the "Clear Input", a log file will be writen to the same location as the program just as a quick trace of urls that have been fetched and any messages in the log section, this is more in case something goes wrong
