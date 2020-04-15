This BootstrapExe project generates a simple native Exe that would be embedded as a resource in the SecureAssembly Project.

Whenever the Secure Assembly Wishes to Secure a managed Assembly it would dump this BootStrapExe onto the specified hard disk location and would add the specified managed assembly to its resources in encrypted form.

When the user starts up the BootstrapExe, it would in turn decrypt the assembly from its resources and would start executing it in memory using interoperability.