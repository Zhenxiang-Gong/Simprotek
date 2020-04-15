!include master.mak

all: mgdhost.dll gentlb copyasm

mgdhost.dll:
	$(_CS) $(_CS_DLL_FLAGS) $*.cs
gentlb:
	tlbexp mgdhost.dll /out:mgdhost.tlb

copyasm:
        copy mgdhost.dll ..\Debug