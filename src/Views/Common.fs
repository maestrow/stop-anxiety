module Views.Common

open Model
module P = Fable.Helpers.React.Props
module R = Fable.Helpers.React

module Styles = 
  let isFlex = "is-flex" 
  let isHorizontalCentered = "is-horizontal-centered" 
  let myPanel = "my-panel"

module Helpers = 
  let title name = R.div [ P.ClassName "title" ] [ R.str name ]
  let subtitle name = R.div [ P.ClassName "subtitle" ] [ R.str name ]

let button text msg (dispatch: Msg->unit) = R.button [ P.OnClick (fun _ -> dispatch msg) ] [ R.str text ]