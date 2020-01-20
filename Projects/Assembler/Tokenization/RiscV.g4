grammar asm_riscv;

prog
   : (line ('!' line)* EOL)*
   ;

line
   : lbl? (assemblerdirective | instruction)? comment?
   ;

instruction
   : opcode expressionlist?
   ;

lbl
   : label ':'?
   ;

assemblerdirective
   : data
   | text
   | rodata
   | bss
   | align
   | include
   ;
   
data
   : '.' D A T A
   ;
   
text
   : '.' T E X T
   ;
   
rodata
   : '.' R O D A T A
   ;


bss
   : '.' B S S
   ;

include
   : INCLUDE name
   ;

expressionlist
   : argument (',' argument)*
   ;

label
   : name
   ;

argument
   : number
   | register_
   | name
   | string
   ;

register_
   : REGISTER
   ;

string
   : STRING
   ;

name
   : NAME
   ;

number
   : SIGN? NUMBER
   ;

opcode
   : OPCODE
   ;

rep
   : REP
   ;

comment
   : COMMENT
   ;


COMMENT
   : '#' ~ [\r\n]* -> skip
   ;


REGISTER
   : X '0' | Z E R O | X '1' | R A | X '2' | S P | X '3' | G P | X '4' | T P | X '5' | T '0' | X '6' | T '1' | X '7' | T '2' | X '8' | S '0' | F P | X '9' | S '1' | X '1' '0' | A '0' | X '1' '1' | A '1' | X '1' '2' | A '2' | X '1' '3' | A '3' | X '1' '4' | A '4' | X '1' '5' | A '5' | X '1' '6' | A '6' | X '1' '7' | A '7' | X '1' '8' | S '2' | X '1' '9' | S '3' | X '2' '0' | S '4' | X '2' '1' | S '5' | X '2' '2' | S '6' | X '2' '3' | S '7' | X '2' '4' | S '8' | X '2' '5' | S '9' | X '2' '6' | S '1' '0' | X '2' '7' | S '1' '1' | X '2' '8' | T '3' | X '2' '9' | T '4' | X '3' '0' | T '5' | X '3' '1' | T '6'
   ;


OPCODE
   : L U I | A U I P C | J A L | J A L R | B E Q | B E Q Z | B N E | B N E Z | B L T | B L T Z | B G E | B G E Z | B L T U | B G E U | L B | L H | L W | L B U | L H U | S B | S H | S W | A D D I | S L T I | S L T I U | X O R I | O R I | A N D I | S L L I | S R L I | S R A I | A D D | S U B | S L L | S L T | S L T U | X O R | S R L | S R A | O R | A N D | N O T | N O P | L I | L A | M V | J | M U L | M U L H | M U L H S U | D I V | D I V U | R E M | R E M U | E C A L L
   ;


NAME
   : [.a-zA-Z] [a-zA-Z0-9."_]*
   ;
NUMBER
   : [0-9a-fA-F] + ('H' | 'h')?
   ;
STRING
   : '\u0027' ~'\u0027'* '\u0027'
   ;
EOL
   : [\r\n] +
   ;
WS
   : [ \t] -> skip
   ;
fragment A
   : ('a' | 'A')
   ;
fragment B
   : ('b' | 'B')
   ;
fragment C
   : ('c' | 'C')
   ;
fragment D
   : ('d' | 'D')
   ;
fragment E
   : ('e' | 'E')
   ;
fragment F
   : ('f' | 'F')
   ;
fragment G
   : ('g' | 'G')
   ;
fragment H
   : ('h' | 'H')
   ;
fragment I
   : ('i' | 'I')
   ;
fragment J
   : ('j' | 'J')
   ;
fragment K
   : ('k' | 'K')
   ;
fragment L
   : ('l' | 'L')
   ;
fragment M
   : ('m' | 'M')
   ;
fragment N
   : ('n' | 'N')
   ;
fragment O
   : ('o' | 'O')
   ;
fragment P
   : ('p' | 'P')
   ;
fragment Q
   : ('q' | 'Q')
   ;
fragment R
   : ('r' | 'R')
   ;
fragment S
   : ('s' | 'S')
   ;
fragment T
   : ('t' | 'T')
   ;
fragment U
   : ('u' | 'U')
   ;
fragment V
   : ('v' | 'V')
   ;
fragment W
   : ('w' | 'W')
   ;
fragment X
   : ('x' | 'X')
   ;
fragment Y
   : ('y' | 'Y')
   ;
fragment Z
   : ('z' | 'Z')
   ;