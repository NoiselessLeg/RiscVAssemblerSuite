# RiscVAssemblerSuite
SP18.605.411.31 Computer Architecture Project

# RiscVAssemblerSuite
Risc-V Assembler Project for Computer Architecture

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
