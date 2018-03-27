module Views.Finish

module P = Fable.Helpers.React.Props
module R = Fable.Helpers.React

open Fable.Core.JsInterop
open Fulma
open Fulma.Layouts
open Fulma.Elements
open Fulma.Elements.Form

open Model
open Views.Common
open Data
open Fable.Helpers.React.Props
open Elmish.React

module private Impl = 
  
  let answerTpl (i: int) answer = 
    let question, comment, idea = steps.Item(i)
    R.article [ P.Style [ CSSProp.BorderBottom "border-bottom: 1px solid black" ]  ] [
      R.span [ P.Class "title is-6" ] [ R.str (sprintf "%d. %s" (i+1) question) ]
      R.p [] [ R.str answer ]
    ]

  let answersTpl dispatch (model: Model) = 
    let answers = model.answers |> List.mapi answerTpl
    Box.box' [] ((Helpers.title "Ваши ответы")::answers)

  let sendFormTpl dispatch model = 
    R.div [ P.Class (["box"; Styles.myPanel] |> String.concat " ") ] [
      Helpers.title "Отправить ответы"
      Helpers.subtitle "Вы можете отправить ответы себе или вашему психологу"
      Field.div [] [
        Label.label [ Label.For "name" ] [ R.str "Ваше имя (псевдоним)" ]
        Input.input [ Input.Id "name"; Input.Placeholder "Ваше имя"; Input.Props [ P.OnBlur (fun e -> !!e.target?value |> UpdateName |> dispatch) ] ]
        Label.label [ Label.For "email" ] [ R.str "Куда отправить ответы" ]
        Input.input [ Input.Id "email"; Input.Placeholder "mail@example.com"; Input.Props [ P.OnBlur (fun e -> !!e.target?value |> UpdateEmail |> dispatch) ] ]
      ]
      Level.level [] [
        Level.left [] [
          R.a [P.Href "#"; P.OnClick (fun _ -> dispatch Msg.Start)] [R.str "В начало"]
        ]
        Level.item [] [
          Button.a [ 
            Button.Props [ P.OnClick (fun _ -> dispatch SendEmail) ]; 
            Button.CustomClass "is-primary is-large" ] 
            [ R.str "Отправить" ]
        ]
      ]
    ]

open Impl

let finish dispatch model = 
  Section.section 
    [ 
      Section.CustomClass ([Styles.isFlex; Styles.isHorizontalCentered] |> String.concat " ")
      Section.Props [ P.Style [ CSSProp.FlexDirection "column" ] ] 
    ] 
    [
      answersTpl dispatch model
      sendFormTpl dispatch model
    ]

  