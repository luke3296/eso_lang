module FsharpInterop
    open  eso_lang_classes;
    open System.Collections.Generic;

    let n = Token( 1, "Smith", "acnd")


    let printToken (tok:Token) =
        printfn "\nDONE\n"
        printfn "%s" tok.name
       // let f (a:string) = a.Length
    let add x y =
        x + y

    let printTokenList (tokList : List<Token>) = 
        for item in tokList do
            printfn "%s" item.name

    let printFromFsharp str =
        printfn "%s\n" str
//    let Parse tokens : Token =
 //       for item in tokens do
 //           printfn "fuck"
  //printfn "done" 

