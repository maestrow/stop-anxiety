module App

open Fable.Core.JsInterop
open Fable.Core
open Fable.Import
open Elmish
open Fable

open Data
open Model
open Views

let init() : Model = Welcome

// UPDATE
let update (model:Model) = function
  | Start -> Step 1
  | Forward -> 
    match model with
      | Step x when x < steps.Length -> Step (x + 1)
      | Step x when x = steps.Length -> Finish
      | _ -> Welcome
  | Backward -> 
    match model with 
      | Step x when x > 1 -> Step (x - 1)
      | _ -> Welcome


let styles: obj = importAll "./styles.sass"
 
open Elmish.React

// App
Program.mkSimple init update view
|> Program.withConsoleTrace
|> Program.withReact "app"
|> Program.run