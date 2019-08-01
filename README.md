# UEFIReadCShasp

UEFIReadCShasp is a C# application that will read a UEFI firmware variable into a buffer and execute it via reflection using the techniques described in [Topher Timzen's post on .NET Machine Code Manipulation](https://www.tophertimzen.com/blog/dotNetMachineCodeManipulation/)

The UEFI Variable read and executed is "CSHARP-UEFI" with the GUID "{E660597E-B94D-4209-9C80-1805B5D19B69}" which is the VirtualBox Test GUID.

## How to Play

1. Fill out `bufferLEN` in `Program.cs` to be the length of your UEFI variables buffer from [UEFIWriteCSharp](http://github.com/perturbed-platypus/UEFIWriteCSharp).
1. Build the project using msbuild / visual studio
2. Run the binary to Hide yo Sh!t!
