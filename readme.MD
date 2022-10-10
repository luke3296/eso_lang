# build instructions
 for windows : open the .sln in VS,  eso_lang/program.cs is where main is

 for linux : use dotnet cli ; cd into director ./eso_lang or ./eso_lang_test or eso_lang-fsharp

              dotnet build

              dotnet run

              dotnet test

# to create this project structure

mkdir eso_lang  

cd eso_lang

dotnet new sln --name esoteric_language

dotnet new console --name eso_lang

dotnet new xunit --name eso_lang_test

dotnet new --lang f# --name eso_lang-fsharp

dotnet sln add ./eso_lang/eso_lang.csproj

dotnet sln add ./eso_lang_tests/eso_lang_tests.csproj

dotnet sln add ./eso_lang-fsharp/eso_lang-fsharp.fsproj

##the test csproj my reference the eso_lang csproj and eso_lang-fsharp fsproj so it can test them

dotnet add ./eso_lang_tests/eso_lang_tests.csproj ./eso_lang-fsharp/eso_lang-fsharp.fsproj

dotnet add ./eso_lang_tests/eso_lang_tests.csproj ./eso_lang/eso_lang.csproj

##the main eso_lang .csproj must refrence the f# .fsproj to use it

dotnet add  ./eso_lang/eso_lang.csproj  ./eso_lang-fsharp/eso_lang-fsharp.fsproj

# lexer to-do
turn the single character matches into multicharacter matches e.g replace '+' with 'T_PLUS', will replace with langugae tokens. our lang will have no single character tokens. Will check for tokens in increasing order of length.
will edit the lexer once we decide token names.


coalesces multiple multi character lexemes into the same token with if-else.

make the Lexer regex based

add tests for the Lexer

# parser to-do

write the bnf rules []

write bnf that avoids left recursion

refactor the token and symbol tables into a global struct / class of there own as they are shared

make the symbol table an array of ints not strings

parse Symbols and Tokens into a parse tree and AST

# execution to-do
parse over the AST

handle the productions

make intermediate representations

generate c# code

compile c# code into executable

# gui to do

ensure its decoupled maybe add another c# project,  add it to the .sln and reference the main eso_lang project

# general to do

keep a live log each or together?

create a list of function that need to be implement to
- generate the tree based of production rules (in the parser)
- handle the production rules (in the execution of the AST)

organize these jobs so they can be implemented in sprints
