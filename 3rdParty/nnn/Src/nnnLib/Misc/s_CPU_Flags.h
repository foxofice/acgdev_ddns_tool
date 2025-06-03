//--------------------------------------------------------------------------------------
// Copyright (c) AcgDev
// https://www.AcgDev.com/
//
// Desc : CPU 指令集/特性
//--------------------------------------------------------------------------------------

#pragma once
#ifndef _NNNLIB___MISC___S_CPU_FLAGS_H_
#define _NNNLIB___MISC___S_CPU_FLAGS_H_

#include "../../common/common.h"

#if (NNN_PLATFORM == NNN_PLATFORM_WIN32)

#include <string>

namespace NNN
{
namespace Misc
{

enum struct es_CPU_Type
{
	Unknown,
	Intel,					// "GenuineIntel"					- Intel
	AMD_K5,					// "AMDisbetter!"					- 早期AMD K5芯片的工程样品芯片
	AMD,					// "AuthenticAMD"					- AMD
	Centour,				// "CentourHaul"					- Centour
	Cyrix,					// "CyrixInstead"					- Cyrix
	Transmeta,				// "GenuineTMx86" 或 "TransmetaCPU"	- Transmeta
	National_Semiconductor,	// "Geode by NSC"					- National Semiconductor
	NexGen,					// "NexGenDriven"					- NexGen
	SiS,					// "SiS SiS SiS"					- SiS
	Rise,					// "RiseRiseRise"					- Rise
	UMC,					// "UMC UMC UMC"					- UMC
	VIA,					// "VIA VIA VIA"					- VIA
};


struct s_CPU_flags
{
	// 构造函数
	NNN_API	s_CPU_flags();

	es_CPU_Type	m_cpu_type	= es_CPU_Type::Unknown;
	std::string	m_Vendor;
	std::string	m_Brand;

