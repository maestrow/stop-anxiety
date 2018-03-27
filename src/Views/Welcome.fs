module Views.Welcome

open Model
open Data
open Views.Common

open Fable.Helpers.React.Props
open Fable.Helpers.React

// Fulma
open Fulma
open Fulma.Layouts
open Fulma.Elements
open Fulma.Elements.Form

let welcome dispatch = 
  section [ Class "section is-flex is-horizontal-centered"
            Style [ JustifyContent "center"; ]]
      [ div [ Class "box my-panel"
              Style [Padding "50px"]]
          [ div [ Class "title has-text-centered" ]
              [ str "Справиться с тревогой" ]
            div [ Class "box"
                  Style [TextAlign "justify"]]
              [ str "Нередко перед каким-либо предстоящим событием у человека возникает чувство тревоги. Оптимальный уровень тревоги является стимулирующим фактором, позволяет поддерживать необходимый тонус. Напротив, завышенная тревога снижает продуктивность деятельности, может парализовать активность, привести к тому, что человек отказывается от своих намерений. 
  Техника адресована тем, кто субъективно ощущает испытываемую перед конкретным событием тревогу как излишнюю, желает ощутить большую внутреннюю стабильность в предстоящей ситуации." ]
            div [ Class "level" ]
              [ div [ Class "level-item" ]
                  [ a [ Href "#"
                        OnClick (fun _ -> dispatch Msg.Start)
                        Class "button is-primary is-large" ]
                      [ str "Начать" ] ] ] ] ]
