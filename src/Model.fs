module Model

// MODEL
type Model = 
  {
    currentPos: Position
    answers: string list
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