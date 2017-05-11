## SoxSharp

SoxSharp is a C# library that serves as a wrapper to [SoX - the Sound eXchange tool](http://sox.sourceforge.net/).

It comes bundled with the SoX 14.4.2 Win32 binary, so if you're on a Windows platform, you don't need any additional prerequisites to start using it. On other platforms (MacOSX and Linux) you will need to set the `SoX.BinaryPath` property to the location where SoX binaries are stored.


### How it works

Usage is pretty straightforward:

```cs
using (Sox sox = new Sox())
{
  sox.Output.Type = FileType.MP3;
  sox.Output.Comment = "Converted using SoX & SoXSharp";

  sox.Process("test.wav", "test.mp3");
}
```
To obtain updated progress information about the operation, you can subscribe to the `OnProgress` event:

```cs
// Subscribe to OnProgress event before calling Process method.
sox.OnProgress += sox_OnProgress;

void sox_OnProgress(object sender, ProgressEventArgs e)
{
  Console.WriteLine("{0} ({1}% completed)", e.Processed.ToString(@"hh\:mm\:ss\.ff"), e.Progress);
}
```
The `Process` method blocks the calling thread until the spawned SoX process ends. 


### Documentation

A detailed description of all the components of the library is available at the [repository wiki](https://github.com/igece/SoxSharp/wiki/Documentation). 

### To do
* Implement handling of SoX filters.
* Port the Test application GUI so it can be used in MacOSX and Linux (using Xamarin).


