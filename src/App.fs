module App

open Fable.Core.JsInterop
open Fable.Core
open Fable.Import
open Elmish
open Fable

open Data
open Model
open Views.Steps
open Views.Welcome
open Views.Finish

let init() : Model = { currentPos = Welcome; answers = List.init steps.Length (fun _ -> "") }

// UPDATE
let update (model:Model) = function
  | Start -> { model with currentPos = Step 1 }
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