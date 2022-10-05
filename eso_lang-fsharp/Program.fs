// For more information see https://aka.ms/fsharp-console-apps
module FsharpInterop

let add x y =
    x + y

let printFromFsharp str =
    printfn "%s\n" str