	struct
	{
		bool SSE3;					// Streaming SIMD Extensions 3 (SSE3). A value of 1 indicates the processor supports this technology.
		bool PCLMULQDQ;				// PCLMULQDQ. A value of 1 indicates the processor supports the PCLMULQDQ instruction.
		bool DTES64;				// 64-bit DS Area. A value of 1 indicates the processor supports DS area using 64-bit layout.
		bool MONITOR;				// MONITOR/MWAIT. A value of 1 indicates the processor supports this feature.
		bool DS_CPL;				// CPL Qualified Debug Store. A value of 1 indicates the processor supports the extensions to the Debug Store feature to allow for branch message storage qualified by CPL.
		bool VMX;					// Virtual Machine Extensions. A value of 1 indicates that the processor supports this technology.
		bool SMX;					// Safer Mode Extensions. A value of 1 indicates that the processor supports this technology. See Chapter 6, “Safer Mode Extensions Reference”.
		bool EIST;					// Enhanced Intel SpeedStep® technology. A value of 1 indicates that the processor supports this technology.
		bool TM2;					// Thermal Monitor 2. A value of 1 indicates whether the processor supports this technology.
		bool SSSE3;					// A value of 1 indicates the presence of the Supplemental Streaming SIMD Extensions 3 (SSSE3). A value of 0 indicates the instruction extensions are not present in the processor.
		bool CNXT_ID;				// L1 Context ID. A value of 1 indicates the L1 data cache mode can be set to either adaptive mode or shared mode. A value of 0 indicates this feature is not supported. See definition of the IA32_MISC_ENABLE MSR Bit 24 (L1 Data Cache Context Mode) for details.
		bool SDBG;					// A value of 1 indicates the processor supports IA32_DEBUG_INTERFACE MSR for silicon debug.
		bool FMA;					// A value of 1 indicates the processor supports FMA extensions using YMM state.
		bool CMPXCHG16B;			// CMPXCHG16B Available. A value of 1 indicates that the feature is available. See the “CMPXCHG8B/CMPXCHG16B—Compare and Exchange Bytes” section in this chapter for a description.
		bool FMA3;					// Fused multiply-add (FMA3)
		bool xTPR;					// xTPR Update Control. A value of 1 indicates that the processor supports changing IA32_MISC_ENABLE[bit 23].
		bool PDCM;					// Perfmon and Debug Capability: A value of 1 indicates the processor supports the performance and debug feature indication MSR IA32_PERF_CAPABILITIES.
		bool PCID;					// Process-context identifiers. A value of 1 indicates that the processor supports PCIDs and that software may set CR4.PCIDE to 1.
		bool FMA4;					// FMA4 Four-operand FMA instruction support.
		bool DCA;					// A value of 1 indicates the processor supports the ability to prefetch data from a memory mapped device.
		bool SSE41;					// A value of 1 indicates that the processor supports SSE4.1.
		bool SSE42;					// A value of 1 indicates that the processor supports SSE4.2.
		bool x2APIC;				// A value of 1 indicates that the processor supports x2APIC feature.
		bool MOVBE;					// A value of 1 indicates that the processor supports MOVBE instruction.
		bool POPCNT;				// A value of 1 indicates that the processor supports the POPCNT instruction.
		bool TSC_Deadline;			// A value of 1 indicates that the processor’s local APIC timer supports one-shot operation using a TSC deadline value.
		bool AESNI;					// A value of 1 indicates that the processor supports the AESNI instruction extensions.
		bool XSAVE;					// A value of 1 indicates that the processor supports the XSAVE/XRSTOR processor extended states feature, the XSETBV/XGETBV instructions, and XCR0.
		bool OSXSAVE;				// A value of 1 indicates that the OS has set CR4.OSXSAVE[bit 18] to enable XSETBV/XGETBV instructions to access XCR0 and to support processor extended state management using XSAVE/XRSTOR.
		bool AVX;					// A value of 1 indicates the processor supports the AVX instruction extensions.
		bool F16C;					// A value of 1 indicates that processor supports 16-bit floating-point conversion instructions.
		bool RDRAND;				// A value of 1 indicates that processor supports RDRAND instruction.
		bool FPU;					// Floating Point Unit On-Chip. The processor contains an x87 FPU.
		bool VME;					// Virtual 8086 Mode Enhancements. Virtual 8086 mode enhancements, including CR4.VME for controlling the feature, CR4.PVI for protected mode virtual interrupts, software interrupt indirection, expansion of the TSS with the software indirection bitmap, and EFLAGS.VIF and EFLAGS.VIP flags.
		bool DE;					// Debugging Extensions. Support for I/O breakpoints, including CR4.DE for controlling the feature, and optional trapping of accesses to DR4 and DR5.
		bool PSE;					// Page Size Extension. Large pages of size 4 MByte are supported, including CR4.PSE for controlling the feature, the defined dirty bit in PDE (Page Directory Entries), optional reserved bit trapping in CR3, PDEs, and PTEs.
		bool TSC;					// Time Stamp Counter. The RDTSC instruction is supported, including CR4.TSD for controlling privilege.
		bool MSR;					// Model Specific Registers RDMSR and WRMSR Instructions. The RDMSR and WRMSR instructions are supported. Some of the MSRs are implementation dependent.
		bool PAE;					// Physical Address Extension. Physical addresses greater than 32 bits are supported: extended page table entry formats, an extra level in the page translation tables is defined, 2-MByte pages are supported instead of 4 Mbyte pages if PAE bit is 1.
		bool MCE;					// Machine Check Exception. Exception 18 is defined for Machine Checks, including CR4.MCE for controlling the feature. This feature does not define the model-specific implementations of machine-check error logging, reporting, and processor shutdowns. Machine Check exception handlers may have to depend on processor version to do model specific processing of the exception, or test for the presence of the Machine Check feature.
		bool CX8;					// CMPXCHG8B Instruction. The compare-and-exchange 8 bytes (64 bits) instruction is supported (implicitly locked and atomic).
		bool APIC;					// APIC On-Chip. The processor contains an Advanced Programmable Interrupt Controller (APIC), responding to memory mapped commands in the physical address range FFFE0000H to FFFE0FFFH (by default - some processors permit the APIC to be relocated).
		bool SEP;					// SYSENTER and SYSEXIT Instructions. The SYSENTER and SYSEXIT and associated MSRs are supported.
		bool MTRR;					// Memory Type Range Registers. MTRRs are supported. The MTRRcap MSR contains feature bits that describe what memory types are supported, how many variable MTRRs are supported, and whether fixed MTRRs are supported.
		bool PGE;					// Page Global Bit. The global bit is supported in paging-structure entries that map a page, indicating TLB entries that are common to different processes and need not be flushed. The CR4.PGE bit controls this feature.
		bool MCA;					// Machine Check Architecture. A value of 1 indicates the Machine Check Architecture of reporting machine errors is supported. The MCG_CAP MSR contains feature bits describing how many banks of error reporting MSRs are supported.
		bool CMOV;					// Conditional Move Instructions. The conditional move instruction CMOV is supported. In addition, if x87 FPU is present as indicated by the CPUID.FPU feature bit, then the FCOMI and FCMOV instructions are supported
		bool PAT;					// Page Attribute Table. Page Attribute Table is supported. This feature augments the Memory Type Range Registers (MTRRs), allowing an operating system to specify attributes of memory accessed through a linear address on a 4KB granularity.
		bool PSE_36;				// 36-Bit Page Size Extension. 4-MByte pages addressing physical memory beyond 4 GBytes are supported with 32-bit paging. This feature indicates that upper bits of the physical address of a 4-MByte page are encoded in bits 20:13 of the page-directory entry. Such physical addresses are limited by MAXPHYADDR and may be up to 40 bits in size.
		bool PSN;					// Processor Serial Number. The processor supports the 96-bit processor identification number feature and the feature is enabled.
		bool CLFSH;					// CLFLUSH Instruction. CLFLUSH Instruction is supported.
		bool DS;					// Debug Store. The processor supports the ability to write debug information into a memory resident buffer. This feature is used by the branch trace store (BTS) and processor event-based sampling (PEBS) facilities (see Chapter 23, “Introduction to Virtual-Machine Extensions,” in the Intel® 64 and IA-32 Architectures Software Developer’s Manual, Volume 3C).
		bool ACPI;					// Thermal Monitor and Software Controlled Clock Facilities. The processor implements internal MSRs that allow processor temperature to be monitored and processor performance to be modulated in predefined duty cycles under software control.
		bool MMX;					// Intel MMX Technology. The processor supports the Intel MMX technology.
		bool FXSR;					// FXSAVE and FXRSTOR Instructions. The FXSAVE and FXRSTOR instructions are supported for fast save and restore of the floating point context. Presence of this bit also indicates that CR4.OSFXSR is available for an operating system to indicate that it supports the FXSAVE and FXRSTOR instructions.
		bool SSE;					// SSE. The processor supports the SSE extensions.
		bool SSE2;					// SSE2. The processor supports the SSE2 extensions.
		bool SS;					// Self Snoop. The processor supports the management of conflicting memory types by performing a snoop of its own cache structure for transactions issued to the bus.
		bool HTT;					// Max APIC IDs reserved field is Valid. A value of 0 for HTT indicates there is only a single logical processor in the package and software should assume only a single APIC ID is reserved. A value of 1 for HTT indicates the value in CPUID.1.EBX[23:16] (the Maximum number of addressable IDs for logical processors in this package) is valid for the package.
		bool TM;					// Thermal Monitor. The processor implements the thermal monitor automatic thermal control circuitry (TCC).
		bool PBE;					// Pending Break Enable. The processor supports the use of the FERR#/PBE# pin when the processor is in the stop-clock state (STPCLK# is asserted) to signal the processor that an interrupt is pending and that the processor should return to normal operation to handle the interrupt. Bit 10 (PBE enable) in the IA32_MISC_ENABLE MSR enables this capability.
		bool FSGSBASE;				// FSGSBASE. Supports RDFSBASE/RDGSBASE/WRFSBASE/WRGSBASE if 1.
		bool MSR_IA32_TSC_ADJUST;	// IA32_TSC_ADJUST MSR is supported if 1.
		bool SGX;					// Supports Intel® Software Guard Extensions (Intel® SGX Extensions) if 1.
		bool BMI1;					// BMI1
		bool HLE;					// TEX: HLE(Hardware Lock Elide/硬件锁消除)
		bool AVX2;					// AVX2
		bool FDP_EXCPTN_ONLY;		// FDP_EXCPTN_ONLY. x87 FPU Data Pointer updated only on x87 exceptions if 1.
		bool SMEP;					// SMEP. Supports Supervisor-Mode Execution Prevention if 1.
		bool BMI2;					// BMI2
		bool ERMS;					// Supports Enhanced REP MOVSB/STOSB if 1.
		bool INVPCID;				// INVPCID. If 1, supports INVPCID instruction for system software that manages process-context identifiers.
		bool RTM;					// TSX: RTM(Restricted Transaction Memory/有限事务内存)
		bool RDT_M;					// RDT-M. Supports Intel® Resource Director Technology (Intel® RDT) Monitoring capability if 1.
		bool DEPFPU_CS_DS;			// Deprecates FPU CS and FPU DS values if 1.
		bool MPX;					// MPX. Supports Intel® Memory Protection Extensions if 1.
		bool RDT_A;					// RDT-A. Supports Intel® Resource Director Technology (Intel® RDT) Allocation capability if 1.
		bool AVX512F;				// AVX512F
		bool AVX512DQ;				// AVX512DQ
		bool RDSEED;				// RDSEED
		bool ADX;					// ADX
		bool SMAP;					// SMAP. Supports Supervisor-Mode Access Prevention (and the CLAC/STAC instructions) if 1.
		bool AVX512_IFMA;			// AVX512_IFMA
		bool CLFLUSHOPT;			// CLFLUSHOPT
		bool CLWB;					// CLWB
		bool Intel_PT;				// Intel Processor Trace.
		bool AVX512PF;				// AVX512PF. (Intel® Xeon PhiTM only.)
		bool AVX512ER;				// AVX512ER. (Intel® Xeon PhiTM only.)
		bool AVX512CD;				// AVX512CD
		bool SHA;					// SHA. supports Intel® Secure Hash Algorithm Extensions (Intel® SHA Extensions) if 1.
		bool AVX512BW;				// AVX512BW
		bool AVX512VL;				// AVX512VL
		bool PREFETCHWT1;			// PREFETCHWT1. (Intel® Xeon PhiTM only.)
		bool AVX512_VBMI;			// AVX512_VBMI
		bool UMIP;					// UMIP. Supports user-mode instruction prevention if 1.
		bool PKU;					// PKU
		bool OSPKE;					// OSPKE. If 1, OS has set CR4.PKE to enable protection keys (and the RDPKRU/WRPKRU instructions).
		bool WAITPKG;				// WAITPKG
		bool GFNI;					// GFNI
		bool AVX512_VPOPCNTDQ;		// AVX512_VPOPCNTDQ. (Intel® Xeon PhiTM only.)
		bool RDPID;					// RDPID and IA32_TSC_AUX are available if 1.
		bool CLDEMOTE;				// CLDEMOTE. Supports cache line demote if 1.
		bool MOVDIRI;				// MOVDIRI. Supports MOVDIRI if 1.
		bool MOVDIR64B;				// MOVDIR64B. Supports MOVDIR64B if 1.
		bool SGX_LC;				// SGX_LC. Supports SGX Launch Configuration if 1.
		bool LAHF;					// LAHF/SAHF available in 64-bit mode.*
		bool LZCNT;					// LZCNT
		bool ABM;					// (AMD)
		bool SSE4a;					// (AMD)
		bool XOP;					// (AMD)
		bool TBM;					// (AMD)
		bool SYSCALL;				// SYSCALL/SYSRET.**
		bool MMXEXT;				// (AMD)
		bool RDTSCP;				// RDTSCP and IA32_TSC_AUX are available if 1.
		bool Intel64;				// Intel® 64 Architecture available if 1.
		bool _3DNOWEXT;				// (AMD)
		bool _3DNOW;				// (AMD)
	} flags;
};

// 全局对象
extern struct s_CPU_flags	g_CPU_flags;

}	// namespace Misc
}	// namespace NNN

#endif	// NNN_PLATFORM_WIN32

#endif	// _NNNLIB___MISC___S_CPU_FLAGS_H_
