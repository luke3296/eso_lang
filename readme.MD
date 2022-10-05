# build instructions
 for windows : open the .sln in VS, select main program.cs as 
 for linux : use dotnet cli ; cd into directory.
              dotnet build
              dotnet run
              dotnet test
# lexer to-do
turn the single character matches into multicharacter matches e.g replace '+' with 'T_PLUS', will replace with langugae tokens. our lang will have no single character tokens. Will check for tokens in increasing order of length.
will edit the lexer once we decide token names.

refactor into a /Types/Lexer.cs file


coaleses multiple multi character lexmems into the same token with if-else.

add tests to the Lexer
#parser to-do

write the bnf rules

add /Types/parser.cs | fs depending on how we interop

parse Symbols and Tokens into a parse tree and AST

#execution to-do

map to c#
