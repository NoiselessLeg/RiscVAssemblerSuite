# RiscVAssemblerSuite
This repo is for the Risc-V Assembler project for SP18.605.411.31 Computer Architecture. This consists of the main assembler, some interfaces designed to utilize it, and a disassembler which hasn't progressed anywhere past creating the initial project for it. So far, the most work has been done with the command line and the main assembler. The GUI opens a neat looking window, but that's about the extent of it. If you are familiar with MVVM, feel free to go to town on it.

# Project Architecture
Assembler.Main - the main assembler binary. This generates a .dll file that is linked against the CmdLine or GUI tool.

Assembler.Common - common utilities shared among all the projects. Generates a .dll file linked against all projects.

Assembler.Disassembler - TBD: a disassembler that will eventually take a binary file and disassemble it into RISC-V instructions and data layout.

Assembler.Gui - WIP: a IDE-like GUI for assembling/disassembling.

Assembler.CmdLine - a command-line interface for assembling files.

# How to use the command line
Options and such:

  <b>-i, --input</b>:   Required. Input assembly files to be assembled.
  
  <b>-l, --logfile</b>:     Log file to output to (optional).
  
  <b>-t, --textaddress</b>:    (Default: 4194304) Base .text segment address (optional, probably shouldn't be modified).
  
  <b>-d, --dataaddress</b>:     (Default: 268500992) Base .data segment address (optional, probably shouldn't be modified).
  
  <b>--help</b>:    Display help screen.
  
  <b>--version</b>:     Display version information.

# What needs done?

A bit still needs work. Code generators need written for various instructions depending on how deep down the rabbit hole we want to go. The pseudo-instruction sizes need determined for the first-pass assembler to calculate the appropriate symbol offsets (right now, everything is assumed to take four bytes).

I've validated some instructions and they do generate the expected values. I'm going to attempt to write up a sequence of unit tests at some point and commit that as part of the project.
