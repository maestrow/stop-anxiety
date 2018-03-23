module Model

// MODEL
type Model = 
  | Welcome
  | Step of int
  | Finish

type Msg =
  | Start
  | Forward
  | Backward