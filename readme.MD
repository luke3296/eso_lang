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


coaleses multiple multi character lexmems into the same token with if-else.

add tests to the Lexer
#parser to-do

write the bnf rules []

write bnf that avoids left recursion

parse Symbols and Tokens into a parse tree and AST

#execution to-do
execute the AST
intermediate representations
map to c#
