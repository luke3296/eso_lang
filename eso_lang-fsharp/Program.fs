module FsharpInterop



    let add x y =
        x + y

    let printFromFsharp str =
        printfn "%s\n" str

    let Parse tokens  =
        for item in tokens do
            printfn "%s" item 
        printfn "done" 
