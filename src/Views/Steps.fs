module Views.Steps

open Model
open Data
open Views.Common

open Fable.Core.JsInterop
module P = Fable.Helpers.React.Props
module R = Fable.Helpers.React

// Fulma
open Fulma
open Fulma.Layouts
open Fulma.Elements
open Fulma.Elements.Form

let stepTpl dispatch (step: int) (currentAnswer: string) = 
  let question, comment, idea = steps.Item(step-1)
  Section.section 
    [ 
      Section.CustomClass ([Styles.isFlex; Styles.isHorizontalCentered] |> String.concat " ")
    ]
    [
      Box.box' [ GenericOption.CustomClass Styles.myPanel ] [
        Helpers.title question
        Helpers.subtitle idea
        Content.content [] [ R.str comment ]
        Field.div [] [
          // R.input
          //   [ 
          //     P.Value currentAnswer
          //     P.Placeholder "Ответ" 
          //     P.OnChange (fun e -> !!e.target?value |> UpdateAnswer |> dispatch)
          //   ]
          Textarea.textarea [ Textarea.Props 
            [
              P.Cols 30.
              P.Rows 10.
              P.Value currentAnswer
              P.Placeholder "Ответ" 
              P.OnChange (fun e -> !!e.target?value |> UpdateAnswer |> dispatch)
            ]] []
          //R.p [] [R.str (sprintf "step: %d; currentAnswer: '%s'" step currentAnswer + "'")]
        ]
        Level.level [] [
          Level.left [] [ R.a [ P.OnClick (fun _ -> dispatch Backward) ] [R.str "Назад"] ]
          Level.item [] [ Button.button [ Button.CustomClass "is-large"; Button.CustomClass "is-primary"; Button.Props [ P.OnClick (fun _ -> dispatch Forward ) ] ] [ R.str "Далее" ] ]
        ]
      ]
    ]
    
  
