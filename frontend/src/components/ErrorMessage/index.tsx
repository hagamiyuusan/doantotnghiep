import { ErrorMessageStyle } from "./style"
interface IProps {
  errorMessage: string
}
export const ErrorMessage = ({ errorMessage }: IProps) => {
  return <ErrorMessageStyle><p>{errorMessage}</p></ErrorMessageStyle>
}
