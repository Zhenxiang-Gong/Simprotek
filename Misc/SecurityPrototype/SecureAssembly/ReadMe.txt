This is main executable. This contains the BootstrapExe.exe and CLRHostExe.Exe as embedded resources in it.

Depending upon the user choice it either generates the CLRHosting version or External DLL Version of 
secured assembly.

When the User requests to secure any manged assembly using External Dll technique, this would dump the 
Bootstrapexe.exe onto hard disk and would embed the managed assembly as a resource into it. 

Note that the gnerated Bootstrapexe.exe would exist with the name specified by the user (by default SecuredExe.exe).

The user may then use the generated native exe in place of the managed assembly.

Note: Before using this, the InvokeAssembly.dll should be registered using the RegAsm command as follows:

<dir>:\> Regasm InvokeAssembly.dll

where <dir> is the directory containing the InvokeAssembly.dll

When the user request to secure any managed assembly using the CLRHosting technique, this would dump
the CLRHostExe.exe onto the hard disk and would embed the managed assembly as a resource into it. 
The dumped CLRHostExe would exist with a name supplied by the user (by default SecuredEXE.EXE); 
In this technique there is no need to register any dlls.