module App

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open Elmish

open Data
open Model
open Views.Steps
open Views.Welcome
open Views.Finish

let init() : Model = { currentPos = Welcome; fromName = ""; email = ""; answers = List.init steps.Length (fun _ -> "") }

[<Emit("emailjs.send('default_service', 'template_rRh1tuIp', $0)")>]
let sendMail data: unit = jsNative

[<Emit("window.open($0)")>]
let windowOpen url: obj = jsNative

[<Emit("encodeURIComponent($0)")>]
let encodeURIComponent arg: string = jsNative

let getMailBody (answers: string list) = 
  let toString (i, q, a) = sprintf "%d. %s\n%s\n" i q a
  steps
  |> List.mapi (fun i (q, _, _) -> i+1, q, answers.[i])
  |> List.map toString
  |> String.concat "\n"

// UPDATE
let update (model:Model) = function
  | Start -> init () |> (fun m -> { m with currentPos = Step 1 })
  | UpdateAnswer answer -> 
    let step = match model.currentPos with
                | Welcome | Finish -> failwith "Unexpected message"
                | Step step -> step
    let newAnswers = model.answers |> List.mapi (fun i v -> if i = step-1 then answer else v)
    { model with answers = newAnswers }
  | Forward -> 
    match model.currentPos with
      | Step x when x < steps.Length -> { model with currentPos = Step (x + 1) }
      | Step x when x = steps.Length -> { model with currentPos = Finish }
      | _ -> init ()
  | Backward -> 
    match model.currentPos with 
      | Step x when x > 1 -> { model with currentPos = Step (x - 1) }
      | _ -> init ()
  | UpdateName name -> { model with fromName = name }
  | UpdateEmail email -> { model with email = email }
  | SendEmail ->
    let subject = sprintf "[Stop Anxiety] Ответы от %s" model.fromName
    let body = getMailBody model.answers |> encodeURIComponent
    windowOpen (sprintf "mailto:%s?subject=%s&body=%s" model.email subject body) |> ignore
    model

let view dispatch (model: Model) =
  match model.currentPos with
  | Welcome -> welcome dispatch
  | Model.Step s -> 
    printfn "step: %d; answer: '%s'" s (model.answers.[s-1])
    stepTpl dispatch s (model.answers.[s-1])
  | Finish -> finish dispatch model


let styles: obj = importAll "./styles.sass"
 
open Elmish.React

// App
Program.mkSimple init update view
|> Program.withConsoleTrace
|> Program.withReact "app"
|> Program.run