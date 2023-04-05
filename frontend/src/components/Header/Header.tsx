interface IProps {
  showModalLogin: boolean
  setShowModalLogin: React.Dispatch<React.SetStateAction<boolean>>
}
import { useRef, useState } from 'react'
import styles from './Header.module.css'
import useClickOutSide from '~/helps/clickOutSide'
const Header = ({ setShowModalLogin }: IProps) => {
  const [openDropdownMenu, setOpenDropdownMenu] = useState<boolean>(false)
  const handleClick = () => {
    setOpenDropdownMenu((prev) => !prev)
  }
  const DropdownMenuRef = useRef(null)
  useClickOutSide(DropdownMenuRef, () => {
    setOpenDropdownMenu(false)
  })
  return (
    <header className={`${styles.header}`}>
      <div className={styles.navbar}>
        <div className={styles.logo}>
          <a href='!#'>DoAnTotNghiep</a>
        </div>
        <ul className={styles.nav_links}>
          <li>
            <a href='!#'>Home</a>
          </li>
          <li>
            <a href='!#'>About</a>
          </li>
          <li>
            <a href='!#'>Services</a>
          </li>
          <li>
            <a href='!#'>Contact</a>
          </li>
        </ul>
        <a href='!#' className={styles.action_btn} onClick={() => setShowModalLogin(true)}>
          Login
        </a>
        <div className={styles.toggle_btn} onClick={handleClick} role='presentation'>
          <i className='fa-solid fa-bars'></i>
        </div>
      </div>
      {/* Dropdown Menu */}
      <ul className={`${styles.dropdown_menu} ${openDropdownMenu ? styles.open : ''}`} ref={DropdownMenuRef}>
        <li>
          <a href='!#'>Home</a>
        </li>
        <li>
          <a href='!#'>About</a>
        </li>
        <li>
          <a href='!#'>Services</a>
        </li>
        <li>
          <a href='!#'>Contact</a>
        </li>
        <li>
          <a href='!#' className={styles.action_btn} onClick={() => setShowModalLogin(true)}>
            Login
          </a>
        </li>
      </ul>
    </header>
  )
}
export default Header
