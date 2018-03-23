module Views

open Model
open Data

module P = Fable.Helpers.React.Props
module R = Fable.Helpers.React

// Fulma
open Fulma.Layouts
open Fulma.Elements

module Styles = 
  let isFlex = "is-flex" 
  let isHorizontalCentered = "is-horizontal-centered" 
  let myPanel = "my-panel"

module Fulma = 
  let title name = R.div [ P.ClassName "title" ] [ R.str name ]
  let subtitle name = R.div [ P.ClassName "subtitle" ] [ R.str name ]

open Fulma
open Fulma.Elements.Form

let button text msg (dispatch: Msg->unit) = R.button [ P.OnClick (fun _ -> dispatch msg) ] [ R.str text ]

let step dispatch (step: int) = 
  let [question; comment; idea] = steps.Item(step-1)
  Section.section 
    [ 
      Section.CustomClass ([Styles.isFlex; Styles.isHorizontalCentered] |> String.concat " ")
    ]
    [
      Box.box' [ GenericOption.CustomClass Styles.myPanel ] [
        Fulma.title question
        Fulma.subtitle idea
        Content.content [] [ R.str comment ]
        Field.div [] [
          Textarea.textarea [Textarea.Props [ P.Cols 30.; P.Rows 10.; P.Placeholder "Ответ" ]] []
        ]
        Level.level [] [
          Level.left [] [ R.a [ P.OnClick (fun _ -> dispatch Backward) ] [R.str "Назад"] ]
          Level.item [] [ Button.button [ Button.CustomClass "is-large"; Button.CustomClass "is-primary"; Button.Props [ P.OnClick (fun _ -> dispatch Forward) ] ] [ R.str "Далее" ] ]
        ]
      ]
    ]
  
let welcome dispatch = 
  R.div [] 
    [ 
      R.div [] [ R.str "Welcome" ]
      R.div [] [ button "Start" Msg.Start dispatch ]
    ]

let finish dispatch = 
  R.div [] [ R.str "Thank you" ]

let view dispatch (model: Model) =
  match model with
  | Welcome -> welcome dispatch
  | Model.Step s -> step dispatch s
  | Finish -> finish dispatch
  