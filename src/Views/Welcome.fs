module Views.Welcome

open Model
open Data
open Views.Common

open Fable.Core.JsInterop
module P = Fable.Helpers.React.Props
module R = Fable.Helpers.React

// Fulma
open Fulma.Layouts
open Fulma.Elements

let welcome dispatch = 
  R.div [] 
    [ 
      R.div [] [ R.str "Welcome" ]
      R.div [] [ button "Start" Msg.Start dispatch ]
    ]
