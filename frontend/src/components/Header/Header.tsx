interface IProps {
  title: string
}

const Header = ({ title }: IProps) => {
  return <div>{title}</div>
}
export default Header
