module Model

// MODEL
type Model = 
  {
    currentPos: Position
    answers: string list
    fromName: string
    email: string
  }
and Position = 
  | Welcome
  | Step of int // starting from 1
  | Finish

type Msg =
  | Start
  | UpdateAnswer of string
  | Forward
  | Backward
  | UpdateName of string
  | UpdateEmail of string
  | SendEmail